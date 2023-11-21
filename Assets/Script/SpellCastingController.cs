using System.Collections;
using UnityEngine;

public class SpellCastingController : MonoBehaviour
{
    public Animator playerAnimator; // Animator for the player's animations
    public Animator spellAnimator;  // Animator for the spell's animations
    public GameObject player;
    public GameObject PauseMenu;
    private PauseMenuController pauseMenuController;
    
    private CharacterStatLogic playerLogic;
    private bool isCasting = false;
    private string currentCastingAnimation = ""; // To keep track of the current casting animation name

    void Start()
    {
        playerLogic = player.GetComponent<CharacterStatLogic>();
        pauseMenuController = PauseMenu.GetComponent<PauseMenuController>();
    }
    private void Update()
    {
        if(pauseMenuController.isPaused)
        {
            return;
        }
        // If the player is casting, don't check for new spell input.
        if (isCasting)
        {
            return;
        }
        // Check which spell is currently pressed and initiate the player's casting animation.
        if (Input.GetKeyDown(KeyCode.Z) && playerLogic.SpendMana(10))
        {
            isCasting = true;
            currentCastingAnimation = "1";
            playerAnimator.SetTrigger("Cast0" + currentCastingAnimation);
        }
        else if (Input.GetKeyDown(KeyCode.X) && playerLogic.SpendMana(15))
        {
            isCasting = true;
            currentCastingAnimation = "2";
            playerAnimator.SetTrigger("Cast0" + currentCastingAnimation);
        }
        else if (Input.GetKeyDown(KeyCode.C) && playerLogic.SpendMana(20))
        {
            isCasting = true;
            currentCastingAnimation = "3";
            playerAnimator.SetTrigger("Cast0" + currentCastingAnimation);
        }
    }

    private void LateUpdate()
    {
        // Check if the player's current casting animation is done, then trigger the spell animation.
        if (isCasting && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentCastingAnimation))
        {
            isCasting = false;
            spellAnimator.SetTrigger("Cast0" + currentCastingAnimation);
            currentCastingAnimation = ""; // Reset the animation name
        }
    }
}