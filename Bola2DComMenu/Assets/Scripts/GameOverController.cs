using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Bounce");
    }

    void OnEnable()
    {
        int playerScore = PlayerPrefs.GetInt("Score");
        scoreText.text = playerScore.ToString();
    }
}
