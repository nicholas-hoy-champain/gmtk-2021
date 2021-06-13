using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{

    [SerializeField] AudioSource audsrcEnemyHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (audsrcEnemyHit == null)
                Debug.LogError("AudioSourceBroken");
            else
                audsrcEnemyHit.Play();

            if (collision.GetComponent<EnemySwordsmanMovement>())
                collision.GetComponent<EnemySwordsmanMovement>().Die();
            else
                collision.GetComponent<ArcherMovementAndAttacking>().Die();
        }
    }
}
