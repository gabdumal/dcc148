using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float shootSpeed;
    public Vector3 direction;
    public float maxX;
    public float maxY;
    public float minY;
    public float maxZ;

    void Start()
    {

    }

    void Update()
    {
        Vector3 movement = direction * shootSpeed * Time.deltaTime;
        Vector3 newPosition = this.transform.position + movement;
        if (newPosition.x < maxX && newPosition.x > -maxX
            && newPosition.y < maxY && newPosition.y > minY
            && newPosition.z < maxZ && newPosition.z > -maxZ
        )
            this.transform.position = newPosition;
        else
            this.gameObject.SetActive(false);
    }
}
