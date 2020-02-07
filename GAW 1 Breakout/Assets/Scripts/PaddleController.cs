using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public GameObject ExtraBallPrefab;
    public StatusHandler StatusHandler;
    public Spawner Spawner;

    public AudioSource powerup1;
    public AudioSource powerup2;
    public AudioSource powerup3;

    public float speed;
    public float leftBoundary;
    public float rightBoundary;


    void Update()
    {
        // MOVE PADDLE
        float horizontal = Input.GetAxis("Horizontal"); // returns -1, 0, 1 for deirection
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        // CONFINE PADDLE WITHIN BOUNDARIES 
        if (transform.position.x < leftBoundary) // If paddle left of left boundary
            transform.position = new Vector2(leftBoundary, transform.position.y);
        if (transform.position.x > rightBoundary) // If Paddle right of right boundary
            transform.position = new Vector2(rightBoundary, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // COLLISION WITH EXTRA BALL POWER-UP
        if (collision.CompareTag("ExtraBall"))
        {
            Debug.LogError("PaddleController - ExtraBall COLLISION");
            Destroy(collision.gameObject); // Destroys other
            Instantiate(ExtraBallPrefab, new Vector3(0, -4, 0), Quaternion.identity);
            powerup1.Play();
        }

        // COLLISION WITH EXTRA LIFE POWER-UP
        if (collision.CompareTag("ExtraLife"))
        {
            Debug.LogError("PaddleController - ExtraLife COLLISION");
            Destroy(collision.gameObject); // Destroys other 
            StatusHandler.lives++;
            powerup2.Play();
        }

        // COLLISION WITH PAUSE WALLS POWER-UP
        if (collision.CompareTag("PauseWalls"))
        {
            Debug.LogError("PaddleController - PauseWalls COLLISION");
            Destroy(collision.gameObject); // Destroys other 
            Spawner.wallTimer -= 10;
            powerup3.Play();
        }
    }
}



