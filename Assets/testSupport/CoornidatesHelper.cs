using UnityEngine;
using System.Collections;

public class CoornidatesHelper : MonoBehaviour {
	
	//41.59032 -87.47359 center
	public static float distanceFromDegreesToMeters(float lat1, float long1, float lat2, float long2) {
		//Debug.Log(Mathf.Sin(lat2*Mathf.Deg2Rad) + " " + Mathf.Sin(lat1*Mathf.Deg2Rad));
		//Debug.Log("XXX:" +Mathf.Cos(lat1*Mathf.Deg2Rad)*Mathf.Cos(lat2*Mathf.Deg2Rad)*Mathf.Cos(long2*Mathf.Deg2Rad-long1*Mathf.Deg2Rad));
		//Debug.Log("YYY: " +(Mathf.Sin(lat1*Mathf.Deg2Rad)*Mathf.Sin(lat2*Mathf.Deg2Rad) + Mathf.Cos(lat1*Mathf.Deg2Rad)*Mathf.Cos(lat2*Mathf.Deg2Rad)*Mathf.Cos(long2*Mathf.Deg2Rad-long1*Mathf.Deg2Rad)));
		float e = Mathf.Acos(Mathf.Sin(lat1*Mathf.Deg2Rad)*Mathf.Sin(lat2*Mathf.Deg2Rad) + Mathf.Cos(lat1*Mathf.Deg2Rad)*Mathf.Cos(lat2*Mathf.Deg2Rad)*Mathf.Cos(long2*Mathf.Deg2Rad-long1*Mathf.Deg2Rad));
		//Debug.Log("E: " +e);
		float distanceInM = e * 6378137; //6378.137f for km;
		return distanceInM;
	}
	/**
	 * Degrees off north
	 **/
	public static float degreesFromDegreesToMeters(float lat1, float long1, float lat2, float long2) {
		float e = Mathf.Acos(Mathf.Sin(lat1)*Mathf.Sin(lat2) + Mathf.Cos(lat1)*Mathf.Cos(lat2)*Mathf.Cos(long2-long1));
		float ecos = Mathf.Acos(Mathf.Sin(lat1)*Mathf.Sin(lat2) + Mathf.Cos(lat1)*Mathf.Cos(lat2)*Mathf.Cos(long1-long1));
		float degree = Mathf.Acos(ecos/e)*Mathf.Rad2Deg;
		if(lat1 >lat2) {
			if (long1 > long2 ){
				return degree;
			}
			else {
				//return -degree;
				return 360.0f - degree;
			}
		}
		else {
			if (long1 > long2) {
				return 180.0f - degree;
			}
			else {
				//return degree - 180.0f;
				return 180.0f + degree;
			}
		}
	}
	public static float getRandomDegrees(){
		return Random.Range(0,360);
	}
	public static float getRandomDistance(){
		return Random.Range(50,200);
	}
	public static float getRandomLati() {
		if(Random.Range(-1.0f,1.0f)>0) {
			//return 41.59032f + Random.Range(-0.005f,-0.0025f);
			return 41.58611f + Random.Range(-0.0005f, -0.0000f); // power building location
		}
		else {
			//return 41.59032f + Random.Range(0.0025f,0.005f);
			return 41.58611f + Random.Range(0.0005f,0.000f);
		}

	}
	public static float getRandomLongi() {
		if(Random.Range(-1.0f,1.0f)>0) {
			//return -87.47359f + Random.Range(-0.002f,-0.001f);
			return -87.47538f + Random.Range(-0.0002f,-0.0000f);
		} else {
			//return -87.47359f + Random.Range(0.001f,0.002f);
			return -87.47538f + Random.Range(0.0000f,0.0002f);
		}
	}
	/**
	 * Test
	 **/
	void Update() {
		Debug.Log("distance: "+ CoornidatesHelper.distanceFromDegreesToMeters(41.59036f, -87.47357f,CoornidatesHelper.getRandomLati(),CoornidatesHelper.getRandomLongi()));
		Debug.Log("degree: "+ CoornidatesHelper.degreesFromDegreesToMeters(41.59036f, -87.47357f,CoornidatesHelper.getRandomLati(),CoornidatesHelper.getRandomLongi()));

	}
	

		
}
