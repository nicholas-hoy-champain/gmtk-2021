using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalArrow : MonoBehaviour
{

    [SerializeField] float flightSpeed;
    public MoveDirection flightDirection = MoveDirection.LEFT;
    Rigidbody2D rb;
    public float secondsToWaitForKilling;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (flightDirection)
        {
            case MoveDirection.UP:
                rb.velocity = new Vector2(0.0f, flightSpeed);
                break;

            case MoveDirection.DOWN:
                rb.velocity = new Vector2(0.0f, -flightSpeed);
                break;

            case MoveDirection.LEFT:
                rb.velocity = new Vector2(-flightSpeed, 0.0f);
                break;

            case MoveDirection.RIGHT:
                rb.velocity = new Vector2(flightSpeed, 0.0f);
                break;
        }

    }

    public void ChangeDirection(MoveDirection newDir)
    {
        flightDirection = newDir;

        GameObject gmObj = this.gameObject;
        switch (newDir)
        {
            case MoveDirection.UP:
                gmObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                break;

            case MoveDirection.DOWN:
                gmObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;

            case MoveDirection.LEFT:
                gmObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;

            case MoveDirection.RIGHT:
                gmObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ally") || collision.gameObject.CompareTag("Player")))
        {
            AllyCombatStatus statusOfAlly = collision.gameObject.GetComponent<AllyCombatStatus>();

            StartCoroutine(nameof(Kill), statusOfAlly);
        }
    }

    IEnumerator Kill(AllyCombatStatus killee)
    {
        yield return new WaitForSeconds(secondsToWaitForKilling);

        if (killee.isPlayer)
            killee.EndGame();
        else
            killee.KillAlly();

        GameObject.Destroy(this.gameObject);
    }
}
