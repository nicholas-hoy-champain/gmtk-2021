using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    [SerializeField] static GameObject captain = null;

    [SerializeField] GameObject arrowPrefab;
    [Space]
    [SerializeField] Color maxDistanceColor;
    [SerializeField] float maxDesiredDistanceFromPlayer;
    [Space]
    [SerializeField] Color minDistanceColor;
    [SerializeField] float minDesiredDistanceFromPlayer;
    [Space]
    [Space]
    [SerializeField] float baseDurationBetweenArrows;
    [SerializeField] float durationNoise;
    [Space]
    [SerializeField] float moveSpeed;
    bool isAttacking;
    bool waitingForDelay = false;
    float distance;
    Vector2 direction;
    float delayBetweenNextShot;
    

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = maxDistanceColor;
        Gizmos.DrawWireSphere(transform.position, maxDesiredDistanceFromPlayer);

        Gizmos.color = minDistanceColor;
        Gizmos.DrawWireSphere(transform.position, minDesiredDistanceFromPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (captain == null)
        {
            captain = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetermineAction();
        DetermineFacingDirection();
        ExecuteAction();
    }

    void DetermineFacingDirection()
    {
            //if (direction.x > 0.5f)
            //{
            //    currentDir = MoveDirection.RIGHT;
            //}
            //else if (direction.x < -0.5f)
            //{
            //    currentDir = MoveDirection.LEFT;
            //}
            //else if (direction.y > 0.5f)
            //{
            //    currentDir = MoveDirection.UP;
            //}
            //else if (direction.y < -0.5f)
            //{
            //    currentDir = MoveDirection.DOWN;
            //}
            ////Debug.Log("Current Inputdir = " + direction + " Now facing: " + currentDir);
    }

    void DetermineAction()
    {
        distance = Vector2.Distance(transform.position, captain.transform.position);

        if (minDesiredDistanceFromPlayer > distance)
        {
            direction = (captain.transform.position - transform.position).normalized;
            isAttacking = false;
        }
        else if (maxDesiredDistanceFromPlayer < distance)
        {
            direction = (transform.position - captain.transform.position).normalized;
            isAttacking = false;
        }
        else // attack
        {
            isAttacking = true;
        }
    }

    void ExecuteAction()
    {
        if (isAttacking && !waitingForDelay)
        {
            FireArrow();
        }
        else if (!isAttacking)
        {
            this.GetComponent<Rigidbody2D>().velocity = (direction * moveSpeed);
        }
    }
    void FireArrow()
    {
        Debug.Log("Firing a cardinal arrow");
        delayBetweenNextShot = Random.Range(baseDurationBetweenArrows - durationNoise, baseDurationBetweenArrows + durationNoise);

        GameObject.Instantiate(arrowPrefab, this.transform.position, Quaternion.identity);

        StartCoroutine(nameof(WaitForDelay));
    }

    IEnumerator WaitForDelay()
    {
        waitingForDelay = true;
        yield return new WaitForSeconds(delayBetweenNextShot);
        waitingForDelay = false;
    }
}
