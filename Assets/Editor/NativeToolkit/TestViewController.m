//
//  TestViewController.m
//  Unity-iPhone
//
//  Created by Mike on 12/4/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "TestViewController.h"
#import "NativeToolkit.h"


@implementation TestViewController

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

- (void)dealloc
{
    [super dealloc];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Touch Handlers

- (IBAction)onTouchDone
{
    [[NativeToolkit sharedManager] sendMessage:@"setCoordinatesFromViewController" param:@"-84.11234"];
	[[NativeToolkit sharedManager] hideViewController];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark UIViewController

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)toInterfaceOrientation
{
	return YES;
}


@end
