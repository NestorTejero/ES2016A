using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int lives = 20;        // The player's score.
    public int money = 100;

    public Text moneyText;
    public Text livesText;
    public GameObject gameOverPanel;
    public Text gameOverText;

    void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    public void LoseLife(int l = 1)
    {
        lives -= l;
        if (lives <= 0)
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        moneyText.text = money.ToString();
        livesText.text = lives.ToString();
    }

}