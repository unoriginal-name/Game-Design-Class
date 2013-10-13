using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {
    public string gameName = "Immunity Game 390";

    public Texture loading_background;
    private Rect loading_rect;

    private float refresh_timeout = 2.0f;
    private float refresh_start = 0.0f;
    private bool refreshing = false;
    private HostData[] host_data = null;

    public MainMenu main_menu;
    private Rect context_menu_rect;
    private GUIStyle regular_btn_style;

    private float horizRatio, vertRatio;
    private float btnX, btnY, btnH, btnW;

    public GUIStyle status_message_style;
    private string status_message = "";

    bool should_destroy = false; // used to tell the object when it should kill itself

	// Use this for initialization
	void Start () {
        context_menu_rect = main_menu.contextMenuRect;
        regular_btn_style = main_menu.regularBtnStyle;

        horizRatio = GlobalScreenResolution.SharedInstance.widthRatio;
        vertRatio = GlobalScreenResolution.SharedInstance.heightRatio;

        btnX = Screen.width * 0.05f;
        btnY = Screen.height * 0.05f;
        btnW = Screen.width * 0.1f;
        btnH = btnW;

        loading_rect = new Rect(0, context_menu_rect.height * .25f, Screen.width, context_menu_rect.height * .5f);
        Debug.Log("0, " + context_menu_rect.height * .25f + ", " + Screen.width + ", " + context_menu_rect.height * .5f);
        DontDestroyOnLoad(this);
    }

    void startServer()
    {
        Network.InitializeServer(32, 25001, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, PlayerPrefs.GetString("username") + "'s Game", "This is a demo game");
    }

    void refreshHostList()
    {
        host_data = null;
        MasterServer.RequestHostList(gameName);
        refreshing = true;
        Debug.Log("refreshing");
        status_message = "Refreshing";
        refresh_start = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        // if we are reentering the main menu then destory this object
        if (Application.loadedLevelName.Equals("CombatScene"))
        {
            should_destroy = true;
        }
        else
        {
            if (should_destroy)
                Destroy(this.gameObject);
        }

        if (refreshing)
        {
            if (Time.time - refresh_start > refresh_timeout)
                refreshing = false;

            if (MasterServer.PollHostList().Length > 0)
            {
                refreshing = false;
                host_data = MasterServer.PollHostList();
                Debug.Log("Received " + host_data.Length + " games");
            }
        }
	}

    void OnServerInitialized()
    {
        Debug.Log("Server initialized");
        // spawn players here
        Application.LoadLevel("CombatScene");
    }

    void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
        // spawn players here
        Application.LoadLevel("CombatScene");
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Application.LoadLevel("MainMenu");
        Destroy(this.gameObject);
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered server");
        }
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(context_menu_rect.xMin + btnX, context_menu_rect.yMin + btnY, btnW, btnH), "Start Server", regular_btn_style))
            {
                Debug.Log("Starting server");
                startServer();
            }

            if (GUI.Button(new Rect(context_menu_rect.xMin + btnX, context_menu_rect.yMin + btnY * 1.2f + btnH, btnW, btnH), "Refresh Hosts", regular_btn_style))
            {
                Debug.Log("Refreshing");
                refreshHostList();
            }

            if (host_data != null)
            {
                for (int i = 0; i < host_data.Length; i++)
                {
                    if (GUI.Button(new Rect(context_menu_rect.xMin + btnX * 1.5f + btnW, context_menu_rect.yMin + btnY * 1.2f + (btnH * i), btnW * 3f, btnH * 0.5f), host_data[i].gameName, regular_btn_style))
                    {
                        Debug.Log("Connecting to game");
                        refreshing = true;
                        status_message = "Connecting to game";
                        Network.Connect(host_data[i]);
                    }
                }
            }
        }

        if (refreshing)
        {
            GUI.DrawTexture(loading_rect, loading_background);
            GUI.Label(loading_rect, status_message, status_message_style);
        }
    }

    void OnDisable()
    {
        Network.Disconnect(1000);
        MasterServer.UnregisterHost();
    }
}
