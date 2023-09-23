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
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float halfHeight;
    [SerializeField] private float halfWidth;
    [SerializeField] private CameraType cameraType;

    // Start is called before the first frame update
    void Start()
    {
        this.halfHeight = Camera.main.orthographicSize;
        this.halfWidth = this.halfHeight * Camera.main.aspect;
    }

    void KillerScrollingBehaviour()
    {
        float newXPosition = Mathf.Clamp(
            this.transform.position.x + xSpeed * Time.deltaTime,
            this.leftEdge.position.x + this.halfWidth,
            this.rightEdge.position.x - this.halfWidth
        );
        Vector3 newPosition = new Vector3(newXPosition, this.transform.position.y, this.transform.position.z);
        transform.position = newPosition;
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
            case CameraType.ContinuousKiller: KillerScrollingBehaviour(); break;
            // case CameraType.ContinuousForce: ForceScrollingBehaviour(); break;
            case CameraType.FollowPlayer: CenteredBehaviour(); break;
                // case CameraType.CameraJump: JumpingBehaviour(); break;
                // case CameraType.SmoothJump: SmoothJumpingBehaviour(); break;
                // case CameraType.BoxCentered: MoveBoxBehaviour(); break;
        }
    }
}
