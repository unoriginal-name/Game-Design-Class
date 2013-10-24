using UnityEngine;
using System.Collections;

//every type of creature -- lymphocite, virus, etc. -- will extend this and set the values there.

public class Creature : MonoBehaviour {
	
	string name = "default"; //creature's name
	
	public enum Mod { BASE, ATTACK, DEFENSE, SPEED };
	public Mod currentMod = Mod.BASE;
	
	public bool isEnemy = false;
	
	AudioSource currentSound;
	
	//Base vs. Current: base is what it is by default, current is what it is when temporary boosts are taken into account
	
	public int MAX_HEALTH = 0;
	int BASE_ATTACK = 0;
	int BASE_DEFENSE = 0;
	int BASE_SPEED = 0;
	
	int CURRENT_HEALTH = 0;
	int CURRENT_ATTACK = 0;
	int CURRENT_DEFENSE = 0;
	int CURRENT_SPEED = 0;
	
	int LEVEL = 0;
	
	//when the creature does something (attacks or defends), reference this and have it play.

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	 * List of methods for getting various stats
	 */ 
	
	public int getMaxHealth()
	{
		return MAX_HEALTH;
	}
	
	public int getBaseAttack()
	{
		return BASE_ATTACK;
	}
	
	public int getBaseDefense()
	{
		return BASE_DEFENSE;
	}
	
	public int getBaseSpeed()
	{
		return BASE_SPEED;
	}
	
	public int getCurrentAttack()
	{
		return CURRENT_ATTACK;
	}
	
	public int getCurrentDefense()
	{
		return CURRENT_DEFENSE;
	}
	
	public int getCurrentSpeed()
	{
		return CURRENT_SPEED;
	}
	
	public string getName()
	{
		return name;
	}
	
	public Mod getMod()
	{
		Mod tempMod = new Mod();
		tempMod = currentMod;
		return tempMod;
	}
	
	public bool checkAllegience()
	{
		return isEnemy;
	}
	
	public int getLevel()
	{
		return LEVEL;
	}
	
	//not entirely sure what this will return yet.
	public void getSprite()
	{
		string spriteName;
		
		spriteName = name;
		
		spriteName += getMod();
		
		if (isEnemy)
			spriteName += "Evil";
		
		//load the sprite and return that?
	}
	
	/*
	 * List of methods for adjusting stats for this battle
	 */ 
	
	public void adjustAttack(int adjustment)
	{
		CURRENT_ATTACK += adjustment;
	}
	
	public void adjustDefense(int adjustment)
	{
		CURRENT_DEFENSE += adjustment;
	}
	
	public void adjustSpeed(int adjustment)
	{
		CURRENT_SPEED += adjustment;
	}
	
	/*
	 * List of methods for adjusting base stats
	 */ 
	
	public void adjustLevel(int adjustment)
	{
		LEVEL += adjustment;
	}
	
	public void adjustAttackPerm(int adjustment)
	{
		BASE_ATTACK += adjustment;
	}
	
	public void adjustDefensePerm(int adjustment)
	{
		BASE_DEFENSE += adjustment;
	}
	
	public void adjustSpeedPerm(int adjustment)
	{
		BASE_SPEED += adjustment;
	}
	
	public void setMaxHealth(int value)
	{
		MAX_HEALTH = value;
	}
	
	
	public void setEnemy()
	{
		isEnemy = true;
	}
	
	public void setSoundEffect(string fileName)
	{
		currentSound.clip = (AudioClip)Resources.Load (fileName);
	}
	
	public void playSound()
	{
		currentSound.Play ();
	}
}
