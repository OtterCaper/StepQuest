using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyTileMap : MonoBehaviour {

    public GameObject MapParent;
    public int mapSizeX = 25;
    public int mapSizeY= 25;
    public TileType[] tileTypes;
    private PlayerController player;

    Node[,] graph;                      //what each tile is touching
    List<Node> currentPath = null;

    public SelectableTile[,] tileMap;  //UI location of each tile
    private int[,] tiles;               //used for the type of each tile

    void Start() {
        player = FindObjectOfType<PlayerController>();
        player.map = this;

        GenerateMapData();
        GeneratePathFindingGraph();
        GenerateMapVisual();
        player.SetGraphedPosition(mapSizeX / 2, mapSizeY / 2); 
    }
    void GenerateMapData() {

        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                tiles[x, y] = 0;
            }
        }
        tiles[5, 5] = 1;
        tiles[6, 5] = 1;
        tiles[7, 5] = 1;
        tiles[8, 5] = 1;
        tiles[5, 6] = 1;
        tiles[8, 6] = 1;


        tiles[3, 0] = 2;
        tiles[3, 1] = 2;
        tiles[3, 2] = 2;
        tiles[3, 3] = 2;
        tiles[3, 4] = 2;
        tiles[4, 0] = 2;
        tiles[4, 1] = 2;
        tiles[4, 2] = 2;
        tiles[4, 3] = 2;
        tiles[4, 4] = 2;
        tiles[5, 0] = 2;
        tiles[5, 1] = 2;
        tiles[5, 2] = 2;
        tiles[5, 3] = 2;
        tiles[5, 4] = 2;
        tiles[6, 0] = 2;
        tiles[6, 1] = 2;
        tiles[6, 2] = 2;
        tiles[6, 3] = 2;
        tiles[6, 4] = 2;
        tiles[7, 0] = 2;
        tiles[7, 1] = 2;
        tiles[7, 2] = 2;
        tiles[7, 3] = 2;
        tiles[7, 4] = 2;
    }
    private float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY) {
        float cost;
        TileType tt = tileTypes[tiles[targetX, targetY]];   
        if (tt.isWalkable == false) {
            cost = Mathf.Infinity;
        }
        else {
            cost = tt.speedCost;
            if (sourceX != targetX && sourceY != targetY) {//break ties for diaganol movement. 
                cost += 0.001f;
            }
        }
        return cost;
    }


    void GeneratePathFindingGraph() {
        //initalize the array
        graph = new Node[mapSizeX, mapSizeY];

        //initalize a node for each spot in array
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                graph[x, y] = new Node();

                graph[x, y].x = x;
                graph[x, y].y = y;
                graph[x, y].cost = tileTypes[tiles[x, y]].speedCost;
            }
        }
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {

                if (x > 0) {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);        //left
                    if(y>0)
                        graph[x, y].neighbours.Add(graph[x - 1, y-1]);        //left down
                    if (y < mapSizeY-1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);        //left up
                }
                    
                if (x < mapSizeX - 1) { 
                    graph[x, y].neighbours.Add(graph[x+1, y]);          //right
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);        //right down
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);        //right up

                }
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);        //down
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y+1]);          //up


            }
        }
    }


    void GenerateMapVisual() {
        tileMap = new SelectableTile[mapSizeX, mapSizeY];

        RectTransform rt = GetComponent<RectTransform>();
        
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = Instantiate(tt.tileVisualPrefab, new Vector2(x*100,y*100),Quaternion.identity) as GameObject;//, this.transform, false) as GameObject;
                go.transform.SetParent(MapParent.transform, false);

                SelectableTile st = go.GetComponent<SelectableTile>();
                st.tileMap = this;
                st.xLoc = x;
                st.yLoc = y;

                tileMap[x, y] = st;

                if (x + 1 == mapSizeX) {
                    rt.sizeDelta = new Vector2(x*100+ 100, y*100 +100);
                }
            }
        }
        
    }
    public void GeneratePathTo(int x, int y) {

        player.currentPath = null;
        player.SetTargetDestination(new Vector2(x, y));

        currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();//nodes havent checked yet.

        Node source = graph[player.xLoc, player.yLoc];
        Node target = graph[x,y];

        dist[source] = 0;       //how far from current location
        prev[source] = null;    //what was the previous node

        //inialize everthing to have infinity distance because we dont have any better solution. 
        //for those you cant reach it will be infinity
        foreach (Node v in graph) {
            if (v != source) {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);   
        }

        while (unvisited.Count > 0) {
            //u is the unvisted node with smallest distance. 
            Node u = null;

            foreach (Node possibleU in unvisited) {
                if (u == null || (dist[possibleU] < dist[u]) ) {
                    u = possibleU;
                }
            }

            if (u == target) {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours) {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x,u.y, v.x, v.y);
                if (alt < dist[v]) {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        //either we found shortest route or there is no target. 

        if (prev[target] == null) {
            //no route to target
            Debug.Log("no route to target found");
            return;
        }
        currentPath = new List<Node>();
        Node curr = target;

        while (curr != null) {
            currentPath.Add(curr);
            curr = prev[curr];
        }
        //now inverse path.
        
        currentPath.Reverse();

        player.currentPath = currentPath;
    }
    public Vector3 TileCordToWorldCord(int x, int y) {
        return tileMap[x, y].GetTileLocation();
    }
    
    
}


