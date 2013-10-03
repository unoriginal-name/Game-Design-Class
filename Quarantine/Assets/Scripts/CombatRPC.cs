using UnityEngine;
using System.Collections;

public class CombatRPC : MonoBehaviour {

    public Gestures gestures;
    public GUIStyle text_style;

    private string message = "No gestures received";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        networkView.RPC("getLastGesture", RPCMode.Others);
	}

    [RPC]
    int getLastGesture()
    {
        return (int)gestures.LastGesture;
    }



    void OnGUI()
    {
        GUI.Label(new Rect(0, Screen.height / 2.0f, Screen.width, 100), message, text_style);
    }
}
