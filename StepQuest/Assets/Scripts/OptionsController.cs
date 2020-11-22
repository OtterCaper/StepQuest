using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
    public Slider volumeSlider;
    public Slider difficultySlider;
    public LevelManager levelManager;

    public Text volumeText;
    public Text difficultyText;

    private MusicPlayer musicManager;
	// Use this for initialization
	void Start () {
        musicManager = GameObject.FindObjectOfType<MusicPlayer>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
        if (musicManager != null) {
            musicManager.SetVolume(volumeSlider.value);
        }
        volumeText.text = ((int)(volumeSlider.value * 100)).ToString() + "%";
        SetDifficultyText();
	
	}
    public void SaveAndExit() {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        levelManager.LoadMainMenu();
    }
    public void ResetToDefault() {
        volumeSlider.value = volumeSlider.maxValue;
        difficultySlider.value = (difficultySlider.minValue + difficultySlider.maxValue)/2;
    }
    private void SetDifficultyText() {
        switch ((int)difficultySlider.value) {
            case 1:
                difficultyText.text = "Easy";
                break;
            case 2:
                difficultyText.text = "Medium";
                break;
            case 3:
                difficultyText.text = "Hard";
                break;
        }

    }
}
