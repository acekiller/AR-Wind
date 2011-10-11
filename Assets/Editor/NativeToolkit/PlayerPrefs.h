//
//  PlayerPrefs.h
//  Unity-iPhone
//
//  Created by Mike on 2/9/11.
//  Copyright 2011 Prime31 Studios. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface PlayerPrefs : NSObject
{

}

+ (void)setInt:(int)value forKey:(NSString*)key;

+ (int)getIntForKey:(NSString*)key;

+ (int)getIntForKey:(NSString*)key defaultValue:(int)defaultValue;


+ (void)setFloat:(CGFloat)value forKey:(NSString*)key;

+ (CGFloat)getFloatForKey:(NSString*)key;

+ (CGFloat)getFloatForKey:(NSString*)key defaultValue:(int)defaultValue;


+ (void)setString:(NSString*)value forKey:(NSString*)key;

+ (NSString*)getStringForKey:(NSString*)key;

+ (NSString*)getStringForKey:(NSString*)key defaultValue:(NSString*)defaultValue;


- (BOOL)hasKey:(NSString*)key;

- (void)removeKey:(NSString*)key;

@end
