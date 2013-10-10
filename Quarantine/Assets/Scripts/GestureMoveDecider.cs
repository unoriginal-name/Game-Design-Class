using UnityEngine;
using System.Collections;

public class GestureMoveDecider : MoveDeciderBase {

    public Gestures gestures;

    public override void TimesUp()
    {
        combat_arbiter.SubmitMove(player.name, getMove());
    }

    private int getMove()
    {
        Gestures.gesture last_gesture = gestures.LastGesture;

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
