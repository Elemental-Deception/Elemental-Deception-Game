using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatLogic: MonoBehaviour
{
    private int health = 100;
    private Animator animator;
    private bool isDead = false;
    public float DelayAnimation;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDmg(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");
        if (health <= 0 && !isDead)
        {
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
        }
    }

    public int getHealth()
    {
        return health;
    }

}
