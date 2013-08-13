// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface StatsScreen : UIViewController {
	UILabel *_rankLabel;
	UIButton *_shareButton;
	UILabel *_totalDistanceLabel;
	UILabel *_totalHardStartsLabel;
	UILabel *_totalHardStopsLabel;
	UILabel *_totalHardTurnsLabel;
	UILabel *_totalNumberOfEventsLabel;
	UILabel *_totalPointsLabel;
}
@property (retain, nonatomic) IBOutlet UIImageView *rankView;
@property (retain, nonatomic) IBOutlet UIButton *badgeNotification;

@property (nonatomic, retain) IBOutlet UILabel *rankLabel;

@property (nonatomic, retain) IBOutlet UIButton *shareButton;

@property (nonatomic, retain) IBOutlet UILabel *totalDistanceLabel;

@property (nonatomic, retain) IBOutlet UILabel *totalHardStartsLabel;

@property (nonatomic, retain) IBOutlet UILabel *totalHardStopsLabel;

@property (nonatomic, retain) IBOutlet UILabel *totalHardTurnsLabel;

@property (nonatomic, retain) IBOutlet UILabel *totalNumberOfEventsLabel;

@property (nonatomic, retain) IBOutlet UILabel *totalPointsLabel;

- (IBAction)toInfo:(id)sender;

- (IBAction)toStats:(id)sender;

- (IBAction)toTrip:(id)sender;

- (IBAction)toTrophies:(id)sender;

- (IBAction)shareButtonSound:(id)sender;

@end
