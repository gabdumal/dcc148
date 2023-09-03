using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public float xSpeed = 3.5f;
    public float impulse = 75;
    public float gravity = 50;

    private float vy;
    private TMP_Text scoreText;
    private int MAX_X = 8;
    private int MAX_Y = 4;

    // Start is called before the first frame update
    void Start()
    {
        vy = 0;
        scoreText = GameObject.Find("InGameScore").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float vx = Input.GetAxis("Horizontal") * xSpeed;

        vy -= gravity * Time.fixedDeltaTime;

        Vector2 v = new Vector2(vx, vy);
        pos += v * Time.fixedDeltaTime;

        if (pos.y < -MAX_Y)
        {
            pos.y = -MAX_Y;
            vy = impulse;
            score++;
            scoreText.text = score.ToString();
        }
        if (pos.x < -MAX_X)
        {
            pos.x = -MAX_X;
        }
        else if (pos.x > MAX_X)
        {
            pos.x = MAX_X;
        }

        transform.position = pos;
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("Score", score);
    }
}
