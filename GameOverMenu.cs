using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathUI;
    [SerializeField] private string menuScene;
    [SerializeField] private UIController ui;

    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI highScoreTMP;

    private int score;
    private int highScore;

    void Awake()
    {
        deathUI.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);     // 0 param is the def value
    }

    void Start()
    {
        GameEvents.current.onDeath += Death;
    }

    void Death()
    {
        score = ui.getScore();
        setScore();
        updateHighScore();
        deathUI.SetActive(true);    // death UI opens when player dies
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private void setScore()
    {
        scoreTMP.text = "SCORE: " + score.ToString();
    }

    private void updateHighScore()
    {
        // updates high score in PlayerPrefs (if necessary)
        if (ui.getScore() > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            Debug.Log("HighScore updated...");
        }

        // writes PlayerPrefs "HighScore" value to highScore text mesh pro
        highScoreTMP.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void resetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScoreTMP.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
