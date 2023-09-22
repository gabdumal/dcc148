using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float xSpeed;
    private float cameraHeight;
    private float cameraWidth;
    private float playerHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        this.cameraHeight = Camera.main.orthographicSize;
        this.cameraWidth = cameraHeight * Camera.main.aspect;
        this.playerHalfWidth = this.transform.localScale.x / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputXOffset = Input.GetAxis("Horizontal");
        float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        float newXPosition = this.transform.position.x + realXOffset;
        if ((newXPosition + playerHalfWidth) < cameraWidth && (newXPosition - playerHalfWidth) > -cameraWidth)
        {
            Vector3 movement = new Vector3(realXOffset, 0, 0);
            this.transform.Translate(movement);
        }
    }
}
