using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShootController : MonoBehaviour
{
    public float shootSpeed;
    public Vector3 direction;
    private float maxX = 8.5f;
    private float maxY = 4.5f;

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
            this.transform.position = newPosition;
        else
            this.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spaceship")
        {
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
