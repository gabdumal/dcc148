using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    private GameObject player;
    private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        Debug.Log(controller.score);
        scoreText = GameObject.Find("GameOverScore").GetComponent<TMP_Text>();
        Debug.Log(scoreText);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Bounce");
    }
}
