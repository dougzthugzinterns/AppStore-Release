using System;
using System.Collections.Generic;

namespace FrameWorkApp
{
	public class AchievementsCalculator
	{
		SDMFileManager fileManager;
		User currentUser;
		AchievementData currentAchievementData;
		//distance constants
		public static int takeTenDist = 10;
		public static int kangarooJackDist = 100;
		public static int crossCountryDist = 4000;
		public static int roadWarriorDist = 1000;
		public static int crikeyDist = 500;
		public static int superSteveDist = 2000;
		//file variables
		private double totalDistance;
		private int totalNumOfTrips;
		//private int numConsecutiveDaysOfAppUse;
		//private DateTime lastDayofAppUse;
		//private int numConsecutiveTripsWithNoIncident;
		//current Trip variables
		private int currentNumHardStarts;
		private int CurrentNumHardStops;
		private int currentNumHardTurns;
		private double currentDistForTrip;
		private bool hasJustUnlockedAchievement;



		public AchievementsCalculator (int numHardStartsForCurrentTrip, int numHardStopsForCurrentTrip, int numHardTurnsForCurrenTrip, double distanceTravelledForThisTrip)
		{
			currentNumHardStarts = numHardStartsForCurrentTrip;
			CurrentNumHardStops = numHardStopsForCurrentTrip;
			currentNumHardTurns = numHardTurnsForCurrenTrip;
			currentDistForTrip = distanceTravelledForThisTrip;
			fileManager = new SDMFileManager ();
			this.currentUser = fileManager.readUserFile();
			currentAchievementData = fileManager.readAchievementsFile();
			//totalDistance = 5000.00; //TEST STUFF
			totalDistance = currentUser.TotalDistance;
			totalNumOfTrips = currentUser.TotalNumberTrips;
			hasJustUnlockedAchievement = false;
			//Dont use below, use AchievementData properties instead
			//numConsecutiveDaysOfAppUse = currentAchievementData.NumberConsecutiveDaysOfAppUse;
			//lastDayofAppUse = currentAchievementData.LastDateOfAppUse;
			//numConsecutiveTripsWithNoIncident = currentAchievementData.NumberConsecutiveTripsWithoutIncidents;
		}

