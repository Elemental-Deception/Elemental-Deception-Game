using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatLogic : MonoBehaviour
{
    public Image HealthBar;
    public Image ManaBar;
    public Image XPBar;
    public double ManaFactor;
    private class CharacterStats
    {
        public float Health { get; set; } = 100;
        public float MaxHealth { get; set; } = 100;
        public float Mana { get; set; } = 100;
        public float MaxMana { get; set; } = 100;
        public float ManaRegenSpeed { get; set; } = 1;
        public float XP { get; set; } = 0;
        public float MaxXP { get; set; } = 100;
        public float Level { get; set; } = 1;
    }

    private CharacterStats characterStats = new CharacterStats();
    private Animator animator;
    private bool isDead = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        double ManaDelta = ManaFactor * characterStats.ManaRegenSpeed * Time.deltaTime;
        GainMana(ManaDelta);
    }

    public void TakeDmg(int damage)
    {
        characterStats.Health -= damage;
        animator.SetTrigger("Hurt");
        if (characterStats.Health <= 0 && !isDead)
        {
            HealthBar.fillAmount = 0f;
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
        }
        else
        {
            HealthBar.fillAmount = characterStats.Health / characterStats.MaxHealth;
        }
    }

    public bool SpendMana(int manaCost)
    {
        if (characterStats.Mana - manaCost >= 0 && !isDead)
        {
            characterStats.Mana -= manaCost;
            ManaBar.fillAmount = characterStats.Mana / characterStats.MaxMana;
            return true;
        }
        else
        {
            Debug.Log("Not Enough Mana!");
            return false;
        }
    }

    public float GetHealth()
    {
        return characterStats.Health;
    }

    public float GetMana(double ManaDelta)
    {
        return characterStats.Mana;
    }

    public float GetXP(float XPDelta)
    {
        return characterStats.XP;
    }

    public void GainHealth(float HealthDelta)
    {
        if(!isDead && (HealthBar.fillAmount < 1f))
        {
            if(((characterStats.Health + (float)HealthDelta) / characterStats.MaxHealth) <= 1f)
            {
                characterStats.Health += (float)HealthDelta;
            }
            else
            {
                characterStats.Health += (characterStats.MaxHealth - characterStats.Health);
            }
            HealthBar.fillAmount = characterStats.Health / characterStats.MaxHealth;
        }
    }

    public void GainMana(double ManaDelta)
    {
        if(!isDead && (ManaBar.fillAmount < 1f))
        {
            if(((characterStats.Mana + (float)ManaDelta) / characterStats.MaxMana) <= 1f)
            {
                characterStats.Mana += (float)ManaDelta;
            }
            else
            {
                characterStats.Mana += (characterStats.MaxMana - characterStats.Mana);
            }
            ManaBar.fillAmount = characterStats.Mana / characterStats.MaxMana;
        }
    }

    public void GainXP(float XPDelta)
    {
        if(!isDead && (XPBar.fillAmount < 1f))
        {
            if(((characterStats.XP + (float)XPDelta) / characterStats.MaxXP) <= 1f)
            {
                characterStats.XP += (float)XPDelta;
            }
            else
            {
                characterStats.XP += (characterStats.MaxXP - characterStats.XP);
            }
            XPBar.fillAmount = characterStats.XP / characterStats.MaxXP;
        }
    }
}