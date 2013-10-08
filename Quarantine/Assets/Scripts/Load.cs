using UnityEngine;
using System.Collections;
using System.IO;

public class Load : MonoBehaviour {
	
	string fileName = "creatureSave.txt";
	
	GameObject playerCreature;
	
	StreamReader sr = new StreamReader("creatureSave.txt");

	// Use this for initialization
	void Start () {
		playerCreature = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void load()
	{
		playerCreature.GetComponent<Creature>().name = sr.ReadLine();
		checkMod(sr.ReadLine ());
		
		playerCreature.GetComponent<Creature>().adjustLevel(int.Parse(sr.ReadLine()));
		
		if (bool.Parse (sr.ReadLine ()) == true)
			playerCreature.GetComponent<Creature>().setEnemy();
		playerCreature.GetComponent<Creature>().MAX_HEALTH = int.Parse(sr.ReadLine());
		playerCreature.GetComponent<Creature>().adjustAttackPerm(int.Parse(sr.ReadLine()));
		playerCreature.GetComponent<Creature>().adjustDefensePerm(int.Parse(sr.ReadLine()));
		playerCreature.GetComponent<Creature>().adjustSpeedPerm(int.Parse(sr.ReadLine()));
	}
	
	void checkMod(string mod)
	{
		if (mod == "BASE")
			playerCreature.GetComponent<Creature>().currentMod = Creature.Mod.BASE;
		else if (mod == "ATTACK")
			playerCreature.GetComponent<Creature>().currentMod = Creature.Mod.ATTACK;
		else if (mod == "DEFENSE")
			playerCreature.GetComponent<Creature>().currentMod = Creature.Mod.DEFENSE;
		else if (mod == "SPEED")
			playerCreature.GetComponent<Creature>().currentMod = Creature.Mod.SPEED;
		else // in case of error, set it to default
			playerCreature.GetComponent<Creature>().currentMod = Creature.Mod.BASE;
	}
}
