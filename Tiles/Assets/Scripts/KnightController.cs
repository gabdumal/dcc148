using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Grid grid;
    public Tilemap groundTilemap;
    public Tilemap buildingsTilemap;
    private bool isMoving = false;
    private Vector3 nextCellPosition;
    // [SerializeField] BoundsInt groundBounds;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentCellPosition = grid.WorldToCell(this.transform.position);
        this.SetPlayerPosition(currentCellPosition);
        this.nextCellPosition = currentCellPosition;
        // this.groundBounds = this.groundTilemap.cellBounds;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.movementType == MovementType.NonContinuous)
            this.NonContinuousMovement();
        else if (this.movementType == MovementType.Continuous)
            this.ContinuousMovement();
    }

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
            && nextCellPosition.y < this.groundTilemap.cellBounds.yMax)
            this.SetPlayerPosition(nextCellPosition);
    }

    private void ContinuousMovement()
    {
        if (!this.isMoving)
        {
            this.SetPlayerPosition(this.nextCellPosition);
        }
        float inputXOffset = Input.GetAxis("Horizontal");
        float inputYOffset = Input.GetAxis("Vertical");
        if (inputXOffset != 0)
        {
            this.isMoving = true;
            float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
            float horizontalDisplacement = this.grid.cellSize.x * realXOffset;

            Vector3 currentCellPosition = grid.WorldToCell(this.transform.position);
            float nextXPosition = currentCellPosition.x + grid.cellSize.x * horizontalDisplacement;

            if (nextXPosition >= this.groundTilemap.localBounds.min.x - 1
                && nextXPosition < this.groundTilemap.localBounds.max.x)
            {
                this.nextCellPosition = new Vector3(
                            nextXPosition,
                            currentCellPosition.y,
                            currentCellPosition.z);
                this.transform.Translate(horizontalDisplacement, 0, 0);
            }
        }
        else if (inputYOffset != 0)
        {
            this.isMoving = true;
            float realYOffset = inputYOffset * Time.fixedDeltaTime * this.ySpeed;
            float verticalDisplacement = this.grid.cellSize.y * realYOffset;

            Vector3 currentCellPosition = grid.WorldToCell(this.transform.position);
            float nextYPosition = currentCellPosition.y + grid.cellSize.y * verticalDisplacement;

            if (nextYPosition >= this.groundTilemap.localBounds.min.y
                && nextYPosition < this.groundTilemap.localBounds.max.y)
            {
                this.nextCellPosition = new Vector3(
                            currentCellPosition.x,
                            currentCellPosition.y + grid.cellSize.y * verticalDisplacement,
                            currentCellPosition.z);
                this.transform.Translate(0, verticalDisplacement, 0);
            }
        }
        else
        {
            this.isMoving = false;
        }
    }
}
