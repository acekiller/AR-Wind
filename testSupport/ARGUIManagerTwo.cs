using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;


public class ARGUIManagerTwo : MonoBehaviour
{
	public GameObject target; // the object to apply the camera texture to
    private Texture2D texture; // local reference of the texture
    
	
	void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
    
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 3.0f;
		float width = 155.0f;
		float height = 35.0f;
		float heightPlus = height + 7.0f;
	

		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Start Capture (low)" ) )
		{
			// start the camera capture and use the returned texture
	        texture = ARBinding.startCameraCapture( false, ARQuality.Low );
	        target.renderer.sharedMaterial.mainTexture = texture;
	        ARBinding.updateMaterialUVScaleForTexture( target.renderer.sharedMaterial, texture );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Start Capture (high)" ) )
		{
	        texture = ARBinding.startCameraCapture( false, ARQuality.High );
	        target.renderer.sharedMaterial.mainTexture = texture;
	        ARBinding.updateMaterialUVScaleForTexture( target.renderer.sharedMaterial, texture );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Start Capture (front, low)" ) )
		{
	        texture = ARBinding.startCameraCapture( true, ARQuality.Low );
	        target.renderer.sharedMaterial.mainTexture = texture;
	        ARBinding.updateMaterialUVScaleForTexture( target.renderer.sharedMaterial, texture );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Start Capture (front, high)" ) )
		{
	        texture = ARBinding.startCameraCapture( true, ARQuality.High );
	        target.renderer.sharedMaterial.mainTexture = texture;
	        ARBinding.updateMaterialUVScaleForTexture( target.renderer.sharedMaterial, texture );
		}
		

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Stop Capture" ) )
		{
	        ARBinding.stopCameraCapture();
	        Destroy( texture );
	        texture = null;
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Set Exposure Mode" ) )
		{
			ARBinding.setExposureMode( ARExposureMode.ContinuousAutoExposure );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Set Focus Mode" ) )
		{
			ARBinding.setFocusMode( ARFocusMode.ContinuousAutoFocus );
		}

		
		xPos = Screen.width - width - 5.0f;
		yPos = Screen.height - height - 5.0f;
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Next" ) )
		{
	        ARBinding.stopCameraCapture();
	        Destroy( texture );
	        texture = null;
	        
	        Application.LoadLevel( "ARtestSceneThree" );
		}
	}
	
	
	void OnApplicationQuit()
	{
        ARBinding.stopCameraCapture();
        Destroy( texture );
        texture = null;
	}
	
	
	void OnApplicationPause( bool paused )
	{
		if( paused )
		{
	        ARBinding.stopCameraCapture();
	        Destroy( texture );
	        texture = null;
		}
	}


}
