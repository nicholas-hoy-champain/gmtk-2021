using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    Vector2 SCREEN_OFFSET = new Vector2(8.0f, 3.5f);

    Vector2 BOTTOM_LEFT_OF_MAP = new Vector2(-7.5f, -7.5f);
    Vector2 TOP_RIGHT_OF_MAP = new Vector2(7.5f, 7.5f);

    [Space]
    [SerializeField] int currentWave = 0;
    [Space]
    [SerializeField] float baseDelayBetweenSwordsmanSpawns;
    [SerializeField] float baseDelayBetweenArcherSpawns;
    [SerializeField] float swordsmanSpawnDelayNoise;
    [SerializeField] float archerSpawnDelayNoise;
    [Space]
    [SerializeField] int TotalArchersInWave = 5;
    [SerializeField] float newWaveArcherMultiplier;
    [Space]
    [SerializeField] int TotalSwordsmanInWave = 10;
    [SerializeField] float newWaveSwordsmanMultiplier;
    [Space]
    [SerializeField] GameObject swordsmanPrefab;
    [SerializeField] GameObject archerPrefab;


    bool isSwordsmanSpawnDelayed;
    bool isArcherSpawnDelayed;
    public static int currentNumOfEnemies;
    public static int numOfArchersSpawnedThisWave = 0;
    public static int numOfSwordsmenSpawnedThisWave = 0;
    float archerSpawnDelay;
    float swordsmanSpawnDelay;

    Vector2 bottomLeftOfScreen;
    Vector2 topRightOfScreen;

    Vector2 currentSpawnLocation;
    bool isValidSpawnLocation;

    // Update is called once per frame
    void Update()
    {
        if ((numOfSwordsmenSpawnedThisWave < TotalSwordsmanInWave) && !isSwordsmanSpawnDelayed)
        {
            Debug.Log("spawn sword");
            SpawnSwordsman();
        }
        if ((numOfArchersSpawnedThisWave < TotalArchersInWave) && !isArcherSpawnDelayed)
        {
            Debug.Log("spawn archer");
            SpawnArcher();
        }

        if ((numOfSwordsmenSpawnedThisWave == TotalSwordsmanInWave) && (numOfArchersSpawnedThisWave == TotalArchersInWave) && (currentNumOfEnemies == 0))
        {
            // wave done
            Debug.Log("Wave done");

            /*
             * currentWave++;
             * HUDmanager.AnnounceWave(currentWave);
             */
        }
    }

    void SpawnArcher()
    {
        archerSpawnDelay = Random.Range(baseDelayBetweenArcherSpawns - archerSpawnDelayNoise, baseDelayBetweenArcherSpawns + archerSpawnDelayNoise);
        if (archerSpawnDelay < 0.0f)
            archerSpawnDelay = 0.0f;

        DetermineSpawnLocation();

        GameObject.Instantiate(archerPrefab, currentSpawnLocation, Quaternion.identity);
        numOfArchersSpawnedThisWave++;
        currentNumOfEnemies++;

        StartCoroutine(nameof(ArcherWaitForDelay));
    }

    void SpawnSwordsman()
    {
        swordsmanSpawnDelay = Random.Range(baseDelayBetweenSwordsmanSpawns - swordsmanSpawnDelayNoise, baseDelayBetweenSwordsmanSpawns + swordsmanSpawnDelayNoise);
        if (swordsmanSpawnDelay < 0.0f)
            swordsmanSpawnDelay = 0.0f;
        
        DetermineSpawnLocation();

        GameObject.Instantiate(swordsmanPrefab, currentSpawnLocation, Quaternion.identity);
        currentNumOfEnemies++;
        numOfSwordsmenSpawnedThisWave++;

        StartCoroutine(nameof(SwordsmanWaitForDelay));
    }

    void DetermineSpawnLocation()
    {
        do
        {
            isValidSpawnLocation = true;

            currentSpawnLocation.x = Random.Range(BOTTOM_LEFT_OF_MAP.x, TOP_RIGHT_OF_MAP.x);
            currentSpawnLocation.y = Random.Range(BOTTOM_LEFT_OF_MAP.y, TOP_RIGHT_OF_MAP.y);


            bottomLeftOfScreen = ((Vector2)transform.position - SCREEN_OFFSET);
            topRightOfScreen = ((Vector2)transform.position + SCREEN_OFFSET);

            if (
                (currentSpawnLocation.x > bottomLeftOfScreen.x) &&  
                (currentSpawnLocation.y > bottomLeftOfScreen.y) &&   
                (currentSpawnLocation.x < topRightOfScreen.x  ) &&   
                (currentSpawnLocation.y < topRightOfScreen.y  )      
                )
                isValidSpawnLocation = false;

        } while (!isValidSpawnLocation);
    }

    IEnumerator ArcherWaitForDelay()
    {
        isArcherSpawnDelayed = true;
        yield return new WaitForSeconds(archerSpawnDelay);
        isArcherSpawnDelayed = false;
    }

    IEnumerator SwordsmanWaitForDelay()
    {
        isSwordsmanSpawnDelayed = true;
        yield return new WaitForSeconds(swordsmanSpawnDelay);
        isSwordsmanSpawnDelayed = false;
    }
}


