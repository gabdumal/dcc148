using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player3Controller : MonoBehaviour
{

    public float playerSpeed = 2f;
    public float enemySpeed = 1f;
    public GameObject enemy;

    [SerializeField] private Vector2 enemyDirection;
    [SerializeField] private Vector2 playerDirection;
    private int enemyIsInFrontOfPlayer = 1;
    private float minimumHuntingDistance = 3f;
    private float maxX = 8f;


    // Start is called before the first frame update
    void Start()
    {
        enemyDirection.x = -1;
        playerDirection.x = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is in front of the player, invert the direction of its movement
        if (enemy.transform.position.x < this.transform.position.x)
            enemyIsInFrontOfPlayer = -1;
        else
            enemyIsInFrontOfPlayer = 1;

        // Move the player based on input
        float dx = Input.GetAxis("Horizontal");
        float playerXDisplacement = Time.deltaTime * dx * playerSpeed;
        if (this.transform.position.x + playerXDisplacement < maxX
            && this.transform.position.x + playerXDisplacement > -maxX)
            transform.Translate(playerXDisplacement, 0, 0, Space.World);

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

        // If player is not facing the enemy and it's in a minimum distance, it moves toward the player
        if (Vector2.Dot(enemyIsInFrontOfPlayer * playerDirection, Vector2.left) == 1
            && Math.Abs(enemy.transform.position.x - this.transform.position.x) < minimumHuntingDistance)
        {
            Vector2 enemyDeltaSpace = Time.deltaTime * enemySpeed * playerDirection;
            enemy.transform.Translate(enemyDeltaSpace, Space.World);
        }
        else
        {
            // Otherwise, move the enemy as the standard behaviour
            Vector2 enemyDeltaSpace = Time.deltaTime * enemySpeed * enemyDirection;
            if (enemyDeltaSpace.x + enemy.transform.position.x < maxX
                && enemyDeltaSpace.x + enemy.transform.position.x > -maxX
            )
                enemy.transform.Translate(enemyDeltaSpace, Space.World);
            else
                enemyDirection.x *= -1;

        }
    }
}
