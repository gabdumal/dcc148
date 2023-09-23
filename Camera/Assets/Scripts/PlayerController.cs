using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float xSpeed;
    public float yImpulse;
    public float gravity;
    public float playerXLimit;
    public float playerYBottomLimit;

    private float playerHalfWidth;
    private float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.playerHalfWidth = this.transform.localScale.x / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputXOffset = Input.GetAxis("Horizontal");
        float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        float newXPosition = this.transform.position.x + realXOffset;

        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        if (spacePressed && this.ySpeed == 0)
            this.ySpeed += this.yImpulse;

        this.ySpeed -= gravity * Time.fixedDeltaTime;
        float realYOffset = ySpeed * Time.fixedDeltaTime;
        float newYPosition = this.transform.position.y + realYOffset;

        if (!((newXPosition + playerHalfWidth) < playerXLimit && (newXPosition - playerHalfWidth) > -playerXLimit))
        {
            realXOffset = 0;
        }

        if (newYPosition < this.playerYBottomLimit)
        {
            realYOffset = 0;
            this.ySpeed = 0;
        }

        Vector3 movement = new Vector3(realXOffset, realYOffset, 0);
        this.transform.Translate(movement);
    }
}
