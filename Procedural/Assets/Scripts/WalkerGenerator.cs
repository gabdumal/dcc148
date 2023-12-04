using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    public Grid[,] gridHandler;
    public List<WalkerObject> walkers;
    public Tilemap tilemap;
    public Tile floor;
    public Tile wall;
    public int mapWidth = 30;
    public int mapHeight = 30;
    public int maximumWalkers = 30;
    public int tileCount = default;
    public float fillPercentage = 0.4f;
    public float waitTime = 0.05f;

    public void Start()
    {
        // InitializeGrid();
    }
}
