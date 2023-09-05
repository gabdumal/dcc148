using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{

    public float playerSpeed = 2f;
    public float enemySpeed = 1f;
    public GameObject enemy;

    [SerializeField] private Vector2 playerDirection;
    private int enemyIsInFrontOfPlayer = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is in front of the player, invert the direction of its movement
        if (enemy.transform.position.x < this.transform.position.x)
            enemyIsInFrontOfPlayer = -1;

        // Move the player based on input
        float dx = Input.GetAxis("Horizontal");
        transform.Translate(Time.deltaTime * dx * playerSpeed, 0, 0, Space.World);

        // Update player rotation to match its direction
        if (dx > 0)
        {
            playerDirection.x = 1;
            transform.eulerAngles = Vector3.forward * -90;
        }
        else if (dx < 0)
        {
            playerDirection.x = -1;
            transform.eulerAngles = Vector3.forward * 90;
        }

        // If player is not facing the enemy, it moves toward the player
        if (Vector2.Dot(enemyIsInFrontOfPlayer * playerDirection, Vector2.left) == 1)
            enemy.transform.Translate(Time.deltaTime * enemySpeed * playerDirection, Space.World);
    }
}
