// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import "StatsScreen.h"

@implementation StatsScreen

@synthesize rankLabel = _rankLabel;
@synthesize shareButton = _shareButton;
@synthesize totalDistanceLabel = _totalDistanceLabel;
@synthesize totalHardStartsLabel = _totalHardStartsLabel;
@synthesize totalHardStopsLabel = _totalHardStopsLabel;
@synthesize totalHardTurnsLabel = _totalHardTurnsLabel;
@synthesize totalNumberOfEventsLabel = _totalNumberOfEventsLabel;
@synthesize totalPointsLabel = _totalPointsLabel;

- (IBAction)toInfo:(id)sender {
}

- (IBAction)toStats:(id)sender {
}

- (IBAction)toTrip:(id)sender {
}

- (IBAction)toTrophies:(id)sender {
}

- (IBAction)shareButtonSound:(id)sender {
}

- (void)dealloc {
    [_badgeNotification release];
    [_rankView release];
    [super dealloc];
}
@end
