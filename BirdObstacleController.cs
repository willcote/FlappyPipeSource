using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdObstacleController : MonoBehaviour
{
    // TODO: change this if possible to not have to drag itself in in Unity
    [SerializeField] private Rigidbody2D rb;

    // adjustable vars
    [SerializeField] private float scrollSpeed = -2f;
    [SerializeField] private float leftBound = -5f;
    [SerializeField] private float rightBound = 5f;

    private float topSpawnValue = 1.15f;
    private float bottomSpawnValue = -0.5f;

    // testing variables (DISABLE)
    // [SerializeField] private float spawnHeightStrength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // rb.GetComponent<Rigidbody2D>();
        ResetObstacle();
        rb.velocity = new Vector2(scrollSpeed, 0f);

        GameEvents.current.onDeath += OnDeath;
    }

    void Update()
    {
        if (transform.position.x < leftBound)
        {
            GameEvents.current.onDeath -= OnDeath;
            Destroy(this.gameObject);
            // ResetObstacle(); // <--- removed this for new scheme. each birdObstacle is now destroyed instead of moved after leaving the screen.
        }
    }

    void ResetObstacle()
    {
        float randomSpawnHeight = Random.Range(bottomSpawnValue, topSpawnValue);
        transform.localPosition = new Vector3(rightBound, randomSpawnHeight , 0f);
    }

    void OnDeath()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
