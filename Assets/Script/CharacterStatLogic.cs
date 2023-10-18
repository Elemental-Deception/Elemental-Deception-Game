using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatLogic : MonoBehaviour
{
    public Image healthBar;
    private class CharacterStats
    {
        public float Health { get; set; } = 100;
        public float MaxHealth { get; set; } = 100;
        public float Mana { get; set; } = 100;
        public float MaxMana { get; set; } = 100;
        public float XP { get; set; } = 0;
    }

    private CharacterStats characterStats = new CharacterStats();
    private Animator animator;
    private bool isDead = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDmg(int damage)
    {
        characterStats.Health -= damage;
        animator.SetTrigger("Hurt");
        if (characterStats.Health <= 0 && !isDead)
        {
            healthBar.fillAmount = 0f;
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
        }
        else
        {
            healthBar.fillAmount = characterStats.Health / characterStats.MaxHealth;
        }
    }

    public void SpendMana(int manaCost)
    {
        if (characterStats.Mana - manaCost >= 0 && !isDead)
        {
            characterStats.Mana -= manaCost;
        }
        else
        {
            Debug.Log("Not Enough Mana!");
        }
    }

    public float GetHealth()
    {
        return characterStats.Health;
    }

    public float GetMana()
    {
        return characterStats.Mana;
    }

    public float GetXP()
    {
        return characterStats.XP;
    }
}