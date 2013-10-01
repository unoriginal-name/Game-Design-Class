using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {
	
	public string level_name = "MainMenu";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(level_name);	
		}
	}
}
