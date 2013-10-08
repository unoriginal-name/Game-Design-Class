using UnityEngine;
using System.Collections;

public class GlobalScreenResolution {
	private static GlobalScreenResolution instance = null;
	public static GlobalScreenResolution SharedInstance {
		get {
			if (instance == null) {
				instance = new GlobalScreenResolution();
			}
			return instance;
		}
	}
	public int expectedWidth = 800;
	public int expectedHeight = 1280;
	
	public float widthRatio;
	public float heightRatio; 
	
	public GlobalScreenResolution() {
		widthRatio = (float)Screen.width/(float)expectedWidth;
		heightRatio = (float)Screen.height/(float)expectedHeight;
	}

}
