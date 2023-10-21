using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum MovementType
{
    NonContinuous,
    Continuous
}

public class KnightController : MonoBehaviour
{
    public MovementType movementType;
    public float xSpeed;
    public float ySpeed;
    public int movingAnimationTime;
    private int movingRemainingTime = 0;
    public Grid grid;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap buildingsTilemap;


    private void SetPlayerPosition(Vector3 cellPosition)
    {
        this.transform.position = new Vector3(
            cellPosition.x + grid.cellSize.x / 2,
            cellPosition.y + grid.cellSize.y / 2,
            cellPosition.z);
    }

    private void NonContinuousMovement()
    {
        bool moveToRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        bool moveToLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        bool moveToUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        bool moveToDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        int horizontalDisplacement = moveToRight ? 1 : moveToLeft ? -1 : 0;
        int verticalDisplacement = moveToUp ? 1 : moveToDown ? -1 : 0;
        Vector3 currentCellPosition = grid.WorldToCell(this.transform.position);
        Vector3 nextCellPosition = new Vector3(
            currentCellPosition.x + grid.cellSize.x * horizontalDisplacement,
            currentCellPosition.y + grid.cellSize.y * verticalDisplacement,
            currentCellPosition.z);

        if (nextCellPosition.x >= this.groundTilemap.cellBounds.xMin
            && nextCellPosition.x < this.groundTilemap.cellBounds.xMax
            && nextCellPosition.y > this.groundTilemap.cellBounds.yMin
            && nextCellPosition.y < this.groundTilemap.cellBounds.yMax
            )
            this.SetPlayerPosition(nextCellPosition);
    }

    private void ContinuousMovement()
    {
        if (this.movingRemainingTime > 0)
            this.movingRemainingTime--;

        float inputXOffset = Input.GetAxis("Horizontal");
        float inputYOffset = Input.GetAxis("Vertical");
        float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        float realYOffset = inputYOffset * Time.fixedDeltaTime * this.ySpeed;
        float xDisplacement = this.grid.cellSize.x * inputXOffset;
        float yDisplacement = this.grid.cellSize.y * inputYOffset;
        this.transform.Translate(xDisplacement, yDisplacement, 0);

        // Vector3 worldToCell = grid.WorldToCell(this.transform.position);
        // Debug.Log(worldToCell);

    }

    // Start is called before the first frame update
    void Start()
    {
        Tilemap[] tilemaps = grid.GetComponentsInChildren<Tilemap>();
        foreach (var tilemap in tilemaps)
        {
            if (tilemap.name == "Ground")
            {
                this.groundTilemap = tilemap;
                continue;
            }
            if (tilemap.name == "Buildings")
            {
                this.buildingsTilemap = tilemap;
                continue;
            }
        }
        Vector3 currentCellPosition = grid.WorldToCell(this.transform.position);
        this.SetPlayerPosition(currentCellPosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.movementType == MovementType.NonContinuous)
            this.NonContinuousMovement();
        else if (this.movementType == MovementType.Continuous)
            this.ContinuousMovement();
    }
}
