using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMotion;
using MonoTouch.CoreLocation;
using MonoTouch.AudioToolbox;

namespace FrameWorkApp
{
	public partial class StopScreen: UIViewController
	{

		public static SDMFileManager fileManager = new SDMFileManager ();
		public static List<CLLocation> listOfTripLocationCoordinates = new List<CLLocation> ();
		RawGPS rawGPS = new RawGPS ();
		public static double distanceTraveledForCurrentTrip = 0;
		public static int numberHardStarts = 0;
		public static int numberHardStops = 0;

		//Declaration Statements

		private const double SPEED_THRESHOLD_BRAKING = 4;
		private const double SPEED_THRESHOLD_STARTS = 4;
		private const double TURNING_THRESHOLD = 30;
		private const double TURNING_SPEED_THRESHOLD = 24;//km/h
		private const int MINIMUM_DISTANCE_REQUIRED = 1;
		private double initialHeading;
		private double finalHeading;
		//Type Codes for Events
		private const int UNKNOWN_EVENT_TYPE = 0;
		private const int HARD_BRAKE_TYPE = 1;
		private const int HARD_ACCEL_TYPE = 2;
		private const int HARD_TURN_TYPE = 3;
		private double[] initialSpeedArray;
		private double[] finalSpeedArray;
		private int initialSpeedArrayCounter;
		private int finalSpeedArrayCounter;
		private bool isCountingInitialSpeed;
		private bool isCountingFinalSpeed;

		private double currentMaxAvgAccel;
		private double avgaccel;
		private double threshold;
		private double maxThresh;
		//number of events
		public static int eventcount = 0;
		//true if event is in progress
		private bool eventInProgress;
		private double currentSpeed;
		//container for current location
		private CLLocationCoordinate2D currentCoord;
		private CMMotionManager _motionManager;
		private SystemSound sound;

