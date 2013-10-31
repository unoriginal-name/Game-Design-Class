using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureUI : MonoBehaviour {
	
	// Set this in the editor
	public GameObject gestureObject;
	private GestureInterpreter gestureInterpreter;
	
	private LineRenderer lineRenderer;
	private LineRenderer gestureRenderer;
	private float lastTimeStamp = 0;
	private GestureInterpreter.gesture lastGesture;
	
	private Vector3[] points = new Vector3[3];
	
	void Start () {
		gestureInterpreter = gestureObject.GetComponent<GestureInterpreter>();
		
		
		lineRenderer = (LineRenderer)gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        lineRenderer.material.color = Color.black;
        lineRenderer.SetWidth(.005f, .005f);
		
		/*
		gestureRenderer = (LineRenderer)gameObject.AddComponent<LineRenderer>();
        gestureRenderer.material = new Material(Shader.Find("Transparent/Diffuse"));
        gestureRenderer.material.color = Color.black;
        gestureRenderer.SetWidth(.005f, .005f);
        */
	}
	
	// Update is called once per frame
	void Update() {
		drawCurrentPath();
		
		checkNewGesture();
		
		//drawLastGesture();
	}
	
	private void drawCurrentPath(){
		LinkedList<Vector3> linePoints = gestureInterpreter.LinePoints;
		
		int i = 0;
        lineRenderer.SetVertexCount(linePoints.Count);
        foreach (Vector3 point in linePoints)
        {
            lineRenderer.SetPosition(i, point);
            i++;
        }
	}
	
	private void checkNewGesture(){
		float timeStamp = gestureInterpreter.LastTimeStamp;
		
		if(timeStamp != lastTimeStamp){
			lastTimeStamp = timeStamp;
			lastGesture = gestureInterpreter.LastGesture;
			
			switch(lastGesture){
			case GestureInterpreter.gesture.up:
				points[0] = new Vector3(0,2,0);
				break;
			case GestureInterpreter.gesture.down:
				points[0] = new Vector3(0,-2,0);
				break;
			case GestureInterpreter.gesture.left:
				points[0] = new Vector3(-2,0,0);
				break;
			case GestureInterpreter.gesture.right:
				points[0] = new Vector3(2,0,0);
				break;
			case GestureInterpreter.gesture.upDown:
				break;
			case GestureInterpreter.gesture.upLeft:
				break;
			case GestureInterpreter.gesture.upRight:
				break;
			case GestureInterpreter.gesture.downUp:
				break;
			case GestureInterpreter.gesture.downLeft:
				break;
			case GestureInterpreter.gesture.downRight:
				break;
			case GestureInterpreter.gesture.leftUp:
				break;
			case GestureInterpreter.gesture.leftDown:
				break;
			case GestureInterpreter.gesture.leftRight:
				break;
			case GestureInterpreter.gesture.rightUp:
				break;
			case GestureInterpreter.gesture.rightDown:
				break;
			case GestureInterpreter.gesture.rightLeft:
				break;
			case GestureInterpreter.gesture.circleLeft:
				break;
			case GestureInterpreter.gesture.circleRight:
				break;
			}
		}
	}

	private void drawLastGesture(){
		gestureRenderer.SetVertexCount(2);
		gestureRenderer.SetPosition(0,new Vector3(0,0,0));
		//gestureRenderer.SetPosition(1,new Vector3(0,1,0));
		
		gestureRenderer.SetPosition(1,points[0]);
	}
}