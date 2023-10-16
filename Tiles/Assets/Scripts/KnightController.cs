using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    // public float xSpeed;
    public float tileSize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputXOffset = Input.GetAxis("Horizontal");
        // float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        float xDisplacement = tileSize * inputXOffset;

        float inputYOffset = Input.GetAxis("Vertical");
        // float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        float yDisplacement = tileSize * inputYOffset;

        this.transform.Translate(xDisplacement, yDisplacement, 0);

    }
}
