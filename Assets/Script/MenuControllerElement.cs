using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuElement : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
