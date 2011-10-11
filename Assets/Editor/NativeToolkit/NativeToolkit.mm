//
//  UnityNativeManager.mm
//  Unity-iPhone
//
//  Created by Mike on 8/7/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "NativeToolkit.h"


void UnityPause( bool pause );
void UnitySendMessage( const char * obj, const char * method, const char * msg );


@implementation NativeToolkit

@synthesize navigationController = _navigationController, animationType = _animationType,
			animationSubtype = _animationSubtype, animationTimingFunction = _animationTimingFunction,
			animationDuration = _animationDuration, keyboardView = _keyboardView;


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

+ (NativeToolkit*)sharedManager
{
	static NativeToolkit *sharedManager = nil;
	
	if( !sharedManager )
		sharedManager = [[NativeToolkit alloc] init];
	
	return sharedManager;
}


- (id)init
{
	if( ( self = [super init] ) )
	{
		// Default to fade animation and reasonable duration
		self.animationType = kCATransitionFade;
		self.animationDuration = 0.3;
		self.animationTimingFunction = [CAMediaTimingFunction functionWithName:kCAMediaTimingFunctionEaseIn];
		_displayingViewController = NO;
		
		_autoAdjustAnimationsForSmoothness = YES;
		
		// listen to notifications pertaining to application life cycle
		[[NSNotificationCenter defaultCenter] addObserver:self
												 selector:@selector(applicationDidBecomeActive:)
													 name:UIApplicationDidBecomeActiveNotification
												   object:nil];
		
		[[NSNotificationCenter defaultCenter] addObserver:self
												 selector:@selector(applicationWillResignActive:)
													 name:UIApplicationWillResignActiveNotification
												   object:nil];
	}
	return self;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSNotifications

- (void)applicationWillResignActive:(NSNotification*)note
{
	NSLog( @"applicationWillResignActive" );
	
	// send the call back to Unity so that we can intelligently handle incoming interruptions
	if( !_displayingViewController )
		[self sendMessage:@"applicationWillResignActive" param:@""];
}


- (void)applicationDidBecomeActive:(NSNotification*)note
{
	NSLog( @"applicationDidBecomeActive" );
	
	// if we are paused with a VC showing, make sure Unity stays paused
	if( _displayingViewController )
		[self pauseUnity:YES];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Private

// if _autoAdjustAnimationsForSmoothness is YES, this will use the smoothest animation
// based on the one chosen
- (NSString*)showAnimationType
{
	if( [_animationType isEqualToString:kCATransitionMoveIn] )
		return kCATransitionReveal;
	
	return _animationType;
}


- (NSString*)hideAnimationType
{
	if( [_animationType isEqualToString:kCATransitionReveal] || [_animationType isEqualToString:kCATransitionPush] )
		return kCATransitionMoveIn;
	
	return _animationType;
}


- (NSString*)oppositeAnimationSubtype
{
	if( _animationSubtype == kCATransitionFromRight )
		return kCATransitionFromLeft;
	
	if( _animationSubtype == kCATransitionFromLeft )
		return kCATransitionFromRight;
	
	if( _animationSubtype == kCATransitionFromTop )
		return kCATransitionFromBottom;
	
	if( _animationSubtype == kCATransitionFromBottom )
		return kCATransitionFromTop;
	
	return nil;
}


- (void)animationDidStop:(CAAnimation*)theAnimation finished:(BOOL)flag
{
	UnityPause( false );
}


- (void)extractUnityKeyboardView
{
	// kill the Unity keyboard view for now.  we will readd it later
	for( UIView *v in [UIApplication sharedApplication].keyWindow.subviews )
	{
		if( [v isMemberOfClass:[UIView class]] && v.hidden )
		{
			// retain the keyboard view
			self.keyboardView = v;
			[v removeFromSuperview];
		}
	}
}


- (void)injectUnityKeyboardView
{
	// early out if we dont have the keyboard view saved
	if( !_keyboardView )
		return;
	
	[[UIApplication sharedApplication].keyWindow addSubview:_keyboardView];
	self.keyboardView = nil;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

- (void)setAnimationType:(NSString*)animationType subtype:(NSString*)animationSubtype
{
	self.animationType = animationType;
	self.animationSubtype = animationSubtype;
}


- (void)showViewControllerWithName:(NSString*)name
{
	// only show one view controller at a time
	if( _displayingViewController )
		return;
	
	// Grab the controller from the given class name. Early out if we dont have it available
	Class controllerClass = NSClassFromString( name );
	if( !controllerClass )
	{
		NSLog( @"view controller with class name %@ does not exist", name );
		return;
	}
	
	[self extractUnityKeyboardView];
	
	// if we have a nibName and we are an iPad see if the file exists and use it if it does
	if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad )
	{
	    // check for iPad nib
		NSString *iPadNib = [name stringByAppendingString:@"-Pad"];
		
		// if the file exists, we are an iPad so load it up
		if( [[NSBundle mainBundle] pathForResource:iPadNib ofType:@"nib"] )
			name = iPadNib;
	}
	
	// Instantiate the controller and wrap it in a UINavigationController
	UIViewController *controller = [[controllerClass alloc] initWithNibName:name bundle:nil];
	_navigationController = [[UINavigationController alloc] initWithRootViewController:controller];
	_navigationController.navigationBarHidden = YES;
	
	UIWindow *window = [UIApplication sharedApplication].keyWindow;
	
	// Set up the fade-in animation
    CATransition *animation = [CATransition animation];
    [animation setType:[self showAnimationType]];
	[animation setSubtype:_animationSubtype];
	[animation setDuration:_animationDuration];
	[animation setTimingFunction:_animationTimingFunction];
    [window.layer addAnimation:animation forKey:@"layerAnimation"];
	
	[window addSubview:_navigationController.view];
	[window bringSubviewToFront:_navigationController.view];
	[controller release];
	
	// pause unity
	_displayingViewController = YES;
	[[NativeToolkit sharedManager] pauseUnity:YES];
}


- (void)hideViewController
{
	// Set up the hide animation
	UIWindow *window = [UIApplication sharedApplication].keyWindow;
	
    CATransition *animation = [CATransition animation];
    [animation setType:[self hideAnimationType]];
	[animation setDuration:_animationDuration];
	[animation setTimingFunction:_animationTimingFunction];
	[animation setDelegate:self];
	
	// Reverse the animationSubtype if we have one
	if( _animationSubtype )
		[animation setSubtype:[self oppositeAnimationSubtype]];
	
    [window.layer addAnimation:animation forKey:@"layerAnimation"];
	
	// let all the views know they are disappearing
	for( UIViewController *con in _navigationController.viewControllers )
		[con viewWillDisappear:YES];
	
	_navigationController.viewControllers = nil;
	[_navigationController.view removeFromSuperview];
	
	[_navigationController release];
	_navigationController = nil;
	
	// read the keyboard view
	[self injectUnityKeyboardView];
	_displayingViewController = NO;
}


- (void)pauseUnity:(BOOL)shouldPause
{
	if( shouldPause )
		UnityPause( true );
	else
		UnityPause( false );
}


- (void)sendMessage:(NSString*)message param:(NSString*)param
{
	UnitySendMessage( "NativeToolkitManager", [message UTF8String], [param UTF8String] );
}


- (void)sendMessage:(NSString*)gameObject message:(NSString*)message param:(NSString*)param
{
	UnitySendMessage( [gameObject UTF8String], [message UTF8String], [param UTF8String] );
}


@end
