using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartVolume : MonoBehaviour {
    private MusicPlayer music;
	// Use this for initialization
	void Start () {
        music = GameObject.FindObjectOfType<MusicPlayer>();
        if (music != null) {
            //print(PlayerPrefsManager.HasMasterVolume());
            if (PlayerPrefsManager.HasMasterVolume()) {
                music.SetVolume(PlayerPrefsManager.GetMasterVolume());
            }
            else {
                PlayerPrefsManager.SetMasterVolume(0.5f);
                music.SetVolume(PlayerPrefsManager.GetMasterVolume());
                print(PlayerPrefsManager.GetMasterVolume());
            }
        }      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
