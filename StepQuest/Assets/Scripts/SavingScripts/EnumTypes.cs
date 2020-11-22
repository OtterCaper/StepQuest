using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ArmorSlotLocation {
    Helm,
    Body,
    Legs
}
public enum ArmorType {
    Light,
    Medium,
    Heavy
}

public enum WeaponType {
    Power,
    Skill,
    Magic
}

public enum Material {
    Metal,
    Wood,
    Cloth,
    Leather,
    Gem,
}
public enum MetalMaterial {
    Rusty,
    Bronze,
    Iron,
    Steel,
    Silver,
    Mithril,
    Adamantium,
    Orichalcum,
}
public enum WoodMaterial {
    Twig,
    Oak,
    Ash,
    Willow,
    Elm,
    Yew,
    Ironwood,
    Ebony,
}
public enum LightMaterial {
    Rags,
    Cotton,
    Fur,
    Wool,
    Linen,
    Silk,
    Elven,
    LunarWeave,
}
public enum LeatherMaterial {
    Scraps,
    Hide,
    Leather,
    Studded,
    Scale,
    Chain,
    RedChain,
    Dragonscale
}
public enum GemMaterial {
    Stone,
    Jade,
    Sapphire,
    Emerald,
    Ruby,
    Diamond,
    Dragonstone,
    Soulstone
}

public enum MonsterStrength {
    BASIC,
    Tough,
    Large,
    Enraged,
    Veteran,
    Ripped,
    Heroic,
    Legendary,  
}

public static class RandomEnum {
    public static T Of<T>() {
        if (!typeof(T).IsEnum) {
            throw new InvalidOperationException("Must use Enum type");
        }
        Array enumValues = Enum.GetValues(typeof(T));
        int RandomValue = UnityEngine.Random.Range(0, enumValues.Length);
        return (T)enumValues.GetValue(RandomValue);
    }

   
    public static T IndexOf<T>(int index) {
        if (!typeof(T).IsEnum) {
            throw new InvalidOperationException("Must use Enum type");
        }
        Array enumValues = Enum.GetValues(typeof(T));
        return (T)enumValues.GetValue(index);
    }
}
    


