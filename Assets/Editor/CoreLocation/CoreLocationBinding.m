//
//  CoreLocationBinding.m
//  CoreLocationTest
//
//  Created by Mike DeSaro on 8/25/10.
//  Copyright 2010 FreedomVOICE. All rights reserved.
//

#import "CoreLocationBinding.h"
#import "CoreLocationManager.h"

bool _coreLocationIsCompassAvailable()
{
	return [[CoreLocationManager sharedManager] isCompassAvailable];
}


void _coreLocationSetDistanceFilter( float distanceFilter )
{
	[[CoreLocationManager sharedManager] setDistanceFilter:distanceFilter];
}


void _coreLocationSetHeadingFilter( float headingFilter )
{
	[[CoreLocationManager sharedManager] setHeadingFilter:headingFilter];
}


void _coreLocationStartUpdatingLocation()
{
	[[CoreLocationManager sharedManager]  startUpdatingLocation];
}


void _coreLocationStopUpdatingLocation()
{
	[[CoreLocationManager sharedManager] stopUpdatingLocation];
}


void _coreLocationStartUpdatingHeading()
{
	[[CoreLocationManager sharedManager] startUpdatingHeading];
}


void _coreLocationStopUpdatingHeading()
{
	[[CoreLocationManager sharedManager] stopUpdatingHeading];
}


double _coreLocationGetMagneticHeading()
{
	return [CoreLocationManager sharedManager].magneticHeading;
}


double _coreLocationGetTrueHeading()
{
	return [CoreLocationManager sharedManager].trueHeading;
}

