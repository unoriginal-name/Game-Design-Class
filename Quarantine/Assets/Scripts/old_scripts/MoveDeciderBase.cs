using UnityEngine;
using System.Collections;

public abstract class MoveDeciderBase : MonoBehaviour {

    public Player player;
    public CombatManagerScript combat_arbiter;

    // react to timer in this method
    public abstract void TimesUp();

}
