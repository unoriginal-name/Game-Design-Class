using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public class native_sockets_demo : MonoBehaviour {
	
	string result = "Didn't run :(";
	
	public GUIStyle MyStyle;

	// Use this for initialization
	void Start () {
		StringBuilder sb = new StringBuilder (256);
		//helloString(sb, "testing", sb.Capacity); //"" + add(2, 10);
		getDNS ("google.com", sb, sb.Capacity);
		result = sb.ToString();
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label(new Rect(20, 20, Screen.width-40, Screen.height-40), result, MyStyle);
	}
	
	[DllImport("native")]
	private static extern int add(int x, int y);
	
	[DllImport("native")]
	private static extern void helloString (StringBuilder dest, string src, int n);
	
	[DllImport("native")]
	private static extern void getDNS(string hostname, StringBuilder result, int n);
}
