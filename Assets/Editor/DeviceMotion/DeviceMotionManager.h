//
//  GyroManager.h
//  GyroscopeTest
//
//  Created by Mike on 8/21/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import <Foundation/Foundation.h>
#include <CoreMotion/CoreMotion.h>


@interface DeviceMotionManager : NSObject
{
	CMMotionManager *_motionManager;
	CMAttitude *_referenceAttitude;
	double _deviceMotionRefreshInterval;
	CADisplayLink *_displayLink;
	BOOL _returnRawGyroData;
	
	CMAttitude *_attitude;
	CMAcceleration userAcceleration;
	CMAcceleration gravity;
}
@property (nonatomic, retain) CMMotionManager *motionManager;
@property (nonatomic, retain) CMAttitude *attitude;
@property (nonatomic, assign) CMAcceleration userAcceleration;
@property (nonatomic, assign) CMAcceleration gravity;


+ (DeviceMotionManager*)sharedManager;

- (void)startDeviceMotion;

- (void)stopDeviceMotion;

- (void)resetDeviceMotion;

- (void)setDeviceMotionInterval:(double)interval;

- (void)setReturnRawGyroData:(BOOL)returnRawData;

@end
