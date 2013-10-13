using UnityEngine;
using System.Collections;

public class CombatRPC : MonoBehaviour {

    public Gestures gestures;
    public GUIStyle text_style;

    public NetworkLineDrawer line_drawer;

    private string message = "No gestures received";

    private int last_gesture;
    private int last_number_of_points = 0;

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
        if (gestures.linePoints.Count > 0)
        {
            if (gestures.linePoints.Count != last_number_of_points)
            {
                // if this is a new line then clear all of the points first
                if (gestures.linePoints.Count < last_number_of_points)
                {
                    Debug.Log("clearing points");
                    line_drawer.networkView.RPC("clearLine", RPCMode.Others);
                }

                // then add the new points
                Debug.Log("adding point");
                last_number_of_points = gestures.linePoints.Count;
                line_drawer.networkView.RPC("addPoint", RPCMode.Others, gestures.linePoints.Last.Value);
            }
        }
        else
        {
            // if the line is empty then clear the remote line
            Debug.Log("clearing points");
            last_number_of_points = 0;
            line_drawer.networkView.RPC("clearLine", RPCMode.Others);
        }
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
