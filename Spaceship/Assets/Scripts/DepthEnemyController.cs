using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthEnemyController : MonoBehaviour
{

    public float enemyXSpeed;
    public float minimumSize;
    public float depthSpeed;
    public float depthConstant;
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

        float depthValue = Math.Max((Mathf.Sin(Time.time * depthSpeed) + 1) * depthConstant, minimumSize);
        Vector3 depth = new Vector3(depthValue, depthValue, depthValue);

        if (newPosition.x > -maxX)
        {
            this.transform.position = newPosition;
            this.transform.localScale = depth;
        }
        else
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
