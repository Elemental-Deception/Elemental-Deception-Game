using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    private GameObject dialogueManager;
    private DialogueTrigger dialogueTrigger;
    private GameObject teleporter;
    private Teleport teleporterScript;


    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager");
        dialogueTrigger = dialogueManager.GetComponent<DialogueTrigger>();
        teleporter = GameObject.Find("PF Props Altar");
        teleporterScript = dialogueManager.GetComponent<Teleport>();
        StartCoroutine(WaitForDialog(1));
    }

    IEnumerator WaitForDialog(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialogueTrigger.TriggerDialogue();
    }

    void Update()
    {
        
    }
}
