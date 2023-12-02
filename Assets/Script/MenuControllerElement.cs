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
        statsSystem.resetStats(element);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
