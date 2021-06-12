using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    ERROR
}

public class AllyStance : MonoBehaviour
{
    public bool isShielding;
    public MoveDirection currentDirection = MoveDirection.ERROR;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.AllyRoster.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
