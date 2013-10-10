using UnityEngine;
using System.Collections;

public class CombatManagerScript : MonoBehaviour {

    public CombatTimer combat_timer;

    private int moves_received = 0;

	// Use this for initialization
	void Start () {
        combat_timer.StartTimer(10);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SubmitMove(string name, int move)
    {
        Debug.Log(name + " chose move " + move);
    }
}
