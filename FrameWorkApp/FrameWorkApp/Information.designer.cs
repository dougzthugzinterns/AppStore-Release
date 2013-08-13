// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FrameWorkApp
{
	[Register ("Information")]
	partial class Information
	{
		[Outlet]
		MonoTouch.UIKit.UIButton badgeNotification { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIPageControl scroller { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrollView { get; set; }

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
			if (scroller != null) {
				scroller.Dispose ();
				scroller = null;
			}

			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (badgeNotification != null) {
				badgeNotification.Dispose ();
				badgeNotification = null;
			}
		}
	}
}
