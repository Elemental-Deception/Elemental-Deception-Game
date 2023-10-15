using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PlayerCharacter : Character
{
    public string deathSceneName;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        base.stats.Add("Health", 100);
        base.stats.Add("Mana", 100);
        base.stats.Add("Speed", 1);
        base.stats.Add("EXP", 0);
        base.stats.Add("Magic Damage", 1);
        base.stats.Add("Magic Speed", 1);

        attacks.Add("Basic Spell", 5);
        attacks.Add("Advanced Spell", 15);
        attacks.Add("Elite Spell", 25);
    }

    // Update is called once per frame
    void Update()
    {
        if(stats["Health"] <= 0)
        {
            PlayerDie();
        }
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("Attack");
        // Send ((attacker damage multiplier) * (attack used's damage))
    }

    public void PlayerHurt()
    {
        animator.SetTrigger("Hurt");
        // Get attack/damage data from attacker
        // Health - ((Attacker's Attack Damage) * (Spell/Attack used's damage))
        if(stats["Health"] <= 0)
        {
            PlayerDie();
        }
    }

    public void PlayerDie()
    {
        animator.SetBool("Dead", true);
        SceneManager.LoadScene(deathSceneName, LoadSceneMode.Single);
    }
}
