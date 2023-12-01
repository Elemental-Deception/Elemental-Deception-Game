using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuElement : MonoBehaviour
{
    public string sceneName;
    private StatsSystem statsSystem;

    public void Start()
    {
        statsSystem = new StatsSystem();
    }

    public void LoadScene(string element)
    {
        statsSystem.resetStats(100, 100, 100, 1, 100, 0, 100, 1, 0, 0, element);
        Debug.Log(statsSystem.getPlayerElement());
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
