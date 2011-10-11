using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;


public class ARGUIManagerThree : MonoBehaviour
{
	public GameObject target; // the object to apply the camera texture to
    private Texture2D texture; // local reference of the texture
	
	
	void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
#if !UNITY_EDITOR
        texture = ARBinding.startCameraCapture( false, ARQuality.High );
        target.renderer.sharedMaterial.mainTexture = texture;
        ARBinding.updateMaterialUVScaleForTexture( target.renderer.sharedMaterial, texture );
#endif
	}
	
    
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 3.0f;
		float width = 155.0f;
		float height = 40.0f;
		
		xPos = Screen.width - width - 5.0f;
		yPos = Screen.height - height - 5.0f;
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Back" ) )
		{
	        ARBinding.stopCameraCapture();
	        Destroy( texture );
	        texture = null;
	        
	        Application.LoadLevel( "ARtestScene" );
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
