using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatsSystem : MonoBehaviour
{
    private float Health;
    private float MaxHealth;
    private float Mana;
    private float ManaRegen; 
    private float MaxMana;
    private float XP;
    private float MaxXP;
    private int Level;
    private int DX;
    private int DY;
    private string PlayerElement;

    public void Update(){
        PlayerPrefs.SetFloat("Health", this.Health);
        PlayerPrefs.SetFloat("MaxHealth",this.MaxHealth);
        PlayerPrefs.SetFloat("Mana",this.Mana);
        PlayerPrefs.SetFloat("ManaRegen",this.ManaRegen);
        PlayerPrefs.SetFloat("MaxMana",this.MaxMana);
        PlayerPrefs.SetFloat("XP",this.XP);
        PlayerPrefs.SetFloat("MaxXP",this.MaxXP);
        PlayerPrefs.SetInt("Level",this.Level);
        PlayerPrefs.SetInt("DX", this.DX);
        PlayerPrefs.SetInt("DY", this.DY);
        PlayerPrefs.SetString("PlayerElement", this.PlayerElement);
    }

    //Getters
    public float getHealth(){
        return PlayerPrefs.GetFloat("Health");
    }
    public float getMaxHealth(){
        return PlayerPrefs.GetFloat("MaxHealth");
    }
    public float getMana(){
        return PlayerPrefs.GetFloat("Mana");
    }
    public float getManaRegen(){
        return PlayerPrefs.GetFloat("ManaRegen");
    }
    public float getMaxMana(){
        return PlayerPrefs.GetFloat("MaxMana");
    }
    public float getXP(){
        return PlayerPrefs.GetFloat("XP");
    }
    public float getMaxXP(){
        return PlayerPrefs.GetFloat("MaxXP");
    }
    public int getLevel(){
        return PlayerPrefs.GetInt("Level");
    }
    public int getDX(){
        return PlayerPrefs.GetInt("DX");
    }
    public int getDY(){
        return PlayerPrefs.GetInt("DY");
    }
    public string getPlayerElement(){
        return PlayerPrefs.GetString("PlayerElement");
    }
    //Setters
    public void setHealth(float Health){
        PlayerPrefs.SetFloat("Health", Health);
        this.Health = Health;
    }
    public void setMaxHealth(float MaxHealth){
        PlayerPrefs.SetFloat("MaxHealth", MaxHealth);
        this.MaxHealth = MaxHealth;
    }
    public void setMana(float Mana){
        PlayerPrefs.SetFloat("Mana", Mana);
        this.Mana = Mana;
    }
    public void setManaRegen(float ManaRegen){
        PlayerPrefs.SetFloat("ManaRegen", ManaRegen);
        this.ManaRegen = ManaRegen;
    }
    public void setMaxMana(float MaxMana){
        PlayerPrefs.SetFloat("MaxMana", MaxMana);
        this.MaxMana = MaxMana;
    }
    public void setXP(float XP){
        PlayerPrefs.SetFloat("XP", XP);
        this.XP = XP;
    }
    public void setMaxXP(float MaxXP){
        PlayerPrefs.SetFloat("MaxXP", MaxXP);
        this.MaxXP = MaxXP;
    }
    public void setLevel(int Level){
        PlayerPrefs.SetInt("Level", Level);
        this.Level = Level;
    }
    public void setDX(int DX){
        PlayerPrefs.SetInt("DX", DX);
        this.DX = DX;
    }
    public void setDY(int DY){
        PlayerPrefs.SetInt("DY", DY);
        this.DY = DY;
    }
    public void setPlayerElement(string PlayerElement){
        PlayerPrefs.SetString("PlayerElement", PlayerElement);
        this.PlayerElement = PlayerElement;
    }

    //Health Functions:
    public void subHealth(float amount){
        PlayerPrefs.SetFloat("Health", (PlayerPrefs.GetFloat("Health") - amount));
        if(PlayerPrefs.GetFloat("Health") < 0f) PlayerPrefs.SetFloat("Health", 0f);
    }
    public void addHealth(float amount){
        PlayerPrefs.SetFloat("Health", (PlayerPrefs.GetFloat("Health") + amount));
        if(PlayerPrefs.GetFloat("Health") > PlayerPrefs.GetFloat("MaxHealth")) PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("MaxHealth"));
    }
    public float HealthPercent(){
        if(PlayerPrefs.GetFloat("Health") == 0) return 0;
        return (float)PlayerPrefs.GetFloat("Health") / PlayerPrefs.GetFloat("MaxHealth");
    }
    //Mana Functions:
    public void subMana(float amount){
        PlayerPrefs.SetFloat("Mana", (PlayerPrefs.GetFloat("Mana") - amount));
        if(PlayerPrefs.GetFloat("Mana") < 0) PlayerPrefs.SetFloat("Mana", 0);
    }
    public void addMana(float amount){
        PlayerPrefs.SetFloat("Mana", (PlayerPrefs.GetFloat("Mana") + amount));
        if(PlayerPrefs.GetFloat("Mana") > PlayerPrefs.GetFloat("MaxMana")) PlayerPrefs.SetFloat("Mana", PlayerPrefs.GetFloat("MaxMana"));
    }
    public float ManaPercent(){
        if(PlayerPrefs.GetFloat("Mana") == 0) return 0;
        return (float)PlayerPrefs.GetFloat("Mana") / PlayerPrefs.GetFloat("MaxMana");
    }
    //XP Functions:
    public void subXP(float amount){
        PlayerPrefs.SetFloat("XP", (PlayerPrefs.GetFloat("XP") - amount));
        while(PlayerPrefs.GetFloat("XP") < 0){
            PlayerPrefs.SetFloat("XP",  PlayerPrefs.GetFloat("XP") + PlayerPrefs.GetFloat("MaxXP"));
            if(PlayerPrefs.GetFloat("Level") <= 0) break;
            PlayerPrefs.SetFloat("Level", PlayerPrefs.GetFloat("Level") - 1);
        };
    }
    public void addXP(float amount){
        Debug.Log("xp "+ PlayerPrefs.GetFloat("XP"));
        Debug.Log("max "+ PlayerPrefs.GetFloat("MaxXP"));
        Debug.Log("amount" + amount);
        PlayerPrefs.SetFloat("XP", (PlayerPrefs.GetFloat("XP") + amount));
        while(PlayerPrefs.GetFloat("XP") > PlayerPrefs.GetFloat("MaxXP")){
            PlayerPrefs.SetFloat("XP",  PlayerPrefs.GetFloat("XP") - PlayerPrefs.GetFloat("MaxXP"));
            PlayerPrefs.SetFloat("Level", PlayerPrefs.GetFloat("Level") + 1);
        }
    }
    public float XPPercent(){
        if(PlayerPrefs.GetFloat("XP") == 0) return 0;
        return PlayerPrefs.GetFloat("XP") / PlayerPrefs.GetFloat("MaxXP");
    }
    public void resetStats(float Health, float MaxHealth, float Mana, float ManaRegen, float MaxMana, float XP, float MaxXP, int Level, int DX, int DY, string PlayerElement){
        this.Health = Health; 
        this.MaxHealth = MaxHealth;
        this.Mana = Mana;
        this.ManaRegen = ManaRegen;
        this.MaxMana = MaxMana;
        this.XP = XP;
        this.MaxXP = MaxXP;
        this.Level = Level;
        this.DX = DX;
        this.DY = DY;
        this.PlayerElement = PlayerElement;
        
        PlayerPrefs.SetFloat("Health", this.Health);
        PlayerPrefs.SetFloat("MaxHealth", this.MaxHealth);
        PlayerPrefs.SetFloat("Mana", this.Mana);
        PlayerPrefs.SetFloat("ManaRegen", this.ManaRegen);
        PlayerPrefs.SetFloat("MaxMana", this.MaxMana);
        PlayerPrefs.SetFloat("XP", this.XP);
        PlayerPrefs.SetFloat("MaxXP", this.MaxXP);
        PlayerPrefs.SetInt("Level", this.Level);
        PlayerPrefs.SetInt("DX", this.DX);
        PlayerPrefs.SetInt("DY", this.DY);
        PlayerPrefs.SetString("PlayerElement", this.PlayerElement);
    }

}
