using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	
	public const int MAX_HEALTH = 100;
	private int current_health;
	
	public HealthBar health_bar;
	
	// Use this for initialization
	void Start () {
		current_health = MAX_HEALTH;
	}
	
	[RPC]
	public void ChangeHealth(int change)
	{
		current_health += change;
		if(current_health > MAX_HEALTH)
			current_health = MAX_HEALTH;
		
		// This should end the match!!!!
		if(current_health < 0)
			current_health = 0;
		
		if(health_bar != null)
		{
			// frequency is inversely propotional to the percentage of health
			health_bar.ChangeFrequency(-(float)change/(float)MAX_HEALTH);
		}
	}
	
	public int GetHealth()
	{
		return current_health;	
	}
}