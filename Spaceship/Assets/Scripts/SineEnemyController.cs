using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineEnemyController : MonoBehaviour
{

    public float enemyXSpeed;
    public float frequency;
    public float amplitude;
    public float rotationSpeed;
    private float maxX = 8.5f;
    private float maxY = 4f;
    private Vector3 updatablePosition;
    public float initialYPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.updatablePosition = this.transform.position;
        updatablePosition.x -= enemyXSpeed * Time.fixedDeltaTime;
        float newY = Mathf.Sin(Time.time * frequency) * amplitude + this.initialYPosition;
        if (newY > maxY)
            newY = maxY;
        else if (newY < -maxY)
            newY = -maxY;
        updatablePosition.y = newY;

        float rotation = Time.fixedDeltaTime * rotationSpeed;

        if (updatablePosition.x > -maxX)
        {
            this.transform.position = this.updatablePosition;
            this.transform.Rotate(Vector3.forward, rotation);
        }
        else
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
