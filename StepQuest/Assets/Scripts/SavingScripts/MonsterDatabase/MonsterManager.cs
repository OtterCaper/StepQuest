using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class MonsterManager : MonoBehaviour {

    public static MonsterManager monsterManager;
    public Sprite[] spriteArray;
    public MonsterDatabase monsterDB;
	// Use this for initialization
	void Awake () {
        monsterManager = this;
        LoadMonsters();
        DontDestroyOnLoad(gameObject);
	}
	


    public void SaveMonsters() {
        //create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(MonsterDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/monster_data.xml", FileMode.Create);
        serializer.Serialize(stream, monsterDB);
        stream.Close();
    }

    public void LoadMonsters() {
        TextAsset myText = Resources.Load("monster_data") as TextAsset;
        if (myText != null) {
           
            XmlSerializer serializer = new XmlSerializer(typeof(MonsterDatabase));
            using (var reader = new System.IO.StringReader(myText.text)) {
                monsterDB = serializer.Deserialize(reader) as MonsterDatabase;
            }
        }
        else {
            Debug.LogError("File does not exist");
        }
    }
}

[System.Serializable]
public class MonsterDatabase {
    [XmlArray("MonsterList")]
    public List<Monster> monsterDatabase = new List<Monster>();



    public Monster RandomMonsterAtLevel(int level) { 
        Monster toReturn = null;
        if (level > 7) {
            level = 7;
        }
        List<Monster> tempDatabase = new List<Monster>();
        foreach (Monster monster in monsterDatabase) { //to heavy??
            if (monster.baseLevel <= level) {
                tempDatabase.Add(monster);
            }
        }
        toReturn = tempDatabase[Random.Range(0, tempDatabase.Count)];
        toReturn = ExtensionMethods.DeepClone<Monster>(toReturn);
        toReturn.GenMonsterStrength(level);

        return toReturn; 
    }

}