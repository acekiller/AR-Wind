using System;


public class CoreLocationData
{
	public double latitude;
	public double longitude;
	public float horizontalAccuracy;
	public float verticalAccuracy;
	public float altitude;
	public float speed;
	public float course;
	
	
	public static CoreLocationData locationDataFromString( string locationString )
	{
		CoreLocationData locationData = new CoreLocationData();
		
		string[] parts = locationString.Split( '|' );
		if( parts.Length == 7 )
		{
			locationData.altitude = float.Parse( parts[0] );
			locationData.latitude = double.Parse( parts[1] );
			locationData.longitude = double.Parse( parts[2] );
			locationData.course = float.Parse( parts[3] );
			locationData.horizontalAccuracy = float.Parse( parts[4] );
			locationData.verticalAccuracy = float.Parse( parts[5] );
			locationData.speed = float.Parse( parts[6] );
		}
		
		return locationData;
	}
	
	
	public override string ToString()
	{
		return string.Format( "<CoreLocationData> latitude: {0}, longitude: {1}, hAccuracy: {2}, vAccuracy: {3}, altitude: {4}, speed: {5}, course: {6}", latitude, longitude, horizontalAccuracy, verticalAccuracy, altitude, speed, course );
	}
}