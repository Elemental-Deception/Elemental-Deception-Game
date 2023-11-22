using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Transform player;
    public float speed = 5.0f;
    public float followDistance = 5.0f;
    public float attackRange = 0.8f;

    private Animator animator;
    private Rigidbody2D rb;

    private const string isWalkingParam = "IsMoving";

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Attack logic here
            animator.SetTrigger("Attack");
            rb.velocity = Vector2.zero; // Stop moving when attacking
        }
        else if (distanceToPlayer <= followDistance)
        {
            // Follow logic here
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
            if (rb.velocity.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            animator.SetBool(isWalkingParam, true);
        }
        else
        {
            // Idle logic here
            rb.velocity = Vector2.zero;
            animator.SetBool(isWalkingParam, false);
        }
    }
}

