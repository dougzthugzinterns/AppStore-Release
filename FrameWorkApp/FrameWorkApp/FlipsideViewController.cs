using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AudioToolbox;

namespace FrameWorkApp
{
	public partial class FlipsideViewController : UIViewController
	{

		private SystemSound sound;

		public FlipsideViewController (IntPtr handle) : base (handle)
		{
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
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



			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
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
		partial void done (UIBarButtonItem sender)
		{
			this.DismissModalViewControllerAnimated (true);
			
			if (Done != null)
				Done (this, EventArgs.Empty);
		}

		public event EventHandler Done;
	}
}

