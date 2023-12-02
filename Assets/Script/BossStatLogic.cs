using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BossStatLogic : MonoBehaviour
{
    private class EnemyStats
    {
        public float Health { get; set; } = 100;
        public float MaxHealth { get; set; } = 100;
    }

    public float EnemyXP;
    public Image healthBar;
    public GameObject enemy;
    public DynamicTextData XPTextData;
    private GameObject player;
    private Vector3 XPVector;
    private Vector3 damageVector;
    private CharacterStatLogic playerLogic;
    private EnemyStats enemyStats = new EnemyStats();
    private Animator animator;
    private bool isDead = false;
    public float DelayAnimation;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerLogic = player.GetComponent<CharacterStatLogic>();
        animator = GetComponent<Animator>();

    }

    public void TakeDmg(int damage)
    {
        float temp;
        Debug.Log(damage);
        

        enemyStats.Health -= damage;
        damageVector = new Vector3(enemy.transform.position.x, enemy.transform.position.y + (float)0.5, enemy.transform.position.z);
        DynamicTextManager.CreateText2D(damageVector, damage.ToString(), DynamicTextManager.defaultData);
        animator.SetTrigger("Hurt");
        Debug.Log("ouch");
        if (enemyStats.Health <= 0)
        {
            healthBar.fillAmount = 0f;
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
            Debug.Log("DEATHHHHHHHHHHHHHHHHHHHHHHHHH");
            DestroyEnemy(DelayAnimation);
        }
        else
        {
            healthBar.fillAmount = enemyStats.Health / enemyStats.MaxHealth;
        }
    }

    public float GetHealth()
    {
        return enemyStats.Health;
    }

    public void DestroyEnemy(float delay)
    {
        playerLogic.GainXP(EnemyXP);
        XPVector = new Vector3(enemy.transform.position.x, enemy.transform.position.y + (float)0.5, enemy.transform.position.z);
        DynamicTextManager.CreateText2D(XPVector, "+" + EnemyXP.ToString() + " XP", XPTextData);
        Destroy(gameObject, delay);
    }

}