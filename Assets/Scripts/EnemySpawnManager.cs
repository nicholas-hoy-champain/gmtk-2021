using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    Vector2 SCREEN_OFFSET = new Vector2(8.0f, 3.5f);

    Vector2 BOTTOM_LEFT_OF_MAP = new Vector2(-7.5f, -7.5f);
    Vector2 TOP_RIGHT_OF_MAP = new Vector2(7.5f, 7.5f);

    [Space]
    [SerializeField] int maxNumOfEnemies;
    [SerializeField] float baseDelayBetweenSpawns;
    [SerializeField] float delayNoise;
    [Space]
    [SerializeField] int minNumOfArchers;
    [SerializeField] int maxNumOfArchers;
    [Space]
    [SerializeField] int minNumOfSwordsmen;
    [SerializeField] int maxNumOfSwordsmen;
    [Space]
    [SerializeField] GameObject swordsmanPrefab;
    [SerializeField] GameObject archerPrefab;


    bool isSpawnDelayed;
    public static int currentNumOfEnemies;
    public static int currentNumOfArchers;
    public static int currentNumOfSwordsmen;
    float currentDelayBeforeNextSpawn;

    Vector2 bottomLeftOfScreen;
    Vector2 topRightOfScreen;

    Vector2 currentSpawnLocation;
    bool isValidSpawnLocation;
    GameObject currentEnemyToSpawn;

    // Update is called once per frame
    void Update()
    {
        if ((currentNumOfEnemies < maxNumOfEnemies) && !isSpawnDelayed)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Debug.Log("Firing an overhead Arrow");
        currentDelayBeforeNextSpawn = Random.Range(baseDelayBetweenSpawns - delayNoise, baseDelayBetweenSpawns + delayNoise);
        if (currentDelayBeforeNextSpawn < 0.0f)
            currentDelayBeforeNextSpawn = 0.0f;
        
        DetermineSpawnLocation();
        DetermineEnemy();

        GameObject.Instantiate(currentEnemyToSpawn, currentSpawnLocation, Quaternion.identity);
        currentNumOfEnemies++;

        StartCoroutine(nameof(WaitForDelay));
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

            Debug.Log("BL" + bottomLeftOfScreen);
            Debug.Log("TR" + topRightOfScreen);

            if (
                (currentSpawnLocation.x > bottomLeftOfScreen.x) &&  
                (currentSpawnLocation.y > bottomLeftOfScreen.y) &&   
                (currentSpawnLocation.x < topRightOfScreen.x  ) &&   
                (currentSpawnLocation.y < topRightOfScreen.y  )      
                )
                isValidSpawnLocation = false;

        } while (!isValidSpawnLocation);
    }

    void DetermineEnemy()
    {
        if (currentNumOfArchers < minNumOfArchers || currentNumOfSwordsmen == maxNumOfSwordsmen)
            currentEnemyToSpawn = archerPrefab;
        else if (currentNumOfSwordsmen < minNumOfSwordsmen || currentNumOfArchers == maxNumOfArchers)
            currentEnemyToSpawn = swordsmanPrefab;
        else if (UnityEngine.Random.Range(0, 2) == 0) 
            currentEnemyToSpawn = archerPrefab;
        else
            currentEnemyToSpawn = swordsmanPrefab;

    }

    IEnumerator WaitForDelay()
    {
        isSpawnDelayed = true;
        yield return new WaitForSeconds(currentDelayBeforeNextSpawn);
        isSpawnDelayed = false;
    }
}
