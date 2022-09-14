using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollerMenu : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    
    private float width;
    [SerializeField] private float scrollSpeed = -0.5f;
    // [SerializeField] private PipeController player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        width = coll.size.x;
        coll.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //This is the infinite scrolling logic
        if (transform.position.x < -width)
        {
            Vector2 resetPos = new Vector2(width * 2f, 0);
            transform.position = (Vector2)transform.position + resetPos;
        }
    }
}
