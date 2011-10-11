//
//  ARBinding.m
//  Unity-iPhone
//
//  Created by Mike on 12/16/10.
//  Copyright 2010 Prime31 Studios. All rights reserved.
//

#import "ARManager.h"


bool _arIsCaptureAvailable()
{
	return [ARManager isCaptureAvailable];
}


void _arStartCameraCapture( bool useFrontCameraIfAvailable, bool useLowQuality, int textureId )
{
	NSArray *devices = [ARManager availableDevices];
	NSString *deviceId;
	
	// Locate our deviceId
	for( AVCaptureDevice *device in devices )
	{
		// do we just want the standard camera or the front?
		if( useFrontCameraIfAvailable && device.position == AVCaptureDevicePositionFront )
		{
			deviceId = device.uniqueID;
			break;
		}
		else if( device.position == AVCaptureDevicePositionBack )
			deviceId = device.uniqueID;
	}
	
	if( !deviceId )
		return;
	
	[ARManager sharedManager].textureId = (GLuint)textureId;
	[[ARManager sharedManager] startCameraCaptureWithDevice:deviceId lowQuality:useLowQuality];
}


void _arStopCameraCapture()
{
	[[ARManager sharedManager] stopCameraCapture];
}


void _arSetExposureMode( int exposureMode )
{
	[[ARManager sharedManager] setExposureMode:exposureMode];
}


void _arSetFocusMode( int focusMode )
{
	[[ARManager sharedManager] setFocusMode:focusMode];
}
