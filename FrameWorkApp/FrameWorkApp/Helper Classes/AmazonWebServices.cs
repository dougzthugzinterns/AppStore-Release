using System;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Text;

namespace FrameWorkApp
{
	public class AmazonWebServices
	{
		UTF8Encoding encoding=new UTF8Encoding();
		SHA256 sha = SHA256.Create ();
		HMACSHA256 hmac= new HMACSHA256();
		string service="dynamodb";
		string region;
		string host;
		string contentType="application/x-amz-json-1.0";
		string dateOnly= DateTime.UtcNow.ToString ("yyyyMMdd");
		string amazonDate= DateTime.UtcNow.ToString ("yyyyMMddTHHmmss")+"Z";
		string hashAlgorithim= "AWS4-HMAC-SHA256";
		string secretKey;
		string accessID;


		public AmazonWebServices (string host, string region, string accessID, string secretKey)
		{
			this.host = host;
			this.region = region;
			this.accessID = accessID;
			this.secretKey = secretKey;
		}
		public Boolean checkIdExists(string id){
			string request = "{\n\t\"TableName\": \"SafeDrivingMateTripData\",\n\t\"ScanFilter\": {\n\t\t\"UserID\": {\n\t\t\t\"AttributeValueList\": [\n\t\t\t\t{\n\t\t\t\t\t\"S\": \"" + id + "\"\n\t\t\t\t}\n\t\t\t],\n\t\t\t\"ComparisonOperator\": \"EQ\"\n\t\t}\n\t},\n\t\"ReturnConsumedCapacity\": \"TOTAL\"\n}";
			//Console.WriteLine ("Request: "+request);
			string response= sendRequest(request, "DynamoDB_20120810.Scan");
			//Console.WriteLine ("Response:" +response);
			if (response.Contains ("\"Count\":0")) {
				//ID Doesn't exist
				return false;
			}
			//ID Exists
			return true;
		}

		//Send Trip Data to DynamoDB database
		public void sendTripData(int distance, int hardStops, int hardStarts, int hardTurns, string id){
			String date = DateTime.UtcNow.ToString ("yyyyMMddHHmmssfffffff");
			String primaryKey=date+"_"+id;
			date = DateTime.Now.ToString ("yyyy-MM-dd");
			String time = DateTime.Now.ToString ("HH:mm:ss");
			//Format PutItem Request for DynamoDB
			string request = "{\n\t\"TableName\": \"SafeDrivingMateTripData\",\n\t\"Item\": {\n\t\t\"ID\": {\n\t\t\t\"S\": \"" + primaryKey + "\"\n\t\t},\n\t\t\"UserID\":{\n\t\t\t\"S\": \"" + id + "\"\n\t\t},\n\t\t\"Date\":{\n\t\t\t\"S\": \"" + date + "\"\n\t\t},\n\t\t\"Time\":{\n\t\t\t\"S\": \"" + time + "\"\n\t\t},\n\t\t\"Distance\":{\n\t\t\t\"N\": \"" + distance + "\"\n\t\t},\n\t\t\"HardStops\":{\n\t\t\t\"N\": \"" + hardStops + "\"\n\t\t},\n\t\t\"HardStarts\":{\n\t\t\t\"N\": \"" + hardStarts + "\"\n\t\t},\n\t\t\"HardTurns\":{\n\t\t\t\"N\": \"" + hardTurns + "\"\n\t\t}\n\t},\n\t\"Expected\":{\n\t\t\"ID\":{\n\t\t\t\"Exists\": false\n\t\t}\n\t}\n}";
			sendRequest (request, "DynamoDB_20120810.PutItem");

		}

		//Send Request to Amazon DynamoDB REST Services.
		private string sendRequest(string body, string amazonTarget ){

			var request = createRequest (body, amazonTarget);
			//Send Request
			try{
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						return "";
					//Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						if(string.IsNullOrWhiteSpace(content)) {
							//Empty Body
							return "";
						}
						else {
							return content;
						}

					}
				}
			}
			//Catch Bad Request Acception
			catch(WebException n){
				string content= new StreamReader(n.Response.GetResponseStream()).ReadToEnd();
				Console.WriteLine (content);
				return "";
			}

		}

		//Create POST Request
		private WebRequest createRequest(string body, string amazonTarget){
			string encodedBody=convertToHex((sha.ComputeHash(encoding.GetBytes(body.ToCharArray ()))));

			//Create Canonical Request
			string canonicalRequest="POST" + '\n' +"/" + '\n' +"" + '\n' +"content-type:"+contentType+"\nhost:dynamodb.us-west-2.amazonaws.com\nx-amz-date:"+amazonDate+"\nx-amz-target:" +amazonTarget+ "\n\n" +"content-type;host;x-amz-date;x-amz-target" + '\n' +encodedBody;
			//Console.WriteLine ("---------CanonicalRequest------\n"+canonicalRequest);

			//Create String to Sign
			string stringToSign= hashAlgorithim+"\n"+amazonDate+"\n"+dateOnly+"/"+region+"/"+service+"/aws4_request\n"+convertToHex((sha.ComputeHash(encoding.GetBytes(canonicalRequest.ToCharArray ()))));
			//Console.WriteLine ("-------stringToSign---------\n"+stringToSign);

			//Create Signature
			string signature= createSignature(secretKey, region, service, stringToSign);
			//Console.WriteLine("Signature: "+signature);

			//Create Authorization String
			string authorizationString=hashAlgorithim+" Credential="+accessID+"/"+dateOnly+"/"+region+"/"+service+"/aws4_request, SignedHeaders=content-type;host;x-amz-date;x-amz-target, Signature="+signature;


			//Create Headers for Request
			var request = HttpWebRequest.Create(host);
			request.ContentType = contentType;
			request.Headers.Add("x-amz-date", amazonDate);
			request.Headers.Add("x-amz-target", amazonTarget);
			request.Headers.Add("Authorization", authorizationString);
			request.Method="POST";

			//Add Body to Request
			byte[] data = encoding.GetBytes(body.ToCharArray ());
			request.ContentLength = data.Length;
			Stream newStream = request.GetRequestStream();
			newStream.Write(data,0,data.Length);
			newStream.Close();
			return request;

		}

		//Create signature for POST request
		private string createSignature(string secretKey, string region, string service, string stringToSign){

			//Get everything as byte arrays
			byte[] secretKeyByte = encoding.GetBytes (("AWS4"+secretKey).ToCharArray());
			byte[] dateByte = encoding.GetBytes (dateOnly.ToCharArray());
			byte[] regionByte = encoding.GetBytes (region.ToCharArray());
			byte[] serviceByte = encoding.GetBytes (service.ToCharArray());

			//Create Date Key
			hmac.Key = secretKeyByte;
			byte[] byteSignature = hmac.ComputeHash (dateByte);

			//Create Reigion Key
			hmac.Key = byteSignature;
			byteSignature = hmac.ComputeHash (regionByte);

			//Create Service Key
			hmac.Key = byteSignature;
			byteSignature = hmac.ComputeHash (serviceByte);

			//Create Signing Key
			hmac.Key = byteSignature;
			byteSignature = hmac.ComputeHash (encoding.GetBytes ("aws4_request"));

			//Create Signature
			hmac.Key = byteSignature;
			byteSignature = hmac.ComputeHash (encoding.GetBytes (stringToSign.ToCharArray ()));
			return  convertToHex(byteSignature);

		}

		//Helper method to convert a byte array to a hex string
		private string convertToHex(byte[] s){
			return BitConverter.ToString (s).Replace ("-", string.Empty).ToLower();
		}

	}
}

