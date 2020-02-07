using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    public StatusHandler StatusHandler; // For - Score
    public AudioSource PaddleCollide;
    public AudioSource BoundCollide;
    public AudioSource BrickCollide;
    public AudioSource BottomCollide;

    public Rigidbody2D rb;

    void Start()
    {
        rb.AddForce(Vector2.up * 300); // Ball fires upward
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // COLLISION WITH SCREEN BOTTOM
        if (collision.CompareTag("LoseZone"))
        {
            Destroy(gameObject); // Destroys Ball instance
            BottomCollide.Play();
        }

        // COLLISION WITH A BRICK
        if (collision.CompareTag("Brick"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y); // Bounce off a brick
            //StatusHandler.score += 200; // Increase Score
            Destroy(collision.gameObject); // Destroys Brick instance
            BrickCollide.Play();
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
