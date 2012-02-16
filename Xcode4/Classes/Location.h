//
//  Location.h
//  Test
//
//  Created by Qiuhao Zhang on 6/22/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//


#define kLocationLatitudeKey	@"Latitude"
#define kLocationLongitudeKey	@"Longitude"
#define kLocationAltitudeKey	@"Altitude"

#import <Foundation/Foundation.h>


@interface Location : NSObject <NSCoding> {
	
	NSString *latitude;
	NSString *longitude;
	NSString *altitude;

}

@property (nonatomic, retain) NSString *latitude;
@property (nonatomic, retain) NSString *longitude;
@property (nonatomic, retain) NSString *altitude;

@end
