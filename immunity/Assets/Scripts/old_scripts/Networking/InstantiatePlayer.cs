using UnityEngine;
using System.Collections;

public class InstantiatePlayer : MonoBehaviour {
	public GameObject player;
	public Transform spawn_point;
	
	void InstantiatePlayerOnNetworkLoadedLevel()
	{
		GameObject new_player = (GameObject)Network.Instantiate(player, spawn_point.position, spawn_point.rotation, 0);
		
		CombatTimer timer = (CombatTimer)GameObject.FindObjectOfType (typeof(CombatTimer));
		timer.objects.Add(new_player);
		
		if(Network.isClient)
		{
			timer.networkView.RPC("UnPauseTimer", RPCMode.All);
		}
		
		CombatRules combat_rules = (CombatRules)GameObject.FindObjectOfType(typeof(CombatRules));
		if(new_player.name.Equals ("Player(Clone)"))
		{
			Debug.Log ("Setting the player of combat rules");
			combat_rules.player = (Character)new_player.GetComponent(typeof(Character));
		}
		else
		{
			Debug.Log ("Setting the enemy of Combat Rules");
			combat_rules.enemy = (Character)new_player.GetComponent(typeof(Character));
		}
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
	}
	

}