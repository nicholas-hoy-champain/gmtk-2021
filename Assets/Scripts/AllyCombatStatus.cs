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


public class AllyCombatStatus : MonoBehaviour
{
    static int CURRENT_MAX_ID = 0;
    public bool isPlayer = false;
    public bool isShielding;
    public MoveDirection currentDirection = MoveDirection.ERROR;
    int id;
    public static Dictionary<MoveDirection, MoveDirection> OppositeDirections = new Dictionary<MoveDirection, MoveDirection>()
    {  { MoveDirection.DOWN, MoveDirection.UP},
       { MoveDirection.UP, MoveDirection.DOWN},
       { MoveDirection.LEFT, MoveDirection.RIGHT},
       { MoveDirection.RIGHT, MoveDirection.LEFT} };


    [SerializeField] GameObject HandContainer;
    [SerializeField] GameObject Spear;
    [SerializeField] GameObject Shield;


    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float endGameMaxHealth;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        GameManager.AllyRoster.Add(this.gameObject);
        id = ++CURRENT_MAX_ID;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayer)
        {
            CheckForPlayerWeaponSwitch();
        }
        else
        {
            ;
        }
    }

    void CheckForPlayerWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StanceSwitch();
        }
    }

    public void StanceSwitch()
    {
        Debug.Log("Ally switching stance");
        if (isShielding)
        {
            Debug.Log("switching to spear");
            Shield.SetActive(false);
            Spear.SetActive(true);
            isShielding = false;
        }
        else
        {
            Debug.Log("switching to shield");
            Shield.SetActive(true);
            Spear.SetActive(false);
            isShielding = true;
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Debug.Log("Player Died");
        }
    }

    public void DamageAlly(int damage)
    {
        health -= damage;
        if (health < 1)
        {
    
            GameManager.AllyRoster.Remove(this.gameObject);
            GameObject.FindObjectOfType<GameManager>().restructure = true;
            GameObject.Destroy(this.gameObject);
        }
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
                    HandContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);      
                    break;
                }
                case MoveDirection.DOWN:
                {
                    HandContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                    break;
                }
                case MoveDirection.LEFT:
                {
                    HandContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                    break;
                }
                case MoveDirection.RIGHT:
                {
                    HandContainer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    break;
                }
            }
        }
    }
}
