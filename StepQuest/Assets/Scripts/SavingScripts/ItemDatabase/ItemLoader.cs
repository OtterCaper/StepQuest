using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLoader : MonoBehaviour {

    //public Text debugText;
    public ItemUI itemUIPrefab;
    public float offset;
    private RectTransform recTransform;
    // Use this for initialization
    void Start () {
        recTransform = GetComponent<RectTransform>();
        Vector2 transformSave = recTransform.sizeDelta;
        int count = 0;
        foreach (Weapon weapon in ItemManager.itemManager.itemDB.weaponDatabase) {
            ItemUI newItemUI = Instantiate(itemUIPrefab,transform) as ItemUI;
            Vector3 t = newItemUI.transform.position;
            newItemUI.transform.position = new Vector3(t.x, t.y - (offset * count), t.z);
            newItemUI.SetItemUI(weapon);
            count++;
            //weapon.Use();
        }
        
        foreach (Armor armor in ItemManager.itemManager.itemDB.armorDatabase) {
            ItemUI newItemUI = Instantiate(itemUIPrefab,transform) as ItemUI;
            Vector3 t = newItemUI.transform.position;
            newItemUI.transform.position = new Vector3(t.x, t.y - (offset * count), t.z);
            newItemUI.SetItemUI(armor);
            count++;
            //armor.Use();
        }
        recTransform.sizeDelta = new Vector2(transformSave.x, offset * count);
    }

    public void LoadRandomItem() {
        //MeleeWeapon newWeapon = new MeleeWeapon();
        //newWeapon.PrintItem();
        
         
    }
}
