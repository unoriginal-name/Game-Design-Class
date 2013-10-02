using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {
    public string gameName = "Immunity Game 390";

    private bool refreshing = false;
    private HostData[] host_data = null;

    public MainMenu main_menu;
    private Rect context_menu_rect;
    private GUIStyle regular_btn_style;

    private float horizRatio, vertRatio;
    private float btnX, btnY, btnH, btnW;

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
	}

    void startServer()
    {
        Network.InitializeServer(32, 25001, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, PlayerPrefs.GetString("username") + "'s Game", "This is a demo game");
    }

    void refreshHostList()
    {
        MasterServer.RequestHostList(gameName);
        refreshing = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (refreshing)
        {
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
    }

    void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
        // spawn players here
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
                        Network.Connect(host_data[i]);
                    }
                }
            }
        }
    }
}
