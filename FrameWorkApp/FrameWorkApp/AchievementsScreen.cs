// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace FrameWorkApp
{
	public partial class AchievementsScreen : UIViewController
	{
		private User currentUser;


		SDMFileManager fileManager;
		public AchievementsScreen (IntPtr handle) : base (handle)
		{
			fileManager = new SDMFileManager ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			MainViewController.hasJustUnlockedAchievement = false;
		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
			AchievementData dataToBeShown = fileManager.readAchievementsFile ();



			currentUser = fileManager.readUserFile ();


			rookieBadge.TouchUpInside += (object sender, EventArgs e) => {


				new UIAlertView ("Rookie", "Earn 1000 Points!", null, "Cool!", null).Show ();

			};

			proBadge.TouchUpInside += (object sender, EventArgs e) => {
			
				new UIAlertView ("Pro", "Earn 5000 Points!", null, "Cool!", null).Show ();

			};

			allstarBadge.TouchUpInside += (object sender, EventArgs e) => {


				new UIAlertView ("All-Star", "Earn 10,000 Points!", null, "Cool!", null).Show ();

			};

			veteranBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView ("Veteran", "Earn 50,000 Points!", null, "Cool!", null).Show ();

			};

			if(currentUser.Rank.Equals("Rookie")) {
				this.rookieBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-24.png"), UIControlState.Normal);
			}


			else if(currentUser.Rank.Equals("Pro")) {
				this.rookieBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-24.png"), UIControlState.Normal);
				this.proBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-20.png"), UIControlState.Normal);
			}


			else if(currentUser.Rank.Equals("All-Star")) {
				this.rookieBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-24.png"), UIControlState.Normal);
				this.proBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-20.png"), UIControlState.Normal);
				this.allstarBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-21.png"), UIControlState.Normal);
			}
			else if(currentUser.Rank.Equals("Veteran")) {
				this.veteranBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-22.png"), UIControlState.Normal);
				this.rookieBadge.SetImage (UIImage.FromFile ("Win.png"), UIControlState.Normal);
				this.proBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-20.png"), UIControlState.Normal);
				this.allstarBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-21.png"), UIControlState.Normal);
			}


			if(dataToBeShown.TrainingWheelsValue){
				this.trainingWheelsBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-16.png"), UIControlState.Normal);
			}
			trainingWheelsBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[0].Title, dataToBeShown.AchievementList[0].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.SmoothOperatorValue){
				this.smoothOperatorBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-19.png"), UIControlState.Normal);
			}
			smoothOperatorBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[1].Title, dataToBeShown.AchievementList[1].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.TediousTurnerValue){
				this.tediousTurnerBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-13.png"), UIControlState.Normal);
			}
			tediousTurnerBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[2].Title, dataToBeShown.AchievementList[2].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.GrannyStyleValue){
				this.grannyStyleBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-02.png"), UIControlState.Normal);
			}
			grannyStyleBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[3].Title, dataToBeShown.AchievementList[3].Description, null, "Cool!", null).Show ();


			};

			if(dataToBeShown.CarefulCatieValue){
				this.carefulCatieBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-11.png"), UIControlState.Normal);
			}
			carefulCatieBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[4].Title, dataToBeShown.AchievementList[4].Description, null, "Cool!", null).Show ();

			};


			if(dataToBeShown.TakeTenValue){
				this.takeTenBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-01.png"), UIControlState.Normal);
			}
			takeTenBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[5].Title, dataToBeShown.AchievementList[5].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.KangarooJackValue){
				this.kangarooJackBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-15.png"), UIControlState.Normal);
			}
			kangarooJackBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[6].Title, dataToBeShown.AchievementList[6].Description, null, "Cool!", null).Show ();

			};


			if(dataToBeShown.CrossCountryValue){
				this.crossCountryBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-04.png"), UIControlState.Normal);
			}
			crossCountryBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[7].Title, dataToBeShown.AchievementList[7].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.RoadWarriorValue){
				this.roadWarriorBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-03.png"), UIControlState.Normal);
			}
			roadWarriorBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[8].Title, dataToBeShown.AchievementList[8].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.CrikeyValue){
				this.crikeyBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-05.png"), UIControlState.Normal);
			}
			crikeyBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[9].Title, dataToBeShown.AchievementList[9].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.SuperSteveValue){
				this.superSteveBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-17.png"), UIControlState.Normal);
			}
			superSteveBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[10].Title, dataToBeShown.AchievementList[10].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.PerfectPeterValue){
				this.perfectPeterBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-14.png"), UIControlState.Normal);
			}
			perfectPeterBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[11].Title, dataToBeShown.AchievementList[11].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.ConstantCommuterValue){
				this.constantCommuterBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-09.png"), UIControlState.Normal);
			}
			constantCommuterBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[12].Title, dataToBeShown.AchievementList[12].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.FrequentFlyerValue){
				this.frequentFlyerBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-18.png"), UIControlState.Normal);
			}
			frequentFlyerBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[13].Title, dataToBeShown.AchievementList[13].Description, null, "Cool!", null).Show ();

			};

			if(dataToBeShown.SteadyPedalerValue){
				this.steadyPedalerBadge.SetImage (UIImage.FromFile ("ACHIEVEMENTS-10.png"), UIControlState.Normal);
			}
			steadyPedalerBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView (dataToBeShown.AchievementList[14].Title, dataToBeShown.AchievementList[14].Description, null, "Cool!", null).Show ();

			};


			showBoatBadge.TouchUpInside += (object sender, EventArgs e) => {

				new UIAlertView ("Show Boat", "You've downloaded Safe Driving Mate!", null, "Cool!", null).Show ();

			};


		}


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
	}
}