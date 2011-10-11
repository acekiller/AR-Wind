using UnityEngine;
using System.Collections;


public class DeviceMotionGUIManager2 : MonoBehaviour
{
	public GUIText text1;
	public GUIText text2;
	public GUIText text3;
	
	private Vector3 _startRotation;
	
	
	void Update()
	{
		Attitude attitude = DeviceMotionBinding.getAttitude();
		
		text1.text = string.Format( "pitch: {0}", attitude.pitch );
		text2.text = string.Format( "roll: {0}", attitude.roll );
		text3.text = string.Format( "yaw: {0}", attitude.yaw );
		
		// modify the cameras angle slightly as the devices moves around
		// for a more convincing effect you can modify the cameras FOV
		Camera.mainCamera.transform.eulerAngles = _startRotation + new Vector3( attitude.roll * -30.0f, attitude.pitch * 30.0f, attitude.yaw * 20.0f );
	}
	
	
	void OnGUI()
	{
		float yPos = 0.0f;
		float xPos = 5.0f;
		float width = 160.0f;
		float buttonHeight = 40.0f;
		float heightPlus = buttonHeight + 5.0f;
		
		
		if( GUI.Button( new Rect( xPos, yPos, width, buttonHeight ), "Start DeviceMotion" ) )
		{
			DeviceMotionBinding.start();
			DeviceMotionBinding.reset();
			_startRotation = Camera.mainCamera.transform.transform.rotation.eulerAngles;
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Stop DeviceMotion" ) )
		{
			DeviceMotionBinding.stop();
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Reset DeviceMotion" ) )
		{
			DeviceMotionBinding.reset();
		}


		if( GUI.Button( new Rect( xPos, yPos += heightPlus * 2, width, buttonHeight ), "Previous Scene" ) )
		{
			Application.LoadLevel( "DeviceMotionTestScene" );
		}
	}
}
