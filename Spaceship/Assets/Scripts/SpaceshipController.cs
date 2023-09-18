using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    public float playerYSpeed;
    public float rotationSpeed;
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
        float inputYOffset = Input.GetAxis("Vertical");
        float inputRotationOffset = -Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(0, inputYOffset * playerYSpeed, 0);
        movement *= Time.fixedDeltaTime;
        Vector3 newPosition = this.transform.position + movement;

        float rotationAngle = inputRotationOffset * rotationSpeed;
        float newRotationZ = rotationAngle * 30 - 90;
        Vector3 rotation = new Vector3(0, 0, newRotationZ);
        this.transform.rotation = Quaternion.Euler(rotation);

        if (newPosition.y > -maxY && newPosition.y < maxY)
        {
            this.transform.position = newPosition;

            bool spacePressed = Input.GetKeyDown(KeyCode.Space);
            if (spacePressed)
            {
                GameObject newShoot = this.shootPool.GetFromPool();
                if (newShoot != null)
                {
                    newShoot.transform.position = newPosition;
                    newShoot.GetComponent<ShootController>().direction = this.transform.up;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag != "SpaceshipShoot")
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
