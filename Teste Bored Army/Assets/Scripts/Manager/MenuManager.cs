using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject warningPlay;

    public void StartGame()
    {
        if (SettingsParameters.instance.timerInput > 0 && SettingsParameters.instance.timerInput <= 180 || SettingsParameters.instance.respawnInput > 0)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            warningPlay.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void SettingsOn()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SettingsOff()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
