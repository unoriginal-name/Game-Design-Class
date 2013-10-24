using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interface : MonoBehaviour {
	private Gestures gestureScript;
	private GUIStyle style = new GUIStyle();
	
	void Start () {
		getScripts();
		
		Screen.orientation = ScreenOrientation.Landscape;
		style.fontSize = 50;
	}
	
	void getScripts(){
		gestureScript = (Gestures)GameObject.Find("Utilities").GetComponent<Gestures>();
	}
	
	void Update () {		
	}
	
	void OnGUI(){
		if(gestureScript == null){
			getScripts();	
			return;
		}
		
		string pathString = "PATH: ";
		string gestureString = "GESTURE CODE: " + (int)gestureScript.LastGesture;
		string timeStampString = "TIME: " + gestureScript.LastTimeStamp;
		
		LinkedList<Gestures.direction> path = gestureScript.LastPath;
		
		if(path != null){
			if(path.Count != 0){
				foreach(Gestures.direction direction in path){
					switch(direction){
					case Gestures.direction.up:
						pathString = pathString + "UP ";
						break;
					case Gestures.direction.down:
						pathString = pathString + "DOWN ";
						break;
					case Gestures.direction.left:
						pathString = pathString + "LEFT ";
						break;
					case Gestures.direction.right:
						pathString = pathString + "RIGHT ";
						break;
					}			
				}
				
				GUI.Label(new Rect(50,50,1000,100),pathString,style);
			}
		}		
		
		GUI.Label(new Rect(50,150,1000,100),gestureString,style);
		GUI.Label(new Rect(50,250,1000,100),timeStampString,style);
	}
}