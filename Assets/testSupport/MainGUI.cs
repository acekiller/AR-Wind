using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;


public class MainGUI : MonoBehaviour
{
	
	public GameObject target; // the object to apply the camera texture to
	public GUITexture myguiTexture;
    private Texture2D texture; // local reference of the texture
	public GameObject model;
	private Material material;
	public GUIText gyroX;
	public GUIText gyroY;
	public GUIText gyroZ;
	public GUIText text4;
	private Vector3 _startRotation;
//	private Quaternion referenceQuaternion;
	private Attitude referenceAttitude;
	public static double trueNorthDegree = 0.0;
	
	public delegate void UpdateCoordinatesHandler(CoreLocationData coreLocationData);
	public static event UpdateCoordinatesHandler onUpdateCoordinates;
	
	public void locationServicesDidUpdate( CoreLocationData locationData )
	{
		if(onUpdateCoordinates != null) {
			onUpdateCoordinates(locationData);

		}

	}
	
	void OnEnable()
	{
		// convenient place to listen to events
		NativeToolkitManager.appWillResignActive += appWillResignActive;
		NativeToolkitManager.setCoordinates += setCoordinates;
	}
	
	
	void OnDisable()
	{
		// stop listenting to events
		NativeToolkitManager.appWillResignActive -= appWillResignActive;
		NativeToolkitManager.setCoordinates -=setCoordinates;
	}
	
	
	// Application will resign active event handler
	void appWillResignActive()
	{
		// this will get fired from native code whenever a call comes in, text message, push
		// notification or other interruption.  This is a great hook for pausing your game
		// so we will show the NativePauseViewController
		NativeToolkitBinding.activateUIWithController( "NativePauseViewController" );
	}
	
	void setCoordinates(string lat) {
		Debug.Log("Lat:" + lat);
	}
	
	void Start()
	{
		Screen.orientation = ScreenOrientation.Portrait;
				// kill the dang keyboard frame
		iPhoneKeyboard.autorotateToPortrait = false;
		iPhoneKeyboard.autorotateToPortraitUpsideDown = false;
		iPhoneKeyboard.autorotateToLandscapeRight = false;
		iPhoneKeyboard.autorotateToLandscapeLeft = false;
		NativeToolkitBinding.init();
		CoreLocationManager.locationServicesDidUpdate += locationServicesDidUpdate;
		//CoreLocationBinding.setDistanceFilter(1);
		CoreLocationBinding.startUpdatingHeading();
		CoreLocationBinding.startUpdatingLocation();

		
//		referenceQuaternion = Quaternion.identity;
	}
	
