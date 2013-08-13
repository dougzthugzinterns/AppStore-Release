using System;

namespace FrameWorkApp
{
	public class Achievement
	{
		private string title;
		private string description;
		private string picturePath;
		private string grayedOutPicturePath;
		private bool activated;
		private int distanceToAccomplish;

		//Achievement Rules

		//distance Achievements Constructor
		public Achievement (string title, string description, string picturePath, string grayedOutPicturePath, bool activated)
		{
			this.title = title;
			this.description = description;
			this.picturePath = picturePath;
			this.grayedOutPicturePath = grayedOutPicturePath;
			this.activated = activated;

		}

		public string Title {
			get{ return title;}
			set { this.title = value;}
		}
		public string Description {
			get{ return description;}
			set { this.description = value;}
		}
		public string PicturePath {
			get{ return picturePath;}
			set { this.picturePath = value;}
		}
		public string GrayedOutPicturePath {
			get{ return grayedOutPicturePath;}
			set { this.grayedOutPicturePath = value;}
		}
		public bool Activated {
			get{ return activated;}
			set { this.activated = value;}
		}


	}
}

