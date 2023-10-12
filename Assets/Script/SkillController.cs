using System.Collections;
using UnityEngine;

public class SpellCastingController : MonoBehaviour
{
    public Animator playerAnimator; // Animator for the player's animations
    public Animator spellAnimator;  // Animator for the spell's animations
    private bool isCasting = false;
    private string currentCastingAnimation = ""; // To keep track of the current casting animation name

    private void Update()
    {
        // If the player is casting, don't check for new spell input.
        if (isCasting) return;

        // Check which spell is currently pressed and initiate the player's casting animation.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isCasting = true;
            currentCastingAnimation = "1";
            playerAnimator.SetTrigger("Cast0" + currentCastingAnimation);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            isCasting = true;
            currentCastingAnimation = "2";
            playerAnimator.SetTrigger("Cast0" + currentCastingAnimation);
        }
        else if (Input.GetKeyDown(KeyCode.C))
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