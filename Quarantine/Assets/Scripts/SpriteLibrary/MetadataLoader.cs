using UnityEngine;
using System.Collections;

public class MetadataLoader : MonoBehaviour {
	
	public string file_name;
	private string text;
	
	// Use this for initialization
	void Start () {
		TextAsset text_file = (TextAsset)Resources.Load (file_name, typeof(TextAsset));
		System.IO.StringReader text_stream = new System.IO.StringReader(text_file.text);
		text = text_stream.ReadToEnd();
	}
	
	void OnGUI() {
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);	
	}
}
