using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string scene1; 
    public string scene2;
    public GameObject player;
    public Sprite OpenDoorImage;
    public Sprite CloseDoorImage;
    public float TimeBeforeNextScene;
    public Animator animator;
    public bool PlayerIsAtTheDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerIsAtTheDoor == true)
        {
            player.SetActive(false);
            StartCoroutine(_OpenDoor());
        }
    }

    public IEnumerator _OpenDoor(){
        transform.GetComponent<SpriteRenderer>().sprite = OpenDoorImage;
        yield return new WaitForSeconds(TimeBeforeNextScene);
        transform.GetComponent<SpriteRenderer>().sprite = CloseDoorImage;
        animator.SetBool("IsTeleporting", true);
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
        if(SceneManager.GetSceneByName(scene1).isLoaded){
            SceneManager.LoadScene(scene2);
        }
        if(SceneManager.GetSceneByName(scene2).isLoaded){
            SceneManager.LoadScene(scene1);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIsAtTheDoor = false;
        animator.SetBool("IsTeleporting", false);
    }
}
