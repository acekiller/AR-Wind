//
//  CoreLocationManager.m
//  CoreLocationTest
//
//  Created by Mike DeSaro on 8/25/10.
//  Copyright 2010 FreedomVOICE. All rights reserved.
//

#import "CoreLocationManager.h"


void UnitySendMessage( const char * className, const char * methodName, const char * param );


@implementation CoreLocationManager

@synthesize locationManager = _locationManager, magneticHeading = _magneticHeading, trueHeading = _trueHeading;

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark NSObject

+ (CoreLocationManager*)sharedManager
{
	static CoreLocationManager *sharedManager = nil;
	
	if( !sharedManager )
		sharedManager = [[CoreLocationManager alloc] init];
	
	return sharedManager;
}


- (id)init
{
	if( ( self = [super init] ) )
	{
		// Create our locationManager with sensible defautls
		_locationManager = [[CLLocationManager alloc] init];
		_locationManager.delegate = self;
		//_locationManager.distanceFilter = 50.0;
		_locationManager.distanceFilter = kCLDistanceFilterNone;
		//_locationManager.desiredAccuracy = kCLLocationAccuracyHundredMeters;
		_locationManager.desiredAccuracy = kCLLocationAccuracyBest;
		
		if( [self isCompassAvailable] )
			_locationManager.headingFilter = kCLHeadingFilterNone;
	}
	
	return self;
}


- (void)dealloc
{
	[_locationManager stopUpdatingLocation];
	self.locationManager = nil;
	
	[super dealloc];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Public

- (BOOL)isCompassAvailable
{
	BOOL respondsToHeading = [CLLocationManager respondsToSelector:@selector(headingAvailable)];
	return ( respondsToHeading && [CLLocationManager headingAvailable] );
}


- (void)setDistanceFilter:(CGFloat)distanceFilter
{
	_locationManager.distanceFilter = distanceFilter;
}


- (void)setHeadingFilter:(CGFloat)headingFilter
{
	_locationManager.headingFilter = headingFilter;
}


- (void)startUpdatingLocation
{
	// Turn on location services
	[_locationManager startUpdatingLocation];
}


- (void)stopUpdatingLocation
{
	// Turn off location services
	[_locationManager stopUpdatingLocation];
}


- (void)startUpdatingHeading
{
	if( [self isCompassAvailable] )
		[_locationManager startUpdatingHeading];
}


- (void)stopUpdatingHeading
{
	if( [self isCompassAvailable] )
		[_locationManager stopUpdatingHeading];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark CoreLocation Delegate messages

// This delegate method is invoked when the location manager has heading data.
- (void)locationManager:(CLLocationManager*)manager didUpdateHeading:(CLHeading*)heading
{
	_magneticHeading = heading.magneticHeading;
	_trueHeading = heading.trueHeading;
}


- (void)locationManager:(CLLocationManager*)manager didUpdateToLocation:(CLLocation*)newLocation fromLocation:(CLLocation*)oldLocation 
{
	// Make sure we arent getting a location from too far in the past
	if( [newLocation.timestamp timeIntervalSinceNow] < -60 )
		return;
	
	_locationUpdateCount++;
	
	// Send the serialized location data
	NSString *locationString = [NSString stringWithFormat:@"%0.9f|%0.9f|%0.9f|%f|%f|%f|%f",
								newLocation.altitude, newLocation.coordinate.latitude, newLocation.coordinate.longitude,
								newLocation.course, newLocation.horizontalAccuracy, newLocation.verticalAccuracy,
								newLocation.speed];
	
	//NSLog(@"%0.9f, %0.9f", newLocation.coordinate.latitude, newLocation.coordinate.longitude);
	UnitySendMessage( "CoreLocationManager", "locationServicesDidUpdateLocation", [locationString UTF8String] );
}


- (void)locationManager:(CLLocationManager*)manager didFailWithError:(NSError*)error
{
	// Disregard unknown location errors
	if( error.code == kCLErrorLocationUnknown )
		return;
	
	if( error.code == kCLErrorDenied )
		UnitySendMessage( "CoreLocationManager", "didFailWithError", "Location Services Disallowed" );
	else
		UnitySendMessage( "CoreLocationManager", "didFailWithError", [[error localizedDescription] UTF8String] );
}


@end
