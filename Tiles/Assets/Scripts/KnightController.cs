using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    // public float xSpeed;
    public float tileSize;
    public Grid grid;
    public int movingAnimationTime;
    private int movingRemainingTime = 0;


    private void setPlayerPosition(Vector3 cellPosition)
    {
        this.transform.position = new Vector3(
            cellPosition.x + grid.cellSize.x / 2,
            cellPosition.y + grid.cellSize.y / 2,
            cellPosition.z);
    }

    private void fixedCellMoving()
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
        
        this.setPlayerPosition(nextCellPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentCellPosition = grid.WorldToCell(this.transform.position);
        this.setPlayerPosition(currentCellPosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.movingRemainingTime > 0)
        {
            this.movingRemainingTime--;
        }

        // float inputXOffset = Input.GetAxis("Horizontal");
        // float inputYOffset = Input.GetAxis("Vertical");
        this.fixedCellMoving();

        // // float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        // float xDisplacement = tileSize * inputXOffset;

        // float inputYOffset = Input.GetAxis("Vertical");
        // // float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        // float yDisplacement = tileSize * inputYOffset;

        // this.transform.Translate(xDisplacement, yDisplacement, 0);

        // Vector3 worldToCell = grid.WorldToCell(this.transform.position);
        // Debug.Log(worldToCell);

    }
}
