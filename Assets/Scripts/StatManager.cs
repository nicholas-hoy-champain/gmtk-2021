using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static int alliesLost, enemiesKilled, waves;
    private static int peakArmySize;

    // Start is called before the first frame update
    void Start()
    {
        alliesLost = 0; 
        peakArmySize = 0; 
        enemiesKilled = 0; 
        waves = 0;
    }

    public static void PassArmySize(int size)
    {
        peakArmySize = Mathf.Max(peakArmySize, size);
    }

    public static void SaveToPlayePrefs()
    {
        PlayerPrefs.SetInt("alliesLost", alliesLost);
        PlayerPrefs.SetInt("peakArmySize", peakArmySize);
        PlayerPrefs.SetInt("enemiesKilled", enemiesKilled);
        PlayerPrefs.SetInt("waves", waves);
    }
}
