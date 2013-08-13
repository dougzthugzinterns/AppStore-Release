using System;
using System.Collections.Generic;

namespace FrameWorkApp
{
	public class AchievementData
	{
		//greyed file paths
		private string trainingWheelsGreyPhotoPath = "TrainingLocked.png";
		private string smoothOperatorGreyPhotoPath = "OperatorLocked.png";
		private string tediousTurnerGreyPhotoPath = "TurnerLocked.png";
		private string grannyStyleGreyPhotoPath = "GrannyLocked.png";
		private string carefulCatieGreyPhotoPath = "CatieLocked.png";
		private string takeTenGreyPhotoPath = "TakeTenLocked.png";
		private string kangarooJackGreyPhotoPath = "KangarooLocked.png";
		private string crossCountryGreyPhotoPath = "XCountryLocked.png";
		private string roadWarriorGreyPhotoPath = "WarriorLocked.png";
		private string crikeyGreyPhotoPath = "CrikeyLocked.png";
		private string superSteveGreyPhotoPath = "SuperLocked.png";
		private string perfectPeterGreyPhotoPath = "PeteLocked.png";
		private string constantCommuterGreyPhotoPath = "CommuterLocked.png";
		private string frequentFlyerGreyPhotoPath = "FlyerLocked.png";
		private string steadyPedalerGreyPhotoPath = "PedalerLocked.png";

		//regular photo file paths
		private string trainingWheelsPhotoPath = "ACHIEVEMENTS-16.png";
		private string smoothOperatorPhotoPath = "ACHIEVEMENTS-19.png";
		private string tediousTurnerPhotoPath = "ACHIEVEMENTS-13.png";
		private string grannyStylePhotoPath = "ACHIEVEMENTS-02.png";
		private string carefulCatiePhotoPath = "ACHIEVEMENTS-11.png";
		private string takeTenPhotoPath = "ACHIEVEMENTS-01.png";
		private string kangarooJackPhotoPath = "ACHIEVEMENTS-15.png";
		private string crossCountryPhotoPath = "ACHIEVEMENTS-04.png";
		private string roadWarriorPhotoPath = "ACHIEVEMENTS-03.png";
		private string crikeyPhotoPath = "ACHIEVEMENTS-05.png";
		private string superStevePhotoPath = "ACHIEVEMENTS-17.png";
		private string perfectPeterPhotoPath = "ACHIEVEMENTS-14.png";
		private string constantCommuterPhotoPath = "ACHIEVEMENTS-09.png";
		private string frequentFlyerPhotoPath = "ACHIEVEMENTS-18.png";
		private string steadyPedalerPhotoPath = "ACHIEVEMENTS-10.png";

		//Achievement names

		public static string smoothOperatorTitle = "Smooth Operator";
		public static string tediousTurnerTitle = "Tedious Turner";
		public static string grannyStyleTitle = "Granny Style";
		public static string carefulCatieTitle = "Careful Catie";
		public static string takeTenTitle = "Take Ten";
		public static string kangarooJackTitle = "Kangaroo Jack";
		public static string crossCountryTitle  = "Cross Country";
		public static string roadWarriorTitle = "Road Warrior";
		public static string crikeyTitle = "Crikey";
		public static string superSteveTitle = "Super Steve";
		public static string trainingWheelsTitle = "Training Wheels";
		public static string perfectPeterTitle = "Perfect Peter";
		public static string constantCommuterTitle = "Constant Commuter";
		public static string frequentFlyerTitle = "Frequent Flyer";
		public static string steadyPedalerTitle = "Steady Pedaler";


		//Values from Achievement File
		private int numberConsecutiveDaysOfAppUse;
		private DateTime lastDateOfAppUse;
		private int numberConsecutiveTripsWithoutIncidents;
		private bool takeTenValue;
		private bool kangarooJackValue;
		private bool crossCountryValue;
		private bool roadWarriorValue;
		private bool crikeyValue;
		private bool superSteveValue;
		private bool smoothOperatorValue;
		private bool tediousTurnerValue;
		private bool grannyStyleValue;
		private bool carefulCatieValue;
		private bool trainingWheelsValue;
		private bool perfectPeterValue;
		private bool constantCommuterValue;
		private bool frequentFlyerValue;
		private bool steadyPedalerValue;

		private List<Achievement> achievementList;

		public List<Achievement> AchievementList{
			get{return achievementList;}
		}
		public bool TrainingWheelsValue {
			get{ return trainingWheelsValue;}
			set{ this.trainingWheelsValue = value;}
		}
		public bool PerfectPeterValue {
			get{ return perfectPeterValue;}
			set{ this.perfectPeterValue = value;}
		}
		public bool ConstantCommuterValue {
			get{ return constantCommuterValue;}
			set{ this.constantCommuterValue = value;}
		}
		public bool FrequentFlyerValue {
			get{ return frequentFlyerValue;}
			set{ this.frequentFlyerValue = value;}
		}
		public bool SteadyPedalerValue {
			get{ return steadyPedalerValue;}
			set{ this.steadyPedalerValue = value;}
		}
		public int NumberConsecutiveDaysOfAppUse {
			get{ return numberConsecutiveDaysOfAppUse;}
			set{ this.numberConsecutiveDaysOfAppUse = value;}
		}
		public DateTime LastDateOfAppUse {
			get{ return lastDateOfAppUse;}
			set{ this.lastDateOfAppUse = value;}
		}
		public int NumberConsecutiveTripsWithoutIncidents {
			get{ return numberConsecutiveTripsWithoutIncidents;}
			set{ this.numberConsecutiveTripsWithoutIncidents = value;}
		}
		public bool TakeTenValue {
			get{ return takeTenValue;}
			set{ this.takeTenValue = value;}
		}
		public bool KangarooJackValue {
			get{ return kangarooJackValue;}
			set{ this.kangarooJackValue = value;}
		}
		public bool CrossCountryValue {
			get{ return crossCountryValue;}
			set{ this.crossCountryValue = value;}
		}
		public bool RoadWarriorValue {
			get{ return roadWarriorValue;}
			set{ this.roadWarriorValue = value;}
		}
		public bool CrikeyValue {
			get{ return crikeyValue;}
			set{ this.crikeyValue = value;}
		}
		public bool SuperSteveValue {
			get{ return superSteveValue;}
			set{ this.superSteveValue = value;}
		}
		public bool SmoothOperatorValue {
			get{ return smoothOperatorValue;}
			set{ this.smoothOperatorValue = value;}
		}
		public bool TediousTurnerValue {
			get{ return tediousTurnerValue;}
			set{ this.tediousTurnerValue = value;}
		}
		public bool GrannyStyleValue {
			get{ return grannyStyleValue;}
			set{ this.grannyStyleValue = value;}
		}
		public bool CarefulCatieValue {
			get{ return carefulCatieValue;}
			set{ this.carefulCatieValue = value;}
		}


