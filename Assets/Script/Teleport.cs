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
    public Animator animator;
    public bool HasCondition;
    public bool MeetsCondition;
    public bool IsToFinalVillage;
    public bool DialogueAlreadyTriggered;
    private DialogueTrigger dialogueTrigger;
    private StatsSystem statsSystem;

    void Start()
    {
        statsSystem = new StatsSystem();
        dialogueTrigger = this.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerIsAtTheTp)
        {
            if(!HasCondition)
            {
                animator.SetBool("IsTeleporting", true);
                StartCoroutine(SceneSwitcher());
            }
            else if(HasCondition && MeetsCondition)
            {
                animator.SetBool("IsTeleporting", true);
                StartCoroutine(SceneSwitcher());
            }
            else if(!DialogueAlreadyTriggered)
            {
                DialogueAlreadyTriggered = true;
                dialogueTrigger.TriggerDialogue();
            }
        }
    }

    public void CheckIfMeetsCondition(int killCount)
    {
        if(HasCondition)
        {
            if(!IsToFinalVillage)
            {
                if(killCount >= 2)
                {
                    MeetsCondition = true;
                }
            }
            else
            {
                if(statsSystem.getLevel() >= 10)
                {
                    MeetsCondition = true;
                }
            }
        }
    }

    public IEnumerator SceneSwitcher()
    {
        yield return new WaitForSeconds(TimeBeforeNextScene);
        if(PlayerIsAtTheTp)
        {
            player.SetActive(false);
            yield return new WaitForSeconds(TimeBeforeNextScene);
            if(SceneManager.GetSceneByName(scene1).isLoaded)
            {
                SceneManager.LoadScene(scene2);
            }
            if(SceneManager.GetSceneByName(scene2).isLoaded)
            {
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
        DialogueAlreadyTriggered = false;
        animator.SetBool("IsTeleporting", false);
    }
}
