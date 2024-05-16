using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Controls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
