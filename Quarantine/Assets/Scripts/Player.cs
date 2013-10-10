using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public const double MAX_HEALTH = 100;
    private double current_health = MAX_HEALTH;

    // these are hard coded stats for a while
    // will need to load these from a file
    // I think Pat is taking care of this
    public const double attack = 200;
    public const double defense = 200;
    public const double speed = 200;

    public Rect location;

    public string player_name;

    public string message = "Player 1";

    // Use this for initialization
    void Start()
    {
        player_name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [RPC]
    public void setText(string new_text)
    {
        message = new_text;
    }

    void OnGUI()
    {
        GUI.Label(location, message);
    }
}
