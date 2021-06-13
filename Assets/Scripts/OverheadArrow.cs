using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadArrow : MonoBehaviour
{
    [SerializeField] float initialTimeBeforeImpact;
    [SerializeField] AudioSource audsrcArrowBlocked;
    [SerializeField] Sprite arrow;

    Collider2D contactCollider;

    float durationOfImpact = 0.05f;
    float durationToDecorateGround = 10.0f;
    SpriteRenderer sprRndrer;

    [HideInInspector] static public FallingArrowSpawnRadius overheadArrowManager;


    // Start is called before the first frame update
    void Start()
    {
        contactCollider = this.gameObject.GetComponent<Collider2D>();
        sprRndrer = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(nameof(TelegraphHit));
    }

    IEnumerator TelegraphHit()
    {
        float progress = 0.0f;

        Color initialColor = sprRndrer.color;
        Color finalColor = initialColor;

        initialColor.a = 0;

        while (progress<initialTimeBeforeImpact)
        {
            sprRndrer.color = Color.Lerp(initialColor, finalColor, progress/initialTimeBeforeImpact);
            progress += Time.deltaTime;
            yield return null;
        }

        sprRndrer.color = finalColor;

        StartCoroutine(nameof(Impact));
    }

    IEnumerator Impact()
    {
        contactCollider.enabled = true;
        this.transform.transform.localScale /= 2.0f;
        yield return new WaitForSeconds(durationOfImpact);


        StartCoroutine(nameof(ChangeToDecoration));
    }

    IEnumerator ChangeToDecoration()
    {
        contactCollider.enabled = false;
        
        this.transform.transform.localScale *= 5.0f;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = arrow;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        overheadArrowManager.currentNumOfArrows--;

        yield return new WaitForSeconds(durationToDecorateGround);
        GameObject.Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ally"))
        {
            AllyCombatStatus statusOfAlly = collision.gameObject.GetComponent<AllyCombatStatus>();

            if (!statusOfAlly.isShielding)
            {
                //Debug.Log("ARROW HIT");
                if (statusOfAlly.isPlayer)
                    statusOfAlly.DamagePlayer(2);
                else
                    statusOfAlly.DamageAlly(2);
            }
            else
                audsrcArrowBlocked.Play();
        }
    }
}
