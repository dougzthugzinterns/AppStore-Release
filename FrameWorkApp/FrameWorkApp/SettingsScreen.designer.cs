// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FrameWorkApp
{
	[Register ("SettingsScreen")]
	partial class SettingsScreen
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch isAudioEnabled { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (isAudioEnabled != null) {
				isAudioEnabled.Dispose ();
				isAudioEnabled = null;
			}
		}
	}
}
