using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelManager : MonoBehaviour
{
    [SerializeField] GameObject Panel1;
    [SerializeField] GameObject Panel2;
    [SerializeField] GameObject Panel3;
    [SerializeField] GameObject Panel4;
    [SerializeField] GameObject Panel5;

    public void ChangeToPanel(int newPanel)
    {
        switch (newPanel)
        {
            case 1:
                Panel1.SetActive(true);
                Panel2.SetActive(false);
                Panel3.SetActive(false);
                Panel4.SetActive(false);
                Panel5.SetActive(false);
                break;
            case 2:
                Panel1.SetActive(false);
                Panel2.SetActive(true);
                Panel3.SetActive(false);
                Panel4.SetActive(false);
                Panel5.SetActive(false);
                break;
            case 3:
                Panel1.SetActive(false);
                Panel2.SetActive(false);
                Panel3.SetActive(true);
                Panel4.SetActive(false);
                Panel5.SetActive(false);
                break;
            case 4:
                Panel1.SetActive(false);
                Panel2.SetActive(false);
                Panel3.SetActive(false);
                Panel4.SetActive(true);
                Panel5.SetActive(false);
                break;
            case 5:
                Panel1.SetActive(false);
                Panel2.SetActive(false);
                Panel3.SetActive(false);
                Panel4.SetActive(false);
                Panel5.SetActive(true);
                break;
            default:
                Debug.LogError("BROKEN");
                break;
        }
    }
}
