using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalArrow : MonoBehaviour
{

    [SerializeField] float flightSpeed;
    public MoveDirection flightDirection = MoveDirection.LEFT;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-flightSpeed, 0.0f);
    }
}
