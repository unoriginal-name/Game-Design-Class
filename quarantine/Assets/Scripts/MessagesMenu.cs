using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MessagesMenu : MonoBehaviour {
	
	public MainMenu main_menu;
	
	private StringBuilder sb;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable() {
		sb = new StringBuilder(1000);
		var webserver = new Webserver("http://immunitygame390.appspot.com");
		var rawMessageData = webserver.getMessages(PlayerPrefs.GetString("username"));
		
		object result;
		if(!rawMessageData.TryGetValue("result", out result)) {
			sb = new StringBuilder(100);
			return;
		}
		if(!((string)result).Equals("ok")) {
			sb = new StringBuilder(100);
			return;
		}
		
		List<object> rawMessages = (List<object>)rawMessageData["messages"];
		for(int i=0; i<rawMessages.Count; i++)
		{
			Dictionary<string,object> message = rawMessages[i] as Dictionary<string, System.Object>;
			sb.AppendFormat("\n\nFrom: {0}\n", (string)message["from"]);
			sb.AppendFormat ("Received: {0}\n", (string)message["timestamp"]);
			sb.AppendFormat ((string)message["content"]+"\n");
		}
	}
	
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label (main_menu.contextMenuRect, sb.ToString(), main_menu.centeredTextStyle);
	}
}
