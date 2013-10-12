using UnityEngine;
using System.Collections;

public class PlaySpriteAnimation : MonoBehaviour {
	public int animation = 0;
	
	void Awake() {
		BroadcastMessage("PrePlay");
		BroadcastMessage("PlayAnimation", animation);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
