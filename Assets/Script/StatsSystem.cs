using UnityEngine;
public class StatsSystem
{
    private int Health;
    private int MaxHealth;
    private int Mana;
    private int ManaRegen; 
    private int MaxMana;
    private int XP;
    private int MaxXP;
    private int Level;

    public StatsSystem(){
        PlayerPrefs.SetInt("Health", 100);
        PlayerPrefs.SetInt("MaxHealth",100);
        PlayerPrefs.SetInt("Mana",100);
        PlayerPrefs.SetInt("ManaRegen",1);
        PlayerPrefs.SetInt("MaxMana",100);
        PlayerPrefs.SetInt("XP",0);
        PlayerPrefs.SetInt("MaxXP",100);
        PlayerPrefs.SetInt("Level",0);
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
    //Setters
    public void setHealth(int Health){
        PlayerPrefs.SetInt("Health", Health);
    }
    public void setMaxHealth(int MaxHealth){
        PlayerPrefs.SetInt("MaxHealth", MaxHealth);
    }
    public void setMana(int Mana){
        PlayerPrefs.SetInt("Mana", Mana);
    }
    public void setManaRegen(int ManaRegen){
        PlayerPrefs.SetInt("ManaRegen", ManaRegen);
    }
    public void setMaxMana(int MaxMana){
        PlayerPrefs.SetInt("MaxMana", MaxMana);
    }
    public void setXP(int XP){
        PlayerPrefs.SetInt("XP", XP);
    }
    public void setMaxXP(int MaxXP){
        PlayerPrefs.SetInt("MaxXP", MaxXP);
    }
    public void setLevel(int Level){
        PlayerPrefs.SetInt("Level", Level);
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
}
