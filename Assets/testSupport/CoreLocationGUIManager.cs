using UnityEngine;
using System.Collections.Generic;


public class CoreLocationGUIManager : MonoBehaviour
{
	public GUIText latitudeText;
	public GUIText longitudeText;
	
	public GUIText magneticHeadText;
	public GUIText trueHeadText;
//	int modulous = 0;
	public GameObject windTurbine;
	public float directionAngle;
	public float CLO_Latitude = 41.59032f; //41.59032 -= -0.00337899
	public float CLO_Longtitude = -41.59032f; //-87.47359 -===-0.0016899
	
	private double myLatitude;
	private double myLongitude;
	
	
	void Start()
	{
		// kill the dang keyboard frame
		iPhoneKeyboard.autorotateToPortrait = false;
		iPhoneKeyboard.autorotateToPortraitUpsideDown = false;
		iPhoneKeyboard.autorotateToLandscapeRight = false;
		iPhoneKeyboard.autorotateToLandscapeLeft = false;
		
		// listen for location changes
		CoreLocationManager.locationServicesDidUpdate += locationServicesDidUpdate;

		CoreLocationBinding.startUpdatingLocation();
		CoreLocationBinding.startUpdatingHeading();
		directionAngle = 120.0f;
	}
	
	
	public void locationServicesDidUpdate( CoreLocationData locationData )
	{
		Debug.Log( "locationServicesDidUpdate event: " + locationData );
		
		myLatitude = locationData.latitude;
		myLongitude = locationData.longitude;
		
		latitudeText.text = myLatitude.ToString();
		longitudeText.text = myLongitude.ToString();
		float dy = (float)myLatitude - CLO_Latitude;
		float dx = (float)myLongitude - CLO_Longtitude;
		float angle = Mathf.Atan2(dx,dy) * Mathf.Rad2Deg;
		directionAngle = angle+180.0f;
	}
	
	
	void Update()
	{
		// only grab the compass data every 10 frames
		//if( modulous++ % 5 == 0 )
		//{
			
		magneticHeadText.text = CoreLocationBinding.getMagneticHeading().ToString();
		trueHeadText.text = CoreLocationBinding.getTrueHeading().ToString();
	
		latitudeText.text = myLatitude.ToString();
		longitudeText.text = myLongitude.ToString();
		
		
		//float targetAngle = (directionAngle-(float)CoreLocationBinding.getTrueHeading()) * Mathf.Deg2Rad ;
			//windTurbine.transform.position = new Vector3(1 * Mathf.Sin(targetAngle),0,1 *  Mathf.Cos(targetAngle));
		//}
	}
	
	
	void OnGUI()
	{
//		float yPos = 10.0f;
//		float xPos = 20.0f;
//		float width = 160.0f;
//
//		
//		if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Is Compass Avaialble?" ) )
//		{
//			bool compassAvailable = CoreLocationBinding.isCompassAvailable();
//			Debug.Log( "CoreMotion isCompassAvailable: " + compassAvailable );
//		}
//		
//		
//		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Set Distance Filter to 10" ) )
//		{
//			CoreLocationBinding.setDistanceFilter( 10 );
//		}
//		
//		
//		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Set Heading Filter to 3" ) )
//		{
//			CoreLocationBinding.setHeadingFilter( 3 );
//		}
//		
//		
//		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Start Updating Location" ) )
//		{
//			CoreLocationBinding.startUpdatingLocation();
//		}
//		
//		
//		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Stop Updating Location" ) )
//		{
//			CoreLocationBinding.stopUpdatingLocation();
//		}
//		
//		
//		// Second column
//		xPos += xPos + width;
//		yPos = 10.0f;
//		
//		if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Start Updating Heading" ) )
//		{
//			CoreLocationBinding.startUpdatingHeading();
//		}
//		
//
//		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Stop Updating Heading" ) )
//		{
//			CoreLocationBinding.stopUpdatingHeading();
//		}
	
	}


}
