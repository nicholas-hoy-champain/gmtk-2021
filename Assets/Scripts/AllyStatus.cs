using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add delay for player's weapon switch

public enum MoveDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    ERROR
}

public class AllyStatus : MonoBehaviour
{
    static int CURRENT_MAX_ID = 0;
    public bool isPlayer = false;
    public bool isShielding;
    public MoveDirection currentDirection = MoveDirection.ERROR;
    int id;

    [SerializeField] GameObject SpearContainer;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.AllyRoster.Add(this.gameObject);
        id = ++CURRENT_MAX_ID;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayer)
        {
            CheckForWeaponSwitch();
        }
        else
        {
            ;
        }
    }

    void CheckForWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player switching stance");
            isShielding = !isShielding;
        }
    }

    public void EndGame()
    {
        Debug.Log("Player Died");
    }

    public void KillAlly()
    {
        Debug.Log("Ally Died");
    }

    public void ChangeDirection(MoveDirection newDirection)
    {
        if(newDirection != currentDirection)
        {
            currentDirection = newDirection;
            switch(newDirection)
            {
                case MoveDirection.UP:
                {
                    SpearContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);      
                    break;
                }
                case MoveDirection.DOWN:
                {
                    SpearContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                    break;
                }
                case MoveDirection.LEFT:
                {
                    SpearContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                    break;
                }
                case MoveDirection.RIGHT:
                {
                    SpearContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    break;
                }
            }
        }
    }
}
