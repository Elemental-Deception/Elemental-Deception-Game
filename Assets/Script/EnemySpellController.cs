using UnityEngine;

public class EnemySpellController : MonoBehaviour
{
    private Transform player;
    public float attackRange = 3.0f; // Set your desired attack range

    private Animator animator;
    public GameObject attack; // Reference to the Box Collider
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private float distance;
    private bool isAttacking = false;
    private float attackCooldown = 2.0f; // Cooldown in seconds
    private float timeSinceLastAttack = 0.0f; // Timer to track cooldown

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = attack.GetComponent<BoxCollider2D>(); // Get the Box Collider component
        boxCollider.isTrigger = true; // Set the collider to be a trigger
        boxCollider.enabled = false; // Initially disable the collider

        audioSource = attack.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").transform; // Find the player
    }

    void Update()
    {
        // Update the cooldown timer
        if (timeSinceLastAttack < attackCooldown)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (isAttacking)
        {
            isAttacking = false;
        }

        // Check if the attack animation is finished
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1.0f)
        {
            // Reset animation state
            animator.ResetTrigger("Attack");
            audioSource.enabled = false;
            boxCollider.enabled = false; // Disable collider when animation is done
        }
        else if (stateInfo.IsName("Attack") && stateInfo.normalizedTime <= 1.0f)
        {
            boxCollider.enabled = true; // Enable the collider for the attack
            audioSource.enabled = true;
        }
        else
        {
            // Check if the player is within attack range and cooldown has passed
            if (Vector2.Distance(transform.position, player.position) <= attackRange && !isAttacking && timeSinceLastAttack >= attackCooldown)
            {
                Attack();
            }
            else
            {
                boxCollider.enabled = false; // Disable collider when animation is done
                audioSource.enabled = false;
            }
        }

        void Attack()
        {
            animator.SetTrigger("Attack");
            isAttacking = true; // Set attacking state
            timeSinceLastAttack = 0.0f;
        }
    }
}
