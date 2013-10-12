using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class SpriteAnimation : MonoBehaviour {
	
	public int cols, rows; 
	public double fps = 24.0;
	
	public int width, height;
	public int start_x, start_y;
	public int spacing_x, spacing_y;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int)(Time.time * fps);
		
		index = index % ( rows * cols);
		
		Vector2 size = new Vector2(1/cols, 1/rows);
		
		int col = index % cols;
		int row = index / cols;
		
		Vector2 offset = new Vector2(cols*size.x, 1 - size.y - rows*size.y);
		
		renderer.material.SetTextureOffset("_MainTex", offset);
		renderer.material.SetTextureScale("_MainTex", size);
	}
}