		public StopScreen (IntPtr handle) : base (handle)
		{
			//Initialize Declarations
			initialSpeedArray = new double[7];
			finalSpeedArray = new double[7];
			initialSpeedArrayCounter = 0;
			finalSpeedArrayCounter = 0;
			isCountingInitialSpeed = false;
			isCountingFinalSpeed = false;
			initialHeading = 0;
			finalHeading = 0;
			//threshold for erratic behavior in G's
			threshold = .24;
			maxThresh = 1.2;
			eventcount = 0;
			//true if event is in progress
			eventInProgress = false;
			//container for current location
			//currentCoord = new CLLocationCoordinate2D ();
			currentSpeed = 0;
			avgaccel = 0;
			currentMaxAvgAccel = 0;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.Portrait;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//Check and make sure GPS is enabled
			if (CLLocationManager.Status == CLAuthorizationStatus.Authorized) {
				rawGPS.createCoordinatesWhenHeadingChangesToAddToList ();

				//Start updating accelerometer data at device motion update interval specified pulling rate
				_motionManager = new CMMotionManager ();
				_motionManager.DeviceMotionUpdateInterval = .5;
				_motionManager.StartDeviceMotionUpdates (NSOperationQueue.CurrentQueue, (data,error) =>

				                                         {
					this.handleDeviceMotionUpdate(data, error);

				});
			}

			else {
				new UIAlertView ("Location Services must be enabled to use application!", "We noticed you have disabled location services for this application. Please enable location services.", null, "Ok", null).Show ();

			}

			// Perform any additional setup after loading the view, typically from a nib.
		}
		public void removeCompassInterferenceOverlay(){
			this.removeCompassInterferenceOverlay ();

		}
		public void handleDeviceMotionUpdate (CMDeviceMotion data, NSError error){
			avgaccel = Math.Sqrt ((data.UserAcceleration.X * data.UserAcceleration.X) +
			                      (data.UserAcceleration.Y * data.UserAcceleration.Y) +
			                      (data.UserAcceleration.Z * data.UserAcceleration.Z));

			//Calculate current speed in Km/Hr

			double speedInMetersSecond = rawGPS.getSpeedInMetersPerSecondUnits ();
			Console.WriteLine ("Speeed right now:"+speedInMetersSecond);
			if (speedInMetersSecond == rawGPS.GpsNotAvailableFlag ()) {
				this.clearArray (initialSpeedArray);
				this.clearArray (finalSpeedArray);
				//Reset for new event
				isCountingFinalSpeed = false;
				isCountingInitialSpeed = false;
				initialSpeedArrayCounter = 0;
				finalSpeedArrayCounter = 0;
			}
			else{
				currentSpeed = rawGPS.convertToKilometersPerHour (speedInMetersSecond);
				//Console.WriteLine (currentSpeed.ToString ());
				//this.currentSpeedLabel.Text = currentSpeed.ToString ();



				//Test if accelerometer event occurs
				if ((avgaccel > threshold) && (avgaccel < maxThresh)) {
					Console.WriteLine ("Event is in progress.");

					if (!eventInProgress) {
						isCountingInitialSpeed = true;
						eventInProgress = true;
						initialHeading = rawGPS.getHeading ();
						Console.WriteLine("Event in Progress Initial heading: " + this.initialHeading.ToString("0.00"));


					}
				}

				//Test if accelerometer event has ended
				else if ((avgaccel < threshold) && eventInProgress) {
					Console.WriteLine ("Event has ended.");
					eventcount++;
					eventInProgress = false;
					isCountingFinalSpeed = true;

					//this.eventCounter.Text = eventcount.ToString ();
				}

				//If event has started, start counting/filling initialSpeedArray
				if (isCountingInitialSpeed == true && initialSpeedArrayCounter < initialSpeedArray.Length) {

					initialSpeedArray [initialSpeedArrayCounter] = currentSpeed;
					Console.WriteLine ("Writing 1: " + currentSpeed);
					initialSpeedArrayCounter++;
					Console.WriteLine ("count1: " + initialSpeedArrayCounter);
				}

				//If event has stopped, start counting/filling finalSpeedArray
				if (isCountingFinalSpeed == true && finalSpeedArrayCounter < finalSpeedArray.Length) {

					finalSpeedArray [finalSpeedArrayCounter] = currentSpeed;
					//Console.WriteLine ("Writing 2: " + currentSpeed);

					if (finalSpeedArrayCounter == 5) {
						finalHeading = rawGPS.getHeading ();
						Console.WriteLine("Event in Progress Final heading: " + this.finalHeading.ToString("0.00"));
					}

					finalSpeedArrayCounter++;

					//Console.WriteLine ("count2: " + finalSpeedArrayCounter);
				}

				//If both speed arrays are full, test event criteria
				if ((initialSpeedArrayCounter == initialSpeedArray.Length) && (finalSpeedArrayCounter == finalSpeedArray.Length)) {
					//Console.WriteLine ("initial" + initialSpeedArray [initialSpeedArray.Length - 1].ToString("0.000"));
					//Console.WriteLine ("final  " + finalSpeedArray [finalSpeedArray.Length - 1].ToString("0.000"));
					this.determineHardStoOrHardStart (initialSpeedArray, finalSpeedArray, initialHeading, finalHeading);

					this.clearArray (initialSpeedArray);
					this.clearArray (finalSpeedArray);
					//Reset for new event
					isCountingFinalSpeed = false;
					isCountingInitialSpeed = false;
					initialSpeedArrayCounter = 0;
					finalSpeedArrayCounter = 0;
				}

				//this.avgAcc.Text = avgaccel.ToString ("0.0000");
				/*
				if (avgaccel > currentMaxAvgAccel) {
					currentMaxAvgAccel = avgaccel;
				}
				*/
			}
			//this.maxAvgAcc.Text = currentMaxAvgAccel.ToString ("0.0000");
		}

