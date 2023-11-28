using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string scene1;
    public string scene2;
    public GameObject player;
    public float TimeBeforeNextScene;
    public bool PlayerIsAtTheTp;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerIsAtTheTp){
            StartCoroutine(SceneSwitcher());
        }    
    }

    public IEnumerator SceneSwitcher()
    {
        yield return new WaitForSeconds(TimeBeforeNextScene);
        if(PlayerIsAtTheTp){
            player.SetActive(false);
            yield return new WaitForSeconds(TimeBeforeNextScene);
            if(SceneManager.GetSceneByName(scene1).isLoaded){
                SceneManager.LoadScene(scene2);
            }
            if(SceneManager.GetSceneByName(scene2).isLoaded){
                SceneManager.LoadScene(scene1);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerIsAtTheTp = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIsAtTheTp = false;
    }
}
