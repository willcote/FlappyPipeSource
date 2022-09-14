using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    private Rigidbody2D rb;

    //behavioral variables
    [SerializeField] private float jumpForce;
    [SerializeField] private float tiltStrength;
    private float neutralTilt = -90f;   // value of z-rotation that is "home", or how the sprite looks normally
    private Vector3 initRotation;
    private bool isAlive = true;
    private bool isPaused = false; // using this to turn off controls when the game is paused



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initRotation = new Vector3(0f, 0f, neutralTilt);

        transform.eulerAngles = initRotation;   //sets the rotation of the pipe
        
        GameEvents.current.onPauseGame += Pause;
        GameEvents.current.onResumeGame += Resume;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Tilt();
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Kill();
        }
    }

    private void Kill()
    {
        isAlive = false;                                // Makes sure the rotation isn't still being updated ++ user can't Jump
        rb.velocity = Vector2.zero;                     // freezes Pipe
        rb.bodyType = RigidbodyType2D.Static;           // freezes Pipe
        GetComponent<Collider2D>().enabled = false;     // turns off collider

        GameEvents.current.Death();     // calls game event Death
    }

    private void Jump()
    {
        if (!isPaused)
        {
            // pipe jumps if space is pressed
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void Tilt()
    {
        transform.eulerAngles = new Vector3(0f, 0f, neutralTilt + (float)(rb.velocity.y * tiltStrength));
    }


    private void Pause()
    {
        isPaused = true;
    }

    private void Resume()
    {
        isPaused = false;
    }
}
