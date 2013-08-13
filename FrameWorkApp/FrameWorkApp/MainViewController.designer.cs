// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FrameWorkApp
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton badgeNotification { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch isAudioEnabled { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton startButton { get; set; }

		[Action ("audioSwitch:")]
		partial void audioSwitch (MonoTouch.Foundation.NSObject sender);

		[Action ("menuButtonTouchDown:")]
		partial void menuButtonTouchDown (MonoTouch.Foundation.NSObject sender);

		[Action ("showInfoTouchDown:")]
		partial void showInfoTouchDown (MonoTouch.Foundation.NSObject sender);

		[Action ("startButtonTouchDown:")]
		partial void startButtonTouchDown (MonoTouch.Foundation.NSObject sender);

		[Action ("toInfo:")]
		partial void toInfo (MonoTouch.Foundation.NSObject sender);

		[Action ("toStats:")]
		partial void toStats (MonoTouch.Foundation.NSObject sender);

		[Action ("toTrip:")]
		partial void toTrip (MonoTouch.Foundation.NSObject sender);

		[Action ("toTrophies:")]
		partial void toTrophies (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (isAudioEnabled != null) {
				isAudioEnabled.Dispose ();
				isAudioEnabled = null;
			}

			if (startButton != null) {
				startButton.Dispose ();
				startButton = null;
			}

			if (badgeNotification != null) {
				badgeNotification.Dispose ();
				badgeNotification = null;
			}
		}
	}
}
