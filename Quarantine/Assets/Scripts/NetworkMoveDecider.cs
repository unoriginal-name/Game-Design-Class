using UnityEngine;
using System.Collections;

public class NetworkMoveDecider : GestureMoveDecider {

    // replace with code that does nothing
    // and an RPC that when called passes data along to the arbiter
    public override void TimesUp()
    {
        if (Network.isClient)
            networkView.RPC("SendMove", RPCMode.OthersBuffered, getMove());
    }

    [RPC]
    public void SendMove(int move)
    {
        combat_arbiter.SubmitMove(player.name, move);
    }
}
