using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    public Text itemName;
    public Text itemDefence;
    public Text itemAttackSpeed;
    public Text itemDamage;
    public Text itemCritChance;
    public Text itemAccuracy;
    public Text itemValue;
    //public Text itemDefence;
    //public Text itemAccuracy;

    public Image image;
    private ItemManager im;
    private Item item;

    public string message; // used for testing.

	// Use this for initialization
	void Awake () {
        im = FindObjectOfType<ItemManager>();
        
        //text = GetComponent<Text>();
        //text = GetComponentInChildren<Text>();
        //image.sprite = im.spriteArray[1];
	}

    public void SetItemUI(Item newItem) {
        item = newItem;
        itemName.text = item.name;
        itemValue.text = item.value.ToString();
        if (item.spriteRef < im.spriteArray.Length && item.spriteRef > 0)
        {
            image.sprite = im.spriteArray[item.spriteRef];
        }
        else {
            image.sprite = im.spriteArray[0];
        }

        newItem.Print();
        if (item.GetType().Name == "Weapon") {
            WeaponUI(); 
        }
        //if type armor
        //if type consumable
        //if type quest item
        
    }
    public void Print() {
        Debug.Log(item.GetType().Name);
        item.Print();
    }

    public void WeaponUI() {
        Weapon theWeapon = (Weapon)item;
        //should not generate name and material here 

        theWeapon.GenerateQuality();
        itemName.text = theWeapon.name;
        itemDefence.text =      "Defence: "         + theWeapon.defence;
        itemAttackSpeed.text =  "AttackSpeed: "     + theWeapon.attackSpeed;
        itemDamage.text =       "Damage: "          + theWeapon.minDamage + " - " + theWeapon.maxDamage;
        itemCritChance.text =   "Critical Chance: " + theWeapon.critChance + "%";
        itemAccuracy.text =     "Accuracy: "        + theWeapon.accuracy;
    }

}
