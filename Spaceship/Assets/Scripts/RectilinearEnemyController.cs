using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectilinearEnemyController : MonoBehaviour
{

    public float enemyXSpeed;
    private float maxX = 8.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(-enemyXSpeed, 0, 0);
        movement *= Time.fixedDeltaTime;
        Vector3 newPosition = this.transform.position + movement;
        if (newPosition.x > -maxX)
            this.transform.position = newPosition;
        else
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

}
