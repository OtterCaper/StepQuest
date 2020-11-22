using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {
    private string loseScene = "Lose";
    private string winScene = "Win";
    private string mainMenu = "MainMenu";

    void Start() {

    }
    private void Update() {
     
    }

    public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
    }
    public void LoadLose() {
        SceneManager.LoadScene(loseScene);
    }
    public void LoadWin() {
        SceneManager.LoadScene(winScene);
    }
    public void LoadMainMenu() {
        if (SceneManager.GetActiveScene().name != mainMenu) {
            SceneManager.LoadScene(mainMenu);
        }
    }
    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//LoadLevel(Application.loadedLevel+1);
    }
    public void QuitRequest(){
        Application.Quit();
    }
    public bool SceneNameIs(string name) {
        return name == SceneManager.GetActiveScene().name; 
    }
    
}
