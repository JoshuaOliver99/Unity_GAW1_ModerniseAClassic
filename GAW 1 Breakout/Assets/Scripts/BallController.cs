using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public StatusHandler StatusHandler;
    public AudioSource PaddleCollide;
    public AudioSource BoundCollide;
    public AudioSource BrickCollide;
    public AudioSource BottomCollide;


    public Rigidbody2D rb;
    public Transform BallSpawn;

    public bool InPlay; // Is main ball is in play?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        // OUT OF PLAY / RESPAWN
        if (!InPlay)
        {
            transform.position = BallSpawn.position;// Ball sticks to Paddle

            if (Input.GetButtonDown("Launch")) // Presses space key
            {
                rb.AddForce(Vector2.up * 400); // Ball fires upward
                InPlay = true; // Play starts
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // COLLISION WITH SCREEN BOTTOM
        if (collision.CompareTag ("LoseZone"))
        {
            rb.velocity = Vector2.zero; // Stops ball trying to move
            StatusHandler.lives--; // Decrese life
            InPlay = false; // Stops play in Update()
            BottomCollide.Play();
        }

        // COLLISION WITH A BRICK
        if (collision.CompareTag("Brick"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y); // Bounce off a brick
            StatusHandler.score += 200; // Increase Score
            BrickCollide.Play();
            Destroy(collision.gameObject); // Destroys Brick instance
        }

        if (collision.CompareTag("Bound"))
        {
            BoundCollide.Play();
        }

        if (collision.CompareTag("Paddle"))
        {
            PaddleCollide.Play();
        }
    }
}
