using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdObstacle;
    [SerializeField] private float spawnInterval = 2f;
    private bool spawnObstacles = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBird(birdObstacle, spawnInterval));
        GameEvents.current.onDeath += OnDeath;
    }


    // BUG:
    //  - This method is called within a 2s timeframe of the player dying and a birdObstacle spawns
    //  - birdObstacle doesn't stop moving because it missed the Death GameEvent call.
    // FIXED by changing out 'yield return new WaitForSeconds(interval);' logic
    // for for-loop logic.

    // A better fix might have been to just move the bird spawning to before the wait.
    private IEnumerator spawnBird(GameObject bird, float interval)
    {
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
        GameObject newBird = Instantiate(bird, Vector3.zero, Quaternion.identity);
        
        //restarts this coroutine
        StartCoroutine(spawnBird(bird, interval));
    }

    private void OnDeath()
    {
        spawnObstacles = false;
    }
}
