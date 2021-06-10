using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public enum Part {Head, Torso, Arm, Leg }
public enum Status {Uninjured, Injured, SeverelyInjured, Disabled }

public class Body
{
    public string name;
    public int hp;
    public int maxHp;
    public Part bodyPart;
    public Equipment equipment;
    public bool disabled;
    int maxEndurance;
    int endurance;
    int offence;
    int defence;
    int strength;
    public Status status;
    public int price;
    public int factor;
    public Body(string name, int maxHp, Part bodyPart, Equipment equipment, int strength, int offence, int defence, int endurance)
    {
        this.name = name;
        this.maxHp = Return.RandomInt(maxHp-1,maxHp+2);
        hp = maxHp;
        this.bodyPart = bodyPart;
        this.equipment = equipment;
        this.endurance = this.maxEndurance = Return.RandomInt(endurance - 1, endurance + 3);
        this.offence = Return.RandomInt(offence - 1, offence +2);
        this.defence = Return.RandomInt(defence - 1, defence +2);
        this.strength = Return.RandomInt(strength- 1 , strength +2);
        factor = (maxHp + Strength + Offence + Defence + Endurance < 8) ? 10 + Return.RandomInt(-3, 3) : (maxHp + Strength + Offence + Defence + Endurance < 15 ) ? 15 + Return.RandomInt(-3, 4): 25 + Return.RandomInt(-4, 5);
        price = (maxHp + Strength + Offence + Defence  + Endurance) * factor;
    }
    public string StatusString()
    {
        if (status == Status.Uninjured) return Color.HEALTH + "Uninjured" + Color.RESET;
        if (status == Status.Injured) return Color.NPC + "Injured" + Color.RESET;
        if (status == Status.SeverelyInjured) return Color.GOLD + "Severely Injured" + Color.RESET;
        return Color.STRENGTH + "Disabled" + Color.RESET;
    }

    public void UpdateStatus()
    {
        if (hp == maxHp) status = Status.Uninjured;
        else if (hp >2 ) status = Status.Injured;
        else if (hp == 1 || hp == 2) status = Status.SeverelyInjured;
        else  status = Status.Disabled;
    }

    public void TakeDamage(int x)
    {
        hp -= x;
        UpdateStatus();
    }
    public void SetHealth(int x)
    {
        hp = x;
        UpdateStatus();
    }



    public void UpdateOffence(int update) 
    {
        offence += update;
        price = (Strength + Offence * 2 + Defence * 2 + Endurance) / 6 * factor;
    }
    public void UpdateDefence(int update)
    {
        defence += update;
        price = (Strength + Offence * 2 + Defence * 2 + Endurance) / 6 * factor;
    }
    public void UpdateStrength(int update)
    {
        strength += update;
        price = (Strength + Offence * 2 + Defence * 2 + Endurance) / 6 * factor;
    }
    public void UpdateEndurance(int update)
    {
        endurance += update;
        price = (Strength + Offence * 2 + Defence * 2 + Endurance) / 6 * factor;
    }

    public int Offence { get { if (hp > 0) if (Endurance > 0) return offence; return 0; } set { offence = value; } }
    public int Defence { get { if (hp > 0) if (Endurance > 0) return defence; return 0; } set { defence = value; } }
    public int Endurance { get { return endurance; } set { endurance = value; } }
    public int Strength { get { return strength; } set { strength = value; } }
}