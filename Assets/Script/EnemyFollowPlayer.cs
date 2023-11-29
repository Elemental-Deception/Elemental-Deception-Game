using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Transform player;
    public float speed = 5.0f;
    public float followDistance = 5.0f;
    public float attackRange = 0.8f;

    private Rigidbody2D rbParent;
    private Animator animator;

    private const string isWalkingParam = "IsMoving";
    private const string attackParam = "Attack";

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        rbParent = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= followDistance && distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }

        if (rbParent.velocity.magnitude < 0.01f)
        {
            rbParent.velocity = new Vector2(0.001f, 0);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rbParent.velocity = direction * speed;

        animator.SetBool(isWalkingParam, true);

        // Flip the parent enemy to face the player
        FlipParentBasedOnDirection(direction.x);
    }

    private void StopMoving()
    {
        rbParent.velocity = Vector2.zero;
        animator.SetBool(isWalkingParam, false);
    }

    private void FlipParentBasedOnDirection(float directionX)
    {
        if ((directionX > 0 && rbParent.transform.localScale.x < 0) ||
            (directionX < 0 && rbParent.transform.localScale.x > 0))
        {
            // Flip the parent by changing its scale
            Vector3 scale = rbParent.transform.localScale;
            scale.x *= -1;
            rbParent.transform.localScale = scale;
        }
    }
}