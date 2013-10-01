using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class FriendsMenu : MonoBehaviour {

	private class Friend {
		public string username = "";
		public string member_since = "";
		public long games_played = 0;
		public Friend(Dictionary<string, object> friendData)
		{
			if(friendData == null)
				return;
			
			object temp;
			if(friendData.TryGetValue("username", out temp))
				username = (string)temp;
			if(friendData.TryGetValue("member_since", out temp))
				member_since = (string)temp;
			if(friendData.TryGetValue("games_played", out temp))
				games_played = (long)temp;
		}
	}
	
	float horizRatio;
	float vertRatio;
		
	private StringBuilder sb;
	
	public MainMenu main_menu = null;
	
	// Use this for initialization
	void Start () {
	}
	
	void OnEnable() {
		sb = new StringBuilder(1000);
		
		var server = new Webserver("http://immunitygame390.appspot.com");
		var rawFriendData = server.getUserFriends(PlayerPrefs.GetString("username"));
				
		object result;
		if(!rawFriendData.TryGetValue("result", out result)) {
			sb = new StringBuilder(100);
			sb.Append ("Error: rawFriendData - Malformed return value");
			return;
		}
		if(!((string)result).Equals("ok")) {
			sb = new StringBuilder(100);
			sb.AppendFormat ("Error: rawFriendData - {0}", (string)result);
			return;
		}
		
		object temp;
		List<object> rawFriends = new List<object>();
		if(rawFriendData.TryGetValue("friends", out temp))
			rawFriends = (List<object>)temp;
			
		foreach(object rawFriend in rawFriends)
		{
			var rawFriendInfo = server.getUserInfo((string)rawFriend);
			
			if(!rawFriendInfo.TryGetValue("result", out result)) {
				sb = new StringBuilder(100);
				sb.Append ("Error: rawFriendInfo - Malformed return value");
				return;
			}
			if(!((string)result).Equals("ok")) {
				sb = new StringBuilder(100);
				sb.AppendFormat ("Error: rawFriendInfo - {0}", (string)result);
				return;
			}
												

			var friend = new Friend(rawFriendInfo);
			sb.AppendFormat("\n\n{0}:\n\tMember Since {1}\n\t{2} Games Played", friend.username, friend.member_since, friend.games_played);
		}
		Debug.Log (sb.ToString());
	}
	
	void OnGUI() {
		if(main_menu == null)
			Debug.LogError ("main_menu is null in friends menu");
		else
			GUI.Label (main_menu.contextMenuRect, sb.ToString(), main_menu.centeredTextStyle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
