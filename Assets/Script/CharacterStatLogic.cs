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
    public string Element;
    public double ManaFactor;
    public class DamageSystem
    {
        public float DamageMultiplier { get; set; } = 1;
        public float FireMultiplier { get; set; } = 1;
        public float WaterMultiplier { get; set; } = 1;
        public float EarthMultiplier { get; set; } = 1;
    }

    public DamageSystem damageSystem = new DamageSystem();
    private GameObject player;
    private GameObject teleporter;
    private Teleport teleporterScript;
    public DynamicTextData XPTextData;
    private Vector3 damageVector;
    private Vector3 XPVector;
    private Animator animator;
    private bool isDead = false;
    private float overflow;
    private int KillCount = 0;

    private StatsSystem statsSystem;

    public void Start()
    {
        statsSystem = new StatsSystem();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        teleporter = GameObject.Find("PF Props Altar");
        teleporterScript = teleporter.GetComponent<Teleport>();
        ChooseElement(Element);
    }
    private void Update(){
        HealthBar.fillAmount = statsSystem.HealthPercent();
        HealthText.SetText((int)(statsSystem.getHealth()) + "/" + (int)(statsSystem.getMaxHealth()));
        HealthShadowText.SetText((int)(statsSystem.getHealth()) + "/" + (int)(statsSystem.getMaxHealth()));
        ManaBar.fillAmount = statsSystem.ManaPercent();
        ManaText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMaxMana()));
        ManaShadowText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMaxMana()));
        XPBar.fillAmount = statsSystem.XPPercent();
        XPText.SetText((int)(statsSystem.getXP()) + "/" + (int)(statsSystem.getMaxXP()));
        XPShadowText.SetText((int)(statsSystem.getXP()) + "/" + (int)(statsSystem.getMaxXP()));
    }
    private void FixedUpdate()
    {
        double ManaDelta = ManaFactor * statsSystem.getManaRegen() * Time.deltaTime;
        ManaText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMaxMana()));
        ManaShadowText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMaxMana()));
        GainMana(ManaDelta);
    }

    public void TakeDmg(int damage)
    {
        statsSystem.subHealth(damage);
        damageVector = new Vector3(player.transform.position.x, player.transform.position.y + (float)0.5, player.transform.position.z);
        DynamicTextManager.CreateText2D(damageVector, damage.ToString(), DynamicTextManager.defaultData);
        animator.SetTrigger("Hurt");
        HealthBar.fillAmount = statsSystem.HealthPercent();
        HealthText.SetText((int)(statsSystem.getHealth()) + "/" + (int)(statsSystem.getMaxHealth()));
        HealthShadowText.SetText((int)(statsSystem.getHealth()) + "/" + (int)(statsSystem.getMaxHealth()));
        if (statsSystem.getHealth() <= 0 && !isDead)
        {
            animator.SetTrigger("Dead");  // Assuming you have a trigger named "Die" to play death animation
            animator.SetBool("IsAlive", false);
            isDead = true; // Mark character as dead
            StartCoroutine(WaitToLoadScene());
        }
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(sceneChangeTime);
        SceneManager.LoadScene(sceneName);
        statsSystem.resetStats();
    }

    public bool SpendMana(int manaCost)
    {
        if (statsSystem.getMana() - manaCost >= 0 && !isDead)
        {
            statsSystem.subMana(manaCost);

            ManaBar.fillAmount = statsSystem.ManaPercent();
            ManaText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMana()));
            ManaShadowText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMana()));
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
        return statsSystem.getHealth();
    }

    public float GetMana(double ManaDelta)
    {
        return statsSystem.getMana();
    }

    public float GetXP(float XPDelta)
    {
        return statsSystem.getXP();
    }

    public void GainHealth(float HealthDelta)
    {
        if(!isDead)
        {
            statsSystem.addHealth((int)HealthDelta);
            HealthBar.fillAmount = statsSystem.HealthPercent();
            HealthText.SetText((int)(statsSystem.getHealth()) + "/" + (int)(statsSystem.getMaxHealth()));
            HealthShadowText.SetText((int)(statsSystem.getHealth()) + "/" + (int)(statsSystem.getMaxHealth()));
        }
    }

    public void GainMana(double ManaDelta)
    {
        if(!isDead)
        {
            statsSystem.addMana((int)ManaDelta);
            ManaBar.fillAmount = statsSystem.ManaPercent();
            ManaText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMaxMana()));
            ManaShadowText.SetText((int)(statsSystem.getMana()) + "/" + (int)(statsSystem.getMaxMana()));
        }
    }

    public void GainXP(float XPDelta)
    {
        int Level = statsSystem.getLevel();
        if(!isDead)
        {
            statsSystem.addXP((int)XPDelta);
            KillCount++;
            teleporterScript.CheckIfMeetsCondition(KillCount);
            XPBar.fillAmount = statsSystem.XPPercent();
            XPText.SetText((int)(statsSystem.getXP()) + "/" + (int)(statsSystem.getMaxXP()));
            XPShadowText.SetText((int)(statsSystem.getXP()) + "/" + (int)(statsSystem.getMaxXP()));
        }
        if(Level != statsSystem.getLevel())
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        XPVector = new Vector3(player.transform.position.x, player.transform.position.y + (float)0.5, player.transform.position.z);
        DynamicTextManager.CreateText2D(XPVector, "Level Up", XPTextData);
        LevelText.SetText("Level: <color=#32CD32>" + (int)(statsSystem.getLevel()) + "</color>");
        LevelShadowText.SetText("Level: " + (int)(statsSystem.getLevel()));

        damageSystem.DamageMultiplier = (float)1.2 * damageSystem.DamageMultiplier;

        statsSystem.setMaxHealth((int)((float)1.2 * statsSystem.getMaxHealth()));
        statsSystem.setMaxMana((int)((float)1.2 * statsSystem.getMaxMana()));
        statsSystem.setMaxXP((int)((float)1.2 * statsSystem.getMaxXP()));
        statsSystem.setManaRegen((int)((float)1.2 * statsSystem.getManaRegen()));
        statsSystem.setHealth(statsSystem.getMaxHealth());
        statsSystem.setMana(statsSystem.getMaxMana());

        HealthBar.fillAmount = statsSystem.HealthPercent();
        HealthText.SetText(((int)(statsSystem.getHealth())) + "/" + ((int)(statsSystem.getMaxHealth())));
        HealthShadowText.SetText(((int)(statsSystem.getHealth())) + "/" + ((int)(statsSystem.getMaxHealth())));
        ManaBar.fillAmount = statsSystem.ManaPercent();
        ManaText.SetText(((int)(statsSystem.getMana())) + "/" + ((int)(statsSystem.getMaxMana())));
        ManaShadowText.SetText(((int)(statsSystem.getMana())) + "/" + ((int)(statsSystem.getMaxMana())));
    }
    public void ChooseElement(string Element){
        if(Element == "Fire"){
            damageSystem.FireMultiplier = (float)1;
            damageSystem.WaterMultiplier = (float)0.75;
            damageSystem.EarthMultiplier = (float)1.25;
        }else if(Element == "Water"){
            damageSystem.FireMultiplier = (float)1.25;
            damageSystem.WaterMultiplier = (float)1;
            damageSystem.EarthMultiplier = (float)0.75;
        }else if(Element == "Earth"){
            damageSystem.FireMultiplier = (float)0.75;
            damageSystem.WaterMultiplier = (float)1.25;
            damageSystem.EarthMultiplier = (float)1;
        }else{
            Debug.Log("Not An Element!");
        }
    }
}