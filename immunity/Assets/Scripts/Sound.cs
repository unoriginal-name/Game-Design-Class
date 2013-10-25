//attach this to the main camera

using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	
	GameObject mainCamera;
	AudioSource BGM;
	string BGMClip; //set this

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
		mainCamera.AddComponent("AudioSource");
		BGM.clip = (AudioClip)Resources.Load (BGMClip);
		BGM.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
