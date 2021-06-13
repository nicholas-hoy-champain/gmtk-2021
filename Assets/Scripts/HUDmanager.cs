using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDmanager : MonoBehaviour
{
    public Image shield, spear;
    public TextMeshProUGUI wave, solider;
    public float timeWave;

    public static Image shieldIcon;
    public static Image spearIcon;
    public static TextMeshProUGUI waveNumberText; 
    public static TextMeshProUGUI soliderNumberText;
    public static float timeWaveTextShows;

    int waveNumber;

    // Start is called before the first frame update
    void Start()
    {
        timeWaveTextShows = timeWave;
        shieldIcon = shield;
        spearIcon = spear;
        waveNumberText = wave;
        soliderNumberText = solider;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void ChangeSoliderNumber(int num)
    {
        soliderNumberText.text = "" + num;
    }

    static public void ChangeStanceIcon(bool isShield)
    {
        if(isShield)
        {
            shieldIcon.transform.SetAsLastSibling();
            shieldIcon.color = Color.white;
            spearIcon.color = new Color(1, 1, 1, .5f);
        }
        else
        {
            spearIcon.transform.SetAsLastSibling();
            spearIcon.color = Color.white;
            shieldIcon.color = new Color(1, 1, 1, .5f);
        }
    }

    public void AnnounceWave(int i)
    {
        waveNumber = i;
        StartCoroutine(nameof(MessageTimed));
    }

    IEnumerator MessageTimed()
    {
        waveNumberText.enabled = true;
        waveNumberText.text = "Wave " + waveNumber;
        yield return new WaitForSeconds(timeWaveTextShows);
        waveNumberText.text = "";
        waveNumberText.enabled = false;
    }
}
