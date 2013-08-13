// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FrameWorkApp
{
	[Register ("StatsScreen")]
	partial class StatsScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton badgeNotification { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel rankLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView rankView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton shareButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalDistanceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalHardStartsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalHardStopsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalHardTurnsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalNumberOfEventsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalPointsLabel { get; set; }

		[Action ("shareButtonSound:")]
		partial void shareButtonSound (MonoTouch.Foundation.NSObject sender);

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
			if (rankView != null) {
				rankView.Dispose ();
				rankView = null;
			}

			if (badgeNotification != null) {
				badgeNotification.Dispose ();
				badgeNotification = null;
			}

			if (rankLabel != null) {
				rankLabel.Dispose ();
				rankLabel = null;
			}

			if (shareButton != null) {
				shareButton.Dispose ();
				shareButton = null;
			}

			if (totalDistanceLabel != null) {
				totalDistanceLabel.Dispose ();
				totalDistanceLabel = null;
			}

			if (totalHardStartsLabel != null) {
				totalHardStartsLabel.Dispose ();
				totalHardStartsLabel = null;
			}

			if (totalHardStopsLabel != null) {
				totalHardStopsLabel.Dispose ();
				totalHardStopsLabel = null;
			}

			if (totalHardTurnsLabel != null) {
				totalHardTurnsLabel.Dispose ();
				totalHardTurnsLabel = null;
			}

			if (totalNumberOfEventsLabel != null) {
				totalNumberOfEventsLabel.Dispose ();
				totalNumberOfEventsLabel = null;
			}

			if (totalPointsLabel != null) {
				totalPointsLabel.Dispose ();
				totalPointsLabel = null;
			}
		}
	}
}
