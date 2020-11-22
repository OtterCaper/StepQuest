using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {


    public static void SavePlayer(PlayerInfo player) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);
        bf.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadPlayer() { //static int[]
        if (File.Exists(Application.persistentDataPath + "/player.sav")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data; //data.stats;
            //will need to be set to void and call multiple setters before returning. 
            //set stats.
            //set inventory
            //set equipped items
        }
        return null;   
    }
}

[Serializable]
public class PlayerData {

    public int[] stats;
    public Weapon weapon;
    public Armor helm;
    public Armor body;
    public Armor legs;
    //list of items inventory
    //list of equipped items

    public PlayerData(PlayerInfo player) {
        stats = new int[9];
        stats[0] = player.level;
        stats[1] = player.maxHealth;
        stats[2] = player.strength;
        stats[3] = player.dexterity;
        stats[4] = player.intelligence;
        stats[5] = player.charisma;
        stats[6] = player.steps;
        stats[7] = player.gold;
        stats[8] = player.currentHealth;
        //set lists 

        weapon = player.weaponEquiped;
        helm = player.helmEquiped;
        body = player.bodyEquiped;
        legs = player.legsEquiped;
    }
} 
