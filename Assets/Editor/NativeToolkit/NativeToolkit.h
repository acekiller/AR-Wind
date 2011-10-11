/*

Calling back to Unity can be done from Objective-C with the following method:
 
UnitySendMessage( "UnityGameController", "someMethodName", [@"argument string" UTF8String] );

NOTE:
 - the first argument is the object name you are sending a message to.  The GameObject name must be the same as the string

 If you are going to call into Unity and it is already paused but the method will be loading a level (async or not)
 be sure to unpause the game first:
 
 UnityPause( false );
 UnitySendMessage( "UnityGameController", "loadScene", [@"PartialScene" UTF8String] );
*/

#import <Foundation/Foundation.h>
#import <QuartzCore/QuartzCore.h>


@interface NativeToolkit : NSObject
{
	UINavigationController *_navigationController;
	NSString *_animationType;
	NSString *_animationSubtype;
	CAMediaTimingFunction *_animationTimingFunction;
	CFTimeInterval _animationDuration;
	BOOL _autoAdjustAnimationsForSmoothness;
	
@private
	UIView *_keyboardView;
	BOOL _displayingViewController;
}
@property (nonatomic, retain) UINavigationController *navigationController;
@property (nonatomic, retain) NSString *animationType; // kCATransitionFade, kCATransitionMoveIn, kCATransitionPush
@property (nonatomic, retain) NSString *animationSubtype; // kCATransitionFromRight, kCATransitionFromLeft, kCATransitionFromTop, kCATransitionFromBottom
@property (nonatomic, retain) CAMediaTimingFunction *animationTimingFunction;
@property (nonatomic, assign) CFTimeInterval animationDuration;
@property (nonatomic, retain) UIView *keyboardView;


+ (NativeToolkit*)sharedManager;

// Sets the animation that will be used for showing/hiding native views
- (void)setAnimationType:(NSString*)animationType subtype:(NSString*)animationSubtype;

// Shows a viewController with the given class name.
- (void)showViewControllerWithName:(NSString*)name;

// Hides the native UI and returns control to Unity
- (void)hideViewController;

- (void)pauseUnity:(BOOL)shouldPause;

// sends a message to the NativePauseAndUIManager.  See the NativeToolkitManager for supported methods.
- (void)sendMessage:(NSString*)message param:(NSString*)param;

// sends a message to any gameObject.
- (void)sendMessage:(NSString*)gameObject message:(NSString*)message param:(NSString*)param;


@end
