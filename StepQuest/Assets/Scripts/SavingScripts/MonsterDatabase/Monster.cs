using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Monster {

    public string name;
    public int spriteRef;
    public int baseLevel; // random encounters will be player level = baselevel + level multiplyer. 
    //public int levelMultiplyer;  //monster stats will be base stats  * level multiplyer 
    //public MonsterStrength strength; // use monster strength as a naming modifyer in acord with level multiplyer 
    //combat values
    public int health;
    public int minDamage;
    public int maxDamage;
    public int accuracy;
    public int attackSpeed;
    public int armor;
    public int difficultyToHit;

    public Monster() { } //default constructor? 
    public Monster(string name, int spriteRef, int baseLevel, int health, int minDamage, int maxDamage, int accuracy, int attackSpeed, int armor, int difficultyToHit) {
        this.name = name;
        this.spriteRef = spriteRef;
        this.baseLevel = baseLevel;
        this.health = health;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.accuracy = accuracy;
        this.attackSpeed = attackSpeed;
        this.armor = armor;
        this.difficultyToHit = difficultyToHit;
    }


    public void GenMonsterStrength(int targetStrength) { // pass in a level value for more controll. 
       
        int multiplyer = targetStrength - baseLevel;
        if (multiplyer > 0) { //zero use base stats. shouldnt be negative but if it is use base stats. 
            name = RandomEnum.IndexOf<MonsterStrength>(multiplyer).ToString() + " " + name;
            multiplyer++; // max level = 8 min level is 0 therefor a rat can have a 9x multiplyer...  
            health *= multiplyer;
            minDamage *= multiplyer;
            maxDamage *= multiplyer;
            //accuracy - constant? 
            //attackSpeed *= multiplyer;
            armor *= multiplyer;
            //difficultyToHit - constant? 
        }
    }

}

