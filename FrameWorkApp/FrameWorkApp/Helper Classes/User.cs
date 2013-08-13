using System;

namespace FrameWorkApp
{
	public class User
	{
		private double totalDistance = 0;
		private int totalNumberOfEvents = 0;
		private int totalPoints = 0;
		private int totalHardStops = 0;
		private int totalHardStarts = 0;
		private int totalHardTurns = 0;
		private int totalNumberTrips = 0;
		private string id="-1";
		private const int UPPER_LIMIT_BEGINNER = 0;
		private const int UPPER_LIMIT_ROOKIE = 1000;
		private const int UPPER_LIMIT_PRO = 5000;
		private const int UPPER_LIMIT_ALLSTAR = 10000;
		private const int UPPER_LIMIT_VETERAN = 50000;
		private int allowAmazonServices = -1;	//-1 Not been Asked Premission, 1 Allow, 0 not allowed

		public User (double totalDistance, int totalPoints, int totalHardStarts, int totalHardStops, int totalHardTurns, int totalNumberTrips, string id, int allowAmazonServices)
		{
			this.totalDistance = totalDistance;
			this.totalPoints = totalPoints;
			this.totalHardStops = totalHardStops;
			this.totalHardStarts = totalHardStarts;
			this.totalHardTurns = totalHardTurns;
			this.totalNumberTrips = totalNumberTrips;
			this.id = id;
			this.allowAmazonServices = allowAmazonServices;
		}

		public double TotalDistance {
			get { return totalDistance;}
			set { this.totalDistance = value;}
		}
		public int AllowAmazonServices{
			get { return allowAmazonServices;}
			set { this.allowAmazonServices = value;}
		}
		public string Id{
			get { return id;}
			set { this.id = value;}
		}
		public int TotalNumberOfEvents {
			get{ return totalHardStops + totalHardStarts + totalHardTurns;}
		}
		public int TotalHardStarts {
			get{ return totalHardStarts;}
			set{ this.totalHardStarts = value;}
		}
		public int TotalHardStops {
			get{ return totalHardStops;}
			set{ this.totalHardStops = value;}
		}
		public int TotalHardTurns {
			get{ return totalHardTurns;}
			set{ this.totalHardTurns = value;}
		}
		public int TotalPoints {
			get { return totalPoints;}
			set { this.totalPoints = value;}
		}
		public int TotalNumberTrips {
			get { return totalNumberTrips;}
			set { this.totalNumberTrips = value;}
		}
		public String Rank{
			get{

				if (totalPoints >= UPPER_LIMIT_VETERAN) {
					return "Veteran";
				}
				else if (totalPoints >= UPPER_LIMIT_ALLSTAR) {
					return "All-Star";
				}
				else if (totalPoints >= UPPER_LIMIT_PRO) {
					return "Pro";
				}
				else if (totalPoints >= UPPER_LIMIT_ROOKIE) {
					return "Rookie";
				}
				else if (totalPoints >= UPPER_LIMIT_BEGINNER) {
					return "Beginner";
				}
				return "";
			}

		}
		public void updateData (double tripDistance, int numOfStops, int numOfStarts, int numOfTurns )
		{
			totalDistance += tripDistance;
			totalNumberOfEvents += numOfStops + numOfStarts;
			totalHardStops += numOfStops;
			totalHardStarts += numOfStarts;
			totalHardTurns += numOfTurns;

			totalPoints = totalPoints + Math.Max (this.getCurrentTripPoints(tripDistance,numOfStops,numOfStarts, numOfTurns),0);
			totalNumberTrips ++;

			                        //return ((int)tripDistance + ((-3) * (numOfStops+numOfStarts)));
		}

		public int getCurrentTripPoints (double tripDistance, int numOfStops, int numOfStarts, int numOfTurns)
		{
			return ((int)(tripDistance*10) + ((-1*10) * (numOfStops+numOfStarts+numOfTurns)));

		}
		public int getPointsPossibleForTrip (double tripDistance){
			return ((int)(tripDistance * 10));
		}
	}
}