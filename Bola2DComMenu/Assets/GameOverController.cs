using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private GameObject player;
    private GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        Debug.Log(controller.score);
        scoreText = GameObject.Find("TxtPontuacao");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
