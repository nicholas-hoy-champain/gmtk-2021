using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public GameObject player;
    public Image barFill;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");

        FrameSize();
        BarFill();
    }

    // Update is called once per frame
    void Update()
    {
        FrameSize();
        BarFill();
    }

    void FrameSize()
    {
        rect.anchorMax = new Vector2(player.GetComponent<AllyCombatStatus>().maxHealth / player.GetComponent<AllyCombatStatus>().endGameMaxHealth, rect.anchorMax.y);
    }

    void BarFill()
    {
        barFill.fillAmount = player.GetComponent<AllyCombatStatus>().health / player.GetComponent<AllyCombatStatus>().maxHealth;
    }
}
