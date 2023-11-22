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
    private const string attackParam = "Attack";

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Movement logic
        if (distanceToPlayer <= followDistance && distanceToPlayer > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // Rotate the parent enemy based on the velocity direction
            RotateParentBasedOnVelocity(rb.velocity.x);

            animator.SetBool(isWalkingParam, true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool(isWalkingParam, false);
        }

        // Attack logic
        if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger(attackParam);
        }
        else
        {
            animator.ResetTrigger(attackParam);
        }
    }

    private void RotateParentBasedOnVelocity(float velocityX)
    {
        Transform parentTransform = transform.parent;

        if ((velocityX < 0 && parentTransform.eulerAngles.y != 180) ||
            (velocityX > 0 && parentTransform.eulerAngles.y != 0))
        {
            parentTransform.eulerAngles = new Vector3(0, velocityX < 0 ? 180 : 0, 0);
        }
    }
}
