//
//  CoreLocationManager.h
//  CoreLocationTest
//
//  Created by Mike DeSaro on 8/25/10.
//  Copyright 2010 FreedomVOICE. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>


@interface CoreLocationManager : NSObject <CLLocationManagerDelegate>
{
	CLLocationManager *_locationManager;
	
	int _locationUpdateCount;
	
	double _magneticHeading;
	double _trueHeading;
}
@property (nonatomic, retain) CLLocationManager *locationManager;


@property (nonatomic, readonly) double magneticHeading;
@property (nonatomic, readonly) double trueHeading;


+ (CoreLocationManager*)sharedManager;

- (BOOL)isCompassAvailable;

// this is the number of meters the user must move before sending an update
- (void)setDistanceFilter:(CGFloat)distanceFilter;

// this is the number of degrees the compass must move before sending an update
- (void)setHeadingFilter:(CGFloat)headingFilter;

- (void)startUpdatingLocation;

- (void)stopUpdatingLocation;

- (void)startUpdatingHeading;

- (void)stopUpdatingHeading;

@end
