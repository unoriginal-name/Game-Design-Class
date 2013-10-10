using UnityEngine;
using System.Collections;

public class NetworkMoveDecider : MoveDeciderBase {

    // replace with code that does nothing
    // and an RPC that when called passes data along to the arbiter
    public override void TimesUp()
    {
        combat_arbiter.SubmitMove(player.name, -1);
    }

    //[RPC]
    //public 
}
