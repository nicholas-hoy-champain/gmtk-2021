using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject[] pauseScreens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                OpenPause();
            }
            else
            {
                ClosePause();
            }
        }
    }

    void CloseAllScreens()
    {
        for(int i = 0; i < pauseScreens.Length; i++)
        {
            pauseScreens[i].SetActive(false);
        }
    }

    void OpenPause()
    {
        Time.timeScale = 0;
        CloseAllScreens();
        pauseScreens[0].SetActive(true);
    }

    public void ClosePause()
    {
        CloseAllScreens();
        Time.timeScale = 1;
    }

    public void OpenScreen(int i)
    {
        CloseAllScreens();
        pauseScreens[i].SetActive(true);
    }

}
