using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";

    const string PLAYER_NAME = "player_name";
    //level_unlocked_#


        /// <summary>
        /// Volume managment
        /// </summary>
        /// <param name="volume"></param>
    public static void SetMasterVolume(float volume) {
        if (volume >= 0f && volume <= 1f) {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else {
            Debug.LogError("Error - master volume out of range");
        }
    }

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
    }

    public static bool HasMasterVolume() {
        return PlayerPrefs.HasKey(MASTER_VOLUME_KEY);
    }


    /// <summary>
    /// Level manamgent
    /// </summary>
    /// <param name="level"></param>
    public static void UnlockLevel(int level) {
        if (level <= SceneManager.sceneCountInBuildSettings - 1) {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // use 1 for true
        }
        else {
            Debug.LogError("Error - trying to unlock level: " + level + " that doesnt exist in build order");
        }
    }

    public static bool IsLevelUnlcoked(int level) {
        bool result = false;
        if (level <= SceneManager.sceneCountInBuildSettings - 1) {
            if (PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) == 1) {
                result = true;
            }
            else {
                Debug.LogError("level: " + level + " is not unlocked ");
            }
        }
        else {
            Debug.LogError("Error - " + level + "is not in build order");
        }
        return result;
    }

    public static void SetDifficulty(float difficulty) {
        if (difficulty >= 1 && difficulty <=3) {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else {
            Debug.LogError("Error - difficulty: " + difficulty + "was not in a valid range");
        }
       
    }
    public static float GetDifficulty() {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }


    public static string GetPlayerName() {
        return PlayerPrefs.GetString(PLAYER_NAME, "Hero");
    }
    public static void SetPlayerName(string newName) {
        PlayerPrefs.SetString(PLAYER_NAME, newName);
    }

}
