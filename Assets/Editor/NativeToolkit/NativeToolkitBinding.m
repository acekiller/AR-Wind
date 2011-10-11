//
//  UIBinding.m
//  Unity-iPhone

#import "NativeToolkit.h"


// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]


void _nativeToolkitInit()
{
	[NativeToolkit sharedManager];
}


void _nativeToolkitActivateUIWithController( const char *controllerName )
{
	NSString *className = [NSString stringWithUTF8String:controllerName];
	
	[[NativeToolkit sharedManager] showViewControllerWithName:className];
}


void _nativeToolkitDeactivateUI()
{
	[[NativeToolkit sharedManager] hideViewController];
}


void _nativeToolkitSetAnimationTypeAndSubtype( const char * type, const char * subType )
{
	[[NativeToolkit sharedManager] setAnimationType:GetStringParam( type ) subtype:GetStringParam( subType )];
}


void _nativeToolkitSetAnimationDuration( float duration )
{
	[NativeToolkit sharedManager].animationDuration = duration;
}