		partial void stopButton (NSObject sender)
		{
			double currentLatitude = rawGPS.getCurrentUserLatitude();
			double currentLongitude = rawGPS.getCurrentUserLongitude();
			if(currentLatitude != rawGPS.GpsNotAvailableFlag() && currentLongitude != rawGPS.getCurrentUserLongitude() )
			{
				CLLocationCoordinate2D stopLocation = new CLLocationCoordinate2D (currentLatitude, currentLongitude);
				rawGPS.listOfRawGPSTripLocationCoordinates.Add (new CLLocation(stopLocation.Latitude, stopLocation.Longitude));
				rawGPS.GMDSSingleCalloutCoordinateBuffer.Add (stopLocation);
				Console.WriteLine ("Added coordinate:" + stopLocation.Latitude + ","+stopLocation.Longitude);
			}
			rawGPS.makeGoogleCalloutUsingBuffer();
			listOfTripLocationCoordinates = rawGPS.listOfRawGPSTripLocationCoordinates;
			distanceTraveledForCurrentTrip = rawGPS.convertMetersToKilometers (rawGPS.CalculateDistanceTraveled (rawGPS.listOfRawGPSTripLocationCoordinates));
			rawGPS.stopGPSReadings ();
			rawGPS.clearPathBuffer();
			this.checkIfDriverTraveledMinimumDistance(MINIMUM_DISTANCE_REQUIRED);
		}
		public void checkIfDriverTraveledMinimumDistance(int minDist){
			if(distanceTraveledForCurrentTrip < minDist){
				fileManager.clearCurrentTripEventFile();
				fileManager.clearCurrentTripDistanceFile();
				fileManager.clearGooglePathFile();
				new UIAlertView ("Oops!", "You have to travel at least " + MINIMUM_DISTANCE_REQUIRED + " Km for it to count as a trip!", null, "My Bad!", null).Show();
				MainNavigationController.goTrip = true;
				DismissViewController(true, null);
			}
		}
		public void clearArray(double[] array){
			for (int i = 0; i< array.Length; i++) {
				array [i] = 0;
			}
		}
		//Get minimum value of an array of doubles method
		public double getMin (double[] array)
		{
			double temp = array [0];
			for (int i = 1; i<array.Length; i++) {
				if (array [i] < temp) {
					temp = array [i];
				}
			}
			return temp;
		}
		//Get maximum value of an array of doubles method
		public double getMax (double[] array)
		{
			double temp = array [0];
			for (int i = 1; i<array.Length; i++) {
				if (array [i] > temp) {
					temp = array [i];
				}
			}
			return temp;
		}
		//Get average value of an array of doubles method
		public double getAvg (double[] array)
		{
			double avg = 0;
			for (int i = 0; i < array.Length; i++) {
				avg += array [i];
			}
			avg = avg / (array.Length);
			return avg;
		}
		//Test for different event types (Hard Start/Stop)
		public void determineHardStoOrHardStart (double[] initialSpeedArray, double[] finalSpeedArray, double initialHeading, double finalHeading)
		{
			Console.WriteLine ("av1: " + this.getAvg (initialSpeedArray));
			Console.WriteLine ("av2: " + this.getAvg (finalSpeedArray));

			Console.WriteLine ("Initial min: " + this.getMin (initialSpeedArray));
			Console.WriteLine ("Final Max: " + this.getMax (finalSpeedArray));


			Console.WriteLine ("Speed: " + this.initialSpeedArray[initialSpeedArray.Length - 1].ToString("0.00"));

			Console.WriteLine ("Speed2: " + this.finalSpeedArray[0].ToString("0.00"));

			//Console.WriteLine ("speed2: " + this.finalSpeedArray [0].ToString ("0.00"));
			Console.WriteLine("intitial heading: " + this.initialHeading.ToString("0.00"));
			Console.WriteLine("final heading: " + this.finalHeading.ToString("0.00"));


			double changeInHeading = normalizeHeadings(initialHeading,finalHeading);


			Console.WriteLine("change in heading: " + changeInHeading.ToString("0.00"));

			//Hard Stop Logic... Using Average of Arrays
			if (initialSpeedArray [initialSpeedArray.Length - 1] < initialSpeedArray [0]) {

				if((changeInHeading>TURNING_THRESHOLD) && (initialSpeedArray[initialSpeedArray.Length - 1] > TURNING_SPEED_THRESHOLD)){
					Console.WriteLine ("Hard Turn recorded!");
					if (MainViewController.isAudioOn) {
						//enable audio
						AudioSession.Initialize ();


						//load the sound
						sound = SystemSound.FromFile ("Sounds/HardTurn.aiff");

						sound.PlaySystemSound ();
					}
					double currentLatitude = rawGPS.getCurrentUserLatitude ();
					double currentLongitude = rawGPS.getCurrentUserLongitude ();
					if(currentLatitude != rawGPS.GpsNotAvailableFlag() && currentLongitude != rawGPS.GpsNotAvailableFlag() ){
						currentCoord.Latitude = currentLatitude;
						currentCoord.Longitude = currentLongitude;

						Event newevent = new Event (currentCoord, HARD_TURN_TYPE);
						//coordList.Add (currentCoord);
						fileManager.addEventToTripEventFile (newevent);
						Console.WriteLine ("Added hard turn:" + newevent.Location.Latitude + ","+newevent.Location.Longitude);
					}

				}else if ((this.getAvg (initialSpeedArray) - this.getAvg (finalSpeedArray)) > SPEED_THRESHOLD_BRAKING) {
					numberHardStops++;
					Console.WriteLine ("Hard stop recorded!");
					if (MainViewController.isAudioOn) {
						//enable audio
						AudioSession.Initialize ();


						//load the sound
						sound = SystemSound.FromFile ("Sounds/HardStop.aiff");

						sound.PlaySystemSound ();
					}
					double currentLatitude = rawGPS.getCurrentUserLatitude ();
					double currentLongitude = rawGPS.getCurrentUserLongitude ();
					if(currentLatitude != rawGPS.GpsNotAvailableFlag() && currentLongitude != rawGPS.GpsNotAvailableFlag()){
						currentCoord.Latitude = currentLatitude;
						currentCoord.Longitude = currentLongitude;
						Event newevent = new Event (currentCoord, HARD_BRAKE_TYPE);
						//coordList.Add (currentCoord);
						fileManager.addEventToTripEventFile (newevent);
						Console.WriteLine ("Added hard stop:" + newevent.Location.Latitude + ","+newevent.Location.Longitude);
					}
				}
			}else{

				//Hard Start Logic... Using Min and Max of Arrays
				if((changeInHeading>TURNING_THRESHOLD) && (initialSpeedArray[initialSpeedArray.Length - 1] > TURNING_SPEED_THRESHOLD)){
					Console.WriteLine ("Hard Turn recorded!");
					if (MainViewController.isAudioOn) {
						//enable audio
						AudioSession.Initialize ();


						//load the sound
						sound = SystemSound.FromFile ("Sounds/HardTurn.aiff");

						sound.PlaySystemSound ();
					}
					double currentLatitude = rawGPS.getCurrentUserLatitude ();
					double currentLongitude = rawGPS.getCurrentUserLongitude ();
					if (currentLatitude != rawGPS.GpsNotAvailableFlag () && currentLongitude != rawGPS.GpsNotAvailableFlag ()) {
						currentCoord.Latitude = currentLatitude;
						currentCoord.Longitude = currentLongitude;
						Event newevent = new Event (currentCoord, HARD_TURN_TYPE);
						//coordList.Add (currentCoord);
						fileManager.addEventToTripEventFile (newevent);
						Console.WriteLine ("Added hard turn:" + newevent.Location.Latitude + ","+newevent.Location.Longitude);
					}

				}else if (((this.getAvg (finalSpeedArray) - this.getAvg (initialSpeedArray)) > SPEED_THRESHOLD_STARTS)) {
					numberHardStarts++;
					Console.WriteLine ("Hard start recorded!");
					if (MainViewController.isAudioOn) {
						//enable audio
						AudioSession.Initialize ();


						//load the sound
						sound = SystemSound.FromFile ("Sounds/HardStart.aiff");

						sound.PlaySystemSound ();
					}
					double currentLatitude = rawGPS.getCurrentUserLatitude ();
					double currentLongitude = rawGPS.getCurrentUserLongitude ();
					if (currentLatitude != rawGPS.GpsNotAvailableFlag () && currentLongitude != rawGPS.GpsNotAvailableFlag ()) {

						currentCoord.Latitude = currentLatitude;
						currentCoord.Longitude = currentLongitude;
						Event newevent = new Event (currentCoord, HARD_ACCEL_TYPE);
						//coordList.Add (currentCoord);
						fileManager.addEventToTripEventFile (newevent);
						Console.WriteLine ("Added hard start:" + newevent.Location.Latitude + ","+newevent.Location.Longitude);
					}
				}

			}
		}


