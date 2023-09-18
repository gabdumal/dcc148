using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public float shootSpeed;
    public Vector3 direction;
    private float maxX = 8.5f;
    private float maxY = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = direction * shootSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = this.transform.position + movement;
        if (newPosition.x < maxX && newPosition.x > -maxX && newPosition.y < maxY && newPosition.y > -maxY)
            this.transform.Translate(movement);
        else
            this.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag != "Spaceship")
        {
            enemy.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
