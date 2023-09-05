using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInimigo : MonoBehaviour
{
    public float speed = 4;
    public int dir = 1;

    private const int MAX_X = 8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += dir * Time.deltaTime * speed;

        if (pos.x > MAX_X)
        {
            dir = -1;
            speed += 0.5f;
        }
        else if (pos.x < -MAX_X)
        {
            dir = 1;
            speed += 0.5f;
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colidiu!");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
