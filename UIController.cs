using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private GameObject fullUI;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        // tmp = GetComponent<TextMeshProUGUI>();
        GameEvents.current.onAddPoint += addPoint;
        GameEvents.current.onDeath += Death;
    }

    private void addPoint()
    {
        points += 1;
        tmp.text = points.ToString();
    }

    //disable UI when dead
    private void Death()
    {
        fullUI.SetActive(false);
    }

    public int getScore()
    {
        return points;
    }
}
