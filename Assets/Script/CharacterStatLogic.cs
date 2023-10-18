using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatLogic: MonoBehaviour
{
    public Image healthBar;
    private float health = 100f;
    private float maxHealth = 100f;
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
            healthBar.fillAmount = 0f;
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
        }
        else
        {
            healthBar.fillAmount = health / maxHealth;
        }
    }

    public float getHealth()
    {
        return health;
    }

}
