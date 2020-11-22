using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenWait : MonoBehaviour {
    public float waitTime = 5;
    // Use this for initialization
    void Start () {
        if (waitTime >= 0) {
            LevelManager.FindObjectOfType<LevelManager>().Invoke("LoadNextLevel", waitTime);
        }
        else {
            Debug.LogError("waitTime cannot be negative!");
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
