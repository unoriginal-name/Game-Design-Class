using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Gestures : MonoBehaviour
{
    #region "Constants"
    private const double TOLERANCE_MODIFIER = 8;
    private const double UPDATE_MODIFIER = 4;

    private const float TAP_TIME = .100f;

    public enum direction
    {
        none,
        up,
        down,
        left,
        right
    };

    public enum gesture
    {
        tap = 0,
        up,
        down,
        left,
        right,
        upDown,
        upLeft,
        upRight,
        downUp,
        downLeft,
        downRight,
        leftUp,
        leftDown,
        leftRight,
        rightUp,
        rightDown,
        rightLeft,
        circleLeft,
        circleRight,
        twoFinger,
        other,
    }
    #endregion

    #region "Attributes"
    private float lastTimeStamp;
    public float LastTimeStamp
    {
        get
        {
            return lastTimeStamp;
        }
        set { }
    }

    private gesture lastGesture;
    public gesture LastGesture
    {
        get
        {
            return lastGesture;
        }
        set { }
    }

    private LinkedList<direction> lastPath;
    public LinkedList<direction> LastPath
    {
        get
        {
            return lastPath;
        }
        set { }
    }

    public LineRenderer lineRenderer;
    public LinkedList<Vector3> linePoints = new LinkedList<Vector3>();

    private LinkedList<direction> path;
    private LinkedList<Vector3> pathPosition;

    private bool checkForRelease = false;
    private bool twoFingerInput = false;

    private float timer = 0;
    #endregion

    #region "Start and Update"
    void Start()
    {
        /*lineRenderer = (LineRenderer)gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Default;
        lineRenderer.material.color = Color.black;
        lineRenderer.SetWidth(.1f, .1f);*/
    }

    void Update()
    {
        if (checkForRelease)
        {
            checkRelease();
        }
        else
        {
            checkButtons();
        }
    }

    private void checkRelease()
    {
        updateLine();

        if (twoFingerInput)
        {
            if (Input.GetMouseButtonUp(1))
            {
                checkForRelease = false;
                twoFingerInput = false;

                lastTimeStamp = Time.time;
                lastPath = null;
                lastGesture = gesture.other;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                checkForRelease = false;

                addGesture(Input.mousePosition.x, Input.mousePosition.y);
                setGesture();

            }
            else if (checkUpdateDistanceReached(Input.mousePosition))
            {
                addGesture(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
    }

    private void checkButtons()
    {
        linePoints = new LinkedList<Vector3>();
        updateLine();

        checkForRelease = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
        twoFingerInput = Input.GetMouseButtonDown(1);

        if (checkForRelease)
        {
            timer = Time.time;

            path = new LinkedList<direction>();
            pathPosition = new LinkedList<Vector3>();

            path.AddLast(direction.none);
            pathPosition.AddLast(Input.mousePosition);
        }
    }

    public void addGesture(float mouseX, float mouseY)
    {
        bool xChange = false;
        bool yChange = false;

        if (Mathf.Abs(mouseX - pathPosition.Last.Value.x) > getTolerance())
        {
            xChange = true;
        }

        if (Mathf.Abs(mouseY - pathPosition.Last.Value.y) > getTolerance())
        {
            yChange = true;
        }

        if (yChange)
        {
            if (mouseY > pathPosition.Last.Value.y)
            {
                path.AddLast(direction.up);
            }
            else
            {
                path.AddLast(direction.down);
            }
        }
        else if (xChange)
        {
            if (mouseX > pathPosition.Last.Value.x)
            {
                path.AddLast(direction.right);
            }
            else
            {
                path.AddLast(direction.left);
            }
        }

        pathPosition.AddLast(new Vector3(mouseX, mouseY, 0));
    }

    private void updateLine()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = .5f; //Camera.main.nearClipPlane;
        linePoints.AddLast(Camera.main.ScreenToWorldPoint(mousePosition));

        int i = 0;
        lineRenderer.SetVertexCount(linePoints.Count);
        foreach (Vector3 point in linePoints)
        {
            lineRenderer.SetPosition(i, point);
            i++;
        }
    }
    #endregion

    #region "Utility Methods"
    private double getTolerance()
    {
        return (Screen.width > Screen.height) ? Screen.height / TOLERANCE_MODIFIER : Screen.width / TOLERANCE_MODIFIER;
    }

    private bool checkUpdateDistanceReached(Vector3 mousePosition)
    {
        return Mathf.Sqrt(Mathf.Pow(pathPosition.Last.Value.x - mousePosition.x, 2) + Mathf.Pow(pathPosition.Last.Value.y - Input.mousePosition.y, 2)) > getUpdateDistance();
    }

    private double getUpdateDistance()
    {
        return getTolerance();
        //return (Screen.width > Screen.height)? Screen.height/UPDATE_MODIFIER : Screen.width/UPDATE_MODIFIER;	
    }

    private LinkedList<direction> removeDuplicates(LinkedList<direction> path1)
    {
        LinkedList<direction> returnPath = new LinkedList<direction>();
        direction last = direction.none;
        foreach (Gestures.direction direction1 in path1)
        {
            print(last + " " + direction1);
            if (direction1 != last)
            {
                returnPath.AddLast(direction1);
            }

            last = direction1;
        }

        return returnPath;
    }

    private void setGesture()
    {
        lastTimeStamp = Time.time;
        lastPath = removeDuplicates(path);

        if (Time.time - timer < TAP_TIME)
        {
            lastGesture = gesture.tap;
        }
        else if (lastPath.Count == 1)
        {
            switch (lastPath.First.Value)
            {
                case direction.up:
                    lastGesture = gesture.up;
                    break;
                case direction.down:
                    lastGesture = gesture.down;
                    break;
                case direction.left:
                    lastGesture = gesture.left;
                    break;
                case direction.right:
                    lastGesture = gesture.right;
                    break;
            }
        }
        else if (lastPath.Count == 2)
        {
            switch (lastPath.First.Value)
            {
                case direction.up:
                    switch (lastPath.First.Next.Value)
                    {
                        case direction.down:
                            lastGesture = gesture.upDown;
                            break;
                        case direction.left:
                            lastGesture = gesture.upLeft;
                            break;
                        case direction.right:
                            lastGesture = gesture.upRight;
                            break;
                    }
                    break;
                case direction.down:
                    switch (lastPath.First.Next.Value)
                    {
                        case direction.up:
                            lastGesture = gesture.downUp;
                            break;
                        case direction.left:
                            lastGesture = gesture.downLeft;
                            break;
                        case direction.right:
                            lastGesture = gesture.downRight;
                            break;
                    }
                    break;
                case direction.left:
                    switch (lastPath.First.Next.Value)
                    {
                        case direction.up:
                            lastGesture = gesture.leftUp;
                            break;
                        case direction.down:
                            lastGesture = gesture.leftDown;
                            break;
                        case direction.right:
                            lastGesture = gesture.leftRight;
                            break;
                    }
                    break;
                case direction.right:
                    switch (lastPath.First.Next.Value)
                    {
                        case direction.up:
                            lastGesture = gesture.rightUp;
                            break;
                        case direction.down:
                            lastGesture = gesture.rightDown;
                            break;
                        case direction.left:
                            lastGesture = gesture.rightLeft;
                            break;
                    }
                    break;
            }
        }
        else
        {
            lastGesture = gesture.other;

            if (lastPath.Count == 4 || lastPath.Count == 5)
            {
                direction[] pathArray = new direction[5];
                LinkedListNode<direction> next = lastPath.First;
                for (int i = 0; i < 5; i++)
                {

                    if (next != null)
                    {
                        pathArray[i] = next.Value;
                        next = next.Next;
                    }
                    else
                    {
                        print("???");
                        pathArray[i] = direction.none;
                        break;
                    }
                }

                if (pathArray[0] == direction.left
                    && pathArray[1] == direction.down
                    && pathArray[2] == direction.right
                    && pathArray[3] == direction.up
                    && (pathArray[4] == direction.left || pathArray[4] == direction.none))
                {
                    lastGesture = gesture.circleLeft;
                }
                else if (pathArray[0] == direction.right
                   && pathArray[1] == direction.down
                   && pathArray[2] == direction.left
                   && pathArray[3] == direction.up
                   && (pathArray[4] == direction.right || pathArray[4] == direction.none))
                {
                    lastGesture = gesture.circleRight;
                }
            }
        }
    }
    #endregion

    public gesture getMove()
    {
        return lastGesture;
    }
}