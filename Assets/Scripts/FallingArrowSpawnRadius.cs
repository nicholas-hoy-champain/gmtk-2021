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

    Vector2 BOTTOM_LEFT_OF_MAP = new Vector2(-7.5f, -7.5f);
    Vector2 TOP_RIGHT_OF_MAP = new Vector2(7.5f, 7.5f);

    [HideInInspector] public int currentNumOfArrows = 0;
    float delayBetweenNextShot;
    bool waitingForDelay = false;

    Vector2 currentTargetPosition;


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
        if ((currentNumOfArrows < maxNumOfArrowsAtOneTime) && !waitingForDelay)
        {
            FireArrow();
        }
    }

    void FireArrow()
    {
        Debug.Log("Firing an overhead Arrow");
        delayBetweenNextShot = Random.Range(baseDurationBetweenArrows - durationNoise, baseDurationBetweenArrows + durationNoise);

        do
        {
            currentTargetPosition = (Vector2)this.transform.position + (Random.insideUnitCircle * radiusOfWhereArrowsSpawn);

        } while (
                (currentTargetPosition.x < BOTTOM_LEFT_OF_MAP.x) ||
                (currentTargetPosition.y < BOTTOM_LEFT_OF_MAP.y) ||
                (currentTargetPosition.x > TOP_RIGHT_OF_MAP.x)   ||
                (currentTargetPosition.y > TOP_RIGHT_OF_MAP.y)
                );




        GameObject.Instantiate(overheadArrowPrefab, currentTargetPosition, Quaternion.identity);
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
