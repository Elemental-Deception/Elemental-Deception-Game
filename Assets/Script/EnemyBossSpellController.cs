using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class EnemyBossSpellController : MonoBehaviour
{
    private enum SpellType { None, Stomp, FireBreath, Spell }

    private Transform player;
    public float attackRange = 4;

    private Animator animator;

    public GameObject stomp;
    public GameObject fireBreath;
    public GameObject spell;
    public GameObject projectilePrefab;
    private BoxCollider2D stompCollider;
    private BoxCollider2D fireBreathCollider;
    private BoxCollider2D spellCollider;

    private bool isCasting = false;

    private float stompCooldown = 10.0f;
    private float fireBreathCooldown = 6.0f;
    private float spellCooldown = 15.0f;
    private float timeSinceLastStomp = 0.0f;
    private float timeSinceLastFireBreath = 0.0f;
    private float timeSinceLastSpell = 0.0f;

    private SpellType currentSpell = SpellType.None;
    private Queue<SpellType> spellQueue = new Queue<SpellType>();

    private void Start()
    {
        animator = GetComponent<Animator>();

        stompCollider = stomp.GetComponent<BoxCollider2D>();
        fireBreathCollider = fireBreath.GetComponent<BoxCollider2D>();
        spellCollider = spell.GetComponent<BoxCollider2D>();

        SetupColliders();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        UpdateCooldowns();

        if (!isCasting && currentSpell == SpellType.None)
        {
            EnqueueSpells();
            TriggerNextSpell();
        }

        CheckSpellAnimationStates();
    }

    private void SetupColliders()
    {
        stompCollider.isTrigger = fireBreathCollider.isTrigger = spellCollider.isTrigger = true;
        stompCollider.enabled = fireBreathCollider.enabled = spellCollider.enabled = false;
    }

    private void UpdateCooldowns()
    {
        if (timeSinceLastStomp <= stompCooldown)
        {
            timeSinceLastStomp += Time.deltaTime;
        }

        if (timeSinceLastFireBreath <= fireBreathCooldown)
        {
            timeSinceLastFireBreath += Time.deltaTime;
        }

        if (timeSinceLastSpell <= spellCooldown)
        {
            timeSinceLastSpell += Time.deltaTime;
        }
    }

    private void EnqueueSpells()
    {
        if (Vector2.Distance(transform.position, player.position) >= attackRange) return;
        // Enqueue spells if their cooldowns have elapsed and not currently casting another spell
        if (!isCasting)
        {
            if (timeSinceLastStomp >= stompCooldown)
            {
                spellQueue.Enqueue(SpellType.Stomp);
                timeSinceLastStomp = 0.0f; // Reset after enqueuing
            }

            if (timeSinceLastFireBreath >= fireBreathCooldown)
            {
                spellQueue.Enqueue(SpellType.FireBreath);
                timeSinceLastFireBreath = 0.0f;
            }

            if (timeSinceLastSpell >= spellCooldown)
            {
                spellQueue.Enqueue(SpellType.Spell);
                timeSinceLastSpell = 0.0f;
            }
        }
    }

    private void TriggerNextSpell()
    {
        if (spellQueue.Count == 0) return;

        currentSpell = spellQueue.Dequeue();
        TriggerSpell(currentSpell);
    }
    private void TriggerSpell(SpellType spellType)
    {
        switch (spellType)
        {
            case SpellType.Stomp:
                animator.SetTrigger("Stomp");
                StartCoroutine(EnableColliderAfterDelay("Stomp",stompCollider, 1.5f));
                break;
            case SpellType.FireBreath:
                animator.SetTrigger("FireBall");
                StartCoroutine(EnableColliderAfterDelay("FireBall", fireBreathCollider, 1.5f));
                break;
            case SpellType.Spell:
                animator.SetTrigger("Spell");
                LaunchProjectile();
                StartCoroutine(EnableColliderAfterDelay("Spell", spellCollider, 1f));
                break;
        }
        isCasting = true;
        animator.SetBool("IsCasting", true);
    }


    private void CheckSpellAnimationStates()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool animationComplete;

        if (isCasting)
        {
            animationComplete = false;
            switch (currentSpell)
            {
                case SpellType.Stomp:
                    if (stateInfo.IsName("Stomp"))
                    {
                        stompCollider.enabled = false;
                        animationComplete = true;
                    }
                    break;
                case SpellType.FireBreath:
                    if (stateInfo.IsName("FireBall"))
                    {
                        fireBreathCollider.enabled = false;
                        animationComplete = true;
                    }
                    break;
                case SpellType.Spell:
                    if (stateInfo.IsName("Spell"))
                    {
                        spellCollider.enabled = false;
                        animationComplete = true;
                    }
                    break;
            }

            if (animationComplete)
            {
                Debug.Log("Coming in here");
                isCasting = false;
                animator.SetBool("IsCasting", false);
                currentSpell = SpellType.None;
            }
            else
            {
                SetupColliders();

            }
        }


        // Trigger the next spell if no spell is currently casting
        if (!isCasting && currentSpell == SpellType.None && spellQueue.Count > 0)
        {
            TriggerNextSpell();
        }
    }
    private void LaunchProjectile()
    {
        float projectileSpeed = 5f; // Adjust the speed as needed
        int numberOfProjectiles = 5; // Number of bullets to spawn

        if (projectilePrefab != null && player != null)
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, spell.transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (player.position - transform.position).normalized;
                    rb.velocity = direction * projectileSpeed;
                }
            }
        }
    }
    private IEnumerator EnableColliderAfterDelay(string spellName, BoxCollider2D collider, float delay)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("waiting a second");
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }
}
