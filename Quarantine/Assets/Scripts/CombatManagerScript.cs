using UnityEngine;
using System.Collections;

public class CombatManagerScript : MonoBehaviour {

    public CombatTimer combat_timer;

    private int moves_received = 0;

    public Player player1;
    public Player player2;

    private int player1_move, player2_move;

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
        if (moves_received == 1)
            player1.setText("Player 1 chose move " + move);
        else
        {
            if (Network.isServer)
                player2.networkView.RPC("setText", RPCMode.AllBuffered, "Player 2 chose move " + move);
            else
                player2.setText("Player 2 chose move " + move);
        }

        // call the start timer on both sides
        if (Network.isClient || Network.isServer) // if we are networked then call the RPC method
            combat_timer.networkView.RPC("StartTimer", RPCMode.AllBuffered, 10.0f);
        else
            combat_timer.StartTimer(10.0f); // otherwise just call the method directly
    }
}
