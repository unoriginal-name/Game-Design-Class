using UnityEngine;
using System.Collections;

public class GestureMoveDecider : MoveDeciderBase {

    public Gestures gestures;

    private float last_timestamp = -100; // should never be negative

    public override void TimesUp()
    {
        if (Network.isClient) // if this is a networking client
            networkView.RPC("SendMove", RPCMode.OthersBuffered, player.player_name, getMove()); // send to the master combat arbiter
        else
            combat_arbiter.SubmitMove(player.player_name, getMove()); // otherwise just talk directly to the master combat arbiter
    }

    [RPC]
    public void SendMove(string name, int move)
    {
        combat_arbiter.SubmitMove(name, move);
    }

    protected int getMove()
    {
        Gestures.gesture last_gesture = gestures.LastGesture;

        // verify that this is a new move
        if (last_timestamp == gestures.LastTimeStamp)
            return -1;

        last_timestamp = gestures.LastTimeStamp;

        // this mapping should probably be customizable by the player
        if (last_gesture == Gestures.gesture.up)
        {
            return 0;
        }
        else if (last_gesture == Gestures.gesture.down)
        {
            return 1;
        }
        else if (last_gesture == Gestures.gesture.right)
        {
            return 2;
        }
        else if (last_gesture == Gestures.gesture.left)
        {
            return 3;
        }
        else if (last_gesture == Gestures.gesture.circleLeft)
        {
            return 4;
        }
        else if (last_gesture == Gestures.gesture.circleRight)
        {
            return 5;
        }
        else if (last_gesture == Gestures.gesture.upDown)
        {
            return 6;
        }
        else if (last_gesture == Gestures.gesture.leftRight)
        {
            return 7;
        }
        else
        {
            return -1;
        }
    }
}
