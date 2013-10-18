using UnityEngine;
using System.Collections;

public class InstantiatePlayer : MonoBehaviour {
	public GameObject player;
	public Transform spawn_point;
	
	void InstantiatePlayerOnNetworkLoadedLevel()
	{
		Network.Instantiate(player, spawn_point.position, spawn_point.rotation, 0);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
	}
}