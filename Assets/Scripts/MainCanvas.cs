using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainCanvas : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject TutorialGamePanel;

    void Start()
    {
        menuPanel.SetActive(true);
        TutorialGamePanel.SetActive(false);
    }

    public void StartGame() 
    {
        Debug.Log("game started!");
        menuPanel.SetActive(false);
    }

    public void StartTutorial()
    {
        Debug.Log("tutorial started!");
        menuPanel.SetActive(false);
        TutorialGamePanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("quit game!");
       
    }
}
