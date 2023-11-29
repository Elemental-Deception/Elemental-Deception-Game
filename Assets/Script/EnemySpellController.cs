using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySpellController : MonoBehaviour
{
    private Transform player;
    public Transform Enemy;
    private float attackRange = 2f;
    private Animator animator;
    public GameObject attack; // Reference to the Box Collider
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private float distance;
    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = attack.GetComponent<BoxCollider2D>(); // Get the Box Collider component
        audioSource = attack.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange && !isAttacking)
        {
            Attack();
        }

        // Check if the attack animation is playing
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1.0f)
        {
            // Animation is playing
            isAttacking = true;
            boxCollider.enabled = true;
            audioSource.enabled = true;
        }
        else if (isAttacking && stateInfo.normalizedTime >= 1.0f)
        {
            // Animation has finished
            isAttacking = false;
            boxCollider.enabled = false;
            audioSource.enabled = false;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        // Collider will be enabled in the Update method
    }
}
