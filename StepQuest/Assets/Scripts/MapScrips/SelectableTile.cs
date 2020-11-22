using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableTile : MonoBehaviour {

    public MyTileMap tileMap;
    //private PlayerController player;
    public int xLoc;
    public int yLoc;

    private void Start() {
        //player = FindObjectOfType<PlayerController>();
    }

    public void SelectedTarget() {

        //RectTransform rt = GetComponent<RectTransform>();
        //float x = rt.transform.position.x;
        //float y = rt.transform.position.y;
        //player.MovePlayer(x, y);
        tileMap.GeneratePathTo(xLoc, yLoc);
    }
    public Vector2 GetTileLocation() {
        RectTransform rt = GetComponent<RectTransform>();
        float x = rt.transform.position.x;
        float y = rt.transform.position.y;
        //player.MovePlayer(x, y);
        return new Vector2(x,y);
    }

}
