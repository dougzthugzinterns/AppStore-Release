// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FrameWorkApp
{
	[Register ("TripSummaryScreen")]
	partial class TripSummaryScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton badgeNotification { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel distanceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel fastAccelsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel hardBrakesLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel hardTurnLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel numHardStartLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel pointsEarnedLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel rankLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView rankView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton shareButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel sharpTurnLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel totalBreakAcessLabel { get; set; }

		[Action ("mapButtonSound:")]
		partial void mapButtonSound (MonoTouch.Foundation.NSObject sender);

		[Action ("shareButtonSound:")]
		partial void shareButtonSound (MonoTouch.Foundation.NSObject sender);

		[Action ("toHome:")]
		partial void toHome (MonoTouch.Foundation.NSObject sender);

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

			if (distanceLabel != null) {
				distanceLabel.Dispose ();
				distanceLabel = null;
			}

			if (fastAccelsLabel != null) {
				fastAccelsLabel.Dispose ();
				fastAccelsLabel = null;
			}

			if (hardBrakesLabel != null) {
				hardBrakesLabel.Dispose ();
				hardBrakesLabel = null;
			}

			if (hardTurnLabel != null) {
				hardTurnLabel.Dispose ();
				hardTurnLabel = null;
			}

			if (numHardStartLabel != null) {
				numHardStartLabel.Dispose ();
				numHardStartLabel = null;
			}

			if (pointsEarnedLabel != null) {
				pointsEarnedLabel.Dispose ();
				pointsEarnedLabel = null;
			}

			if (rankLabel != null) {
				rankLabel.Dispose ();
				rankLabel = null;
			}

			if (shareButton != null) {
				shareButton.Dispose ();
				shareButton = null;
			}

			if (sharpTurnLabel != null) {
				sharpTurnLabel.Dispose ();
				sharpTurnLabel = null;
			}

			if (totalBreakAcessLabel != null) {
				totalBreakAcessLabel.Dispose ();
				totalBreakAcessLabel = null;
			}
		}
	}
}
