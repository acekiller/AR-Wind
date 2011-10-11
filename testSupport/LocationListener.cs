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
		
		
		if(data != null){
			//Debug.Log("latitude: " + data.latitude + " longitude: " + data.longitude);
			double distance = CalculateHelper.calculateDistance(data.latitude, data.longitude, CoornidatesHelper.getRandomLati(),CoornidatesHelper.getRandomLongi());
			Debug.Log("+++++++distance: " + distance);
			this.transform.localScale = Vector3.one * (float)(1/distance);
		
			//float angle = CoornidatesHelper.degreesFromDegreesToMeters(lat1,long1,CoornidatesHelper.getRandomLati(),CoornidatesHelper.getRandomLongi());
			float angle = CoornidatesHelper.getRandomDegrees();
			Vector3 position = Quaternion.Euler(0,angle,0) * Vector3.forward;
			this.transform.position = position;

		}
		else 
			Debug.Log("Data is null");
		//Debug.Log("Test native code: " + CalculateHelper.calculateDistance(41.59032, -87.47359, 41.6175, -87.48512));
		
		//CoreLocationBinding.stopUpdatingLocation();
		//Debug.Log("angle:"+ angle + " distance:"+ distance + "scale:" + this.transform.localScale + " position:" + this.transform.position);
	}

}
