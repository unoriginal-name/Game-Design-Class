using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MessagesMenu : MonoBehaviour {
	
	float horizRatio;
	float vertRatio;

	public MainMenu main_menu;
	
	private StringBuilder sb;
	
	private Rect toRect;
	private Rect messageRect;
	private Rect sendRect;
	
	private string toField = "To";
	private string content = "Message";
	
	// Use this for initialization
	void Start () {
		horizRatio = GlobalScreenResolution.SharedInstance.widthRatio;
		vertRatio = GlobalScreenResolution.SharedInstance.heightRatio;
			
		toRect = new Rect(20*horizRatio, Screen.height/2, (Screen.width)/2 - 40*horizRatio, 60*vertRatio);
		messageRect = new Rect(20*horizRatio, toRect.yMax + 20*vertRatio, Screen.width - 40*horizRatio, 150*vertRatio);
		sendRect = new Rect(20*horizRatio, messageRect.yMax + 20 * vertRatio, Screen.width/2 - 40*horizRatio, 70*vertRatio);
	
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
		toField = GUI.TextField(toRect, toField);
		content = GUI.TextArea(messageRect, content);
		if(GUI.Button(sendRect, "Send", main_menu.regularBtnStyle))
		{
			Debug.Log ("Send button pressed");
			var webserver = new Webserver("http://immunitygame390.appspot.com");
			webserver.sendMessage(toField, PlayerPrefs.GetString("username"), content);
			toField = "";
			content = "";
		}
	}
}
