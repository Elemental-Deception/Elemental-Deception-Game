using System.Collections;
using UnityEngine;

public class SpellCastingController : MonoBehaviour
{
    public Animator playerAnimator; // Animator for the player's animations
    public Animator spellAnimator;  // Animator for the spell's animations
    public GameObject player;
    public GameObject PauseMenu;
    private PauseMenuController pauseMenuController;
    public GameObject spell; // Assuming this is the spell GameObject
    public Camera cam;

    [SerializeField] private float boxColliderOffset;
    [SerializeField] private float yPositionOffset = 1.0f; // Serialized field to adjust the Y offset in Unity Editor
                                          // 
    private CharacterStatLogic playerLogic;
    private bool isCasting = false;
    private string currentCastingAnimation = ""; // To keep track of the current casting animation name
    private Vector2 originalColliderOffset; // To store the original offset of the collider

    void Start()
    {
        playerLogic = player.GetComponent<CharacterStatLogic>();
        pauseMenuController = PauseMenu.GetComponent<PauseMenuController>();
        BoxCollider2D collider = spell.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            originalColliderOffset = collider.offset; // Store the original offset
        }
        pauseMenuController = PauseMenu.GetComponent<PauseMenuController>();
    }

    private void Update()
    {
        if(pauseMenuController.isPaused)
        {
            return;
        }
        if (isCasting)
        {
            return;
        }
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        mousePos.z = 0; // Set to a fixed Z value appropriate for your game

        if (Input.GetKeyDown(KeyCode.Z) && playerLogic.SpendMana(10))
        {
            StartCasting("1", mousePos);
        }
        else if (Input.GetKeyDown(KeyCode.X) && playerLogic.SpendMana(15))
        {
            StartCasting("2", mousePos);
        }
        else if (Input.GetKeyDown(KeyCode.C) && playerLogic.SpendMana(20))
        {
            StartCasting("3", mousePos);
        }
    }

    private void StartCasting(string animationNumber, Vector3 castPosition)
    {
        isCasting = true;
        currentCastingAnimation = animationNumber;
        playerAnimator.SetTrigger("Cast0" + currentCastingAnimation);

        // Add the specified Y offset to the position
        castPosition.y += yPositionOffset;
        spell.transform.position = castPosition;

        // Adjust the Box Collider's offset based on the original offset
        BoxCollider2D collider = spell.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.offset = new Vector2(0f, -1.5f);
        }
    }

    private void LateUpdate()
    {
        if (isCasting && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentCastingAnimation))
        {
            ResetCollider();
            isCasting = false;
            spellAnimator.SetTrigger("Cast0" + currentCastingAnimation);
            currentCastingAnimation = ""; // Reset the animation name
        }
    }

    private void ResetCollider()
    {
        // Reset the collider's offset to its original position
        BoxCollider2D collider = spell.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.offset = originalColliderOffset;
        }
    }
}
