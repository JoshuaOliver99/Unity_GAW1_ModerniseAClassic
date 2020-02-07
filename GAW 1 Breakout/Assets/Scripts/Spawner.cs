using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public BallController BallController; // For - InPlay

    // RELATED TO WALL SPAWNING
    public GameObject WallPrefab;
    public float wallTimer = 0.0f; //  Wall timer (public for PaddleController power-up
    float spawnWallDelay = 10; // Wall spawn delay
    float[] wallSpawns = { 4.25f, 3.75f, 3.25f, 2.75f, 2.25f, 1.75f, 1.25f, 0.75f, 0.25f, -0.25f, -0.75f, -1.25f }; // 12 wall positions
    int wallPosition = 0; // For cycling wall placement

    // RELATED TO POWER-UP SPAWNING
    public GameObject PUExtraBallPrefab;
    public GameObject PUExtraLifePrefab;
    public GameObject PUPauseWallsPrefab;
    float powerUpTimer = 0.0f; // Power-up timer 
    float spawnPowerUpDelay = 15; // Power-up spawn delay


    void Start()
    {
        // SPAWN STARTING WALLS
        for (int i = 0; i < 3; i++) // Number of walls
            spawnWall();
    }

    void Update()
    {
        // PAUSE TIMERS WHEN NOT PLAYING
        if (BallController.InPlay)
        {
            wallTimer += Time.deltaTime;
            powerUpTimer += Time.deltaTime;
        }
        
        wallSpawner();
        powerUpSpawner();
    }


    void powerUpSpawner() // CONTROLLS POWER-UP SPAWNING
    {
        // SPAWN AFTER DELAY
        if (powerUpTimer >= spawnPowerUpDelay)
        {
            int spawnCase = Random.Range(1, 4); // Random int for random power-up

            switch (spawnCase)
            {
                case 1:
                    Instantiate(PUExtraBallPrefab, new Vector3(Random.Range(-4, 5), 6, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(PUExtraLifePrefab, new Vector3(Random.Range(-4, 5), 6, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(PUPauseWallsPrefab, new Vector3(Random.Range(-4, 5), 6, 0), Quaternion.identity);
                    break;
                default:
                    Debug.LogError("Spawner - POWER-UP RANGE OUT OF BOUNDS");
                    break;
            }
            powerUpTimer = 0.0f; // Reset timer
        }
    }


    void wallSpawner() // CONTROLLS WALL SPAWNING
    {
        // RESET WALL POSITION
        if (wallPosition >= wallSpawns.Length) // If at max height
        {
            wallPosition = 0;
        }

        // SPAWN AFTER DELAY
        if (wallTimer >= spawnWallDelay)
        {
            spawnWall();
            wallTimer = 0; // Reset timer
        }
    }

    void spawnWall() // SPAWNS AN INSTANCE OF A WALL
    {
        var newWall = Instantiate(WallPrefab, new Vector3(0, wallSpawns[wallPosition], 0), Quaternion.identity); // Spawn a wall

        // COLOUR THE WALL
        for(int i = 0; i < newWall.transform.childCount; i++) // For each brick in the wall
        {
            GameObject brick = newWall.transform.GetChild(i).gameObject;

            // COLOUR WALLS ACCORDING TO Y POSITION
            switch (wallPosition)
            {
                case 0: case 4: case 8:
                    brick.GetComponent<SpriteRenderer>().color = new Color(255f, 0, 0, 255f); // R
                    break;
                case 1: case 5: case 9:
                    brick.GetComponent<SpriteRenderer>().color = new Color(0, 255f, 0, 255f); // G
                    break;
                case 2: case 6: case 10:
                    brick.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255f, 255f); // B
                    break;
                case 3: case 7: case 11:
                    brick.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0, 255f); // Y
                    break;
                default:
                    Debug.LogError("Spawner - WALL POSITION OUT OF BOUNDS");
                    break;
            }
        }
        wallPosition++; // Move to next place
    }
}