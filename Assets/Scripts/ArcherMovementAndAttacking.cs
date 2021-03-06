using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovementAndAttacking : MonoBehaviour
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
    [SerializeField] MoveDirection facingDirection;
    bool alive = true;

    Animator anim;

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
        anim = GetComponent<Animator>();
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
        if (direction.x > 0.5f)
        {
            facingDirection = MoveDirection.RIGHT;
        }
        else if (direction.x < -0.5f)
        {
            facingDirection = MoveDirection.LEFT;
        }
        else if (direction.y > 0.5f)
        {
            facingDirection = MoveDirection.UP;
        }
        else if (direction.y < -0.5f)
        {
            facingDirection = MoveDirection.DOWN;
        }

        if (isAttacking)
            direction = (captain.transform.position - transform.position).normalized;
        //Debug.Log("Current Inputdir = " + direction + " Now facing: " + currentDir);

        anim.SetFloat("faceX", direction.x);
        anim.SetFloat("faceY", direction.y);
    }

    void DetermineAction()
    {
        distance = Vector2.Distance(transform.position, captain.transform.position);

        if (distance < minDesiredDistanceFromPlayer)
        {
            direction = (transform.position - captain.transform.position).normalized;
            isAttacking = false;
            //Debug.Log("Getting further away");
        }
        else if (distance > maxDesiredDistanceFromPlayer)
        {
            direction = (captain.transform.position - transform.position).normalized;
            isAttacking = false;
            //Debug.Log("Getting closer");

        }
        else // attack
        {
            //Debug.Log("Attacking");

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
        delayBetweenNextShot = Random.Range(baseDurationBetweenArrows - durationNoise, baseDurationBetweenArrows + durationNoise);

        GameObject theArrow = GameObject.Instantiate(arrowPrefab, this.transform.position, Quaternion.identity);
        theArrow.GetComponent<CardinalArrow>().ChangeDirection(facingDirection);

        StartCoroutine(nameof(WaitForDelay));
    }

    IEnumerator WaitForDelay()
    {
        waitingForDelay = true;
        yield return new WaitForSeconds(delayBetweenNextShot);
        waitingForDelay = false;
    }

    public void Die()
    {
        if(alive)
        {
            alive = false;
            GameObject.Destroy(this.gameObject);
            StatManager.enemiesKilled++;
            EnemySpawnManager.currentNumOfEnemies--;
        }
    }
}
