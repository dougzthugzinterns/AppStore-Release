// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FrameWorkApp
{
	public partial class MenuScreen : UITableViewController
	{
		public MenuScreen (IntPtr handle) : base (handle)
		{
		}

		public override bool ShouldAutorotate ()
		{
			return false;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.Portrait;
		}

		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation ()
		{
			return UIInterfaceOrientation.Portrait;
		}

		/*public override bool shouldAutorotate
		{
			return [[self.viewControllers lastObject] shouldAutorotate];
		}

		-(NSUInteger)supportedInterfaceOrientations
		{
			return [[self.viewControllers lastObject] supportedInterfaceOrientations];
		}

		- (UIInterfaceOrientation)preferredInterfaceOrientationForPresentation
		{
			return [[self.viewControllers lastObject] preferredInterfaceOrientationForPresentation];
		}
*/
		partial void backButton (NSObject sender)
		{
			DismissViewController(true,null);
		}
	
	}
}
