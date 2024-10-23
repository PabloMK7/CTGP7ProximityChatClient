using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{

    public string mainScene = "MainScene";
    public string settingsScene = "SettingsScene";

    public void TogglePage()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == mainScene)
        {
            SceneManager.LoadScene(settingsScene);
        }
        else if (currentScene == settingsScene)
        {
            SceneManager.LoadScene(mainScene);
        }
    }
}
