using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour {
    public int skillCoefficent = 10;
    public string weaponName = "Long Sword";       //to be removed later
    public string helmName= "Great Helm";
    public string bodyName = "Breastplate"; 
    public string legsName = "Plate Leggings";

    public bool fight;
    
    //--Utilities--
    private ItemManager im;         //to be removed later??
    private MonsterManager mm;
    //--Combatants--
    private PlayerInfo player;
    public Monster monster;

    //--Combat progression--
    public Slider playerSlider;
    public Slider monsterSlider;
    public int playerSpeed;
    public int playerArmor;
    public int monsterSpeed;
    private int actionPoints;
    private float skill;
    
	// Use this for initialization
	void Start () {
        im = FindObjectOfType<ItemManager>();
        mm = FindObjectOfType<MonsterManager>();
        player = FindObjectOfType<PlayerInfo>();

        GetNewRandomMonster();

        UpdateCombat();
    }

    public void Update() {
        if ((player.currentHealth > 0 && monster.health > 0) && fight == true) {
            ProgressCombat();
        }
    }
    void ProgressCombat() {
        monsterSlider.value += monsterSpeed * Time.deltaTime;
        playerSlider.value += playerSpeed * Time.deltaTime;

        if (monsterSlider.value >= actionPoints) {
            monsterSlider.value = 0;
            print(monster.name + " attacks.");
            AttackPlayer();
        }
        if (playerSlider.value >= actionPoints) {
            playerSlider.value = 0;
            print(player.playerName + " attacks.");
            AttackMonster();
        }
    }

    public void UpdateCombat() {
        player.currentHealth = player.maxHealth;
        playerSpeed = player.GetPlayerSpeed();
        monsterSpeed = monster.attackSpeed;
        playerArmor = player.GetPlayerArmor();
        skill = player.GetPlayerWeaponSkill();

        actionPoints = playerSpeed;
        if (playerSpeed < monsterSpeed) {
            actionPoints = monsterSpeed;
        }
        //print(monsterSpeed);
        //print(playerSpeed);
        monsterSlider.maxValue = actionPoints;
        playerSlider.maxValue = actionPoints;

    }


    public void GetNewWeapon() {
        //im.itemDB.RandomWeapon().Print();
        player.weaponEquiped = im.itemDB.GetWeaponOfName(weaponName, player.level/skillCoefficent);
    }
    public void GetNewArmor() {
        player.helmEquiped = im.itemDB.GetArmorOfName(helmName, player.level/ skillCoefficent);
        player.bodyEquiped = im.itemDB.GetArmorOfName(bodyName, player.level/skillCoefficent);
        player.legsEquiped = im.itemDB.GetArmorOfName(legsName, player.level/skillCoefficent);
    }
    public void GetNewRandomMonster() {
        monster = mm.monsterDB.RandomMonsterAtLevel(player.level / 10);
        UpdateCombat();
    }

    public void Attack() {
        fight = !fight;
    }
    public void AttackPlayer() {
        if (RollToHit(monster.accuracy, 1, 30)) {
            int damage = RollDamage(monster.minDamage,
                      monster.maxDamage,
                      0,
                      1,
                      playerArmor);
            print(monster.name + " deals " + damage + " to " + player.playerName);
            player.currentHealth -= damage;
        }
        else {
            print(monster.name + " misses.");
        }
    }
    public void AttackMonster() {
        if (RollToHit(player.weaponEquiped.accuracy, skill, monster.difficultyToHit)) {
            int damage = RollDamage(player.weaponEquiped.minDamage,
                      player.weaponEquiped.maxDamage,
                      player.weaponEquiped.critChance,
                      skill,
                      monster.armor);
            monster.health -= damage;
            print(player.playerName + " deals " + damage + " to " + monster.name);
        }
        else {
            print(player.playerName + " misses.");
        }
    }
    public bool RollToHit(float attackerAccuracy, float skill, float difficultyClass) {
        bool result = false;
        float roll = Random.Range(0, 100);
        //print("Roll: " + roll);
        //print("attack:" + ((roll + attackerAccuracy + (skill / skillCoefficent)) + " vs " + difficultyClass));
        
        if (roll == 100) {
            result = true;
        }
        else if ((roll + attackerAccuracy + (skill/skillCoefficent)) >= difficultyClass) {
            
            result = true;
        }
        else {
            result = false;
        }
        return result;
    }

    public int RollDamage(float minDamage, float maxDamage, float critChance, float skill, float armor) {
        if (armor <= 0)
            Debug.Log("armor of value: " + armor + " is invalid");
        float roll = Random.Range(minDamage, maxDamage);
        if (Random.Range(0, 100) <= critChance || roll == maxDamage) {
            roll = maxDamage;
            print("crit!!");
        }
        
        //print("damage roll: "+ roll);
        return (int)((Mathf.Pow(roll,2) * ((skill+skillCoefficent)/skillCoefficent) / armor )+ minDamage);

    }
}
