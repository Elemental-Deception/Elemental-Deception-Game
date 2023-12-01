using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatsSystem : MonoBehaviour
{
    private int Health;
    private int MaxHealth;
    private int Mana;
    private int ManaRegen; 
    private int MaxMana;
    private int XP;
    private int MaxXP;
    private int Level;
    private int DX;
    private int DY;
    private string PlayerElement;

    public void Update(){
        PlayerPrefs.SetInt("Health", this.Health);
        PlayerPrefs.SetInt("MaxHealth",this.MaxHealth);
        PlayerPrefs.SetInt("Mana",this.Mana);
        PlayerPrefs.SetInt("ManaRegen",this.ManaRegen);
        PlayerPrefs.SetInt("MaxMana",this.MaxMana);
        PlayerPrefs.SetInt("XP",this.XP);
        PlayerPrefs.SetInt("MaxXP",this.MaxXP);
        PlayerPrefs.SetInt("Level",this.Level);
        PlayerPrefs.SetInt("DX", this.DX);
        PlayerPrefs.SetInt("DY", this.DY);
        PlayerPrefs.SetString("PlayerElement", this.PlayerElement);
    }

    //Getters
    public int getHealth(){
        return PlayerPrefs.GetInt("Health");
    }
    public int getMaxHealth(){
        return PlayerPrefs.GetInt("MaxHealth");
    }
    public int getMana(){
        return PlayerPrefs.GetInt("Mana");
    }
    public int getManaRegen(){
        return PlayerPrefs.GetInt("ManaRegen");
    }
    public int getMaxMana(){
        return PlayerPrefs.GetInt("MaxMana");
    }
    public int getXP(){
        return PlayerPrefs.GetInt("XP");
    }
    public int getMaxXP(){
        return PlayerPrefs.GetInt("MaxXP");
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
    public void setHealth(int Health){
        PlayerPrefs.SetInt("Health", Health);
        this.Health = Health;
    }
    public void setMaxHealth(int MaxHealth){
        PlayerPrefs.SetInt("MaxHealth", MaxHealth);
        this.MaxHealth = MaxHealth;
    }
    public void setMana(int Mana){
        PlayerPrefs.SetInt("Mana", Mana);
        this.Mana = Mana;
    }
    public void setManaRegen(int ManaRegen){
        PlayerPrefs.SetInt("ManaRegen", ManaRegen);
        this.ManaRegen = ManaRegen;
    }
    public void setMaxMana(int MaxMana){
        PlayerPrefs.SetInt("MaxMana", MaxMana);
        this.MaxMana = MaxMana;
    }
    public void setXP(int XP){
        PlayerPrefs.SetInt("XP", XP);
        this.XP = XP;
    }
    public void setMaxXP(int MaxXP){
        PlayerPrefs.SetInt("MaxXP", MaxXP);
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
    public void subHealth(int amount){
        PlayerPrefs.SetInt("Health", (PlayerPrefs.GetInt("Health") - amount));
        if(PlayerPrefs.GetInt("Health") < 0) PlayerPrefs.SetInt("Health", 0);
    }
    public void addHealth(int amount){
        PlayerPrefs.SetInt("Health", (PlayerPrefs.GetInt("Health") + amount));
        if(PlayerPrefs.GetInt("Health") > PlayerPrefs.GetInt("MaxHealth")) PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("MaxHealth"));
    }
    public float HealthPercent(){
        if(PlayerPrefs.GetInt("Health") == 0) return 0;
        return (float)PlayerPrefs.GetInt("Health") / PlayerPrefs.GetInt("MaxHealth");
    }
    //Mana Functions:
    public void subMana(int amount){
        PlayerPrefs.SetInt("Mana", (PlayerPrefs.GetInt("Mana") - amount));
        if(PlayerPrefs.GetInt("Mana") < 0) PlayerPrefs.SetInt("Mana", 0);
    }
    public void addMana(int amount){
        PlayerPrefs.SetInt("Mana", (PlayerPrefs.GetInt("Mana") + amount));
        if(PlayerPrefs.GetInt("Mana") > PlayerPrefs.GetInt("MaxMana")) PlayerPrefs.SetInt("Mana", PlayerPrefs.GetInt("MaxMana"));
    }
    public float ManaPercent(){
        if(PlayerPrefs.GetInt("Mana") == 0) return 0;
        return (float)PlayerPrefs.GetInt("Mana") / PlayerPrefs.GetInt("MaxMana");
    }
    //XP Functions:
    public void subXP(int amount){
        PlayerPrefs.SetInt("XP", (PlayerPrefs.GetInt("XP") - amount));
        while(PlayerPrefs.GetInt("XP") < 0){
            PlayerPrefs.SetInt("XP",  PlayerPrefs.GetInt("XP") + PlayerPrefs.GetInt("MaxXP"));
            if(PlayerPrefs.GetInt("Level") <= 0) break;
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") - 1);
        };
    }
    public void addXP(int amount){
        PlayerPrefs.SetInt("XP", (PlayerPrefs.GetInt("XP") + amount));
        while(PlayerPrefs.GetInt("XP") > PlayerPrefs.GetInt("MaxXP")){
            PlayerPrefs.SetInt("XP",  PlayerPrefs.GetInt("XP") - PlayerPrefs.GetInt("MaxXP"));
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
    }
    public float XPPercent(){
        if(PlayerPrefs.GetInt("XP") == 0) return 0;
        return (float)PlayerPrefs.GetInt("XP") / PlayerPrefs.GetInt("MaxXP");
    }
    public void resetStats(int Health, int MaxHealth, int Mana, int ManaRegen, int MaxMana, int XP, int MaxXP, int Level, int DX, int DY, string PlayerElement){
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
        
        PlayerPrefs.SetInt("Health", this.Health);
        PlayerPrefs.SetInt("MaxHealth", this.MaxHealth);
        PlayerPrefs.SetInt("Mana", this.Mana);
        PlayerPrefs.SetInt("ManaRegen", this.ManaRegen);
        PlayerPrefs.SetInt("MaxMana", this.MaxMana);
        PlayerPrefs.SetInt("XP", this.XP);
        PlayerPrefs.SetInt("MaxXP", this.MaxXP);
        PlayerPrefs.SetInt("Level", this.Level);
        PlayerPrefs.SetInt("DX", this.DX);
        PlayerPrefs.SetInt("DY", this.DY);
        PlayerPrefs.SetString("PlayerElement", this.PlayerElement);
    }

}
