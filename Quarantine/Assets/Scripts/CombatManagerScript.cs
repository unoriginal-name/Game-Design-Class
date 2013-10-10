using UnityEngine;
using System.Collections;

public class CombatManagerScript : MonoBehaviour {

    public CombatTimer combat_timer;

	// Use this for initialization
	void Start () {
        combat_timer.StartTimer(10);
	}

    void TimesUp()
    {
        Debug.Log("Times Up");
        combat_timer.StartTimer(10);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
