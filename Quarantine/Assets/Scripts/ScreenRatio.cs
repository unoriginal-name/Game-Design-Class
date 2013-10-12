using UnityEngine;
using System.Collections;

public class ScreenRatio : MonoBehaviour {
	
	public int expected_width = 1280;
	public int expected_height = 800;
	
	public float horiz = 1;
	public float vert = 1;
	
	void Update()
	{
		horiz = (float)Screen.width/(float)expected_width;
		vert = (float)Screen.height/(float)expected_height;
	}
}
