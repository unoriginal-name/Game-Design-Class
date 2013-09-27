using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	string[] labels = new string[3];
	public enum labelIndices{
		touch1 = 0,
		touch2,
		touch3
	};
	
	private GUIStyle style = new GUIStyle();
	
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
		style.fontSize = 50;
	}
	
	void Update () {
	}
	
	void OnGUI(){
		GUI.Label(new Rect(50,50,1000,100),labels[(int)labelIndices.touch1],style);
		GUI.Label(new Rect(50,150,1000,100),labels[(int)labelIndices.touch2],style);
		GUI.Label(new Rect(50,250,1000,100),labels[(int)labelIndices.touch3],style);
	}
	
	void changeLabel(LabelHolder labelHolder){
		labels[(int)labelHolder.indice] = labelHolder.label;
	}
}