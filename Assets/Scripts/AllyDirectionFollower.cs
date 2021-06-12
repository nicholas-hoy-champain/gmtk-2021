using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDirectionFollower : MonoBehaviour
{
    [SerializeField] float delayPerUnitOfDistance;
    [SerializeField] bool needsToTurn = false;
    [SerializeField] float timeRealizing = 0;
    float timeRealizingStart = 0;
    float reactionBoon;

    GameObject player;
    AllyCombatStatus allyCombatStatus;

    // Start is called before the first frame update
    void Start()
    {
        reactionBoon = Random.Range(.9f, 1.1f);
        player = GameObject.FindGameObjectWithTag("Player");
        allyCombatStatus = GetComponent<AllyCombatStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!needsToTurn && player.GetComponent<AllyCombatStatus>().currentDirection != allyCombatStatus.currentDirection)
        {
            timeRealizing = delayPerUnitOfDistance * (transform.position - player.transform.position).magnitude * reactionBoon;
            timeRealizingStart = timeRealizing;
            needsToTurn = true;
        }
        else if (needsToTurn)
        {
            if (timeRealizing > 0)
            {
                timeRealizing -= Time.deltaTime;
                if(timeRealizing <= 0)
                {
                    allyCombatStatus.ChangeDirection(player.GetComponent<AllyCombatStatus>().currentDirection);
                    needsToTurn = false;
                }
            }
        }
    }
}
