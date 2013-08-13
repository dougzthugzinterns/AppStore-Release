// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FrameWorkApp
{
	[Register ("TripLogDetailScreen")]
	partial class TripLogDetailScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton badgeNotification { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel hardStartsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel hardStopsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel hardTurnsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel pointsEarned { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel tripDistanceLabel { get; set; }

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
			if (pointsEarned != null) {
				pointsEarned.Dispose ();
				pointsEarned = null;
			}

			if (badgeNotification != null) {
				badgeNotification.Dispose ();
				badgeNotification = null;
			}

			if (hardStartsLabel != null) {
				hardStartsLabel.Dispose ();
				hardStartsLabel = null;
			}

			if (hardStopsLabel != null) {
				hardStopsLabel.Dispose ();
				hardStopsLabel = null;
			}

			if (hardTurnsLabel != null) {
				hardTurnsLabel.Dispose ();
				hardTurnsLabel = null;
			}

			if (tripDistanceLabel != null) {
				tripDistanceLabel.Dispose ();
				tripDistanceLabel = null;
			}
		}
	}
}
