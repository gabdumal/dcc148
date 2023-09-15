using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public float shootXSpeed = 6f;
    private float maxX = 8.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(shootXSpeed, 0, 0);
        movement *= Time.fixedDeltaTime;
        Vector3 newPosition = this.transform.position + movement;
        if (newPosition.x < maxX)
            this.transform.position = newPosition;
        else
            this.gameObject.SetActive(false);
    }
}
