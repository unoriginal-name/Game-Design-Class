using UnityEngine;
using System.Collections;

//every type of creature -- lymphocite, virus, etc. -- will extend this and set the values there.

public class Creature : MonoBehaviour {
	
	string name; //creature's name
	
	enum Mod { BASE, ATTACK, DEFENSE, SPEED };
	Mod currentMod;
	
	bool isEnemy = false;
	
	//Base vs. Current: base is what it is by default, current is what it is when temporary boosts are taken into account
	
	int MAX_HEALTH;
	int BASE_ATTACK;
	int BASE_DEFENSE;
	int BASE_SPEED;
	
	int CURRENT_HEALTH;
	int CURRENT_ATTACK;
	int CURRENT_DEFENSE;
	int CURRENT_SPEED;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public int getMaxHealth()
	{
		return MAX_HEALTH;
	}
	
	int getBaseAttack()
	{
		return BASE_ATTACK;
	}
	
	int getBaseDefense()
	{
		return BASE_DEFENSE;
	}
	
	int getBaseSpeed()
	{
		return BASE_SPEED;
	}
	
	
	
	int getCurrentAttack()
	{
		return CURRENT_ATTACK;
	}
	
	int getCurrentDefense()
	{
		return CURRENT_DEFENSE;
	}
	
	int getCurrentSpeed()
	{
		return CURRENT_SPEED;
	}
	
	void adjustAttack(int adjustment)
	{
		CURRENT_ATTACK += adjustment;
	}
	
	void adjustDefense(int adjustment)
	{
		CURRENT_DEFENSE += adjustment;
	}
	
	void adjustSpeed(int adjustment)
	{
		CURRENT_SPEED += adjustment;
	}
	
	
	string getName()
	{
		return name;
	}
	
	
	Mod getMod()
	{
		Mod tempMod = new Mod();
		tempMod = currentMod;
		return tempMod;
	}
	
	
	void setEnemy()
	{
		isEnemy = true;
	}
	
	bool checkAllegience()
	{
		return isEnemy;
	}
	
	//not entirely sure what this will return yet.
	void getSprite()
	{
		string spriteName;
		
		spriteName = name;
		
		spriteName += getMod();
		
		if (isEnemy)
			spriteName += "Evil";
		
		//load the sprite and return that?
	}
}
