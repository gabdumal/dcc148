using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float cameraXLimit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newX = player.transform.position.x;
        newX = Math.Clamp(newX, -cameraXLimit, cameraXLimit);
        Vector3 newPosition = new Vector3(newX, this.transform.position.y, this.transform.position.z);
        transform.position = newPosition;
    }
}
