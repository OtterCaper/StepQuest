using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]//does each class need this?? 
public abstract class Item {
    public string name;
    public int value;
    public int spriteRef;
    public Item() { }
    
    public Item(string name, int value, int spriteRef) {
        this.name = name;
        this.value = value;
    }
    public virtual void Print() {
        Debug.Log(""); 
        Debug.Log("this is a Item name " + name);
    }
    public virtual void Use() {
        Debug.Log("using Item name " + name);
    }
   
}

[System.Serializable]
public abstract class Equipment : Item {
    public int defence;
    public Material material;
    public int attackSpeed;
    public int levelMultiplyer; // to use when creating item the enum value
    public Equipment() { }
    public Equipment(string name, int value, int spriteRef, Material material, int defence, int attackSpeed) :base(name, value, spriteRef){
        this.material = material;
        this.defence = defence;
        this.attackSpeed = attackSpeed;
    }
    public override void Print() {
        Debug.Log("this is a Item name " + name);
    }
    public override void Use() {
        Debug.Log("using equpement name " + name);
    }
    public virtual void GenerateQuality() { //if value <0 do random value. testing random material generation.

        if (material == Material.Metal) {
            MetalMaterial randomMaterial = RandomEnum.Of<MetalMaterial>();
            
            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Wood) {
            WoodMaterial randomMaterial = RandomEnum.Of<WoodMaterial>();

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Cloth) {
            LightMaterial randomMaterial = RandomEnum.Of<LightMaterial>();

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Leather) {
            LeatherMaterial randomMaterial = RandomEnum.Of<LeatherMaterial>();

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Gem) {
            GemMaterial randomMaterial = RandomEnum.Of<GemMaterial>();

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        

        //change values based on material quality;
        defence *= levelMultiplyer;
        //attackSpeed *= levelMultiplyer;??
    }
    public virtual void GenerateQuality(int value) {
        if (value > 7) {
            value = 7;
        }
        if (material == Material.Metal) {
            MetalMaterial randomMaterial = RandomEnum.IndexOf<MetalMaterial>(value);

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Wood) {
            WoodMaterial randomMaterial = RandomEnum.IndexOf<WoodMaterial>(value);

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Cloth) {
            LightMaterial randomMaterial = RandomEnum.IndexOf<LightMaterial>(value);

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Leather) {
            LeatherMaterial randomMaterial = RandomEnum.IndexOf<LeatherMaterial>(value);

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }
        else if (material == Material.Gem) {
            GemMaterial randomMaterial = RandomEnum.IndexOf<GemMaterial>(value);

            name = randomMaterial.ToString() + " " + name;
            levelMultiplyer = (int)randomMaterial + 1;//starts at zero
        }


        //change values based on material quality;
        defence *= levelMultiplyer;
    }
}
[System.Serializable]
public class Weapon : Equipment {
    //String name;
    public int minDamage;
    public int maxDamage;
    public int critChance;
    public int accuracy;
    public WeaponType weaponType;
    public Weapon() { }
  
    public Weapon(string name, int value, int spriteRef, Material material, int defence, int minDamage, int maxDamage, 
        int critChance, WeaponType weaponType, int attackSpeed, int accuracy): base(name, value, spriteRef, material, defence, attackSpeed) {
        
        //this.name = name;
        //this.value = value;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.critChance = critChance;
        this.weaponType = weaponType;
        this.accuracy = accuracy;
    }
    public override void Print() {
        Debug.Log("this is a weapon name " + name);

    }
    public override void Use() {
        Debug.Log("using equpement name " + name);
        Attack();
    }
    public void Attack() {
        Debug.Log("Attacking: " + minDamage + " minDamage done...");
    }
    public override void GenerateQuality() {
        base.GenerateQuality();
        minDamage *= levelMultiplyer;
        maxDamage *= levelMultiplyer;
        //critChance *= levelMultiplyer; ???
        //accuracy *= levelMultiplyer; ???
    }
    public override void GenerateQuality(int value) {
        base.GenerateQuality(value);
        minDamage *= levelMultiplyer;
        maxDamage *= levelMultiplyer;
    }


}
[System.Serializable]
public class Armor : Equipment {
    //String name;
    public ArmorType armorType;
    public ArmorSlotLocation slot;
    public Armor() { }
    public Armor(string name, int value, int spriteRef, Material material, int defence, int attackSpeed, ArmorType armorType, ArmorSlotLocation slot): base(name, value, spriteRef, material, defence, attackSpeed) {
        this.name = name;
        this.armorType = armorType;
        this.slot = slot;
    }
    public override void Print() {
        Debug.Log("this is a armor name " + name);
    }
    public override void Use() {
        Debug.Log("using armor name " + name);
        Defend();
    }
    public void Defend() {
        Debug.Log("defending: " + defence + " damage blocked");
    }

}

public static class ExtensionMethods {
    public static T DeepClone<T>(T objectToClone) {
        using (var memoryStream = new MemoryStream()) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, objectToClone);
            memoryStream.Position = 0;

            return (T)formatter.Deserialize(memoryStream);
        }
    }
}




