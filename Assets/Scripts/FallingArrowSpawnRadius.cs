using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingArrowSpawnRadius : MonoBehaviour
{
    [SerializeField] float radiusOfWhereArrowsSpawn;
    [SerializeField] float baseDurationBetweenArrows;
    [SerializeField] float durationNoise;
    [SerializeField] int maxNumOfArrowsAtOneTime;
    [SerializeField] GameObject overheadArrowPrefab;
    [SerializeField] GameObject player;
    
    
    [HideInInspector] public int currentNumOfArrows = 0;
    float delayBetweenNextShot;
    bool waitingForDelay = false;



    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfWhereArrowsSpawn);
    }

    private void Start()
    {
        OverheadArrow.overheadArrowManager = this;
    }


    // Update is called once per frame
    void Update()
    {
        if((currentNumOfArrows < maxNumOfArrowsAtOneTime) && !waitingForDelay)
        {
            FireArrow();
        }
    }

    void FireArrow()
    {
        Debug.Log("Firing an overhead Arrow");
        delayBetweenNextShot = Random.Range(baseDurationBetweenArrows - durationNoise, baseDurationBetweenArrows + durationNoise);

        GameObject.Instantiate(overheadArrowPrefab, Random.insideUnitCircle * radiusOfWhereArrowsSpawn, Quaternion.identity);
        currentNumOfArrows++;

        StartCoroutine(nameof(WaitForDelay));
    }

    IEnumerator WaitForDelay()
    {
        waitingForDelay = true;
        yield return new WaitForSeconds(delayBetweenNextShot);
        waitingForDelay = false;
    }
}
