using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    ContinuousKiller,
    ContinuousForce,
    FollowPlayer,
    CameraJump,
    SmoothJump,
    BoxCentered,
}

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float xSpeed;
    public CameraType cameraType;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float halfHeight;
    [SerializeField] private float halfWidth;
    [SerializeField] private float cameraLeftMargin;
    [SerializeField] private float cameraRightMargin;
    private float playerHalfWidth;


    // Start is called before the first frame update
    void Start()
    {
        this.halfHeight = Camera.main.orthographicSize;
        this.halfWidth = this.halfHeight * Camera.main.aspect;
        this.playerHalfWidth = player.localScale.x / 2;
    }

    void KillerScrollingBehaviour()
    {
        if (player.position.x - this.playerHalfWidth <= this.cameraLeftMargin
        || player.position.x + this.playerHalfWidth >= this.cameraRightMargin
        )
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void ForceScrollingBehaviour()
    {
        if (player.position.x - this.playerHalfWidth <= this.cameraLeftMargin)
        {
            float newPlayerXPosition = this.cameraLeftMargin + this.playerHalfWidth;
            float playerXOffset = newPlayerXPosition - this.player.position.x;
            player.Translate(playerXOffset, 0, 0);
        }
        else if (player.position.x + this.playerHalfWidth >= this.cameraRightMargin)
        {
            float newPlayerXPosition = this.cameraRightMargin - this.playerHalfWidth;
            float playerXOffset = newPlayerXPosition - this.player.position.x;
            player.Translate(playerXOffset, 0, 0);
        }
    }

    void ScrollingBehaviour(CameraType cameraType)
    {
        float newXPosition = Mathf.Clamp(
            this.transform.position.x + xSpeed * Time.deltaTime,
            this.leftEdge.position.x + this.halfWidth,
            this.rightEdge.position.x - this.halfWidth
        );

        this.cameraLeftMargin = newXPosition - this.halfWidth;
        this.cameraRightMargin = newXPosition + this.halfWidth;

        if (cameraType == CameraType.ContinuousKiller)
        {
            this.KillerScrollingBehaviour();
        }
        else if (cameraType == CameraType.ContinuousForce)
        {
            this.ForceScrollingBehaviour();
        }

        Vector3 newPosition = new Vector3(newXPosition, this.transform.position.y, this.transform.position.z);
        this.transform.position = newPosition;
    }

    void CenteredBehaviour()
    {
        float newXPosition = Mathf.Clamp(
            this.player.position.x,
            this.leftEdge.position.x + this.halfWidth,
            this.rightEdge.position.x - this.halfWidth
        );
        Vector3 newPosition = new Vector3(newXPosition, this.transform.position.y, this.transform.position.z);
        transform.position = newPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (cameraType)
        {
            case CameraType.ContinuousKiller: ScrollingBehaviour(CameraType.ContinuousKiller); break;
            case CameraType.ContinuousForce: ScrollingBehaviour(CameraType.ContinuousForce); break;
            case CameraType.FollowPlayer: CenteredBehaviour(); break;
                // case CameraType.CameraJump: JumpingBehaviour(); break;
                // case CameraType.SmoothJump: SmoothJumpingBehaviour(); break;
                // case CameraType.BoxCentered: MoveBoxBehaviour(); break;
        }
    }
}
