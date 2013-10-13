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
	
	void OnGUI()
	{
		GUI.color = Color.red;
		GUI.Box (new Rect(Screen.width/2, (int)(Screen.height * 0.01), healthBarLength, 25), "Health: " + currentHealth + "/" + MAX_HEALTH);
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
