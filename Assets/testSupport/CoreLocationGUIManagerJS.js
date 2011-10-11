
var text1:GUIText;
var text2:GUIText;

var text3:GUIText;
var text4:GUIText;
var modulous:int = 0;


function Start()
{
	// kill the dang keyboard frame
	iPhoneKeyboard.autorotateToPortrait = false;
	iPhoneKeyboard.autorotateToPortraitUpsideDown = false;
	iPhoneKeyboard.autorotateToLandscapeRight = false;
	iPhoneKeyboard.autorotateToLandscapeLeft = false;
	
	// listen for location changes
	CoreLocationManager.locationServicesDidUpdate += locationServicesDidUpdate;
}


function locationServicesDidUpdate( locationData:CoreLocationData )
{
	Debug.Log( "locationServicesDidUpdate event: " + locationData );
	
	text1.text = locationData.latitude.ToString();
	text2.text = locationData.longitude.ToString();
}


function Update()
{
	// only grab the gyro data every 10 frames
	if( modulous++ % 10 == 0 )
	{
		text3.text = CoreLocationBinding.getMagneticHeading().ToString();
		text4.text = CoreLocationBinding.getTrueHeading().ToString();
	}
}


function OnGUI()
{
	var yPos:float = 10.0f;
	var xPos:float = 20.0f;
	var width:float = 160.0f;
	var heightPlus:float = 50.0f;

	
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Is Compass Avaialble?" ) )
	{
		var compassAvailable:boolean = CoreLocationBinding.isCompassAvailable();
		Debug.Log( "CoreMotion isCompassAvailable: " + compassAvailable );
	}
	
	
	yPos += heightPlus;
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Set Distance Filter to 10" ) )
	{
		CoreLocationBinding.setDistanceFilter( 10 );
	}
	
	
	yPos += heightPlus;
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Set Heading Filter to 3" ) )
	{
		CoreLocationBinding.setHeadingFilter( 3 );
	}
	
	
	yPos += heightPlus;
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Start Updating Location" ) )
	{
		CoreLocationBinding.startUpdatingLocation();
	}
	
	
	yPos += heightPlus;
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Stop Updating Location" ) )
	{
		CoreLocationBinding.stopUpdatingLocation();
	}
	
	
	// Second column
	xPos += xPos + width;
	yPos = 10.0f;
	
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Start Updating Heading" ) )
	{
		CoreLocationBinding.startUpdatingHeading();
	}
	

	yPos += heightPlus;
	if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Stop Updating Heading" ) )
	{
		CoreLocationBinding.stopUpdatingHeading();
	}

}