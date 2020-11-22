using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    LevelManager levelManger;
    public string levelToLoad = "";
	// Use this for initialization
	void Start () {
        levelManger = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (levelManger.SceneNameIs("Options")) {
                FindObjectOfType<OptionsController>().SaveAndExit();
            }
            else if (levelToLoad != "") {
                levelManger.LoadLevel(levelToLoad);
            }
            else {
                levelManger.LoadMainMenu();
            }
            
            
        }
	}
}
