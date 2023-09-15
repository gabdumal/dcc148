using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    public float playerYSpeed = 3.5f;
    public GameObject shootPrefab;
    private float maxY = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputYOffset = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0, inputYOffset * playerYSpeed, 0);
        movement *= Time.fixedDeltaTime;
        Vector3 newPosition = this.transform.position + movement;
        if (newPosition.y > -maxY && newPosition.y < maxY)
        {
            this.transform.position = newPosition;

            bool spacePressed = Input.GetKeyDown(KeyCode.Space);
            if (spacePressed)
            {
                Instantiate(shootPrefab, newPosition, Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
