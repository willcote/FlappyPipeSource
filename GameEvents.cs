using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    void Awake()
    {
        current = this;
    }

    // list of methods that will run when called
    public event Action onDeath;
    // player calls this method when they die
    public void Death()
    {
        if (onDeath != null)
        {
            onDeath();
        }
    }

    // when the player gets through a bird-obstacle and earns a point
    public event Action onAddPoint;
    public void addPoint()
    {
        if (onAddPoint != null)
        {
            onAddPoint();
        }
    }
    
    // pauses game
    public event Action onPauseGame;
    public void pauseGame()
    {
        if (onPauseGame != null)
        {
            onPauseGame();
        }
    }
    
    // resumes game
    public event Action onResumeGame;
    public void resumeGame()
    {
        if (onResumeGame != null)
        {
            onResumeGame();
        }
    }

}
