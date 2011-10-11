using UnityEngine;
using System;
using System.Collections.Generic;


// Any methods that Obj-C calls back using UnitySendMessage should be present here
public class CoreLocationManager : MonoBehaviour
{
	// Event
	public delegate void LocationServicesDidUpdateEventHandler( CoreLocationData locationData );
	public static event LocationServicesDidUpdateEventHandler locationServicesDidUpdate;
	
	public static CoreLocationData lastLocationData;
	
    void Awake()
    {
		// Set the GameObject name to the class name for easy access from Obj-C
		gameObject.name = this.GetType().ToString();

    }
	
	
	public void locationServicesDidUpdateLocation( string returnString )
	{
		// this is the location data for use in your game.  It is stored in a static ivar for easy access
		CoreLocationData locationData = CoreLocationData.locationDataFromString( returnString );
		CoreLocationManager.lastLocationData = locationData;
		
		// kick off the event
		if( locationServicesDidUpdate != null )
			locationServicesDidUpdate( locationData );
	}
	
	
	public void didFailWithError( string error )
	{
		Debug.Log( "CoreLocation failed with error: " + error );
	}

}