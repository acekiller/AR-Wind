using UnityEngine;
using System.Collections.Generic;


public class DeviceMotionGUIManager : MonoBehaviour
{
	public GUIText text1;
	public GUIText text2;
	public GUIText text3;
	public GUIText text4;
	public GameObject centerCube;
	void Start()
	{
		// kill the dang keyboard frame
		iPhoneKeyboard.autorotateToPortrait = false;
		iPhoneKeyboard.autorotateToPortraitUpsideDown = false;
		iPhoneKeyboard.autorotateToLandscapeRight = false;
		iPhoneKeyboard.autorotateToLandscapeLeft = false;
	}
	
	
	void Update()
	{
//		Vector3 tempFromVector = centerCube.transform.rotation * Vector3.up;
//		Vector3 dirctionFromVector = tempFromVector * 10;
//		
		centerCube.transform.rotation = Quaternion.LookRotation(centerCube.transform.position - Camera.mainCamera.transform.position);
//		Vector3 tempVector = centerCube.transform.rotation * Vector3.up;
//		Vector3 dirctionVector = tempVector * 10;
//		Vector3 toVector = dirctionFromVector - dirctionVector;
//		centerCube.transform.position = dirctionVector;
//		centerCube.transform.rotation = Camera.mainCamera.transform.rotation;
		//Vector3 tempVector = Camera.mainCamera.transform
		//centerCube.transform.localRotation = Quaternion.Euler(0,30,0);
		Transform transformation = Camera.mainCamera.transform;
		centerCube.transform.RotateAround(transformation.position,Vector3.up,transformation.rotation.eulerAngles.y);
		centerCube.transform.RotateAround(transformation.position,Vector3.forward,transformation.rotation.eulerAngles.z);
		centerCube.transform.RotateAround(transformation.position,Vector3.right,transformation.rotation.eulerAngles.x);

		if( DeviceMotionBinding.returnRawGyroData )
		{
			Vector3 rawGyroData = DeviceMotionBinding.getRawGyroData();
			
			text1.text = rawGyroData.x.ToString();
			text2.text = rawGyroData.y.ToString();
			text3.text = rawGyroData.z.ToString();
			text4.text = "";
		}
		else
		{
			Quaternion gyroQuaternion = DeviceMotionBinding.getNormalizedQuarternion();
			
			text1.text = gyroQuaternion.x.ToString();
			text2.text = gyroQuaternion.y.ToString();
			text3.text = gyroQuaternion.z.ToString();
			text4.text = gyroQuaternion.w.ToString();
			
			// as an quick and dirty example, just blindly apply the gyroQuaternion to the camera
			//Camera.mainCamera.transform.rotation = Quaternion.Inverse(gyroQuaternion);
			//centerCube.transform.rotation = Quaternion.Inverse(gyroQuaternion);
		}
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
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Stop DeviceMotion" ) )
		{
			DeviceMotionBinding.stop();
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Reset DeviceMotion" ) )
		{
			DeviceMotionBinding.reset();
		}

		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Toggle Raw Gyro Data" ) )
		{
			DeviceMotionBinding.setReturnRawGyroData( !DeviceMotionBinding.returnRawGyroData );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Get Gravity and Accel" ) )
		{
			GravityAndAcceleration gravAndAccel = DeviceMotionBinding.getGravityAndAcceleration();
			Debug.Log( gravAndAccel );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Get Attitude" ) )
		{
			Attitude attitude = DeviceMotionBinding.getAttitude();
			Debug.Log( attitude );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += heightPlus, width, buttonHeight ), "Next Scene" ) )
		{
			Application.LoadLevel( "DeviceMotionTestScene2" );
		}
	}


}
