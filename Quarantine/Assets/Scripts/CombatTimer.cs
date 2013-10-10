using UnityEngine;
using System.Collections;

public class CombatTimer : MonoBehaviour {

    private float countdown_time;
    private bool running = false;

    public GameObject world;

	// Use this for initialization
	void Start () {
	
	}

    [RPC]
    public bool StartTimer(float time)
    {
        Debug.Log("Starting timer for " + time + " seconds");
        if (running)
            return false;

        countdown_time = time;
        running = true;
        return true;
    }

    [RPC]
    public void StopTimer()
    {
        Debug.Log("Stopping timer");
        running = false;
    }

    public float GetTimeLeft()
    {
        if (countdown_time <= 0)
            return 0; // doesn't make sense to return a value less than 0

        return countdown_time;
    }
	
	// Update is called once per frame
	void Update () {
        if (!running)
            return; // do nothing if not running

        countdown_time -= (float)Time.deltaTime; // subtract time since last update

        if (countdown_time <= 0)
        {
            running = false;
            Debug.Log("Time's up");
            world.BroadcastMessage("TimesUp");
        }
	}
}
