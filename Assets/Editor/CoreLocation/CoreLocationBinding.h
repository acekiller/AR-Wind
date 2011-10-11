//
//  CoreLocationBinding.h
//  CoreLocationTest
//
//  Created by Mike DeSaro on 8/25/10.
//  Copyright 2010 FreedomVOICE. All rights reserved.
//

#import <Foundation/Foundation.h>


bool _coreLocationIsCompassAvailable();

void _coreLocationSetDistanceFilter( float distanceFilter );

void _coreLocationSetHeadingFilter( float headingFilter );

void _coreLocationStartUpdatingLocation();

void _coreLocationStopUpdatingLocation();

void _coreLocationStartUpdatingHeading();

void _coreLocationStopUpdatingHeading();

double _coreLocationGetMagneticHeading();

double _coreLocationGetTrueHeading();
