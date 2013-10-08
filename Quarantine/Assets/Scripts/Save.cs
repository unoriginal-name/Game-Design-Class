using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Save : MonoBehaviour {
	
	string fileName = "creatureSave.txt";
	
	GameObject playerCreature;
	
	int i = 0; //variable used for testing purposes

	// Use this for initialization
	void Start () {
		playerCreature = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		if (i == 10)
			save ();
	}
	
	void save()
	{
		var sr = File.CreateText (fileName);
		
		sr.WriteLine(playerCreature.GetComponent<Creature>().getName());
		sr.WriteLine(playerCreature.GetComponent<Creature>().getMod());
		sr.WriteLine(playerCreature.GetComponent<Creature>().getLevel());
		sr.WriteLine(playerCreature.GetComponent<Creature>().checkAllegience());
		sr.WriteLine(playerCreature.GetComponent<Creature>().getMaxHealth());
		sr.WriteLine(playerCreature.GetComponent<Creature>().getBaseAttack());
		sr.WriteLine(playerCreature.GetComponent<Creature>().getBaseDefense());
		sr.WriteLine(playerCreature.GetComponent<Creature>().getBaseSpeed());

		sr.Close();	
	}
}
