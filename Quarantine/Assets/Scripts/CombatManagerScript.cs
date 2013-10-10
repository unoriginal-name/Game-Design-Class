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
        moves_received++;

        if (moves_received < 2)
            return;

        // make a decision about what happend (e.g. player took damage, enemy took none)

        // update the player objects

        // call the start timer on both sides
        if (Network.isClient || Network.isServer) // if we are networked then call the RPC method
            combat_timer.networkView.RPC("StartTimer", RPCMode.AllBuffered, 10);
        else
            combat_timer.StartTimer(10); // otherwise just call the method directly
    }
}
