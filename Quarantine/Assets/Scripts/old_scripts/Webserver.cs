using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Webserver {
	
	public string base_url;
	
	public Webserver(string base_url) {
		this.base_url = base_url;
	}
	
	private Dictionary<string, object> WebRequest(string path_url, WWWForm form = null) {
		Debug.Log (base_url + path_url);
		WWW getReq;
		if(form != null) {
			getReq = new WWW(base_url + path_url, form);
		} else {
			getReq = new WWW(base_url + path_url);
		}
		
		// bad for battery
		while(!getReq.isDone)
		{}
		
		if(getReq.error != null)
		{
			Debug.LogError("registeruser: " + getReq.error);
			Dictionary<string, object> result = new Dictionary<string, object>();
			result.Add("result", getReq.error);
			return result;
		} else {
			return Json.Deserialize(getReq.text) as Dictionary<string, object>;
		}
	}
	
	public Dictionary<string, object> registerUser(string username) {
		WWWForm form = new WWWForm();
		form.AddField ("username", username);
			
		return WebRequest("/user/create/"+ WWW.EscapeURL(username), form);
	}
	
	public Dictionary<string, object> getUserInfo(string username) {
		return WebRequest("/user/profile/" + WWW.EscapeURL(username));
	}
	
	public Dictionary<string, object> getUserFriends(string username) {
		return WebRequest("/user/profile/" + WWW.EscapeURL(username) + "/friends");
	}
	
	public Dictionary<string, object> addFriend(string username, string friend) {
		WWWForm form = new WWWForm();
		form.AddField("username", friend);
		return WebRequest("/user/profile/" + WWW.EscapeURL(username) + "/friends", form);
	}
	
	public Dictionary<string, object> getMessages(string username) {
		return WebRequest ("/user/profile/" + WWW.EscapeURL(username) + "/messages");
	}
	
	public Dictionary<string, object> sendMessage(string to_user, string from_user, string message) {
		WWWForm form = new WWWForm();
		form.AddField("from", from_user);
		form.AddField("content", message);
		return WebRequest ("/user/profile/" + WWW.EscapeURL(to_user) + "/messages", form);
	}
}
