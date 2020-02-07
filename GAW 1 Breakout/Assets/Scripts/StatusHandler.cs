using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatusHandler : MonoBehaviour
{
    public BallController BallController; // For - lives count, Score, Inplay
    public GameObject GameOver;

    public int score = 0;
    public int lives = 3;

    public Text scoreText;
    public Text livesText;
    public Text finalScore;

    void Start()
    {
        GameOver.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (lives <= 0)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
            finalScore.text = "FINAL SCORE: " + score;

            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("MainMenu");
            //Application.Quit();
        }

        scoreText.text = "SCORE: " + score;
        livesText.text = "LIVES: " + lives;
    }

}
