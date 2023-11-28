using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CharacterStatLogic : MonoBehaviour
{
    public Image HealthBar;
    public Image ManaBar;
    public Image XPBar;
    public int sceneChangeTime;
    public string sceneName;
    public TMP_Text LevelText;
    public TMP_Text LevelShadowText;
    public TMP_Text HealthText;
    public TMP_Text HealthShadowText;
    public TMP_Text ManaText;
    public TMP_Text ManaShadowText;
    public TMP_Text XPText;
    public TMP_Text XPShadowText;
    public double ManaFactor;
    public class CharacterStats
    {
        public float DamageMultiplier { get; set; } = 1;
        public float Health { get; set; } = 100;
        public float MaxHealth { get; set; } = 100;
        public float Mana { get; set; } = 100;
        public float MaxMana { get; set; } = 100;
        public float ManaRegenSpeed { get; set; } = 1;
        public float XP { get; set; } = 0;
        public float MaxXP { get; set; } = 100;
        public float Level { get; set; } = 1;
    }

    public CharacterStats characterStats = new CharacterStats();
    private GameObject player;
    public DynamicTextData XPTextData;
    private Vector3 damageVector;
    private Vector3 XPVector;
    private Animator animator;
    private bool isDead = false;
    private float overflow;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        double ManaDelta = ManaFactor * characterStats.ManaRegenSpeed * Time.deltaTime;
        ManaText.SetText((int)(Math.Round(characterStats.Mana)) + "/" + (int)(Math.Round(characterStats.MaxMana)));
        ManaShadowText.SetText((int)(Math.Round(characterStats.Mana)) + "/" + (int)(Math.Round(characterStats.MaxMana)));
        GainMana(ManaDelta);
    }

    public void TakeDmg(int damage)
    {
        characterStats.Health -= damage;
        damageVector = new Vector3(player.transform.position.x, player.transform.position.y + (float)0.5, player.transform.position.z);
        DynamicTextManager.CreateText2D(damageVector, damage.ToString(), DynamicTextManager.defaultData);
        animator.SetTrigger("Hurt");
        if (characterStats.Health <= 0 && !isDead)
        {
            characterStats.Health = 0;
            HealthBar.fillAmount = 0f;
            HealthText.SetText(0 + "/" + (int)(Math.Round(characterStats.MaxHealth)));
            HealthShadowText.SetText(0 + "/" + (int)(Math.Round(characterStats.MaxHealth)));
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
            StartCoroutine(WaitToLoadScene());
        }
        else
        {
            HealthBar.fillAmount = characterStats.Health / characterStats.MaxHealth;
            HealthText.SetText((int)(Math.Round(characterStats.Health)) + "/" + (int)(Math.Round(characterStats.MaxHealth)));
            HealthShadowText.SetText((int)(Math.Round(characterStats.Health)) + "/" + (int)(Math.Round(characterStats.MaxHealth)));
        }
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(sceneChangeTime);
        SceneManager.LoadScene(sceneName);
    }

    public bool SpendMana(int manaCost)
    {
        if (characterStats.Mana - manaCost >= 0 && !isDead)
        {
            characterStats.Mana -= manaCost;
            ManaBar.fillAmount = characterStats.Mana / characterStats.MaxMana;
            ManaText.SetText((int)(Math.Round(characterStats.Mana)) + "/" + (int)(Math.Round(characterStats.MaxMana)));
            ManaShadowText.SetText((int)(Math.Round(characterStats.Mana)) + "/" + (int)(Math.Round(characterStats.MaxMana)));
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
            HealthText.SetText((int)(Math.Round(characterStats.Health)) + "/" + (int)(Math.Round(characterStats.MaxHealth)));
            HealthShadowText.SetText((int)(Math.Round(characterStats.Health)) + "/" + (int)(Math.Round(characterStats.MaxHealth)));
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
        overflow = 0;
        if(!isDead && (XPBar.fillAmount < 1f))
        {
            if(((characterStats.XP + (float)XPDelta) / characterStats.MaxXP) < 1f)
            {
                characterStats.XP += (float)XPDelta;
            }
            else
            {
                overflow = (XPDelta - (characterStats.MaxXP - characterStats.XP));
                LevelUp();
            }
            XPBar.fillAmount = characterStats.XP / characterStats.MaxXP;
            XPText.SetText((int)(Math.Round(characterStats.XP)) + "/" + (int)(Math.Round(characterStats.MaxXP)));
            XPShadowText.SetText((int)(Math.Round(characterStats.XP)) + "/" + (int)(Math.Round(characterStats.MaxXP)));
            if(overflow != 0)
            {
                GainXP(overflow);
            }
        }
    }

    public void LevelUp()
    {
        characterStats.XP = 0;
        XPBar.fillAmount = 0f;
        XPText.SetText((int)(Math.Round(characterStats.XP)) + "/" + (int)(Math.Round(characterStats.MaxXP)));
        XPShadowText.SetText((int)(Math.Round(characterStats.XP)) + "/" + (int)(Math.Round(characterStats.MaxXP)));
        XPVector = new Vector3(player.transform.position.x, player.transform.position.y + (float)0.5, player.transform.position.z);
        DynamicTextManager.CreateText2D(XPVector, "Level Up", XPTextData);
        characterStats.Level++;
        LevelText.SetText("Level: <color=#32CD32>" + (int)(Math.Round(characterStats.Level)) + "</color>");
        LevelShadowText.SetText("Level: " + (int)(Math.Round(characterStats.Level)));
        characterStats.DamageMultiplier = (float)1.2 * characterStats.DamageMultiplier;
        characterStats.MaxXP = (float)1.2 * characterStats.MaxXP;
        characterStats.MaxHealth = (float)1.2 * characterStats.MaxHealth;
        characterStats.MaxMana = (float)1.2 * characterStats.MaxMana;
        characterStats.ManaRegenSpeed = (float)1.2 * characterStats.ManaRegenSpeed;
        characterStats.Health = characterStats.MaxHealth;
        characterStats.Mana = characterStats.MaxMana;
        HealthBar.fillAmount = 1f;
        HealthText.SetText((int)(Math.Round(characterStats.Health)) + "/" + (int)(Math.Round(characterStats.MaxHealth)));
        HealthShadowText.SetText((int)(Math.Round(characterStats.Health)) + "/" + (int)(Math.Round(characterStats.MaxHealth)));
        ManaBar.fillAmount = 1f;
        ManaText.SetText((int)(Math.Round(characterStats.Mana)) + "/" + (int)(Math.Round(characterStats.MaxMana)));
        ManaShadowText.SetText((int)(Math.Round(characterStats.Mana)) + "/" + (int)(Math.Round(characterStats.MaxMana)));
    }
}