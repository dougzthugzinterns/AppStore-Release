// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface TripSummaryScreen : UIViewController {
	UIButton *_badgeNotification;
	UILabel *_distanceLabel;
	UILabel *_fastAccelsLabel;
	UILabel *_hardBrakesLabel;
	UILabel *_hardTurnLabel;
	UILabel *_numHardStartLabel;
	UILabel *_pointsEarnedLabel;
	UILabel *_rankLabel;
	UIImageView *_rankView;
	UIButton *_shareButton;
	UILabel *_sharpTurnLabel;
	UILabel *_totalBreakAcessLabel;
}

@property (nonatomic, retain) IBOutlet UIButton *badgeNotification;

@property (nonatomic, retain) IBOutlet UILabel *distanceLabel;

@property (nonatomic, retain) IBOutlet UILabel *fastAccelsLabel;

@property (nonatomic, retain) IBOutlet UILabel *hardBrakesLabel;

@property (nonatomic, retain) IBOutlet UILabel *hardTurnLabel;

@property (nonatomic, retain) IBOutlet UILabel *numHardStartLabel;

@property (nonatomic, retain) IBOutlet UILabel *pointsEarnedLabel;

@property (nonatomic, retain) IBOutlet UILabel *rankLabel;

@property (nonatomic, retain) IBOutlet UIImageView *rankView;

@property (nonatomic, retain) IBOutlet UIButton *shareButton;

@property (nonatomic, retain) IBOutlet UILabel *sharpTurnLabel;

@property (nonatomic, retain) IBOutlet UILabel *totalBreakAcessLabel;

- (IBAction)toInfo:(id)sender;

- (IBAction)toStats:(id)sender;

- (IBAction)toTrip:(id)sender;

- (IBAction)toTrophies:(id)sender;

- (IBAction)toHome:(id)sender;

- (IBAction)shareButtonSound:(id)sender;

- (IBAction)mapButtonSound:(id)sender;

@end
