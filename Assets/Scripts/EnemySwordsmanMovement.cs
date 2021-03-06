using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check every now and then, not every frame


public class EnemySwordsmanMovement : MonoBehaviour
{
    Vector2 targetPos;
    public float moveSpeed;
    Rigidbody2D rb;
    public MoveDirection currentDir;
    Vector2 direction;
    public float secondsToWaitForKilling;

    bool isMoving = true;
    bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineTarget();
        DetermineFacingDirection();
        
        if (isMoving)
            Movement();
    }

    void DetermineFacingDirection()
    {
        if (direction.x > 0.5f)
        {
            currentDir = MoveDirection.RIGHT;
        }
        else if (direction.x < -0.5f)
        {
            currentDir = MoveDirection.LEFT;
        }
        else if (direction.y > 0.5f)
        {
            currentDir = MoveDirection.UP;
        }
        else if (direction.y < -0.5f)
        {
            currentDir = MoveDirection.DOWN;
        }
        //Debug.Log("Current Inputdir = " + direction + " Now facing: " + currentDir);
    }

    void Movement()
    {
        direction = (targetPos - (Vector2)this.transform.position);
        direction = direction.normalized;

        rb.velocity = (direction * moveSpeed);
    }

    void DetermineTarget()
    {
        float currentLow = 10000.0f;
        float currentDis;
        
        foreach(GameObject i in GameManager.AllyRoster)
        {

            currentDis = Vector3.Distance(this.transform.position, i.transform.position);

            if (currentDis < currentLow)
            {
                currentLow = currentDis;
                targetPos = i.transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ally") || collision.gameObject.CompareTag("Player")))
        {
            AllyCombatStatus statusOfAlly = collision.gameObject.GetComponent<AllyCombatStatus>();

            StartCoroutine(nameof(Kill), statusOfAlly);
        }
    }

    IEnumerator Kill(AllyCombatStatus killee)
    {
        isMoving = false;
        

        if (killee.isPlayer)
            killee.DamagePlayer(1);
        else
            killee.DamageAlly(1);
        yield return new WaitForSeconds(secondsToWaitForKilling);

        isMoving = true;
    }

    public void Die()
    {
        if (alive)
        {
            alive = false;
            GameObject.Destroy(this.gameObject);
            StatManager.enemiesKilled++;
            EnemySpawnManager.currentNumOfEnemies--;
        }
    }
}
