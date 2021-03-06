using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handle simultaenous input in opposing directions

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float force;
    [SerializeField] float slowingLerpValue;

    AllyCombatStatus stance;
    Vector2 inputDir;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stance = GetComponent<AllyCombatStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckDirection();
    }

    void FixedUpdate()
    {
        ChangeVelocity();
    }

    void CheckDirection()
    {
        if (inputDir.x > 0.5f)
        {
            stance.ChangeDirection(MoveDirection.RIGHT);
        }
        else if (inputDir.x < -0.5f)
        {
            stance.ChangeDirection(MoveDirection.LEFT);
        }
        else if (inputDir.y > 0.5f)
        {
            stance.ChangeDirection(MoveDirection.UP);
        }
        else if (inputDir.y < -0.5f)
        {
            stance.ChangeDirection(MoveDirection.DOWN);
        }

        //Debug.Log("Current Inputdir = " + inputDir + " Now facing: " + stance.currentDirection);
    }

    void CheckInput()
    {
        inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputDir.sqrMagnitude < 0.2f)
        {
            inputDir = Vector2.zero;
        }
        else
        {
            inputDir.Normalize();
        }
    }

    void ChangeVelocity()
    {
        //rb.velocity = inputDir * speed;
        rb.AddForce(inputDir * force);

        if (inputDir == Vector2.zero)
        {
            rb.velocity = rb.velocity.normalized * Mathf.Lerp(rb.velocity.magnitude, 0, slowingLerpValue);
        }
        else if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
