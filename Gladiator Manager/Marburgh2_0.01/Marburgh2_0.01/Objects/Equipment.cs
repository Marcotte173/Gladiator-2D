using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

public enum EquipmentType {Armor, Weapon, Shield}
public class Equipment
{
    public string name;
    public int hp;
    public int maxHp;
    public int damage;
    public int offence;
    int defence;
    public int effect1;
    public EquipmentType type;
    public int value;
    public int weight;

    public Equipment(string name, int effect1, EquipmentType type, int value, int weight)
    {
        this.name = name;
        this.effect1 = effect1;
        this.type = type;
        this.value = value;
        this.weight = weight;
        if (type == EquipmentType.Armor)
        {
            hp = maxHp = effect1;
        }
        if (type == EquipmentType.Weapon)
        {
            damage = effect1;
        }
        if (type == EquipmentType.Shield)
        {
            defence = effect1;
        }
    }
    public string CheckStatus()
    {
        if (name == "None") return "";
        else if (hp == maxHp) return  Color.HEALTH + " Undamaged" + Color.RESET;
        else if (hp < maxHp && hp > 2) return Color.NPC + " Damaged" + Color.RESET;
        else if (hp > 0) return Color.GOLD + " Severely Damaged" + Color.RESET;
        else return Color.STRENGTH + " Destroyed" + Color.RESET;
    }
    public int Defence(Gladiator g)
    {
        if (g.Disabled(g.leftArm)) return 0;
        return defence;
    }

    public Equipment Copy()
    {
        return (Equipment)MemberwiseClone();
    }
}