using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 2f;
    public float enemySpeed = 1f;
    public GameObject enemy;

    [SerializeField] private Vector2 playerDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int enemyDirection = 1;
        if (enemy.transform.position.x < this.transform.position.x)
            enemyDirection = -1;

        float dx = Input.GetAxis("Horizontal");
        transform.Translate(Time.deltaTime * dx * playerSpeed, 0, 0, Space.World);

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

        if (Vector2.Dot(enemyDirection * playerDirection, Vector2.left) == 1)
        {
            enemy.transform.Translate(Time.deltaTime * enemySpeed * playerDirection, Space.World);
        }
    }
}
