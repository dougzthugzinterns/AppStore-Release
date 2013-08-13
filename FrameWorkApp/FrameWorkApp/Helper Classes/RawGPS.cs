using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using System.Collections.Generic;

namespace FrameWorkApp
{
	public partial class RawGPS 
	{
		private const double gpsNotAvailableFlag = -500;
		public double GpsNotAvailableFlag() {
			return gpsNotAvailableFlag;
		}

		SDMFileManager fileManager = new SDMFileManager();
		GoogleMapsDirectionService googlePathWhileDriving = new GoogleMapsDirectionService();
		private List<CLLocationCoordinate2D> gmdsSingleCalloutCoordinateBuffer;
		public List<CLLocationCoordinate2D> GMDSSingleCalloutCoordinateBuffer {
			get {return gmdsSingleCalloutCoordinateBuffer;}
		}

		public void clearPathBuffer(){
			gmdsSingleCalloutCoordinateBuffer.Clear ();
		}

		public List<CLLocation> listOfRawGPSTripLocationCoordinates { get; set; }
		//CLLocationManager AppDelegate.commonLocationManager;

		public RawGPS ()
		{
			//AppDelegate.commonLocationManager.Delegate = new LocationManagerDelegate ();
			listOfRawGPSTripLocationCoordinates = new List<CLLocation> ();
			gmdsSingleCalloutCoordinateBuffer = new List<CLLocationCoordinate2D> ();
		}

		public double getCurrentUserLatitude ()
		{
			AppDelegate.commonLocationManager.DesiredAccuracy = CLLocation.AccuracyBest;
			if (CLLocationManager.LocationServicesEnabled) {
				AppDelegate.commonLocationManager.StartUpdatingLocation ();
				AppDelegate.commonLocationManager.StopUpdatingLocation ();
				CLLocation currentLocation = AppDelegate.commonLocationManager.Location;
				if (currentLocation != null) {
					return AppDelegate.commonLocationManager.Location.Coordinate.Latitude;
				} else {
					return GpsNotAvailableFlag();
				}
			}
			return GpsNotAvailableFlag();
		}

		public double getCurrentUserLongitude ()
		{
			AppDelegate.commonLocationManager.DesiredAccuracy = CLLocation.AccuracyBest;
			if (CLLocationManager.LocationServicesEnabled) {
				AppDelegate.commonLocationManager.StartUpdatingLocation ();
				AppDelegate.commonLocationManager.StopUpdatingLocation ();
				CLLocation currentLocation = AppDelegate.commonLocationManager.Location;
				if (currentLocation != null) {
					return AppDelegate.commonLocationManager.Location.Coordinate.Longitude;
				}
			} 
			return GpsNotAvailableFlag();
		}

		public double getSpeedInMetersPerSecondUnits ()
		{
			double currentSpeed = 0;
			AppDelegate.commonLocationManager.DesiredAccuracy = CLLocation.AccuracyBest;
			if (CLLocationManager.LocationServicesEnabled) {
				AppDelegate.commonLocationManager.StartUpdatingLocation ();
				CLLocation currentLocation = AppDelegate.commonLocationManager.Location;
				Console.WriteLine ("CommonLocationManager.location"+AppDelegate.commonLocationManager.Location);
				if(currentLocation != null){
					currentSpeed = AppDelegate.commonLocationManager.Location.Speed;
					if(currentSpeed < 0){
						currentSpeed = 0;
					}
					return currentSpeed;	
				}
			}
			return GpsNotAvailableFlag();

		}

		public double convertToKilometersPerHour (double metersPerSecond)
		{
			return (metersPerSecond / 1000.0) * 3600;
		}

		public double convertMetersToKilometers (double meters)
		{
			return meters / 1000.0;
		}

