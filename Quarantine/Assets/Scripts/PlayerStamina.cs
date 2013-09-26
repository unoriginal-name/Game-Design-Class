using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour {
	public int MAX_STAMINA = 100;  //once we've got creatures, we'll get the max stamina from the creature data
	public int currentStamina;
	
	public float staminaBarLength;
	
	// Use this for initialization
	void Start () {
		staminaBarLength = Screen.width/2;
		currentStamina = MAX_STAMINA;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.color = Color.yellow;
		GUI.Box (new Rect(0, (int)(Screen.height * 0.05), staminaBarLength, Screen.height/25), "Stamina: " + currentStamina + "/" + MAX_STAMINA);
	}
	
	public void adjustCurrentStamina(int adjustment)
	{
		currentStamina += adjustment;
		
		if (currentStamina < 1)
			currentStamina = 0;
		if (currentStamina > MAX_STAMINA)
			currentStamina = MAX_STAMINA;
		if (MAX_STAMINA < 1)
			MAX_STAMINA = 1;
		staminaBarLength = Screen.width/2 * (currentStamina / (float)MAX_STAMINA);
	}
}
