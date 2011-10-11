using UnityEngine;
using System.Collections;

public class LocationListener : MonoBehaviour {


	
	// Use this for initialization
	void OnEnable () {
		MainGUI.onUpdateCoordinates += updateCoordinates;

	}
	
	// Update is called once per frame
	void OnDisable () {
		MainGUI.onUpdateCoordinates -= updateCoordinates;

	}

	
	
	public void updateCoordinates(CoreLocationData data) {
		//float lat1 = data.latitude * Mathf.Rad2Deg;
		//float long1 = data.longitude * Mathf.Rad2Deg;
		//float distance = CoornidatesHelper.distanceFromDegreesToMeters(lat1,long1,CoornidatesHelper.getRandomLati(),CoornidatesHelper.getRandomLongi());
		//float distance = CoornidatesHelper.getRandomDistance();
		float distance;
		float angle;
		float latitudeTarget = CoornidatesHelper.getRandomLati();
		float longitudeTarget = CoornidatesHelper.getRandomLongi();
		//float latitudeTarget = 41.586135f;
		//float longitudeTarget = -87.474901f; 
		if(data != null){
			
			double latitudeRef = data.latitude;
			double longitudeRef = data.longitude;
			
	
			distance = (float)CalculateHelper.calculateDistanceFromGPS(latitudeRef, longitudeRef, latitudeTarget,longitudeTarget);
			//Debug.Log("The distance of turbine and phone: " + distance);
			//float angle = CoornidatesHelper.degreesFromDegreesToMeters(lat1,long1,CoornidatesHelper.getRandomLati(),CoornidatesHelper.getRandomLongi());
			//float angle = CoornidatesHelper.getRandomDegrees();
			angle = (float)CalculateHelper.calculateDegreeFromGPS(latitudeRef, longitudeRef, latitudeTarget,longitudeTarget);

		}
		else {
			
			Debug.Log("No real GPS data of the phone");
			distance = CoornidatesHelper.getRandomDistance();
			angle = CoornidatesHelper.getRandomDegrees();
		}
		
		float realAngle = angle - (float)MainGUI.trueNorthDegree;
		if(realAngle < 0)
			realAngle = realAngle + 360.0f;
		
		Vector3 position = Quaternion.Euler(0, realAngle, 0) *Vector3.forward;
		
		this.transform.localScale = Vector3.one * (1/distance);
		//Vector3 position = Quaternion.Euler(0,angle,0) * Vector3.forward;
		this.transform.position = position;
		Debug.Log("Distance: " + distance + " Degree: " + angle + " Degree in Phone: " + realAngle);
		
		//Debug.Log("Test native code: " + CalculateHelper.calculateDistance(41.59032, -87.47359, 41.6175, -87.48512));
		//CoreLocationBinding.stopUpdatingLocation();
		//Debug.Log("angle:"+ angle + " distance:"+ distance + "scale:" + this.transform.localScale + " position:" + this.transform.position);
	}

}
