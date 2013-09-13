using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int MAX_HEALTH = 100;
	public int currentHealth = 100;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width/2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onGUI()
	{
		GUI.Box (new Rect(Screen.height/2, Screen.width/2, healthBarLength, 200), currentHealth + "/" + MAX_HEALTH);
		
		if (GUI.Button(new Rect(10, 40, 10, 10), "testing"))
			print("Button clicked");
	}
	/**
	 * If damaged, pass in a negative number.  If healed, a positive number.
	 */ 
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
	}
}
