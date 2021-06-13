using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsTextDisplay : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI txt = GetComponent<TextMeshProUGUI>();
        txt.text = "Playthrough Statistics\nAllies Lost: " + PlayerPrefs.GetInt("alliesLost") +
"\nPeak Army Size: " + PlayerPrefs.GetInt("peakArmySize") +
"\nEnemies Killed: " + PlayerPrefs.GetInt("enemiesKilled") +
"\nWaves: " + PlayerPrefs.GetInt("waves");
    }
}
