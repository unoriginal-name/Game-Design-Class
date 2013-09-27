using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class WWWTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var jsonString = "{\"array\": [1.44, 2, 3], " +
							"\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
							"\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
							"\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
							"\"int\": 65536, " +
							"\"float\": 3.14159, " +
							"\"bool\": true, " +
							"\"null\": null }";
		
		var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
		
		Debug.Log ("deserialized: " + dict.GetType());
		Debug.Log ("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
		Debug.Log ("dict['string']: " + (string) dict["string"]);
		Debug.Log ("dict['float']: " + (double)dict["float"]);
		Debug.Log ("dict['int']: " + (long) dict["int"]);
		Debug.Log ("dict['unicode']: " + (string) dict["unicode"]);
		
		var str = Json.Serialize(dict);
		
		Debug.Log ("serialized: " + str);
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}