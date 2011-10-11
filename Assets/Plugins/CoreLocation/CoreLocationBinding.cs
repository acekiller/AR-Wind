using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


// All Objective-C exposed methods should be bound here
public class CoreLocationBinding
{
    [DllImport("__Internal")]
    private static extern bool _coreLocationIsCompassAvailable();
 
    public static bool isCompassAvailable()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			return _coreLocationIsCompassAvailable();
		return false;
    }


    [DllImport("__Internal")]
    private static extern void _coreLocationSetDistanceFilter( float distanceFilter );
 
    public static void setDistanceFilter( float distanceFilter )
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_coreLocationSetDistanceFilter( distanceFilter );
    }
	
	
	[DllImport("__Internal")]
    private static extern void _coreLocationSetHeadingFilter( float headingFilter );
 
    public static void setHeadingFilter( float headingFilter )
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_coreLocationSetHeadingFilter( headingFilter );
    }


    [DllImport("__Internal")]
    private static extern void _coreLocationStartUpdatingLocation();
 
    public static void startUpdatingLocation()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_coreLocationStartUpdatingLocation();
    }


    [DllImport("__Internal")]
    private static extern void _coreLocationStopUpdatingLocation();
 
    public static void stopUpdatingLocation()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_coreLocationStopUpdatingLocation();
    }


    [DllImport("__Internal")]
    private static extern void _coreLocationStartUpdatingHeading();
 
    public static void startUpdatingHeading()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_coreLocationStartUpdatingHeading();
    }


    [DllImport("__Internal")]
    private static extern void _coreLocationStopUpdatingHeading();
 
    public static void stopUpdatingHeading()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_coreLocationStopUpdatingHeading();
    }
	

    [DllImport("__Internal")]
    private static extern double _coreLocationGetMagneticHeading();
 
    public static double getMagneticHeading()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			return _coreLocationGetMagneticHeading();
		return 0.0;
    }


	[DllImport("__Internal")]
    private static extern double _coreLocationGetTrueHeading();
 
    public static double getTrueHeading()
    {
        // Call plugin only when running on real device
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			return _coreLocationGetTrueHeading();
		return 0.0;
    }

}
