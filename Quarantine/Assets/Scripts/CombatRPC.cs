using UnityEngine;
using System.Collections;

public class CombatRPC : MonoBehaviour {

    public Gestures gestures;
    public GUIStyle text_style;

    private string message = "No gestures received";

    private int last_gesture;

	// Use this for initialization
	void Start () {
        /*if (!networkView.isMine)
        {
            enabled = false;
        }*/
	}
	
	// Update is called once per frame
	void Update () {
        networkView.RPC("setLastGesture", RPCMode.Others, (int)gestures.LastGesture);
	}

    [RPC]
    void setLastGesture(int last_gesture)
    {
        this.last_gesture = last_gesture;
        message = "Last gesture: " + this.last_gesture;
    }



    void OnGUI()
    {
        GUI.Label(new Rect(0, Screen.height / 2.0f, Screen.width, 100), message, text_style);
    }
}
