// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface TripLogDetailScreen : UIViewController {
	UILabel *_hardStartsLabel;
	UILabel *_hardStopsLabel;
	UILabel *_hardTurnsLabel;
	UILabel *_tripDistanceLabel;
}
@property (retain, nonatomic) IBOutlet UIButton *badgeNotification;

@property (nonatomic, retain) IBOutlet UILabel *hardStartsLabel;

@property (nonatomic, retain) IBOutlet UILabel *hardStopsLabel;

@property (nonatomic, retain) IBOutlet UILabel *hardTurnsLabel;

@property (nonatomic, retain) IBOutlet UILabel *tripDistanceLabel;

- (IBAction)toInfo:(id)sender;

- (IBAction)toStats:(id)sender;

- (IBAction)toTrip:(id)sender;

- (IBAction)toTrophies:(id)sender;

@end
