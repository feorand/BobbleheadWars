using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject alien;

    public int maxAlentsOnScreen;
    public int totalAliens;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;

    private int aliensOnScreen = 0;
    private float generatedSpawnTime = .0f;
    private float currentSpawnTime = .0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime >= generatedSpawnTime)
        {
            currentSpawnTime = 0;
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
            {
                var previousSpawnLocations = new List<int>();

                if (aliensPerSpawn > spawnPoints.Length)
                {
                    aliensPerSpawn = spawnPoints.Length - 1;
                }

                aliensPerSpawn = aliensPerSpawn > totalAliens ? aliensPerSpawn - totalAliens : aliensPerSpawn;

                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    if (aliensOnScreen < maxAlentsOnScreen)
                    {
                        aliensOnScreen += 1;

                        int spawnPoint = -1;
                        while (spawnPoint == -1)
                        {
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1);

                            if (!previousSpawnLocations.Contains(randomNumber))
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }

                        var spawnLocation = spawnPoints[spawnPoint];
                        var newAlien = Instantiate(alien);
                        newAlien.transform.position = spawnLocation.transform.position;
                    }
                }
            }
        }
	}
}