	void Update() {
		if( DeviceMotionBinding.returnRawGyroData )
		{
			Vector3 rawGyroData = DeviceMotionBinding.getRawGyroData();
			
			gyroX.text = rawGyroData.x.ToString();
			gyroY.text = rawGyroData.y.ToString();
			gyroZ.text = rawGyroData.z.ToString();
			text4.text = "";
		}
		else
		{
			Quaternion gyroQuaternion = DeviceMotionBinding.getNormalizedQuarternion();
			
//			text1.text = gyroQuaternion.x.ToString();
//			text2.text = gyroQuaternion.y.ToString();
//			text3.text = gyroQuaternion.z.ToString();
//			text4.text = gyroQuaternion.w.ToString();
			gyroX.text = gyroQuaternion.eulerAngles.x.ToString();
			gyroY.text = gyroQuaternion.eulerAngles.y.ToString();
			gyroZ.text = gyroQuaternion.eulerAngles.z.ToString();
			text4.text = "";
//			Attitude attitude = DeviceMotionBinding.getAttitude();

//			text1.text = attitude.roll.ToString();
//			text2.text = attitude.pitch.ToString();
//			text3.text = attitude.yaw.ToString();
//			text4.text = "";
			
			// as an quick and dirty example, just blindly apply the gyroQuaternion to the camera
			
// Rotate Camera			
			//Quaternion xyQuaternion = Quaternion.Euler(gyroQuaternion.eulerAngles.x,gyroQuaternion.eulerAngles.y,gyroQuaternion.eulerAngles.z);

			//Camera.mainCamera.transform.eulerAngles =_startRotation + new Vector3( attitude.pitch * -30.0f, attitude.roll * -30.0f, 0 );
			//Camera.mainCamera.transform.eulerAngles =new Vector3( attitude.pitch * -Mathf.Rad2Deg, attitude.roll * -Mathf.Rad2Deg, 0 );
//			if(referenceQuaternion != Quaternion.identity) {
//				gyroQuaternion = referenceQuaternion* Quaternion.Inverse(gyroQuaternion);
//			} else{
//				gyroQuaternion = Quaternion.Inverse(gyroQuaternion);
//				gyroQuaternion *= Quaternion.AngleAxis(90.0f,Vector3.right);
//			}
//			Vector3 atitudeV = attitude.ToVector3();
//			if (referenceAttitude != null) {
//				attitude = attitude * referenceAttitude;
//			}
			
			//Camera.mainCamera.transform.eulerAngles =new Vector3( attitude.pitch * -Mathf.Rad2Deg, attitude.roll * -Mathf.Rad2Deg, 0 );
			Quaternion xyQuaternion = Quaternion.Euler(-gyroQuaternion.eulerAngles.x,-gyroQuaternion.eulerAngles.y,gyroQuaternion.eulerAngles.z);
			Camera.mainCamera.transform.rotation = xyQuaternion;
			//Camera.mainCamera.transform.rotation = Quaternion.Euler(gyroQuaternion.eulerAngles.x,gyroQuaternion.eulerAngles.y,0);
			//target.transform.rotation = Quaternion.LookRotation(target.transform.position - Camera.mainCamera.transform.position);
			target.transform.rotation = Camera.mainCamera.transform.rotation * Quaternion.AngleAxis(90.0f,Vector3.forward);
			//target.transform.Rotate(Vector3.forward,90.0f);
			Vector3 p = Camera.mainCamera.ScreenToWorldPoint(new Vector3(320, 480, 1.5f));
			target.transform.position = p;
//			model.transform.rotation = Quaternion.Euler(0,0,0);
//----------Test Data---------------------------------------------------------------------------			
//			float testRotation = 45.0f;
//			float testYRotation = 30.0f;
//			float testXRotation = 20.0f;
//			Camera.mainCamera.transform.rotation = Quaternion.Euler(testXRotation,testYRotation,testRotation);
			
//			target.transform.rotation = Camera.mainCamera.transform.rotation * Quaternion.AngleAxis(90.0f,Vector3.forward);
//			Vector3 p = Camera.mainCamera.ScreenToWorldPoint(new Vector3(320, 480, 1.5f));
//			target.transform.position = p;
//			model.transform.rotation = Quaternion.Euler(0,0,0);
//-------------------------------------------------------------------------------------			
//Position Cube
//			float normalDistance = 1.2f;
//			Vector3 vectorDistance = Vector3.forward * normalDistance;
//			
//			model.transform.position = Quaternion.Euler(0,gyroQuaternion.eulerAngles.y,0) * vectorDistance;
//			
//			Vector3 upVector = Vector3.up * normalDistance * Mathf.Sin(-gyroQuaternion.eulerAngles.x*Mathf.Deg2Rad);
//			
//			model.transform.position = model.transform.position + upVector;
//-------------------------------------------------------------------------------------
//			float zRotate = Camera.mainCamera.transform.rotation.x;
//			Quaternion zQuaternion = Quaternion.Euler(-zRotate/2,0,0);
//			Camera.mainCamera.transform.rotation *= zQuaternion;
			
//			//target.transform.rotation = Quaternion.Inverse(gyroQuaternion);
//			
//			Transform cameraTransform = Camera.mainCamera.transform;
//
//			float normalDistance = 1.6f;
//			Vector3 vectorDistance = Vector3.forward * normalDistance;
//			target.transform.position = Camera.mainCamera.transform.rotation * vectorDistance;
			
		}
	
	}
	
	
		
	
	private bool isVisible = false;
    
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = 160.0f;
		float height = 40.0f;
		float heightPlus = height + 10.0f;
	
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Menu" ) )
		{
			isVisible = !isVisible;
		}
		
		if(isVisible)
		{

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Input Coordinates" ) )
		{
			NativeToolkitBinding.setAnimationTypeAndSubtype( AnimationType.Reveal, AnimationSubtype.FromTop );
			NativeToolkitBinding.activateUIWithController( "AddTurbineController" );
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Start Motion" ) )
		{
			//_startRotation = Camera.mainCamera.transform.transform.rotation.eulerAngles;
			DeviceMotionBinding.start();
			//Get true north degree
			trueNorthDegree = CoreLocationBinding.getTrueHeading();
			//Debug.Log("True north degree: " + trueNorthDegree);
			

		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Reset Motion" ) )
		{
			DeviceMotionBinding.reset();
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Start Capture (high)" ) )
		{
	        texture = ARBinding.startCameraCapture( false, ARQuality.High );
			//material.mainTexture = texture;
			//Graphics.DrawTexture(new Rect(0,50,480,960),texture);
	        target.renderer.sharedMaterial.mainTexture = texture;
			//myguiTexture.texture = texture;
//			target.texture = texture;
	        ARBinding.updateMaterialUVScaleForTexture( target.renderer.sharedMaterial, texture );
			//myguiTexture.texture = texture;
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "StopMotion" ) )
		{
			DeviceMotionBinding.stop(); 
		}

		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Stop Capture" ) )
		{
	        ARBinding.stopCameraCapture();
	        Destroy( texture );
	        texture = null;
		}
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, height ), "Random Turbine" ) )
		{
	        if(onUpdateCoordinates != null){
				onUpdateCoordinates(null);
		}
		}
			
		}
		
//		
//		xPos = Screen.width - width - 5.0f;
//		yPos = Screen.height - height - 5.0f;
//		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Next" ) )
//		{
//	        ARBinding.stopCameraCapture();
//	        Destroy( texture );
//	        texture = null;
//	        
//	        Application.LoadLevel( "ARtestSceneTwo" );
//		}
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

