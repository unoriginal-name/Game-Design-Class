using UnityEngine;
using System.Collections;

public class CombatHUD : MonoBehaviour {

    public Texture combatHUD;

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), combatHUD);
	}
}
