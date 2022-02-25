using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject menu;

    public void StartGame() 
    {
        Debug.Log("game started!");
        menu.SetActive(false);
    }

    public void StartTutorial()
    {
        Debug.Log("tutorial started!");
        menu.SetActive(false);

    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("quit game!");
       
    }
}
