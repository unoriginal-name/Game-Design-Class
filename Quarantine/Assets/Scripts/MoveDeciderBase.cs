using UnityEngine;
using System.Collections;

public class MoveDeciderBase : MonoBehaviour {
    public virtual int GetMove()
    {
        return -1;
    }
}
