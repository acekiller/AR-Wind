using UnityEngine;
using System;
using System.Collections.Generic;



public class NativeToolkitManager : MonoBehaviour
{
	// Delegates and events
	public delegate void ApplicationWillResignActiveHandler(); 
	public delegate void CoordinatesHandler(string coordinates);
	
	// Fired whenever an interruption occurs (text message, push notification, app gets sent to background, etc)
	public static event ApplicationWillResignActiveHandler appWillResignActive;
	public static event CoordinatesHandler setCoordinates;

	 
    void Awake()
    {
		gameObject.name = this.GetType().ToString();
    }
    
    
    public void applicationWillResignActive( string empty )
    {
    	if( appWillResignActive != null )
    		appWillResignActive();
    }
	public void setCoordinatesFromViewController(string coordinates) {
		if(setCoordinates != null){
			setCoordinates(coordinates);
		}
	}
	public void loadLevel( string scene )
	{
		Application.LoadLevel( scene );
	}
	
	
	public void loadLevelAsync( string scene )
	{
		Application.LoadLevelAsync( scene );
	}
	
	
	public void loadLevelAdditive( string scene )
	{
		Application.LoadLevelAdditive( scene );
	}
	
	
	public void loadLevelAdditiveAsync( string scene )
	{
		Application.LoadLevelAdditiveAsync( scene );
	}

}