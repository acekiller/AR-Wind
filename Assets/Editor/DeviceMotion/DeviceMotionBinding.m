//
//  GyroBinding.m
//  GyroscopeTest
//
//  Created by Mike on 8/21/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "DeviceMotionManager.h"
#include <CoreMotion/CoreMotion.h>


void _deviceMotionStart()
{
	[[DeviceMotionManager sharedManager] startDeviceMotion];
}


void _deviceMotionStop()
{
	[[DeviceMotionManager sharedManager] stopDeviceMotion];
}


void _deviceMotionReset()
{
	[[DeviceMotionManager sharedManager] resetDeviceMotion];
}


void _deviceMotionGetRawGyroData( float * gyroVector )
{
    gyroVector[0] = [DeviceMotionManager sharedManager].motionManager.gyroData.rotationRate.x;
    gyroVector[1] = [DeviceMotionManager sharedManager].motionManager.gyroData.rotationRate.y;
    gyroVector[2] = [DeviceMotionManager sharedManager].motionManager.gyroData.rotationRate.z;
}


void _deviceMotionGetNormalizedQuarternion( float * quart )
{
    quart[0] = [DeviceMotionManager sharedManager].attitude.quaternion.x;
    quart[1] = [DeviceMotionManager sharedManager].attitude.quaternion.y;
    quart[2] = [DeviceMotionManager sharedManager].attitude.quaternion.z;
	quart[3] = [DeviceMotionManager sharedManager].attitude.quaternion.w;
}


void _deviceMotionSetInterval( double interval )
{
	[[DeviceMotionManager sharedManager] setDeviceMotionInterval:interval];
}


void _deviceMotionSetReturnRawGyroData( bool returnRawGyroData )
{
	[[DeviceMotionManager sharedManager] setReturnRawGyroData:returnRawGyroData];
}


void _deviceMotionGetGravityAndAcceleration( float * gravity, float * acceleration )
{
    acceleration[0] = [DeviceMotionManager sharedManager].userAcceleration.x;
    acceleration[1] = [DeviceMotionManager sharedManager].userAcceleration.y;
    acceleration[2] = [DeviceMotionManager sharedManager].userAcceleration.z;
	
	gravity[0] = [DeviceMotionManager sharedManager].gravity.x;
	gravity[1] = [DeviceMotionManager sharedManager].gravity.y;
	gravity[2] = [DeviceMotionManager sharedManager].gravity.z;
}


void _deviceMotionGetAttitude( float * attitude )
{
    attitude[0] = [DeviceMotionManager sharedManager].attitude.pitch;
    attitude[1] = [DeviceMotionManager sharedManager].attitude.roll;
    attitude[2] = [DeviceMotionManager sharedManager].attitude.yaw;
}
