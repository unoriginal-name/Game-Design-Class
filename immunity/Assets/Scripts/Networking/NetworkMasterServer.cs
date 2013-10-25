using UnityEngine;

public class NetworkMasterServer : MonoBehaviour {
	
	public enum MenuState
	{
		NETWORK_LOBBY, // Master Game Server Lobby
		INGAME, // In a game as either a GS or a client
	}
	
	public MenuState game_menu_state = MenuState.NETWORK_LOBBY;
	
	public string game_type = "Immunity Game 390 v1.0";
	
	private string game_name = "Demo";
	public int server_port = 25001;
	
	private float last_host_list_request = -1000.0f;
	private float host_list_refresh_timeout = 10.0f;
	private ConnectionTesterStatus nat_capable = ConnectionTesterStatus.Undetermined;
	private bool filter_nat_hosts = false;
	private bool probing_public_ip = false;
	private bool done_testing = false;
	private float timer = 0.0f;
	private string test_message = "Testing NAT Capabilities";
	public GUIStyle format = new GUIStyle();
	
	private bool use_nat = false;
	
	void OnFailedToConnectToMasterServer(NetworkConnectionError info)
	{
		Debug.Log (info);
	}
	
	void OnFailedToConnect(NetworkConnectionError info)
	{
		Debug.Log (info);	
	}
	
	void OnGUI()
	{
		ShowGUI();	
	}
	
	void Awake()
	{
		DontDestroyOnLoad(this);
		
		// start connection test
		nat_capable = Network.TestConnection();
		
		// What kind of IP does this machine have
		if(Network.HavePublicAddress())
			Debug.Log ("This machine has a public IP address");
		else
			Debug.Log ("This machine has a private IP address");
		
		game_menu_state = MenuState.NETWORK_LOBBY;
	}
	
	void Update () {
		if(!done_testing)
			TestConnection();
		
		if(Time.realtimeSinceStartup > last_host_list_request + host_list_refresh_timeout)
		{
			MasterServer.ClearHostList(); // clear current local list of GS's prior to refreshing
			MasterServer.RequestHostList(game_type); // get an update of available games
			
			last_host_list_request = Time.realtimeSinceStartup;
			Debug.Log ("Refresh Available GS List");
		}
	}
	
	void TestConnection() {
		nat_capable = Network.TestConnection();
		
		switch(nat_capable)
		{
		case ConnectionTesterStatus.Error:
			test_message = "Problem determining NAT capabilities";
			done_testing = true;
			Debug.Log (test_message);
			break;
		case ConnectionTesterStatus.Undetermined:
			test_message = "Testing NAT capabilities";
			done_testing = false;
			//Debug.Log (test_message);
			break;
		case ConnectionTesterStatus.PublicIPIsConnectable:
			test_message = "Directly connectable public IP address";
			use_nat = false;
			done_testing = true;
			Debug.Log (test_message);
			break;
		case ConnectionTesterStatus.PublicIPPortBlocked:
			test_message = "Non-connectable public IP address (port " + server_port + " blocked), running a server is impossible";
			use_nat = false;
			
			// if no NAT punchtrhough test has been performed on this public IP, force a test
			if(!probing_public_ip)
			{
				Debug.Log("Testing if firewall can be circumvented");
				nat_capable = Network.TestConnectionNAT();
				probing_public_ip = true;
				timer = Time.time + 10;
			}
			else if(Time.time > timer)
			{
				probing_public_ip = false; // reset
				use_nat = true;
				done_testing = true;
				Debug.Log (test_message);
			}
			break;
		case ConnectionTesterStatus.PublicIPNoServerStarted:
			test_message = "Public IP address but server not initialized, " +
				"it must be started to check server accessibility." +
					" Restart connection test when ready";
			done_testing = true;
			Debug.Log (test_message);
			break;
		default:
			test_message = "Error in test routine, got " + nat_capable;
			if(string.Compare("limit", 0, nat_capable.ToString(), 0, 7) == 0)
			{
				use_nat = true;
				done_testing = true;
				Debug.Log (test_message);
				break;
			}
			break;
		}
	}
	
	void ShowGUI()
	{
		if(game_menu_state == MenuState.NETWORK_LOBBY)
		{
			if(Network.peerType == NetworkPeerType.Disconnected)
			{
				format.fontSize = 28;
				game_name = GUI.TextField(new Rect(((Screen.width/2)*0.2f)+200, (Screen.height/2)-100, 200, 30), game_name);
				
				// Start a new server
				if(GUI.Button (new Rect((Screen.width/2-100)*0.2f, (Screen.height/2)-100, 200, 30), "Start Server"))
				{
					if(done_testing) // if done testing usnig the more thorough results of the testing to setup the server
					{
						Network.InitializeServer(2, server_port, use_nat);
					} else {
						Network.InitializeServer(32, server_port, !Network.HavePublicAddress());
						MasterServer.updateRate = 3;
						
						MasterServer.RegisterHost(game_type, game_name, "Demo combat");
					}
				}
				
				// extract the list of available GS's into a local variable array for processing
				HostData[] data = MasterServer.PollHostList();
				int _cnt = 0;
				
				//Debug.Log ("# of hosts: " + data.Length);
				
				// loop through all available and display each so we can choose to join
				foreach(HostData gs in data)
				{
					// don't display NAT enabled games if we can't do punchtrhough
					//if(!(filter_nat_hosts && gs.useNat))
					//	continue;
					// don't display full servers
					if(gs.connectedPlayers >= gs.playerLimit)
						continue;
						
					// build a name string to use for display the GS in the list
					string name = gs.gameName + "-" + gs.comment + "(" + gs.connectedPlayers + "/" + gs.playerLimit + ")";
					
					if(GUI.Button(new Rect(((Screen.width/2)-100)*0.2f, (Screen.height/2)+(50*_cnt), 600, 30), name)) {
						// enable nat functionality based on GS host we're connecting to
						use_nat = gs.useNat;
						if(use_nat)
						{
							Debug.Log ("Using NAT punchthrough to connect");
							Network.Connect(gs.guid);
						} else {
							Debug.Log ("Connecting directly to host");
							Network.Connect(gs.ip, gs.port);
						}
					}
					_cnt++;
				}
			}
		}
		
	}
}
