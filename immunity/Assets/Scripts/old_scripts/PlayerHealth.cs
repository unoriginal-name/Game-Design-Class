//note: attach the heartbeat sound effect to the main camera

using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	//basically, I want to get the creature's max health from its creature data and set that equal to the max health over here.
	
	GameObject creature;
	AudioSource sound_name;
	
	int MAX_HEALTH;
	
	int currentHealth;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {
		creature = GameObject.Find("Player");
		creature.AddComponent ("sound_name");
		MAX_HEALTH = creature.GetComponent<Creature>().MAX_HEALTH;
		currentHealth = MAX_HEALTH;
		healthBarLength = Screen.width/2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.color = Color.red;
		GUI.Box (new Rect(0, (int)(Screen.height * 0.01), healthBarLength, Screen.height/25), "Player: " + currentHealth + "/" + MAX_HEALTH);
	}
	
	public void adjustCurrentHealth(int adjustment)
	{
		currentHealth += adjustment;
		
		if (currentHealth < 1)
			currentHealth = 0;
		if (currentHealth > MAX_HEALTH)
			currentHealth = MAX_HEALTH;
		if (MAX_HEALTH < 1)
			MAX_HEALTH = 1;
		healthBarLength = Screen.width/2 * (currentHealth / (float)MAX_HEALTH);
		
		int soundEffect = currentHealth / MAX_HEALTH;
		
		if (soundEffect <= 0.75)
		{
			sound_name.clip = (AudioClip)Resources.Load("ThreeQuarterHealth_Heartbeat");
			sound_name.Play();
		}
		if (soundEffect <= 0.5)
		{
			sound_name.clip = (AudioClip)Resources.Load("Half_health_heartbeat");
			sound_name.Play();
		}
		if (soundEffect <= 0.25)
		{
			sound_name.clip = (AudioClip)Resources.Load("Quarter_health_heartbeat");
			sound_name.Play();
		}
	}
}