		public AchievementData (int numberConsecutiveDaysOfAppUse, DateTime lastDateOfAppUse, int numberConsecutiveTripsWithoutIncidents, List<bool> achievementsActivated)
		{
			this.numberConsecutiveDaysOfAppUse = numberConsecutiveDaysOfAppUse;
			this.lastDateOfAppUse = lastDateOfAppUse;
			this.numberConsecutiveTripsWithoutIncidents = numberConsecutiveTripsWithoutIncidents;
			this.takeTenValue = achievementsActivated [0];
			this.kangarooJackValue = achievementsActivated [1];
			this.crossCountryValue= achievementsActivated [2];
			this.roadWarriorValue= achievementsActivated [3];
			this.crikeyValue= achievementsActivated [4];
			this.superSteveValue= achievementsActivated [5];
			this.smoothOperatorValue= achievementsActivated [6];
			this.tediousTurnerValue= achievementsActivated [7];
			this.grannyStyleValue= achievementsActivated [8];
			this.carefulCatieValue= achievementsActivated [9];
			this.trainingWheelsValue = achievementsActivated [10];
			this.perfectPeterValue = achievementsActivated [11];
			this.constantCommuterValue = achievementsActivated [12];
			this.frequentFlyerValue = achievementsActivated [13];
			this.steadyPedalerValue = achievementsActivated [14];
			achievementList = new List<Achievement> ();
			this.addAchievementsToList ();
		}

		public void addAchievementsToList(){
			achievementList.Add(new Achievement(trainingWheelsTitle, "You have taken your first trip!", trainingWheelsPhotoPath, trainingWheelsGreyPhotoPath, trainingWheelsValue));
			achievementList.Add(new Achievement(smoothOperatorTitle, "You had no hard brakes in a trip.", smoothOperatorPhotoPath, smoothOperatorGreyPhotoPath, smoothOperatorValue));
			achievementList.Add(new Achievement(tediousTurnerTitle, "You had no hard turns in a trip.", tediousTurnerPhotoPath, tediousTurnerGreyPhotoPath, tediousTurnerValue));
			achievementList.Add(new Achievement(grannyStyleTitle, "You had a 50 Km or greater trip with no incident!", grannyStylePhotoPath, grannyStyleGreyPhotoPath, grannyStyleValue));
			achievementList.Add(new Achievement(carefulCatieTitle, "You had a 50 Km or greater trip with no hard starts!", carefulCatiePhotoPath, carefulCatieGreyPhotoPath, carefulCatieValue));
			achievementList.Add(new Achievement(takeTenTitle, "You drove 10 Km.", takeTenPhotoPath, takeTenGreyPhotoPath, takeTenValue));
			achievementList.Add(new Achievement(kangarooJackTitle, "You drove " + AchievementsCalculator.kangarooJackDist + "Km.", kangarooJackPhotoPath, kangarooJackGreyPhotoPath, kangarooJackValue));
			achievementList.Add(new Achievement(crossCountryTitle, "You drove " + AchievementsCalculator.crossCountryDist + "Km.", crossCountryPhotoPath, crossCountryGreyPhotoPath, crossCountryValue));
			achievementList.Add(new Achievement(roadWarriorTitle, "You drove " + AchievementsCalculator.roadWarriorDist + "Km.", roadWarriorPhotoPath, roadWarriorGreyPhotoPath, roadWarriorValue));
			achievementList.Add(new Achievement(crikeyTitle, "You drove " + AchievementsCalculator.crikeyDist + "Km.", crikeyPhotoPath, crikeyGreyPhotoPath, crikeyValue));
			achievementList.Add(new Achievement(superSteveTitle, "You drove " + AchievementsCalculator.superSteveDist + "Km.", superStevePhotoPath, superSteveGreyPhotoPath, superSteveValue));
			achievementList.Add(new Achievement(perfectPeterTitle, "You have taken ten trips with no incident!", perfectPeterPhotoPath, perfectPeterGreyPhotoPath, perfectPeterValue));
			achievementList.Add(new Achievement(constantCommuterTitle, "You have used the app for fourteen consecutive days!", constantCommuterPhotoPath, constantCommuterGreyPhotoPath, constantCommuterValue));
			achievementList.Add(new Achievement(frequentFlyerTitle, "You have used the app for seven consecutive days!", frequentFlyerPhotoPath, frequentFlyerGreyPhotoPath, frequentFlyerValue));
			achievementList.Add(new Achievement(steadyPedalerTitle, "You had no hard starts in a trip!", steadyPedalerPhotoPath, steadyPedalerGreyPhotoPath, steadyPedalerValue));

		}
	}
}

