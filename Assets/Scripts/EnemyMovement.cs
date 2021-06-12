using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check every now and then, not every frame


public class EnemyMovement : MonoBehaviour
{
    Vector2 targetPos;
    public float moveSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineTarget();


        Movement();
    }

    void Movement()
    {

        Vector2 direction = new Vector2(this.transform.position.x, this.transform.position.y);
        direction = (targetPos - direction).normalized;

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
}
