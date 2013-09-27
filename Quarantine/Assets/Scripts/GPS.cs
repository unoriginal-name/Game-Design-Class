using UnityEngine;
using System.Collections;

public class GPS : MonoBehaviour {
	private GameObject interfaceScript;
	
	bool ready = false;
	string location;
	void Start(){
		interfaceScript = GameObject.Find("Interface");
		
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){
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
			print ("Timed out");
			return;
		}
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			print ("Unable to determine device location");
			return;
		}
		// Access granted and location value could be retrieved
		else {
			ready = true;
			printLocation ();
		}
		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop ();	
	}
	
	void Update(){
		if(ready){
			printLocation ();
		}
	}
	
	void printLocation(){
		location = "Location: " + Input.location.lastData.latitude + " " +
			       Input.location.lastData.longitude + " " +
			       Input.location.lastData.altitude + " " +
			       Input.location.lastData.horizontalAccuracy + " " +
			       Input.location.lastData.timestamp;
		
		interfaceScript.SendMessage("changeLabel",new LabelHolder(location,Interface.labelIndices.touch2));
	}
	
	/*
	void OnGUI(){
		GUI.TextField(new Rect(30,30, 800,300),location);
	}
	*/
}