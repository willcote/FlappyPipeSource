using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScrollerMenu : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    
    private float width;
    private float scrollSpeed = -2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        width = coll.size.x;
        rb.velocity = new Vector2(scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        if (transform.position.x < -(width * 2))
        {
            Vector2 resetPos = new Vector2(width * 4f, 0);
            transform.position = (Vector2)transform.position + resetPos;
        }
    }
}
