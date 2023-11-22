using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatLogic : MonoBehaviour
{
    private class EnemyStats
    {
        public float Health { get; set; } = 100;
        public float MaxHealth { get; set; } = 100;
    }

    public float EnemyXP;
    public Image healthBar;
    public GameObject enemy;
    private GameObject player;
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
        enemyStats.Health -= damage;
        damageVector = new Vector3(enemy.transform.position.x, enemy.transform.position.y + (float)0.5, enemy.transform.position.z);
        DynamicTextManager.CreateText2D(damageVector, damage.ToString(), DynamicTextManager.defaultData);
        animator.SetTrigger("Hurt");
        if (enemyStats.Health <= 0 && !isDead)
        {
            healthBar.fillAmount = 0f;
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
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
        Destroy(gameObject, delay);
    }

}