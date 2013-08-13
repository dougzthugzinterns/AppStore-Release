using System;
using MonoTouch.CoreLocation;

namespace FrameWorkApp
{
	public class Trip
	{
		private DateTime dateTime;
		private int hardTurns;
		private int hardBrakes;
		private int hardStarts;
		private double distance;

		public Trip (DateTime dateTime, int hardStarts,int hardBrakes, int hardTurns, double distance)
		{
			this.dateTime = dateTime;
			this.distance = distance;
			this.hardStarts = hardStarts;
			this.hardBrakes = hardBrakes;
			this.hardTurns = hardTurns;
		}

		public int Points(){
			return ((int)(distance*10) + ((-1*10) * (hardBrakes+hardStarts+hardTurns)));
		}
		public DateTime DateTime{
			get { return dateTime; }
			set { this.dateTime = value; }
		}

		public int NumberOfEvents{
			get { return hardBrakes+hardTurns+hardStarts; }
		}

		public int HardBrakes{
			get{ return hardBrakes;}
			set{ hardBrakes = value;}
		}
		public int HardStarts{
			get{ return hardStarts;}
			set{ hardStarts = value;}
		}
		public int HardTurns{
			get{ return hardTurns;}
			set{ hardTurns = value;}
		}
		public double Distance{
			get{ return distance;}
			set{ distance = value;}
		}
		public override string ToString ()
		{
			return DateTime.ToString ("MM/dd/yyyy h:mmtt");
		}

	}
}

