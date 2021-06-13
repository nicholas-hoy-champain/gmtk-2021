using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] float timeBeforeStartScreen;

    [Header("Screens")]
    public GameObject start;
    public GameObject options;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBeforeStartScreen > 0)
        {
            timeBeforeStartScreen -= Time.deltaTime;
            if(timeBeforeStartScreen <= 0)
            {
                start.SetActive(true);
            }
        }
    }

    void CloseScreens()
    {
        start.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
    }

    public void ToStart()
    {
        CloseScreens();
        start.SetActive(true);
    }

    public void ToOptions()
    {
        CloseScreens();
        options.SetActive(true);
    }

    public void ToCredits()
    {
        CloseScreens();
        credits.SetActive(true);
    }

    public void LoadTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
