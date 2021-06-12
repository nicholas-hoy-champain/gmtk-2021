using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStance : MonoBehaviour
{
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
