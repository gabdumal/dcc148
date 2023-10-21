using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Tilemap groundTilemap;
    [SerializeField] private float halfHeight;
    [SerializeField] private float halfWidth;
    [SerializeField] private int xSection;
    [SerializeField] private int ySection;
    // [SerializeField] private Bounds localBounds;


    // Start is called before the first frame update
    void Start()
    {
        this.halfHeight = Camera.main.orthographicSize;
        this.halfWidth = this.halfHeight * Camera.main.aspect;
        // this.localBounds = this.groundTilemap.localBounds;
        Vector3 currentPlayerCell = this.groundTilemap.WorldToCell(player.position);
        this.xSection = Mathf.FloorToInt((currentPlayerCell.x - this.groundTilemap.cellBounds.xMin + 1) / (2 * this.halfWidth));
        this.ySection = Mathf.FloorToInt((currentPlayerCell.y - this.groundTilemap.cellBounds.yMin - 1) / (2 * this.halfHeight));
        this.transform.position = new Vector3(this.GetNewXPosition(), this.GetNewYPosition(), this.transform.position.z);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentPlayerCell = this.groundTilemap.WorldToCell(player.position);
        int currentXSection = Mathf.FloorToInt((currentPlayerCell.x - this.groundTilemap.cellBounds.xMin + 1) / (2 * this.halfWidth));
        int currentYSection = Mathf.FloorToInt((currentPlayerCell.y - this.groundTilemap.cellBounds.yMin - 1) / (2 * this.halfHeight));
        if (currentXSection != this.xSection)
        {
            this.xSection = currentXSection;
            this.transform.position = new Vector3(this.GetNewXPosition(), this.transform.position.y, this.transform.position.z);
        }
        if (currentYSection != this.ySection)
        {
            this.ySection = currentYSection;
            this.transform.position = new Vector3(this.transform.position.x, this.GetNewYPosition(), this.transform.position.z);
        }
    }

    private float GetNewXPosition()
    {
        return Mathf.Clamp(
                this.groundTilemap.localBounds.min.x + this.halfWidth + 2 * this.xSection * this.halfWidth,
                this.groundTilemap.localBounds.min.x + this.halfWidth,
                this.groundTilemap.localBounds.max.x - this.halfWidth
            );
    }

    private float GetNewYPosition()
    {
        return Mathf.Clamp(
            this.groundTilemap.localBounds.min.y + this.halfHeight + 2 * this.ySection * this.halfHeight,
            this.groundTilemap.localBounds.min.y + this.halfHeight + 1,
            this.groundTilemap.localBounds.max.y - this.halfHeight
        );
    }
}
