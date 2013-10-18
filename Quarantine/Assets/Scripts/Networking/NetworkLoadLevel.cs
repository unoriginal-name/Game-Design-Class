using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class NetworkLoadLevel : MonoBehaviour {
	
	// keep track of the last level prefix (increment each time a new level is loaded)
	private int last_level_prefix = 0;
	private NetworkMasterServer mgs;
		
	void Awake()
	{
		// Network level loading is done in a separate channel
		DontDestroyOnLoad(this);
		networkView.group = 1;
		
		// Application.LoadLevel("NetworkCombat");
		mgs = GameObject.Find ("Network Manager").GetComponent<NetworkMasterServer>() as NetworkMasterServer;
	}
	
	void Update() {
		// when the server is started issue a "LoadLevel" RPC call for the local GS client
		// to load his own level then also buffer the LoadLevel request so any new client
		// that connects will also receive the "LoadLevel"
		if(Network.peerType != NetworkPeerType.Disconnected && mgs.game_menu_state == NetworkMasterServer.MenuState.NETWORK_LOBBY && Network.isServer)
		{
			// make sure no old RPC calls are buffered and then send load level command
			Network.RemoveRPCsInGroup(0);
			Network.RemoveRPCsInGroup(1);
			
			// Load level with incremented level prefix (for view IDs)
			networkView.RPC("LoadLevel", RPCMode.AllBuffered, "NetworkCombat", last_level_prefix + 1);
		}
	}
	
	[RPC]
	void LoadLevel(string level, int level_prefix)
	{
		if(mgs.game_menu_state != NetworkMasterServer.MenuState.INGAME)
		{
			mgs.game_menu_state = NetworkMasterServer.MenuState.INGAME;
			Debug.Log ("Loading level " + level + " with prefix " + level_prefix);
			last_level_prefix = level_prefix;
			
			// There is no reason to send any more data over the network on the default channel
			// because we are about to load the level all of the objects will get deleted anyways
			Network.SetSendingEnabled(0, false);
			
			// We need to stop receiving because first the level must be loaded
			// Once the level is loaded, RPCs and other state update attached to objects
			// in the level are allowed to fire
			Network.isMessageQueueRunning = false;
			
			// All network views loaded from a level will get a prefix into their NetworkViewID
			// this will prevent old updates from clients leaking into a newly created scene
			Network.SetLevelPrefix(level_prefix);
			Application.LoadLevel(level);
		}
	}
	
	void OnLevelWasLoaded()
	{
		// if we've gone back to the MainMenu
		if(Application.loadedLevelName == "MainMenu")
			Destroy (this);
		
		// Allow receiving data again
		Network.isMessageQueueRunning = true;
		// Now the level has been loaded and we can start sending out data again
		Network.SetSendingEnabled(0, true);
		
		// Once the level is loaded we need to instantiate our local player
		// we could use Network.Instantiate but that only lets us send
		GameObject go;
		if(Network.isServer)
			go = GameObject.Find ("PlayerSpawn");
		else
			go = GameObject.Find ("EnemySpawn");
		
		InstantiatePlayer io = (InstantiatePlayer)go.GetComponent(typeof(InstantiatePlayer));
		io.SendMessage("InstantiatePlayerOnNetworkLoadedLevel", SendMessageOptions.RequireReceiver);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
	}
	
	void OnDisconnectedFromServer() {
		// if we lose the connection to the server then return to the Main Menu
		Application.LoadLevel("MainMenu");
	}
}
