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
        this.Health = 100;
        this.MaxHealth = 100;
        this.Mana = 100;
        this.ManaRegen = 1;
        this.MaxMana = 100;
        this.XP = 0;
        this.MaxXP = 100;
        this.Level = 0;

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
        return Health;
    }
    public int getMaxHealth(){
        return MaxHealth;
    }
    public int getMana(){
        return Mana;
    }
    public int getManaRegen(){
        return ManaRegen;
    }
    public int getMaxMana(){
        return MaxMana;
    }
    public int getXP(){
        return XP;
    }
    public int getMaxXP(){
        return MaxXP;
    }
    public int getLevel(){
        return Level;
    }
    //Setters
    public void setHealth(int Health){
        this.Health = Health;
    }
    public void setMaxHealth(int MaxHealth){
        this.MaxHealth = MaxHealth;
    }
    public void setMana(int Mana){
        this.Mana = Mana;
    }
    public void setManaRegen(int ManaRegen){
        this.ManaRegen = ManaRegen;
    }
    public void setMaxMana(int MaxMana){
        this.MaxMana = MaxMana;
    }
    public void setXP(int XP){
        this.XP = XP;
    }
    public void setMaxXP(int MaxXP){
        this.MaxXP = MaxXP;
    }
    public void setLevel(int Level){
        this.Level = Level;
    }

    //Health Functions:
    public void subHealth(int amount){
        Health -= amount;
        if(Health < 0) Health = 0;
    }
    public void addHealth(int amount){
        Health += amount;
        if(Health > MaxHealth) Health = MaxHealth;
    }
    public float HealthPercent(){
        if(Health == 0) return 0;
        return (float)Health / MaxHealth;
    }
    //Mana Functions:
    public void subMana(int amount){
        Mana -= amount;
        if(Mana < 0) Mana = 0;
    }
    public void addMana(int amount){
        Mana += amount;
        if(Mana > MaxMana) Mana = MaxMana;
    }
    public float ManaPercent(){
        if(Mana == 0) return 0;
        return (float)Mana / MaxMana;
    }
    //XP Functions:
    public void subXP(int amount){
        XP -= amount;
        while(XP < 0){
            XP += MaxXP;
            if(Level <= 0) break;
            Level -=1;
        };
    }
    public void addXP(int amount){
        XP += amount;
        while(XP > MaxXP){
            XP -= MaxXP;
            Level +=1;
        }
    }
    public float XPPercent(){
        if(XP == 0) return 0;
        return (float)XP / MaxXP;
    }
}
