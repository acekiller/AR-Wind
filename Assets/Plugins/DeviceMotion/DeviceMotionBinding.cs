using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public struct Attitude
{
	public float pitch;
	public float roll;
	public float yaw;
	
	public Attitude( float[] attitude )
	{
		pitch = attitude[0];
		roll = attitude[1];
		yaw = attitude[2];
	}
	
	
	public override string ToString()
	{
		return string.Format( "<Attitude> pitch: {0}, roll: {1}, yaw: {2}", pitch, roll, yaw );
	}
	
	
	public Vector3 ToVector3()
	{
		return new Vector3( pitch, roll, yaw );
	}
}


public struct GravityAndAcceleration
{
	public Vector3 gravity;
	public Vector3 acceleration;
	
	public GravityAndAcceleration( float[] grav, float[] accel )
	{
		gravity = new Vector3( grav[0], grav[1], grav[2] );
		acceleration = new Vector3( accel[0], accel[1], accel[2] );
	}


	public override string ToString()
	{
		return string.Format( "<GravityAndAcceleration> gravity.x: {0}, gravity.y: {1}, gravity.z: {2}, accel.x: {3}, accel.y: {4}, accel.z: {5}", gravity.x, gravity.y, gravity.z, acceleration.x, acceleration.y, acceleration.z );
	}
}


public class DeviceMotionBinding
{
	public static bool returnRawGyroData;
	
    [DllImport("__Internal")]
    private static extern void _deviceMotionStart();

	// Starts up the gyroscope listener
    public static void start()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_deviceMotionStart();
    }
	
	
    [DllImport("__Internal")]
    private static extern void _deviceMotionStop();

	// Stops the gyroscope listener
    public static void stop()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_deviceMotionStop();
    }


    [DllImport("__Internal")]
    private static extern void _deviceMotionReset();

	// Calibrates the zero point of the gyroscope to the current orientation
    public static void reset()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_deviceMotionReset();
    }


    [DllImport("__Internal")]
    private static extern void _deviceMotionGetRawGyroData( float[] rawGyroData );
 
	// Gets the raw gyro data.  This will only have defined results if setReturnRawGyroData was called with 'true'
    public static Vector3 getRawGyroData()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			float[] av = {0, 0, 0};
			_deviceMotionGetRawGyroData( av );
			
			// this is the gyroscope data for use in your game.  Auto parsed into the correct orientation based on landscape/portrait
			if( Screen.orientation == ScreenOrientation.Landscape )
				return new Vector3( av[1], av[0], av[2] );
			else
				return new Vector3( av[0], av[1], av[2] );
		}
		
		return Vector3.zero;
    }

	
    [DllImport("__Internal")]
    private static extern void _deviceMotionGetNormalizedQuarternion( float[] quart );
 
	// Gets the normalized gyro attitude quaternion.  This will only have defined results if setReturnRawGyroData was called with 'false'
    public static Quaternion getNormalizedQuarternion()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			float[] quart = {0, 0, 0, 0};
			_deviceMotionGetNormalizedQuarternion( quart );
			
			// this is the gyroscope data for use in your game.  Auto parsed into the correct orientation based on landscape/portrait
			if( Screen.orientation == ScreenOrientation.Landscape )
				return new Quaternion( quart[1], quart[0], quart[2], quart[3] );
			else
				return new Quaternion( quart[0], quart[1], quart[2], quart[3] );
		}
		
		return new Quaternion();
    }
	

    [DllImport("__Internal")]
    private static extern void _deviceMotionSetInterval( double interval );

	// Defaults to 1/30.  The only only viable interval is 1/60 if your game is running at 60 FPS
    public static void setInterval( double interval )
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_deviceMotionSetInterval( interval );
    }
	
	
    [DllImport("__Internal")]
    private static extern void _deviceMotionSetReturnRawGyroData( bool returnRawGyroData );
 
	// Defaults to false and allows calling getNormalizedQuarternion to get a normalized quaternion.  If true, use getRawGyroData
	// to get the raw gyro velocity readings.
    public static void setReturnRawGyroData( bool returnRawGyroData )
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
			_deviceMotionSetReturnRawGyroData( returnRawGyroData );
		DeviceMotionBinding.returnRawGyroData = returnRawGyroData;
    }


    [DllImport("__Internal")]
    private static extern void _deviceMotionGetGravityAndAcceleration( float[] gravity, float[] acceleration );
 
	// Gets the actual gravity and user acceleration as measured by the gyro
    public static GravityAndAcceleration getGravityAndAcceleration()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			float[] grav = {0, 0, 0};
			float[] accel = {0, 0, 0};
			_deviceMotionGetGravityAndAcceleration( grav, accel );
			
			return new GravityAndAcceleration( grav, accel );
		}
		
		return new GravityAndAcceleration();
    }


    [DllImport("__Internal")]
    private static extern void _deviceMotionGetAttitude( float[] attitude );
 
	// Gets the current attitude of the device as measured by the gyros
	public static Attitude getAttitude()
    {
        if( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			float[] attitude = {0, 0, 0};
			_deviceMotionGetAttitude( attitude );
			return new Attitude( attitude );
		}
		
		return new Attitude();
    }

	
}
