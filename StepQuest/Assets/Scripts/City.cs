using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    RectTransform location;
    PlayerController thePlayer;
	// Use this for initialization
	void Start () {
        location = GetComponent<RectTransform>();
        thePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetPlayerDestination() {
        thePlayer.SetTargetDestination(location.localPosition);
        //Debug.Log(location.localPosition);
    }
}
