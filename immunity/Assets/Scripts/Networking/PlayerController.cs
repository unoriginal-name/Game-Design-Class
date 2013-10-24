using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(networkView.isMine)
		{
			// enable the gestures
			Gestures gesture_component = (Gestures)GetComponent(typeof(Gestures));
			gesture_component.enabled = true;
			
			// enable the gestures move decider
			GestureMovePicker gesture_move_picker_component = (GestureMovePicker)GetComponent(typeof(GestureMovePicker));
			
			// find the combat rules and add it to the gesturemove picker
			gesture_move_picker_component.combat_rules = (CombatRules)GameObject.FindObjectOfType(typeof(CombatRules));
			
			// find the stamina bar and add it to the gesturemove picker
			gesture_move_picker_component.stamina = (Stamina)GameObject.FindObjectOfType(typeof(Stamina));
			gesture_move_picker_component.enabled = true;
						
			// hook up the health bar to the green bar
			Character character_component = (Character)GetComponent(typeof(Character));
			GameObject player_health = GameObject.Find ("CombatUI/PlayerHealth");
			character_component.health_bar = (HealthBar)player_health.GetComponent(typeof(HealthBar));
			character_component.health_bar.enabled = true;
		} else {
			// disable the gestures
			Gestures gesture_component = (Gestures)GetComponent(typeof(Gestures));
			gesture_component.enabled = true;
			
			// disable the gestures move decider
			GestureMovePicker gesture_move_picker_component = (GestureMovePicker)GetComponent(typeof(GestureMovePicker));
			gesture_move_picker_component.enabled = false;
			
			// hook up the health bar to the red bar
			Character character_component = (Character)GetComponent(typeof(Character));
			GameObject enemy_health = GameObject.Find ("CombatUI/EnemyHealth");
			character_component.health_bar = (HealthBar)enemy_health.GetComponent(typeof(HealthBar));
			character_component.health_bar.enabled = true;
		}
	}

}
