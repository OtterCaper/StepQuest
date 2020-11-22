using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    public bool fadeOut = true;
    public float duration = 1;
    public float wait =1;
    private Image fader;
    private Color currentColor = Color.black;
    private bool active = false;
	// Use this for initialization
	void Start () {
        fader = GetComponent<Image>();
        if (fadeOut == true) {
            currentColor.a = 0;
            fader.color = currentColor;
            StartCoroutine(StartFade(wait));
        }
        else {
            currentColor.a = 1;
            fader.color = currentColor;
            active = true;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (active) {
            Fade();
        }
        
    }
    private void Fade() {
        float alphaChange =  Time.deltaTime / duration;        
        if (fadeOut == true) {
            currentColor.a += alphaChange;
        }
        else {
            currentColor.a -= alphaChange;
        }
        if (currentColor.a <= 0) { //disable after fade in.
            gameObject.SetActive(false);
        }
        fader.color = currentColor;
    }
    IEnumerator StartFade(float time) {
        yield return new WaitForSeconds(time);
        active = true;
        

    }
}
