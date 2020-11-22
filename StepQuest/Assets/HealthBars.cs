using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour {
    public Slider playerHealthbar;
    public Slider enemyHealthbar;
    public Text pHp;
    public Text eHp;
    public Text pName;
    public Text eName;

    private PlayerInfo player;
    public Monster enemy;
	// Use this for initialization
	void Start () {
        SetUp();
	}
	
	// Update is called once per frame
	void Update () {
        playerHealthbar.value = player.currentHealth;
        enemyHealthbar.value = enemy.health;
        UpdateText();
	}
    public void SetUp() {
        SetUpPlayerHealth();
        SetUpEnemyHealth();
    } 
    void SetUpPlayerHealth() {
        player = FindObjectOfType<PlayerInfo>();
        pName.text = player.playerName;
        playerHealthbar.maxValue = player.maxHealth;
        playerHealthbar.value = player.currentHealth;
    }
    void SetUpEnemyHealth() {
        enemy = FindObjectOfType<Combat>().monster;
        eName.text = enemy.name;
        enemyHealthbar.maxValue = enemy.health;
        enemyHealthbar.value = enemy.health;
    }
    void UpdateText() {
        pHp.text = playerHealthbar.value + " / " + playerHealthbar.maxValue;
        eHp.text = enemyHealthbar.value + " / " + enemyHealthbar.maxValue;
    }
}
