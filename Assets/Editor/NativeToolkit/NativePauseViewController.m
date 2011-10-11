//
//  NativePauseViewController.m
//  Unity-iPhone
//
//  Created by Mike on 12/12/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "NativePauseViewController.h"
#import "NativeToolkit.h"


@implementation NativePauseViewController

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

- (void)dealloc
{
    [super dealloc];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Touch Handlers

- (IBAction)onTouchUnpause
{
	[[NativeToolkit sharedManager] hideViewController];
}


- (IBAction)onTouchLoadLevel2
{
	// unpause and hide ourself before trying to call through to Unity
	[[NativeToolkit sharedManager] pauseUnity:NO];
	[[NativeToolkit sharedManager] hideViewController];
	[[NativeToolkit sharedManager] sendMessage:@"loadLevel" param:@"level2"];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark UIViewController

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)toInterfaceOrientation
{
	return YES;
}


- (void)viewWillAppear:(BOOL)animated
{
	[super viewWillAppear:animated];
	
	[UIApplication sharedApplication].statusBarHidden = NO;
}


- (void)viewWillDisappear:(BOOL)animated
{
	[super viewWillDisappear:animated];
	
	[UIApplication sharedApplication].statusBarHidden = YES;
}


@end