		public void recalculateAchievements(){
			AchievementData updatedAchievementData = currentAchievementData;
			bool beforeCalc = false;

			foreach (Achievement currentAchievement in updatedAchievementData.AchievementList) {


				if (AchievementData.smoothOperatorTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.SmoothOperatorValue;
					updatedAchievementData.SmoothOperatorValue = (currentAchievementData.SmoothOperatorValue == true || CurrentNumHardStops == 0) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.SmoothOperatorValue);

				} else if (AchievementData.tediousTurnerTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.TediousTurnerValue;
					updatedAchievementData.TediousTurnerValue = (currentAchievementData.TediousTurnerValue == true || currentNumHardTurns == 0) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.TediousTurnerValue);

				} else if (AchievementData.grannyStyleTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.GrannyStyleValue;
					updatedAchievementData.GrannyStyleValue = (currentAchievementData.GrannyStyleValue == true || (currentDistForTrip >= 50.00 && (currentNumHardStarts + CurrentNumHardStops + currentNumHardTurns == 0))) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.GrannyStyleValue);

				} else if (AchievementData.carefulCatieTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.CarefulCatieValue;
					updatedAchievementData.CarefulCatieValue = (currentAchievementData.CarefulCatieValue == true || (currentDistForTrip >= 50.00 && (currentNumHardStarts == 0))) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.CarefulCatieValue);

				} else if (AchievementData.takeTenTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.TakeTenValue;
					updatedAchievementData.TakeTenValue = (totalDistance >= takeTenDist) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.TakeTenValue);

				} else if (AchievementData.kangarooJackTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.KangarooJackValue;
					updatedAchievementData.KangarooJackValue = (totalDistance >= kangarooJackDist) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.KangarooJackValue);

				} else if (AchievementData.crossCountryTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.CrossCountryValue;
					updatedAchievementData.CrossCountryValue = (totalDistance >= crossCountryDist) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.CrossCountryValue);

				} else if (AchievementData.roadWarriorTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.RoadWarriorValue;
					updatedAchievementData.RoadWarriorValue = (totalDistance >= roadWarriorDist) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.RoadWarriorValue);

				} else if (AchievementData.crikeyTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.CrikeyValue;
					updatedAchievementData.CrikeyValue = (totalDistance >= crikeyDist) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.CrikeyValue);

				} else if (AchievementData.superSteveTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.SuperSteveValue;
					updatedAchievementData.SuperSteveValue = (totalDistance >= superSteveDist) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.SuperSteveValue);

				} else if (AchievementData.trainingWheelsTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.TrainingWheelsValue ;
					updatedAchievementData.TrainingWheelsValue = (currentUser.TotalNumberTrips >= 1) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.TrainingWheelsValue);

				} else if (AchievementData.perfectPeterTitle.Equals (currentAchievement.Title)) {
					if ((CurrentNumHardStops + currentNumHardStarts + currentNumHardTurns) == 0) {
						updatedAchievementData.NumberConsecutiveTripsWithoutIncidents ++;
					} else {
						updatedAchievementData.NumberConsecutiveTripsWithoutIncidents = 0;
					}
					beforeCalc = updatedAchievementData.PerfectPeterValue ;
					updatedAchievementData.PerfectPeterValue = (currentAchievementData.PerfectPeterValue == true || updatedAchievementData.NumberConsecutiveTripsWithoutIncidents >= 10) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.PerfectPeterValue);

				}else if (AchievementData.constantCommuterTitle.Equals (currentAchievement.Title)) {
					TimeSpan daysOfAppBeingNotUsed = DateTime.Today.Date.Subtract (updatedAchievementData.LastDateOfAppUse);
					if ( daysOfAppBeingNotUsed.Equals(TimeSpan.FromDays(1))) {
						updatedAchievementData.NumberConsecutiveDaysOfAppUse ++;
					} else if( daysOfAppBeingNotUsed > TimeSpan.FromDays(1)) {
						updatedAchievementData.NumberConsecutiveDaysOfAppUse = 0;
					}
					updatedAchievementData.LastDateOfAppUse = DateTime.Today.Date;
					beforeCalc = updatedAchievementData.ConstantCommuterValue ;
					updatedAchievementData.ConstantCommuterValue = (currentAchievementData.ConstantCommuterValue == true || updatedAchievementData.NumberConsecutiveDaysOfAppUse >= 14) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.ConstantCommuterValue);

				}else if (AchievementData.frequentFlyerTitle.Equals (currentAchievement.Title)) {
					TimeSpan daysOfAppBeingNotUsed = DateTime.Today.Date.Subtract (updatedAchievementData.LastDateOfAppUse);
					if ( daysOfAppBeingNotUsed.Equals(TimeSpan.FromDays(1))) {
						updatedAchievementData.NumberConsecutiveDaysOfAppUse ++;
					} else if( daysOfAppBeingNotUsed > TimeSpan.FromDays(1)) {
						updatedAchievementData.NumberConsecutiveDaysOfAppUse = 0;
					}
					updatedAchievementData.LastDateOfAppUse = DateTime.Today.Date;
					beforeCalc = updatedAchievementData.FrequentFlyerValue ;
					updatedAchievementData.FrequentFlyerValue = (currentAchievementData.FrequentFlyerValue == true || updatedAchievementData.NumberConsecutiveDaysOfAppUse >= 7) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.FrequentFlyerValue);

				}else if (AchievementData.steadyPedalerTitle.Equals (currentAchievement.Title)) {
					beforeCalc = updatedAchievementData.SteadyPedalerValue ;
					updatedAchievementData.SteadyPedalerValue = (currentAchievementData.SteadyPedalerValue == true || currentNumHardStarts == 0) ? true : false;
					this.detectIfAchievementUnlocked (beforeCalc, updatedAchievementData.SteadyPedalerValue);
				}
			}
			fileManager.updateAchievementsFile(updatedAchievementData);

		}
		public void detectIfAchievementUnlocked(bool beforeCalc, bool afterCalc){
			if (beforeCalc != afterCalc) {
				hasJustUnlockedAchievement = true;      
			}
		}
		public bool getAchievementHasBeenUnlocked{
			get{return hasJustUnlockedAchievement;}
		}

	}
}