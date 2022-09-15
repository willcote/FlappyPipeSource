using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdObstacle;
    [SerializeField] private UIController ui;
    [SerializeField] private GameObject floatingText;

    private bool spawnObstacles = true;

    private int score;
    private float spawnInterval;
    private float initSpawnInterval = 2f;

    private int spawnCounter = 0; // tracks how many birds have been spawned for spawhn height logic

    private float lastSpawnHeight;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBird(birdObstacle, spawnInterval));
        GameEvents.current.onDeath += OnDeath;
        score = ui.getScore();
        spawnInterval = initSpawnInterval;

        GameEvents.current.onAddPoint += intervalUpdater;
    }


    void intervalUpdater()
    {
        // gets the number of points
        score = ui.getScore();
        // hard-coded point values as update triggers
        switch(score)
        {
            case 4:
                spawnInterval = 1.8f;
                showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            case 8:
                spawnInterval = 1.4f;
                // showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            case 12:
                spawnInterval = 1.2f;
                // showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            case 15:
                spawnInterval = 1.0f;
                // showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            case 18:
                spawnInterval = 0.8f;
                // showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            case 21:
                spawnInterval = 0.6f;
                // showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            case 23:
                spawnInterval = 0.4f;
                // showFloatingText();
                // Debug.Log("spawnInterval changed to " + spawnInterval.ToString());
                break;
            default:
                // Debug.Log("no spawnrate change...");
                break;
        }
        // changes spawnInterval value (also hard-coded probably)
    }

    void showFloatingText()
    {
        var go = Instantiate(floatingText, new Vector3(0, 0, 0), Quaternion.identity);
        go.GetComponent<TextMeshProUGUI>().text = "+ difficulty";
        
        Debug.Log("show floating text...");
    }


    // BUG:
    //  - This method is called within a 2s timeframe of the player dying and a birdObstacle spawns
    //  - birdObstacle doesn't stop moving because it missed the Death GameEvent call.
    // FIXED by changing out 'yield return new WaitForSeconds(interval);' logic
    // for for-loop logic.

    // A better fix might have been to just move the bird spawning to before the wait.
    private IEnumerator spawnBird(GameObject bird, float interval)
    {
        interval = spawnInterval;
        for (float timer = interval; timer >= 0f; timer -= Time.deltaTime)
        {
            if (spawnObstacles)
            {
                yield return null;
            }
            else
            {
                yield break;
            }
        }

        // yield return new WaitForSeconds(interval);

        // generate new height based on lastSpawnHeight
        // set spawnHeight of new bird
        // save new spawnHeight as lastSpawnHeight

        spawnCounter += 1;
        GameObject newBird = Instantiate(bird, Vector3.zero, Quaternion.identity);
        newBird.GetComponent<BirdObstacleController>().Init(lastSpawnHeight, spawnCounter);
        lastSpawnHeight = newBird.GetComponent<BirdObstacleController>().getSpawnHeight();

        // print(lastSpawnHeight.ToString());
        // print(spawnCounter.ToString());

        if (spawnCounter > 1)
            spawnCounter = 0;   // resests counter once it's over 2

        //restarts this coroutine
        StartCoroutine(spawnBird(bird, interval));
    }

    private void OnDeath()
    {
        spawnObstacles = false;
    }

    public float getLastSpawnHeight()
    {
        return lastSpawnHeight;
    }
}
