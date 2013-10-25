using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkLineDrawer : MonoBehaviour {

    private LineRenderer line_renderer;
    private List<Vector3> points;

    public Color line_color;

	// Use this for initialization
	void Start () {
        points = new List<Vector3>();

        line_renderer = gameObject.AddComponent<LineRenderer>();
        //line_renderer.material = new Material(Shader.Find("Particles/Additive"));
        line_renderer.SetColors(line_color, line_color);
        line_renderer.SetWidth(0.01f, 0.01f);
        line_renderer.SetVertexCount(points.Count);
	}
	
	// Update is called once per frame
	void Update () {
        line_renderer.SetVertexCount(points.Count);

        for (int i = 0; i < points.Count; i++ )
        {
            line_renderer.SetPosition(i, points[i]);
        }
	}

    [RPC]
    void addPoint(Vector3 point)
    {
        points.Add(point);
    }

    [RPC]
    void clearLine()
    {
        points = new List<Vector3>();
    }
}
