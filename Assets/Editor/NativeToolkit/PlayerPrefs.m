//
//  PlayerPrefs.m
//  Unity-iPhone
//
//  Created by Mike on 2/9/11.
//  Copyright 2011 Prime31 Studios. All rights reserved.
//

#import "PlayerPrefs.h"


@implementation PlayerPrefs

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark int

+ (void)setInt:(int)value forKey:(NSString*)key
{
	[[NSUserDefaults standardUserDefaults] setInteger:value forKey:key];
}


+ (int)getIntForKey:(NSString*)key
{
	return [self getIntForKey:key defaultValue:0];
}


+ (int)getIntForKey:(NSString*)key defaultValue:(int)defaultValue
{
	int val = [[NSUserDefaults standardUserDefaults] integerForKey:key];
	
	if( val == 0 )
		return defaultValue;
	return val;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark float

+ (void)setFloat:(CGFloat)value forKey:(NSString*)key
{
	[[NSUserDefaults standardUserDefaults] setInteger:value forKey:key];
}


+ (CGFloat)getFloatForKey:(NSString*)key
{
	return [self getFloatForKey:key defaultValue:0.0f];
}


+ (CGFloat)getFloatForKey:(NSString*)key defaultValue:(int)defaultValue
{
	int val = [[NSUserDefaults standardUserDefaults] floatForKey:key];
	
	if( val == 0.0f )
		return defaultValue;
	return val;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark string

+ (void)setString:(NSString*)value forKey:(NSString*)key
{
	[[NSUserDefaults standardUserDefaults] setObject:value forKey:key];
}


+ (NSString*)getStringForKey:(NSString*)key
{
	return [self getStringForKey:key defaultValue:0];
}


+ (NSString*)getStringForKey:(NSString*)key defaultValue:(NSString*)defaultValue
{
	NSString* val = [[NSUserDefaults standardUserDefaults] stringForKey:key];
	
	if( !val )
		return defaultValue;
	return val;
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark helpers

- (BOOL)hasKey:(NSString*)key
{
	NSDictionary *defs = [[NSUserDefaults standardUserDefaults] dictionaryRepresentation];
	
	return [[defs allKeys] containsObject:key];
}


- (void)removeKey:(NSString*)key
{
	[[NSUserDefaults standardUserDefaults] removeObjectForKey:key];
}

@end
