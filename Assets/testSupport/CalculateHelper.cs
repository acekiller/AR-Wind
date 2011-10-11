using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class CalculateHelper : MonoBehaviour {

	
	[DllImport("__Internal")]
	
	private static extern double _calculateDistanceFromGPS(double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget);
	
	
	public static double calculateDistanceFromGPS(double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			return _calculateDistanceFromGPS(latitudeRef, longitudeRef, latitudeTarget, longitudeTarget);
		
		return 0.000;
	}
	
	[DllImport("__Internal")]
	
	private static extern double _calculateDegreeFromGPS(double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget);
	
	
	public static double calculateDegreeFromGPS(double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			return _calculateDegreeFromGPS(latitudeRef, longitudeRef, latitudeTarget, longitudeTarget);
		
		return 0.000;
	}
	
	
}
