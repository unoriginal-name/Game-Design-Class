using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public class native_sockets_demo : MonoBehaviour {
	
	string labelText = "Didn't run :(";
	
	public GUIStyle MyStyle;

	// Use this for initialization
	void Start () {
		StringBuilder err = new StringBuilder (256);
		StringBuilder labelBuilder = new StringBuilder();
		StringBuilder buffer = new StringBuilder(256);
		StringBuilder htmlBuilder = new StringBuilder();
		
		int sockfd = createTCPSocket("www.google.com", "80", err);
		if(sockfd < 0)
		{
			labelText = "ERROR: " + err.ToString();
			return;
		}
		
		string msg = "GET / HTTP/1.1\r\nHost: www.google.com\r\nConnection: close\r\n\r\n";
		int bytes_sent = sendMsg (sockfd, msg, msg.Length);
		labelBuilder.AppendFormat("bytes sent: {0}\n", bytes_sent);
		int total_bytes_recv = 0;
		int bytes_recv = recvMsg (sockfd, buffer, buffer.Capacity);
		while(bytes_recv > 0)
		{
			total_bytes_recv += bytes_recv;
			htmlBuilder.AppendFormat("{0}", buffer.ToString());
			
			bytes_recv = recvMsg(sockfd, buffer, buffer.Capacity);
		}
		closeSocket(sockfd);
		
		labelBuilder.AppendFormat("bytes received: {0}\n", total_bytes_recv);
		labelBuilder.AppendFormat("Response: \n{0}", htmlBuilder.ToString());
		
		labelText = labelBuilder.ToString();
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.Label(new Rect(20, 20, Screen.width-40, Screen.height-40), labelText, MyStyle);
	}
	
	[DllImport("native")]
	private static extern int add(int x, int y);
	
	[DllImport("native")]
	private static extern void helloString (StringBuilder dest, string src, int n);
	
	[DllImport("native")]
	private static extern void getDNS(string hostname, StringBuilder result, int n);
	
	[DllImport("native")]
	private static extern int createTCPSocket(string hostname, string port, StringBuilder error);
	
	[DllImport("native")]
	private static extern int sendMsg(int sockfd, string msg, int len);
	
	[DllImport("native")]
	private static extern int recvMsg(int sockfd, StringBuilder msg, int len);
	
	[DllImport("native")]
	private static extern void closeSocket(int sockfd);
}