		public void makeGoogleCalloutUsingBuffer(){
			if(this.gmdsSingleCalloutCoordinateBuffer.Count >1){ // Needs to have at least 2 coordinates to make the callout
				List<Google.Maps.Polyline> singleCalloutPolyLines = googlePathWhileDriving.performGoogleDirectionServiceApiCallout(gmdsSingleCalloutCoordinateBuffer);
				List<String> pathStringList = new List<String>();
				foreach(Google.Maps.Polyline eachLine in singleCalloutPolyLines){
					pathStringList.Add (eachLine.Path.EncodedPath);
				}
				fileManager.updateGooglePathFile(pathStringList);
				CLLocationCoordinate2D lastCoordinateInBuffer = gmdsSingleCalloutCoordinateBuffer [gmdsSingleCalloutCoordinateBuffer.Count - 1];
				gmdsSingleCalloutCoordinateBuffer.Clear();
				gmdsSingleCalloutCoordinateBuffer.Add (lastCoordinateInBuffer);
			}
		}

		public void createCoordinatesWhenHeadingChangesToAddToList ()
		{
			AppDelegate.commonLocationManager.DesiredAccuracy = CLLocation.AccuracyBest;
			AppDelegate.commonLocationManager.HeadingFilter = 30;
			CLLocation newCoordinate;

			if (CLLocationManager.LocationServicesEnabled) {
				AppDelegate.commonLocationManager.StartUpdatingLocation ();
			}
			if (CLLocationManager.HeadingAvailable) {
				AppDelegate.commonLocationManager.StartUpdatingHeading ();
			}

			AppDelegate.commonLocationManager.UpdatedHeading += (object sender, CLHeadingUpdatedEventArgs e) => {
				Double lattitude= this.getCurrentUserLatitude ();
				Double longitude=this.getCurrentUserLongitude ();
				if (lattitude != gpsNotAvailableFlag && longitude != gpsNotAvailableFlag)
				{
					newCoordinate = new CLLocation (lattitude, longitude);
					listOfRawGPSTripLocationCoordinates.Add (newCoordinate);
					//Add to Temp File
					CLLocationCoordinate2D newCoordinate2D = new CLLocationCoordinate2D(lattitude, longitude);
					fileManager.addLocationToTripDistanceFile(newCoordinate2D);
					if(gmdsSingleCalloutCoordinateBuffer.Count >=10) {
						makeGoogleCalloutUsingBuffer();
					}
					gmdsSingleCalloutCoordinateBuffer.Add (newCoordinate2D);
					Console.WriteLine ("Added coordinate:" + newCoordinate2D.Latitude + ","+newCoordinate2D.Longitude);
				}
			};
		}
		public double getHeading(){
			AppDelegate.commonLocationManager.DesiredAccuracy = CLLocation.AccuracyBest;
			if (CLLocationManager.LocationServicesEnabled) {
				AppDelegate.commonLocationManager.StartUpdatingLocation ();
			}
			if (CLLocationManager.HeadingAvailable) {
				AppDelegate.commonLocationManager.StartUpdatingHeading ();
			}

			CLHeading head = AppDelegate.commonLocationManager.Heading;
			Console.WriteLine ("head is: "+head);

			double heading = 0;
			/** old code that is begging to be replaced...
			if (head.TrueHeading != null) {
			head = AppDelegate.commonLocationManager.Heading;
			heading = head.TrueHeading;
			Console.WriteLine ("Heading" + heading.ToString("0.00"));
			}
			**/
			//did this fix it??


			bool fixHead = false;
			if (head == null) {
				while (fixHead == false) {
					if (head == null) {
						head = AppDelegate.commonLocationManager.Heading;
						Console.WriteLine ("Heading is NULL");
					} else {
						fixHead = true;
						heading = head.TrueHeading;
						Console.WriteLine ("Heading is Fixed");
					}
				}
			} else {
				heading = head.TrueHeading;
			}




			//last resort
			/**
			if (head == null) {
				new UIAlertView ("oops!", "We are having trouble reading sensor data from your phone. Let's give it another shot!", null, "Ok", null).Show ();

			}
			**/

			Console.WriteLine ("heading is: "+heading);

			return heading;
		} 

		public void stopGPSReadings(){
			AppDelegate.commonLocationManager.StopUpdatingLocation ();
		}

		public double CalculateDistanceTraveled (List<CLLocation> locations)
		{
			double distance = 0;
			double partialDistance = 0;
			for (int i = 0; i<locations.Count; i++) {
				if (i + 1 < locations.Count) {
					partialDistance = locations [i].DistanceFrom (locations [i + 1]);
					distance = distance + partialDistance;
				}
			}
			return distance;
		}
	}
}