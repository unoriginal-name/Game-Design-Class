using UnityEngine;
using System.Collections;

public class GPS : MonoBehaviour {
	public enum status{
		serviceOff = 0,
		timedOut,
		connectionFailed,
		ready
	}
	
	private status gpsStatus;
	public status GPSStatus{
		get{
			return gpsStatus;
		}
		set{}
	}
	
	private string location;
	public string Location{
		get{
			return location;
		}
		set{}
	}
	
	void Start(){
		gpsStatus = status.ready;
		
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){
			gpsStatus = status.serviceOff;
			print ("Location Service OFF");
			return;
		}
		
		// Start service before querying location
		Input.location.Start (1,1);
		
		// Wait until service initializes
		float timeStart = Time.time;
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && Time.time - timeStart < maxWait) {	
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1) {
			gpsStatus = status.timedOut;
			print ("Timed out");
			return;
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			gpsStatus = status.connectionFailed;
			print ("Unable to determine device location");
			return;
		}
		
		// Access granted and location value could be retrieved
		else {
			gpsStatus = status.ready;
		}
	}
	
	void Update(){
		if(gpsStatus == status.ready){
			location = "Location[ " + 
				"Latitude:" + Input.location.lastData.latitude + " " +
			    "Longitude:" + Input.location.lastData.longitude + " " +
			    "Altitude:" + Input.location.lastData.altitude + " " +
			    "Accuracy:" + Input.location.lastData.horizontalAccuracy + " " +
			    "Time Stamp:" + Input.location.lastData.timestamp + " " + 
			"]";
		}
	}
	
}