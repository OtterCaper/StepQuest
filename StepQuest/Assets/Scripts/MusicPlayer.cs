using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour  {

    private static MusicPlayer instance = null;
    public AudioClip[] audioArray;

    private AudioSource music;

    // Use this for initialization
    void Awake () {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
        }
    }
    private void Start() {
        music.volume = PlayerPrefsManager.GetMasterVolume();
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (music) {
            if (SceneManager.GetActiveScene().buildIndex < audioArray.Length) {
                AudioClip newMusic = audioArray[SceneManager.GetActiveScene().buildIndex];

                if (newMusic != null && newMusic != music.clip) {
                    music.Stop();
                    music.clip = newMusic;
                    //music.volume = PlayerPrefsManager.GetMasterVolume();
                    music.loop = true;
                    music.Play();
                }
            }
        } 
    }
    public void SetVolume(float newVolume) {
        music.volume = newVolume;
    }
    
}
