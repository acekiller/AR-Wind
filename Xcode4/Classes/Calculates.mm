//
//  Calculates.m
//  Unity-iPhone
//
//  Created by Qiuhao Zhang on 7/7/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Calculates.h"


@implementation Calculates

@end


extern "C" {
	
	double _calculateDistanceFromGPS (double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget) {
		
		double rad = M_PI / 180.0;
		
		double e = acos(sin(latitudeRef * rad) * sin(latitudeTarget * rad) 
						+ cos(latitudeRef *rad) * cos(latitudeTarget *rad) * cos((longitudeTarget-longitudeRef) * rad));
//		
//		double a = 6378.137
//		double f = 1 / 298.257;
//		double F = (latitudeRef + latitudeTarget) / 2;
//		double G = (latitudeRef - latitudeTarget) / 2;
//		double ramda = (longitudeRef - longitudeTarget) / 2;
//		
//		double S = 
		
		//return e * 6371.0;
		return e * 6378137; 
	}
	
	
	// Degree off north
	double _calculateDegreeFromGPS (double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget) {
		
		
		double rad = M_PI / 180.0;
		double e = acos(sin(latitudeRef * rad) * sin(latitudeTarget * rad) 
						+ cos(latitudeRef *rad) * cos(latitudeTarget *rad) * cos((longitudeTarget-longitudeRef) * rad));
		double ecos = acos(sin(latitudeRef * rad) * sin(latitudeTarget * rad) 
						   + cos(latitudeRef * rad) * cos(latitudeTarget * rad) * cos((longitudeRef - longitudeRef) * rad));
		
		double degree = acos(ecos/e) / rad;
		
		if (latitudeTarget > latitudeRef) {
			if (longitudeTarget > longitudeRef) {
				return degree;
			}
			else {
				//return -degree;
				return 360.0 - degree;
			}

		}
		else {
			if (longitudeTarget > longitudeRef) {
				return 180.0 - degree;
			}
			else {
				//return degree - 180.0;
				return 180.0 + degree;
			}

		}
	}

	
}