		public double normalizeHeadings(double initialHeading, double finalHeading){

			Console.WriteLine ("Before Normalized Initial Heading:" + initialHeading.ToString ());
			Console.WriteLine ("Before Normalized Final Heading:" + finalHeading.ToString ());

			if (Math.Abs (finalHeading - initialHeading) > 180) {
				return 360 - Math.Abs (finalHeading - initialHeading);
			}
			return Math.Abs(finalHeading-initialHeading);
		}
		partial void stopButtonSound (NSObject sender)
		{
			if (MainViewController.isAudioOn) {
				//enable audio
				AudioSession.Initialize ();
				//load the sound
				sound = SystemSound.FromFile ("Sounds/Screech.aiff");

				sound.PlaySystemSound ();
			}

		}
		public override void ViewWillAppear (bool animated)
		{
			//add users location when trip starts
			if (CLLocationManager.Status == CLAuthorizationStatus.Authorized) {
				double currentLatitude = rawGPS.getCurrentUserLatitude ();
				double currentLongitude = rawGPS.getCurrentUserLongitude ();
				if(currentLatitude != rawGPS.GpsNotAvailableFlag() && currentLongitude != rawGPS.GpsNotAvailableFlag()){
					CLLocationCoordinate2D startLocation = new CLLocationCoordinate2D (currentLatitude,currentLongitude);
					rawGPS.listOfRawGPSTripLocationCoordinates.Add (new CLLocation(startLocation.Latitude, startLocation.Longitude));
					rawGPS.GMDSSingleCalloutCoordinateBuffer.Add (startLocation);
					Console.WriteLine ("Added start location:" + startLocation.Latitude + ","+startLocation.Longitude);
				}
			}

			base.ViewWillAppear (animated);

			//Disable Phone Idling
			//UIApplication.SharedApplication.IdleTimerDisabled = true;
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			_motionManager.StopDeviceMotionUpdates ();
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();

			ReleaseDesignerOutlets ();
		}
	}
}