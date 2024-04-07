using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float spawnCooldown = 1.0f; // Cooldown time between dog spawns
    private float lastSpawnTime; // Time when the last dog was spawned

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time - lastSpawnTime >= spawnCooldown)
        {
            // On spacebar press, spawn a new dog and update the last spawn time
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                lastSpawnTime = Time.time;
            }
        }
    }
}
