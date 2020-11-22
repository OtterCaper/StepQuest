using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    
    public string playerName; //to be stored in player prefs 

    public int level = 1;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int strength = 0;
    public int dexterity = 0;
    public int intelligence = 0;
    public int charisma = 0; 

    public int steps = 0;
    public int gold = 0;


    public Weapon weaponEquiped;
    public Armor helmEquiped;
    public Armor bodyEquiped;
    public Armor legsEquiped;

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        Load();
        
    }

    public void Save() {
        SaveLoadManager.SavePlayer(this);
        PlayerPrefsManager.SetPlayerName(playerName);
    }

    public void Load() {
        //int[] loadedStats = SaveLoadManager.LoadPlayer();


        //if (loadedStats != null) {
        //    level = loadedStats[0];
        //    health = loadedStats[1];
        //    strength = loadedStats[2];
        //    dexterity = loadedStats[3];
        //    intelligence = loadedStats[4];
        //    charisma = loadedStats[5];
        //    steps = loadedStats[6];
        //    gold = loadedStats[7];
        //}
        PlayerData data = SaveLoadManager.LoadPlayer();
        if (data != null) {
            level = data.stats[0];
            maxHealth = data.stats[1];
            strength = data.stats[2];
            dexterity = data.stats[3];
            intelligence = data.stats[4];
            charisma = data.stats[5];
            steps = data.stats[6];
            gold = data.stats[7];
            currentHealth = data.stats[8];

            weaponEquiped = data.weapon; //need to check to see if this is not null? 
            helmEquiped = data.helm;
            bodyEquiped = data.body;
            legsEquiped = data.legs;
        }

        playerName = PlayerPrefsManager.GetPlayerName();
        //print(playerName);
    }

    void OnApplicationPause(bool pause) { //called when application is closed. 
        if (pause) {                                  //PlayerPrefsManager.SetStepCount(currentSteps);
            Save();
        }
    }

    public int GetPlayerSpeed() {
        int speed = 0;
        if (weaponEquiped != null) {
            speed += weaponEquiped.attackSpeed;
        }
        if (helmEquiped != null) {
            speed += helmEquiped.attackSpeed;
        }
        if (bodyEquiped != null){
            speed += bodyEquiped.attackSpeed;
        }
        if (legsEquiped != null) {
            speed += legsEquiped.attackSpeed;
        }
        if (speed == 0) {
            speed = 1;
        }
        return speed;

    }
    public int GetPlayerArmor() {
        int armor = 0;
        if (weaponEquiped != null) {
            armor += weaponEquiped.defence;
        }
        if (helmEquiped != null) {
            armor += helmEquiped.defence;
        }
        if (bodyEquiped != null) {
            armor += bodyEquiped.defence;
        }
        if (legsEquiped != null) {
            armor += legsEquiped.defence;
        }
        if (armor == 0) {
            armor = 1;
        }
        return armor;
    }

    public int GetPlayerWeaponSkill() {
        WeaponType attackType = weaponEquiped.weaponType;
        int skill = 1;

        if (attackType == WeaponType.Magic) {
            skill = intelligence;
        }
        else if (attackType == WeaponType.Power) {
            skill = strength;
        }
        else if (attackType == WeaponType.Skill) {
            skill = dexterity;
        }
        

        return skill;
    }




}
