using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatTimer : MonoBehaviour {

    private float countdown_time;
    private bool running = false;
	private bool paused = false;

    public List<GameObject> objects;

    public void StartTimer(float time)
    {
        Debug.Log("Starting timer for " + time + " seconds");

        countdown_time = time;
        running = true;
    }
	
	public void PauseTimer()
	{
		paused = true;	
		
		foreach(GameObject obj in objects)
		{
			obj.BroadcastMessage("TimerPaused");	
		}
	}
	
	public void UnPauseTimer()
	{
		paused = false;	
		
		foreach(GameObject obj in objects)
		{
			obj.BroadcastMessage("TimerUnpaused");
		}
	}
	
	public bool IsPaused()
	{
		return paused;	
	}
	
	public bool IsRunning()
	{
		return running;
	}

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
	
	void Update () {
        if (!running || paused)
            return; // do nothing if not running

        countdown_time -= (float)Time.deltaTime; // subtract time since last update

        if (countdown_time <= 0)
        {
            running = false;
            Debug.Log("Time's up");
            foreach(GameObject obj in objects)
			{
				obj.BroadcastMessage("TimesUp");
			}
        }
	}
}
