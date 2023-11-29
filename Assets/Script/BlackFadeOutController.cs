using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackFadeOutController : MonoBehaviour
{
    public string sceneName;
    public int sceneChangeTime;
    void Start()
    {
        StartCoroutine(WaitToLoadScene());
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(sceneChangeTime);
        SceneManager.LoadScene(sceneName);
    }
}
