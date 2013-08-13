using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.CoreLocation;
using System.Collections.Generic;
using MonoTouch.AudioToolbox;


namespace FrameWorkApp
{
	public partial class MainViewController : UIViewController
	{
		private SystemSound sound;


		SDMFileManager fileManager= new SDMFileManager();
		CLLocationManager manager = new CLLocationManager ();
		public static bool isAudioOn = true;
		public static bool hasJustUnlockedAchievement = false;

	
		//Event Type Constants
		const int UNKNOWN_EVENT_TYPE= 0;
		const int HARD_BRAKE_TYPE = 1;
		const int HARD_ACCEL_TYPE = 2;
		const int HARD_TURN_TYPE = 3;

		//Constants for Amazon Web Services
		const string ACCESSID = "AKIAJZAYKXMCJPGUESMA";
		const string PRIVATEKEY = "0mad7Rwe6/zkXGa2sOILB3gEMcfEZvpQIalORBRz";
		const string HOST = "https://dynamodb.us-west-2.amazonaws.com";
		const string REGION="us-west-2";


		public MainViewController (IntPtr handle) : base (handle)
		{
			// Custom initialization
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.Portrait;
		}
		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation ()
		{
			return UIInterfaceOrientation.Portrait;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}
		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RawGPS rawGPS = new RawGPS ();
			double totalDistance = rawGPS.convertMetersToKilometers(rawGPS.CalculateDistanceTraveled(new List<CLLocation>(fileManager.readDataFromTripDistanceFile())));
			User currentUser = fileManager.readUserFile ();

			//Phone Crashed during a Trip in Progress, write data recovered to trip log.
			if (fileManager.currentTripInProgress() && totalDistance >= 1) {


				//Read Data from Recovered File.
				Event[] events = fileManager.readDataFromTripEventFile ();
				int numberOfStarts = 0;
				int numberOfStops = 0;
				int numberOfTurns = 0;
				foreach (var e in events) {
					if (e.Type== HARD_ACCEL_TYPE) {
						numberOfStarts++;
					}
					else if (e.Type == HARD_BRAKE_TYPE) {
						numberOfStops++;
					}
					else if (e.Type == HARD_TURN_TYPE) {
						numberOfTurns++;
					}
				}
				if(currentUser.AllowAmazonServices==1){
					AmazonWebServices amazon = new AmazonWebServices (HOST, REGION, ACCESSID, PRIVATEKEY);

					if (currentUser.Id == "-1") {
						//Generate Unique ID
						string id = "";
						do {
							Random random = new Random ();
							id = random.Next ().ToString ("X8") + random.Next ().ToString ("X8");
						} while(amazon.checkIdExists(id));
						currentUser.Id=id;
					}
					//Send data to Amazon Web Services
					amazon.sendTripData((int)totalDistance, numberOfStops, numberOfStarts, numberOfTurns, currentUser.Id);
				}

				fileManager.addDataToTripLogFile(new Trip(fileManager.getDateOfLastPointEnteredInCurrentTrip(), numberOfStarts,numberOfStops,numberOfTurns, (int)totalDistance));
				//Update user data
				currentUser.updateData (totalDistance, numberOfStops, numberOfStarts, numberOfTurns);
				fileManager.updateUserFile(currentUser);

				//Clear current trip files
				fileManager.deleteCurrentTripData();
				fileManager.deleteCurrentTripDistanceFile ();

				//Update Achievements Files
				AchievementsCalculator calcuationAfterCrashedTrip = new AchievementsCalculator(numberOfStarts, numberOfStops, numberOfTurns, totalDistance);
				calcuationAfterCrashedTrip.recalculateAchievements ();

				//Display Alert
				new UIAlertView ("Trip Data Recovered!", "We detected your phone has shut down during a trip, but good news, we managed to recover your data up to that point your phone shut down.", null, "Yay!", null).Show ();
			}
			if(CLLocationManager.Status==CLAuthorizationStatus.NotDetermined){

				manager.AuthorizationChanged += (sender,e) => {
					if(CLLocationManager.Status == CLAuthorizationStatus.Denied){
						new UIAlertView("Location Services must be enabled to use application!","We noticed you have disabled location services for this application. Please enable these before continuing. Please enable these before starting a new trip.", null, "OK", null).Show();
					}
				};
				manager.StartUpdatingLocation ();
				manager.StopUpdatingLocation ();
			}

			else{
				if(CLLocationManager.Status == CLAuthorizationStatus.Denied){
					new UIAlertView("Location Services must be enabled to use application!","We noticed you have disabled location services for this application. Please enable these before continuing. Please enable these before starting a new trip.", null, "OK", null).Show();
				}
			}

			if (currentUser.AllowAmazonServices == -1) {
				UIAlertView newAlert=new UIAlertView("Send Data", "Would you like to send data about your trips to help improve Safe Driving Mate? Don't worry, all recorded information is annonyous and will not be disclosed to third parties.", null, "No", "Yes");
				newAlert.Clicked += (object sender2, UIButtonEventArgs e) => {
					if(e.ButtonIndex==1){
						//If Yes Button is Hit
						currentUser.AllowAmazonServices=1;     
					}
					else{
						currentUser.AllowAmazonServices=0;
					}
					fileManager.updateUserFile(currentUser);
				};
				newAlert.Show ();

			}


		}


		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (MainViewController.isAudioOn) {
				isAudioEnabled.SetState (true,false);
			} else {
				isAudioEnabled.SetState (false, false);
			}
			if(MainViewController.hasJustUnlockedAchievement){
				this.badgeNotification.SetImage (UIImage.FromFile ("BadgesNotifications.png"),UIControlState.Normal);
			}
			else{
				this.badgeNotification.SetImage(UIImage.FromFile("BadgesOff.png"), UIControlState.Normal);
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

		}

		public override void ViewWillDisappear (bool animated)
		{
				base.ViewWillDisappear (animated);


		}

		partial void audioSwitch (NSObject sender)
		{
			if (isAudioEnabled.On) {
				MainViewController.isAudioOn = true;
			} else {
				MainViewController.isAudioOn = false;
			}
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
		#endregion

		partial void toInfo (NSObject sender)
		{
			MainNavigationController.goInfo = true;
			DismissViewController(false,null);
		}
		partial void toStats (NSObject sender)
		{
			MainNavigationController.goStats = true;
			DismissViewController(false,null);
		}
		partial void toTrip (NSObject sender)
		{
			MainNavigationController.goTrip = true;
			DismissViewController(false,null);
		}
		partial void toTrophies (NSObject sender)
		{
			MainNavigationController.goTrophies = true;
			DismissViewController(false,null);
		}

		partial void showInfoTouchDown (NSObject sender)
		{

			/*if (MainViewController.isAudioOn) {
				//enable audio
				AudioSession.Initialize ();


				//load the sound
				sound = SystemSound.FromFile ("Sounds/Water.aiff");

				sound.PlaySystemSound ();
			}*/

		}



		partial void startButtonTouchDown (NSObject sender)
		{
			if (MainViewController.isAudioOn) {
				//enable audio
				AudioSession.Initialize ();


				//load the sound
				sound = SystemSound.FromFile ("Sounds/PowerWrench.aiff");

				sound.PlaySystemSound ();
			}

		}
		partial void menuButtonTouchDown (NSObject sender)
		{
			/*if (MainViewController.isAudioOn) {
				//enable audio
				AudioSession.Initialize ();


				//load the sound
				sound = SystemSound.FromFile ("Sounds/KeyboardClick.aiff");

				sound.PlaySystemSound ();
			}*/

		}
	}
}