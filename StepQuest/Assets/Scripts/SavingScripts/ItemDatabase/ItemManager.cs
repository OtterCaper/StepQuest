using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;                   //basic xml attributes
using System.Xml.Serialization;     //access to smlserializer
using System.IO;                    //file managment
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

    public static ItemManager itemManager;
    public Sprite[] spriteArray;
    //list of items;
    public ItemDatabase itemDB;

    // Signleton pattern
    void Awake () {
        itemManager = this;
        LoadItems();
        DontDestroyOnLoad(gameObject);
	}

    

    public void SaveItems() {
        //create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/item_data.xml" , FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();
    }

    public void LoadItems() {
        TextAsset myText = Resources.Load("item_data") as TextAsset;
        if (myText != null) {
            XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
            using (var reader = new System.IO.StringReader(myText.text)) {
                itemDB = serializer.Deserialize(reader) as ItemDatabase;
            }
        }
        else {
           Debug.LogError("File does not exist");
        }
    }

}

[System.Serializable]
public class ItemDatabase {

    [XmlArray("WeaponDatabase")]
    public List<Weapon> weaponDatabase = new List<Weapon>();
    [XmlArray("ArmorDatabase")]
    public List<Armor> armorDatabase = new List<Armor>();

    //public List<Consumable> consumableDatabase = new List<Consumable>();
    //public List<QuestItem> QuestItemDatabase = new List<QuestItem>();


    public Weapon RandomWeapon() {
        Weapon toReturn = null;

        toReturn =  weaponDatabase[Random.Range(0, weaponDatabase.Count)];
        toReturn = ExtensionMethods.DeepClone<Weapon>(toReturn);
        toReturn.GenerateQuality();

        return toReturn;
      
    }

    public Weapon GetWeaponOfName(string name, int level) {
        Weapon toReturn = null;
        toReturn = weaponDatabase.Find(Weapon => Weapon.name == name);
        toReturn = ExtensionMethods.DeepClone<Weapon>(toReturn);
        toReturn.GenerateQuality(level);
        return toReturn;
    }
    public Armor GetArmorOfName(string name, int level) {
        Armor toReturn = null;
        toReturn = armorDatabase.Find(Armor => Armor.name == name);
        toReturn = ExtensionMethods.DeepClone<Armor>(toReturn);
        toReturn.GenerateQuality(level);
        return toReturn;
    }
}


//[System.Serializable]
//public class ItemEntry {
//    public string itemName;
//    //
//}