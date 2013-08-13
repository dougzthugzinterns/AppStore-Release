// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface Information : UIViewController {
	UIPageControl *_scroller;
	UIScrollView *_scrollView;
}

@property (nonatomic, retain) IBOutlet UIPageControl *scroller;

@property (nonatomic, retain) IBOutlet UIScrollView *scrollView;
@property (retain, nonatomic) IBOutlet UIButton *badgeNotification;

- (IBAction)toInfo:(id)sender;

- (IBAction)toStats:(id)sender;

- (IBAction)toTrip:(id)sender;

- (IBAction)toTrophies:(id)sender;

@end
