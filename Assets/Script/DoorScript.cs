using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public GameObject player;
    public Sprite OpenDoorImage;
    public Sprite CloseDoorImage;
    public float TimeBeforeNextScene;
    public bool PlayerIsAtTheDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerIsAtTheDoor == true)
        {
            StartCoroutine(_OpenDoor());
        }
    }

    public IEnumerator _OpenDoor(){
        transform.GetComponent<SpriteRenderer>().sprite = OpenDoorImage;
        player.SetActive(false);
        yield return new WaitForSeconds(TimeBeforeNextScene);
        transform.GetComponent<SpriteRenderer>().sprite = CloseDoorImage;
        yield return new WaitForSeconds(TimeBeforeNextScene);
        SceneSwitcher();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerIsAtTheDoor = true;
        }
    }

    public void SceneSwitcher()
    {
        if(SceneManager.GetSceneByName("Home").isLoaded){
            SceneManager.LoadScene("Village");
        }
        if(SceneManager.GetSceneByName("Village").isLoaded){
            SceneManager.LoadScene("Home");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIsAtTheDoor = false;
    }
}
