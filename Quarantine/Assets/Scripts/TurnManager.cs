using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {
	
	public GameObject playerPrefab;
	
	List<Player> players = new ArrayList<Player>();
	
	int currentPlayerIndex = 0;
	
	public static TurnManager instance;
	
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		players[currentPlayerIndex].TurnUpdate();
	}
	
	// turn will update after a player does something
	public void nextTurn()
	{
		if (currentPlayerIndex + 1 < players.Count())
		{
			currentPlayerIndex++;
		}
		else
		{
			currentPlayerIndex = 0;
		}
	}
	
	// Take the player's monster and the random encounter and pit them against each other
	void generatePlayers()
	{
		UserPlayer player;
		AIPlayer enemy;
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(Screen.width*0.1, Screen.height*0.75, 0), Quaternion.Euler (new Vector3())).getComponent<UserPlayer>);
		players.add(player);
		
		enemy = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(Screen.width*0.9, Screen.height*0.75, 0), Quaternion.Euler (new Vector3())).getComponent<UserPlayer>);
		players.add(enemy);
	}
}
