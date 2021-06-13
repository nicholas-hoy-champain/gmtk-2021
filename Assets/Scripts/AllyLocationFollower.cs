using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyLocationFollower : MonoBehaviour
{
    [SerializeField] Transform player;
    GameManager gameManager;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 intendedOffset;

    Rigidbody2D rb;

    [SerializeField] float maxSpeed;
    [SerializeField] bool needsToApproach = false;
    [SerializeField] float delayPerUnitOfDistance;
    [SerializeField] float timeRealizing = 0;
    [SerializeField] float timeRealizingStart = 0;

    float reactionBoon;

    float speed = 0;

    public bool isMoving = true;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        reactionBoon = Random.Range(.9f, 1.1f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            rb.velocity = Vector2.zero;
            return;
        }


        //Intended Offset
        if(gameManager.allyFolder.transform.childCount <= 5)
        {
            switch(player.GetComponent<AllyCombatStatus>().currentDirection)
            {
                case MoveDirection.UP:
                    intendedOffset = offset;
                    break;
                case MoveDirection.LEFT:
                    intendedOffset = new Vector3(-offset.y, offset.x);
                    break;
                case MoveDirection.RIGHT:
                    intendedOffset = new Vector3(offset.y, -offset.x);
                    break;
                case MoveDirection.DOWN:
                    intendedOffset = -offset;
                    break;
            }
        }
        else
        {
            intendedOffset = offset;
        }

        // Test for distance to
        Vector3 idealPosition = player.transform.position + intendedOffset;
        float distance = (idealPosition - transform.position).magnitude;


        float accecptableDistance = 0.025f;



        if(!needsToApproach && distance > accecptableDistance)
        {
            timeRealizing = delayPerUnitOfDistance * (transform.position - player.transform.position).magnitude * reactionBoon;
            timeRealizingStart = timeRealizing;
            if (timeRealizingStart == 0) timeRealizingStart = 1;
            needsToApproach = true;
        }
        else if(needsToApproach && distance < accecptableDistance && player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < .1f)
        {
            needsToApproach = false;
            rb.velocity = Vector2.zero;
        }
        else if(needsToApproach)
        {
            if(timeRealizing >= 0)
            {
                timeRealizing -= Time.deltaTime;
            }

            if(timeRealizing < timeRealizingStart * .5f && player.GetComponent<AllyCombatStatus>().currentDirection != GetComponent<AllyCombatStatus>().currentDirection)
            {
                timeRealizing = Mathf.Min(timeRealizing + Time.deltaTime * 2, timeRealizingStart * .5f);
            }

            speed = Mathf.Lerp(maxSpeed, 0, timeRealizing / timeRealizingStart);

            //rb.velocity = (idealPosition - transform.position).normalized * speed;
            //
            //if(distance < speed * Time.deltaTime)
            //{
            //    rb.velocity = Vector2.zero;
            //    transform.position = idealPosition;
            //}
        }


    }

    private void FixedUpdate()
    {
        if (needsToApproach)
        {
            Vector3 idealPosition = player.transform.position + intendedOffset;
            float distance = (idealPosition - transform.position).magnitude;

            rb.velocity = (idealPosition - transform.position).normalized * speed;

            if (distance < speed * Time.fixedDeltaTime && player.GetComponent<AllyCombatStatus>().currentDirection == GetComponent<AllyCombatStatus>().currentDirection)
            {
                rb.velocity = Vector2.zero;
                transform.position = idealPosition;
            }
        }
    }

    public void RetrieveOffset()
    {
        offset = gameManager.offsets[transform.GetSiblingIndex()];
    }

    public void StopMoving()
    {
        Debug.Log("Ally stopped moving");
        isMoving = false;
    }
}