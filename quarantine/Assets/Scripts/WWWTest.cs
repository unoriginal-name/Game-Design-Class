using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class WWWTest : MonoBehaviour {
	
	private Dictionary<string,object> user_profile;
	
	// Use this for initialization
	IEnumerator Start () {
		
		WWW getUserProfile = new WWW("http://immunitygame390.appspot.com/user/profile/vanderhush");
		yield return getUserProfile;

		if(getUserProfile.error != null)
		{
			Debug.LogError("getUserProfile: " + getUserProfile.error);
			return false;
		} else {
			Debug.Log (getUserProfile.text);
			
			user_profile = Json.Deserialize(getUserProfile.text) as Dictionary<string, object>;
			List<object> friends = (List<object>) user_profile["friends"];
			for(int i=0; i<friends.Count; i++)
			{
				Debug.Log ("friend["+i+"]: " + (string)friends[i]);	
			}
			Debug.Log ("games played: " + (long) user_profile["games_played"]);
			Debug.Log ("member_since: " + (string) user_profile["member_since"]);
			Debug.Log ("username: " + (string) user_profile["username"]);
		}
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}