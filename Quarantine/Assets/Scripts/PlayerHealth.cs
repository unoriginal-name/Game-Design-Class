using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
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
		GUI.Box (new Rect(0, (int)(Screen.height * 0.01), healthBarLength, 25), "Player: " + currentHealth + "/" + MAX_HEALTH);
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
	}
}
