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

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

        intendedOffset = offset;

        // Test for distance to
        Vector3 idealPosition = player.transform.position + intendedOffset;
        float distance = (idealPosition - transform.position).magnitude;

        float speed = 0;

        if(!needsToApproach && distance > 0.05f)
        {
            timeRealizing = delayPerUnitOfDistance * (transform.position - player.transform.position).magnitude;
            timeRealizingStart = timeRealizing;
            needsToApproach = true;
        }
        else if(needsToApproach && distance < 0.05f)
        {
            needsToApproach = false;
            rb.velocity = Vector2.zero;
        }
        else if(needsToApproach)
        {
            if(timeRealizing > 0)
            {
                timeRealizing -= Time.deltaTime;
            }
            speed = Mathf.Lerp(maxSpeed, 0, timeRealizing / timeRealizingStart);

            rb.velocity = (idealPosition - transform.position).normalized * speed;
        }


    }

    public void RetrieveOffset()
    {
        //Debug.Log(transform.parent.childCount - transform.GetSiblingIndex() - 1);
        offset = gameManager.offsets[transform.parent.childCount - 1 - transform.GetSiblingIndex()];
        offset = gameManager.offsets[transform.GetSiblingIndex()];
        //Debug.Log(offset);
        //.allyFolder.transform.GetChild()
    }
}