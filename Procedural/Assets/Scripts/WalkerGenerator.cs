using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    public enum GridElement
    {
        FLOOR,
        WALL,
        EMPTY
    }

    public Tilemap tilemap;
    public Tile floor;
    public Tile wall;
    public int mapWidth = 30;
    public int mapHeight = 30;
    public float maximumFillPercentage = 0.4f;
    public float waitTime = 0.05f;
    private WalkerObject walker;
    private GridElement[,] virtualGrid;
    private int tileCount = default;
    private int halfMapWidth;
    private int halfMapHeight;

    public void Start()
    {
        this.halfMapWidth = this.mapWidth / 2;
        this.halfMapHeight = this.mapHeight / 2;
        this.InitializeGrid();
    }

    private void InitializeGrid()
    {
        // Clear the grid
        this.virtualGrid = new GridElement[this.mapWidth, this.mapHeight];
        for (int x = 0; x < virtualGrid.GetLength(0); x++)
            for (int y = 0; y < virtualGrid.GetLength(1); y++)
                this.virtualGrid[x, y] = GridElement.EMPTY;

        // Set walker and position first tile
        Vector3Int centerTileIndex = new Vector3Int(this.virtualGrid.GetLength(0) / 2, this.virtualGrid.GetLength(1) / 2, 0);
        this.walker = new WalkerObject(new Vector2(centerTileIndex.x, centerTileIndex.y), this.GetRandomDirection(), 0.5f);
        this.virtualGrid[centerTileIndex.x, centerTileIndex.y] = GridElement.FLOOR;
        this.tilemap.SetTile(VirtualGridToTilemap(centerTileIndex), this.floor);
        this.tileCount++;

        StartCoroutine(CreateFloors());
    }

    private Vector3Int VirtualGridToTilemap(Vector3Int virtualGridTile)
    {
        return new Vector3Int(virtualGridTile.x - this.halfMapWidth, virtualGridTile.y - this.halfMapHeight, virtualGridTile.z);
    }

    private Vector2 GetRandomDirection()
    {
        int randomValue = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);
        switch (randomValue)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    private void ChanceToRedirect()
    {
        float randomValue = UnityEngine.Random.value * 3.99f;
        if (randomValue < this.walker.GetChanceToChangeDirection())
        {
            this.walker.SetDirection(this.GetRandomDirection());
        }
    }

    private IEnumerator CreateFloors()
    {
        // Create only if it is not full
        while ((float)this.tileCount / (float)this.virtualGrid.Length < this.maximumFillPercentage)
        {
            bool hasCreatedFloor = false;
            Vector2 walkerPosition = this.walker.GetPosition();
            Vector3Int currentTileIndex = new Vector3Int((int)walkerPosition.x, (int)walkerPosition.y, 0);

            if (this.virtualGrid[currentTileIndex.x, currentTileIndex.y] != GridElement.FLOOR)
            {
                this.virtualGrid[currentTileIndex.x, currentTileIndex.y] = GridElement.FLOOR;
                tileCount++;
                this.tilemap.SetTile(VirtualGridToTilemap(currentTileIndex), this.floor);
                hasCreatedFloor = true;
            }

            // Update walker
            ChanceToRedirect();
            this.walker.UpdatePosition(this.virtualGrid.GetLength(0), this.virtualGrid.GetLength(1));

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(this.waitTime);
            }
        }

        // StartCoroutine(CreateWalls());
    }

    // private IEnumeration CreateWalls()
    // {
    //     return false;
    // }

}
