using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class CalculateHelper : MonoBehaviour {

	
	[DllImport("__Internal")]
	
	private static extern double _calculateDistance(double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget);
	
	
	public static double calculateDistance(double latitudeRef, double longitudeRef, double latitudeTarget, double longitudeTarget)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor)
			return _calculateDistance(latitudeRef, longitudeRef, latitudeTarget, longitudeTarget);
		
		return 0.000;
	}
	
}
