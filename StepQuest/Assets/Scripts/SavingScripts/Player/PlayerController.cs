using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    private RectTransform rT;     //get from player saved data on start
    public int xLoc;            //xLocation on the graph not gamespace
    public int yLoc;
    //private Vector2 targetDestination;  //used for calculating distance. 
    public MyTileMap map;

    public List<Node> currentPath = null;
    //public int nextCost;
    public Text distanceText;
    // Use this for initialization
	void Start () {
        rT = GetComponent<RectTransform>();

        //set default target destination .. to save in player information later
        //targetDestination = (new Vector2(xLoc, yLoc));
	}
	
	// Update is called once per frame
	void Update () {
        int distance = 0;
        if (currentPath != null) {
            int currNode = 0;
            
            while (currNode < currentPath.Count -1) {
                Vector3 start = map.TileCordToWorldCord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCordToWorldCord(currentPath[currNode+1].x, currentPath[currNode +1].y);
                Debug.DrawLine(start, end, Color.red);
                distance += currentPath[currNode].cost;
                currNode++;
                
            }

           
        }
        distanceText.text = "" + distance;

    }

    public void SetTargetDestination(Vector2 newDestination) {      //should pass in a scriptable object?? 
        //targetDestination = newDestination;
        Distance();
    }
    public float Distance() { //need to take into account all the distances of the path. not from first point to end point. 
        float distance = 0;
        
        if (PlayerPrefsManager.GetDifficulty() != 1) {  //if set on easy
            //float deltaX = targetDestination.x - xLoc;  //x2-x1
            //float deltaY = targetDestination.y - yLoc;  //y2-y1
            //distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            //distance = 
        }
        else {
            Debug.Log("Difficulty Set to Easy");
        }
        
        return distance;

    }
    public void MovePlayer() {
        //moves to last node as if it has a cost of one and counts last node as a cost of one in display but not execution. 

        if (currentPath == null)
            return;

        currentPath.RemoveAt(0); 
        xLoc = currentPath[0].x;
        yLoc = currentPath[0].y;
        rT.transform.position = map.TileCordToWorldCord(currentPath[0].x, currentPath[0].y);

        if (currentPath.Count == 1) {
            //only one left and it will be the destination. so clear the pathfinding. 
            currentPath = null;
        }
       

    }

    public Vector2 GetGraphedPosition() {
        return new Vector2(xLoc, yLoc);
    }
    public void SetGraphedPosition(int x, int y) {
        xLoc = x;
        yLoc = y;
    }

}
