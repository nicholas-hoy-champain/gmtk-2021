using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float force;

    Vector2 inputDir;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        ChangeVelocity();
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
        if(rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
