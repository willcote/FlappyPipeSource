using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool gamePaused = false;
    private bool isDead = false;    // used to disable pause menu after dying

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private string menuScene;


    void Start()
    {
        pauseMenuUI.SetActive(false); // init's pause menu to be off
        GameEvents.current.onDeath += Death;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            OpenCloseMenu();
        }
        else
        {
            Resume();
        }
    }

    void OpenCloseMenu()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;    // freezes the game
        gamePaused = false;
        GameEvents.current.resumeGame();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    // freezes the game
        gamePaused = true;
        GameEvents.current.pauseGame();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(menuScene);
        // Debug.Log("Loading menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private void Death()
    {
        isDead = true;
    }
}
