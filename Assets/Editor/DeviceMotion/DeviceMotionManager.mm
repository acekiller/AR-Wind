//
//  GyroManager.m
//  GyroscopeTest
//
//  Created by Mike on 8/21/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "DeviceMotionManager.h"
#include <mach/mach_time.h>
#import <QuartzCore/QuartzCore.h>


@implementation DeviceMotionManager

@synthesize motionManager = _motionManager, attitude = _attitude, userAcceleration, gravity;

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

+ (DeviceMotionManager*)sharedManager
{
	static DeviceMotionManager *sharedSingleton;
	
	if( !sharedSingleton )
		sharedSingleton = [[DeviceMotionManager alloc] init];
	
	return sharedSingleton;
}


- (id)init
{
	if( self = [super init] )
	{
		_deviceMotionRefreshInterval = 1 / 60.0;
	}
	return self;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Private

- (void)updateCoreMotionValue:(id)sender
{
	// Either return the rawGyroData or a quarternion
	if( _returnRawGyroData )
		return;
	
	CMDeviceMotion *deviceMotion = _motionManager.deviceMotion;		
	CMAttitude *attitude = deviceMotion.attitude;
	
	// If we have a reference attitude, multiply attitude by its inverse
	// After this call, attitude will contain the rotation from referenceAttitude
	// to the current orientation instead of from the fixed reference frame to the current orientation
	if( _referenceAttitude )
		[attitude multiplyByInverseOfAttitude:_referenceAttitude];

	self.attitude = attitude;
	userAcceleration = deviceMotion.userAcceleration;
	gravity = deviceMotion.gravity;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

- (void)startDeviceMotion
{
	// early out if we are already running
	if( _motionManager )
		return;
	
	// setup the motionManager
	_motionManager = [[CMMotionManager alloc] init];
	
	if( _returnRawGyroData )
	{
		_motionManager.gyroUpdateInterval = _deviceMotionRefreshInterval;
		[_motionManager startGyroUpdates];
	}
	else
	{
		_motionManager.deviceMotionUpdateInterval = _deviceMotionRefreshInterval;
		[_motionManager startDeviceMotionUpdates];
	}
	
	if( _motionManager.gyroAvailable )
	{
		_displayLink = [[CADisplayLink displayLinkWithTarget:self selector:@selector(updateCoreMotionValue:)] retain];
		[_displayLink setFrameInterval:1];
		[_displayLink addToRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
	}
	else
	{
		NSLog( @"No gyroscope on device." );
		[_motionManager release];
	}
}


- (void)stopDeviceMotion
{
	if( _motionManager )
	{
		[_motionManager stopGyroUpdates];
		[_motionManager stopDeviceMotionUpdates];
		[_motionManager release];
		_motionManager = nil;
		
		[_displayLink invalidate];
		_displayLink = nil;
	}
}


- (void)resetDeviceMotion
{
	if( _motionManager )
	{
		// reset the reference attitude
		CMDeviceMotion *dm = _motionManager.deviceMotion;
		_referenceAttitude = [dm.attitude retain];
	}
}


- (void)setDeviceMotionInterval:(double)interval
{
	_deviceMotionRefreshInterval = interval;
	
	// set both update types just to be sure
	_motionManager.gyroUpdateInterval = interval;
	_motionManager.deviceMotionUpdateInterval = interval;
}


- (void)setReturnRawGyroData:(BOOL)returnRawData
{
	_returnRawGyroData = returnRawData;
	
	// start and stop the updater
	[self stopDeviceMotion];
	[self startDeviceMotion];
}


@end
