// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface MainViewController : UIViewController {
	UISwitch *_isAudioEnabled;
	UIButton *_startButton;
}

@property (nonatomic, retain) IBOutlet UISwitch *isAudioEnabled;

@property (nonatomic, retain) IBOutlet UIButton *startButton;
@property (retain, nonatomic) IBOutlet UIButton *badgeNotification;



- (IBAction)audioSwitch:(id)sender;

- (IBAction)toInfo:(id)sender;

- (IBAction)toStats:(id)sender;

- (IBAction)toTrip:(id)sender;

- (IBAction)toTrophies:(id)sender;

- (IBAction)showInfoTouchDown:(id)sender;

- (IBAction)startButtonTouchDown:(id)sender;

- (IBAction)menuButtonTouchDown:(id)sender;

@end
