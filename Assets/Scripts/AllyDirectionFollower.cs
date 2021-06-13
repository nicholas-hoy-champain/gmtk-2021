using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDirectionFollower : MonoBehaviour
{
    [SerializeField] float delayPerUnitOfDistance;
    [SerializeField] bool needsToTurn = false;
    [SerializeField] bool needsToStanceSwap = false;
    [SerializeField] float timeRealizing = 0;
    float timeRealizingStart = 0;

    [SerializeField] float stanceTimeRealizing = 0;
    float stanceTimeRealizingStart = 0;

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
        UpdateTurning();
        UpdateStance();
    }

    void UpdateTurning()
    {
        if (!needsToTurn && player.GetComponent<AllyCombatStatus>().currentDirection != allyCombatStatus.currentDirection)
        {
            timeRealizing = delayPerUnitOfDistance * (transform.position - player.transform.position).magnitude * reactionBoon;
            timeRealizingStart = timeRealizing;
            if (timeRealizingStart == 0) timeRealizingStart = 1;
            needsToTurn = true;
        }
        else if (needsToTurn)
        {
            if (timeRealizing >= 0)
            {
                timeRealizing -= Time.deltaTime;
                if (timeRealizing <= 0)
                {
                    allyCombatStatus.ChangeDirection(player.GetComponent<AllyCombatStatus>().currentDirection);
                    needsToTurn = false;
                }
            }
        }
    }

    void UpdateStance()
    {
        if (!needsToStanceSwap && player.GetComponent<AllyCombatStatus>().isShielding != allyCombatStatus.isShielding)
        {
            stanceTimeRealizing = delayPerUnitOfDistance * (transform.position - player.transform.position).magnitude * reactionBoon;
            stanceTimeRealizingStart = stanceTimeRealizing;
            if (stanceTimeRealizingStart == 0) stanceTimeRealizingStart = 1;
            needsToStanceSwap = true;
        }
        else if (needsToStanceSwap)
        {
            if (stanceTimeRealizing >= 0)
            {
                stanceTimeRealizing -= Time.deltaTime;
                if (stanceTimeRealizing <= 0)
                {
                    allyCombatStatus.StanceSwitch();
                    needsToStanceSwap = false;
                }
            }
        }
    }
}
