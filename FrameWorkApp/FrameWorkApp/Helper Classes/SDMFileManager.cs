using System;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace FrameWorkApp
{
	public class SDMFileManager
	{
		String libraryAppFolder;
		String currentTripEventFile;
		String currentTripDistanceFile;
		String tripLogFile;
		String tripHistoryFolder;
		String userFile;
		String achievementsFile;
		String googlePathFile;

		const int MAX_NUMBER_OF_TRIPS = 10;

		public SDMFileManager ()
		{
			//Set Paths for All Files
			var libraryCache = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library", "Caches");
			libraryAppFolder = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library", "SafeDrivingMate");

			currentTripEventFile = Path.Combine (libraryCache, "currentTripEvents_SafeDrivingMate.txt");
			tripHistoryFolder = Path.Combine (libraryAppFolder, "tripHistory");
			tripLogFile = Path.Combine (tripHistoryFolder, "tripHistory.txt");
			currentTripDistanceFile = Path.Combine (libraryCache, "currentTripDistance_SafeDrivingMate.txt");
			userFile = Path.Combine (libraryAppFolder, "userData.txt");
			achievementsFile = Path.Combine (libraryAppFolder, "achievementsData.txt");
			googlePathFile = Path.Combine (libraryAppFolder, "googlePathData.txt");



			//Make Sure all files and folders already exist if not create them
			if (!Directory.Exists (libraryAppFolder)) {
				Directory.CreateDirectory (libraryAppFolder);
			}
			if(!Directory.Exists(tripHistoryFolder)){
				Directory.CreateDirectory (tripHistoryFolder);
				File.Create (tripLogFile).Close ();
			}
			if (!File.Exists (currentTripDistanceFile)) {
				File.Create (currentTripDistanceFile).Close ();
			}
			if (!File.Exists (currentTripEventFile)) {
				File.Create (currentTripEventFile).Close ();
			}
			if (!File.Exists (userFile)) {
				File.Create (userFile).Close ();
				File.WriteAllText (userFile, "0, 0, 0, 0, 0, 0, -1");
			}
			if (!File.Exists (achievementsFile)) {
				File.Create (achievementsFile).Close ();
				File.WriteAllText (achievementsFile, "0;01/01/1900;0;false;false;false;false;false;false;false;false;false;false;false;false;false;false;false");
			}
			if (!File.Exists (googlePathFile)) {
				File.Create (googlePathFile).Close ();
			}

			//Console.WriteLine ("Trip Log Path:" + tripLogFile);
			//Console.WriteLine ("Current Trip Event File Path: " + currentTripEventFile);
			//Console.WriteLine ("Current Trip Distance File Path: " + currentTripDistanceFile);
		}

		//Overall Helper Methods
		//Returns True of Trip is still in progress, false if not.
		public Boolean currentTripInProgress ()
		{
			if (File.Exists (currentTripEventFile) && File.Exists (currentTripDistanceFile)) {
				if (File.ReadAllText (currentTripEventFile).Length!=0 || File.ReadAllText (currentTripDistanceFile).Length!=0) {
					return true;
				}
			}
			return false;
		}

		//Get Last Date of Any point being entered into a file
		public DateTime getDateOfLastPointEnteredInCurrentTrip ()
		{
			DateTime eventFileDateTime = File.GetLastWriteTime (currentTripEventFile);
			DateTime distanceFileDateTime = File.GetLastWriteTime (currentTripDistanceFile);

			if (eventFileDateTime.CompareTo (distanceFileDateTime) > 0) {
				return eventFileDateTime;
			} else {
				return distanceFileDateTime;
			}
		}
		//-------Trip Log Methods-------
		//Adds Trip to Trip Log
		public void addDataToTripLogFile (Trip newTrip)
		{
			//Get All current Trips
			Trip[] currentTripsLogged=this.readDataFromTripLogFile ();

			ArrayList listOfTrips = new ArrayList();
			listOfTrips.AddRange (currentTripsLogged);

			//Check how many trips are currenty in trip log
			if (listOfTrips.Count < MAX_NUMBER_OF_TRIPS) {
				//Less than Max
				listOfTrips.Insert (0, newTrip);
			} else {
				//At Max
				//Remove Coordiate files for last Trip
				File.Delete(Path.Combine (tripHistoryFolder, ((Trip)listOfTrips[9]).DateTime.ToString ("MMddyyyyhmm")+"_events.txt"));
				File.Delete(Path.Combine (tripHistoryFolder, ((Trip)listOfTrips[9]).DateTime.ToString ("MMddyyyyhmm")+"_distance.txt"));
				File.Delete(Path.Combine (tripHistoryFolder, ((Trip)listOfTrips[9]).DateTime.ToString ("MMddyyyyhmm")+"_paths.txt"));
				//Remove Last Trip in List
				listOfTrips.RemoveAt (9);
				//Add New Trip at Head
				listOfTrips.Insert (0, newTrip);
			}
			//Create Copy of Files
			File.Copy(currentTripEventFile, Path.Combine (tripHistoryFolder, newTrip.DateTime.ToString ("MMddyyyyhmm")+"_events.txt"), true);
			File.Copy(currentTripDistanceFile, Path.Combine (tripHistoryFolder, newTrip.DateTime.ToString ("MMddyyyyhmm")+"_distance.txt"), true);
			File.Copy(googlePathFile, Path.Combine (tripHistoryFolder, newTrip.DateTime.ToString ("MMddyyyyhmm")+"_paths.txt"), true);

			//Write everything back to file
			this.writeTripLogFile ((Trip[])listOfTrips.ToArray (typeof(Trip)));

		}

		//Write Trips back to Trip log
		private void writeTripLogFile(Trip[] trips){
			File.WriteAllText (tripLogFile, "");
			foreach(Trip t in trips){
				FileStream currentTripFile_FileStream = File.Open (tripLogFile, FileMode.Append);
				StreamWriter currentTripFile_SteamWriter = new StreamWriter (currentTripFile_FileStream);
				currentTripFile_SteamWriter.WriteLine (t.DateTime.ToString ("MM/dd/yyyy h:mmtt") + "," + t.HardStarts+","+t.HardBrakes+","+t.HardTurns+","+t.Distance);

				currentTripFile_SteamWriter.Close ();
				currentTripFile_FileStream.Close ();
				Console.WriteLine ("Trip History Updated");
			}

		}

		//Reads Trip Log File back in returning a array of Trip objects.
		public Trip[] readDataFromTripLogFile ()
		{
			ArrayList temporaryArrayListForData = new ArrayList ();

			foreach (String line in File.ReadLines (tripLogFile)) {
				String[] coordinatesSplitAtComma = line.Split (',');
				Trip newTrip = new Trip (DateTime.ParseExact (coordinatesSplitAtComma [0], "MM/dd/yyyy h:mmtt", null), int.Parse (coordinatesSplitAtComma [1]),int.Parse (coordinatesSplitAtComma [2]),int.Parse (coordinatesSplitAtComma [3]), double.Parse (coordinatesSplitAtComma [4]));
				temporaryArrayListForData.Add (newTrip);
			}
			return (Trip[])temporaryArrayListForData.ToArray (typeof(Trip));
		}

		public List<String> readSavedGooglePathData (Trip t)
		{
			string googlePathFile=Path.Combine (tripHistoryFolder, t.DateTime.ToString ("MMddyyyyhmm")+"_paths.txt");
			List<string> googlePathsRead = new List<string> ();
			foreach (string currentPath in File.ReadLines(googlePathFile)) {
				googlePathsRead.Add (currentPath);
			}
			return googlePathsRead;
		}

		//Read Saved Trip Event Data for a Specifc Trip
		public  Event[] readSavedTripEventData (Trip t)
		{
			ArrayList temporaryArrayListForCoordinateData = new ArrayList ();
			//Location of Where Event File is for Specific Trip
			string eventFileLocation=Path.Combine (tripHistoryFolder, t.DateTime.ToString ("MMddyyyyhmm")+"_events.txt");
			//Read File
			foreach (String line in File.ReadLines (eventFileLocation)) {
				String[] coordinateDataSplitAtComma = line.Split (',');
				CLLocationCoordinate2D newCoordinate = new CLLocationCoordinate2D (Double.Parse (coordinateDataSplitAtComma [0]), Double.Parse (coordinateDataSplitAtComma [1]));
				Event newEvent = new Event (newCoordinate, int.Parse (coordinateDataSplitAtComma [2]));
				temporaryArrayListForCoordinateData.Add (newEvent);
			}
			return (Event[])temporaryArrayListForCoordinateData.ToArray (typeof(Event));
		}

		//Read Saved Trip Distance Data for a Specific Trip
		public CLLocationCoordinate2D[] readSavedTripDistanceData (Trip t)
		{
			ArrayList temporaryArrayListForData = new ArrayList ();
			//Location for Distance File for a Specific Trip 
			string distanceFileLocation=Path.Combine (tripHistoryFolder, t.DateTime.ToString ("MMddyyyyhmm")+"_distance.txt");
			//Read File
			foreach (String line in File.ReadLines (distanceFileLocation)) {
				String[] splitLine = line.Split (',');
				CLLocationCoordinate2D newCoordinate = new CLLocationCoordinate2D (Double.Parse (splitLine [0]), Double.Parse (splitLine [1]));
				temporaryArrayListForData.Add (newCoordinate);
			}
			return (CLLocationCoordinate2D[])temporaryArrayListForData.ToArray (typeof(CLLocationCoordinate2D));
		}

		//Clear All Trip History
		public void clearTripHistory(){
			DirectoryInfo tripHistory = new DirectoryInfo(tripHistoryFolder);
			//Remove all Files
			foreach (FileInfo file in tripHistory.GetFiles())
			{
				file.Delete(); 
			}
			//Recreate the Trip Log File.
			File.Create (tripLogFile).Close ();

		}

		//------Trip Event File Methods------
		//Add Event Cooridnate to Current Trip Event File
		public  void addEventToTripEventFile (Event newEvent)
		{
			FileStream currentTripFile_FileStream = File.Open (currentTripEventFile, FileMode.Append);
			StreamWriter currentTripFile_SteamWriter = new StreamWriter (currentTripFile_FileStream);

			currentTripFile_SteamWriter.WriteLine (newEvent.Location.Latitude + "," + newEvent.Location.Longitude + "," + newEvent.Type);

			currentTripFile_SteamWriter.Close ();
			currentTripFile_FileStream.Close ();
		}

		//Reads Current Trip Event File back in and returns a Array of CLLocationCordinates2D objects.
		public  Event[] readDataFromTripEventFile ()
		{
			ArrayList temporaryArrayListForCoordinateData = new ArrayList ();

			foreach (String line in File.ReadLines (currentTripEventFile)) {
				String[] coordinateDataSplitAtComma = line.Split (',');
				CLLocationCoordinate2D newCoordinate = new CLLocationCoordinate2D (Double.Parse (coordinateDataSplitAtComma [0]), Double.Parse (coordinateDataSplitAtComma [1]));
				Event newEvent = new Event (newCoordinate, int.Parse (coordinateDataSplitAtComma [2]));
				temporaryArrayListForCoordinateData.Add (newEvent);
			}
			return (Event[])temporaryArrayListForCoordinateData.ToArray (typeof(Event));
		}

		//Clears Current Trip Event File
		public void clearCurrentTripEventFile ()
		{
			File.WriteAllText (currentTripEventFile, "");
		}

		//-------Trip Distance File Methods-------
		//Adds a location to the Current ,ooTrip Distance File
		public  void addLocationToTripDistanceFile (CLLocationCoordinate2D newCoordiante)
		{
			FileStream currentTripFile_FileStream = File.Open (currentTripDistanceFile, FileMode.Append);
			StreamWriter currentTripFile_SteamWriter = new StreamWriter (currentTripFile_FileStream);

			currentTripFile_SteamWriter.WriteLine (newCoordiante.Latitude + "," + newCoordiante.Longitude);

			currentTripFile_SteamWriter.Close ();
			currentTripFile_FileStream.Close ();
		}

		//Reads Current Trip Distance File back in returns a Array of CLLocation objects.
		public  CLLocation[] readDataFromTripDistanceFile ()
		{
			ArrayList temporaryArrayListForData = new ArrayList ();

			foreach (String line in File.ReadLines (currentTripDistanceFile)) {
				String[] splitLine = line.Split (',');
				CLLocation newCoordinate = new CLLocation (Double.Parse (splitLine [0]), Double.Parse (splitLine [1]));
				temporaryArrayListForData.Add (newCoordinate);
			}
			return (CLLocation[])temporaryArrayListForData.ToArray (typeof(CLLocation));
		}

		//Clears Current Trip Distance File.
		public void clearCurrentTripDistanceFile ()
		{
			File.WriteAllText (currentTripDistanceFile, "");
		}

		//User Data File Methods
		public void updateUserFile (User updatedUser)
		{
			String dataToWrite = updatedUser.TotalDistance + "," + updatedUser.TotalPoints + "," + updatedUser.TotalHardStops + "," + updatedUser.TotalHardStarts+ "," + updatedUser.TotalHardTurns + "," + updatedUser.TotalNumberTrips+","+updatedUser.Id;
			File.WriteAllText (userFile, dataToWrite);

		}

		public User readUserFile ()
		{
			String dataFromFile = File.ReadAllText (userFile);
			String[] splitDataFromFile = dataFromFile.Split (',');

			User userData = new User (Double.Parse(splitDataFromFile[0]), int.Parse(splitDataFromFile[1]), int.Parse(splitDataFromFile[2]) , int.Parse(splitDataFromFile[3]), int.Parse(splitDataFromFile[4]), int.Parse (splitDataFromFile[5]), splitDataFromFile[6].Trim());
			return userData;
		}

		public void clearUserFile ()
		{
			File.WriteAllText (userFile, "");
		}



		
		
		//Achievements Data File Methods
		public void updateAchievementsFile(AchievementData achievementData){
			String dataToWrite = achievementData.NumberConsecutiveDaysOfAppUse + ";" + achievementData.LastDateOfAppUse.ToString () + ";" + achievementData.NumberConsecutiveTripsWithoutIncidents + ";" + achievementData.TakeTenValue + ";" + achievementData.KangarooJackValue + ";" + achievementData.CrossCountryValue + ";" + achievementData.RoadWarriorValue + ";" + achievementData.CrikeyValue + ";" + achievementData.SuperSteveValue + ";" + 
				achievementData.SmoothOperatorValue + ";" + achievementData.TediousTurnerValue + ";" + achievementData.GrannyStyleValue + ";" + achievementData.CarefulCatieValue + ";" + achievementData.TrainingWheelsValue + ";" + achievementData.PerfectPeterValue + ";" + achievementData.ConstantCommuterValue + ";" + achievementData.FrequentFlyerValue + ";" + achievementData.SteadyPedalerValue;
			File.WriteAllText (achievementsFile,dataToWrite);
		}



		public AchievementData readAchievementsFile(){
			String dataFromFile = File.ReadAllText (achievementsFile);
			String[] splitDataFromFile = dataFromFile.Split (';');
			List<bool> achievementValuesFromFile = new List<bool> ();
			for (int i = 3; i<splitDataFromFile.Length; i++) {
				achievementValuesFromFile.Add(bool.Parse(splitDataFromFile[i]));
			}

			AchievementData dataToWrite = new AchievementData (int.Parse (splitDataFromFile[0]),DateTime.Parse(splitDataFromFile[1]),int.Parse(splitDataFromFile[2]),achievementValuesFromFile);
			
			return dataToWrite;
		}


		public void clearAchievementsFile ()
		{
			File.WriteAllText (achievementsFile, "");
		}

		public void updateGooglePathFile(List<String> pathStringsToBeAdded){
			FileStream currentGooglePathFile_FileStream = File.Open (googlePathFile, FileMode.Append);
			StreamWriter currentTripFile_SteamWriter = new StreamWriter (currentGooglePathFile_FileStream);
			foreach (String singlePath in pathStringsToBeAdded) {
				currentTripFile_SteamWriter.WriteLine (singlePath);
			}

			currentTripFile_SteamWriter.Close ();
			currentGooglePathFile_FileStream.Close ();
		}

		public List<String> readGooglePathFile(){
			List<string> googlePathsRead = new List<string> ();
			foreach (string currentPath in File.ReadLines(googlePathFile)) {
				googlePathsRead.Add (currentPath);
			}
			return googlePathsRead;
		}

		public void clearGooglePathFile ()
		{
			File.WriteAllText (googlePathFile, "");
		}

	}
}