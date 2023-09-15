using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    public float playerYSpeed = 3.5f;
    public GameObject shootPrefab;
    public int shootPoolSize;
    private ObjectPool shootPool;
    private float maxY = 4f;

    // Start is called before the first frame update
    void Start()
    {
        this.shootPool = new ObjectPool(shootPrefab, shootPoolSize);
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
                GameObject newShoot = this.shootPool.GetFromPool();
                newShoot.transform.position = new Vector3(this.transform.position.x + 1f, newPosition.y, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
