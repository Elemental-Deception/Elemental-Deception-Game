using UnityEngine;
using System.Collections;

public class EnemyFollowPlayer : MonoBehaviour
{

    public Transform player;  // Reference to the player's transform
    public Transform Enemy;
    public float speed = 5.0f;  // Speed at which the enemy moves
    public float followDistance = 5.0f;  // The distance at which the enemy starts following the player.
    public float attackRange = 0.8f;

    private Animator animator;
    private Rigidbody2D rb;

    // Animator parameter names, adjust these to match your setup in the Animator
    private const string isWalkingParam = "IsMoving";

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= followDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
            if (rb.velocity.x > 0.1f)
            {
                flip(true);
            }
            else if (rb.velocity.x < -0.1f)
            {
                flip(false);
            }
            // Set animation state to walking (or whichever animation you want)
            animator.SetBool(isWalkingParam, true);
        }
        else if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else{
            rb.velocity = Vector2.zero;

            // Set animation state to idle or whichever state when the enemy isn't walking
            animator.SetBool(isWalkingParam, false);
        }
    }

    // Optionally, if you want the 2D sprite to face the direction of movement:
    private void flip(bool flipped)
    {
        Enemy.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }
}