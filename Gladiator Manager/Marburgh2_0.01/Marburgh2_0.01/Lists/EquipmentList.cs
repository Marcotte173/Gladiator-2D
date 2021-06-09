using System;
using System.Collections.Generic;
using System.Text;

public class EquipmentList
{   
    /// <summary>
    /// Slot is empty
    /// </summary>
    public static Equipment noWeapon = new Equipment(Color.MITIGATION + "Fist" + Color.RESET, 1, EquipmentType.Weapon,0,0);
    public static Equipment noArmor = new Equipment(Color.MITIGATION + "None" + Color.RESET, 0,  EquipmentType.Armor, 0, 0);
    public static Equipment noShield = new Equipment(Color.MITIGATION + "Fist" + Color.RESET, 0, EquipmentType.Shield, 0, 0);

    /// <summary>
    ///  WEAPONS
    /// </summary>
    public static Equipment shortSword = new Equipment(Color.ITEM + "Short Sword" + Color.RESET, 2,  EquipmentType.Weapon, 250, 1);
    public static Equipment longSword = new Equipment(Color.ITEM+ "Long Sword" + Color.RESET,   3,  EquipmentType.Weapon, 400, 1);
    public static Equipment armingsword = new Equipment(Color.ITEM + "Arming Sword" + Color.RESET, 4, EquipmentType.Weapon, 700, 1);

    /// <summary>
    ///  Shield
    /// </summary>
    public static Equipment buckler = new Equipment(Color.ITEM + "Buckler" + Color.RESET, 2, EquipmentType.Shield, 250, 1);
    public static Equipment smallShield = new Equipment(Color.ITEM + "Small Shield" + Color.RESET, 3, EquipmentType.Shield, 400, 1);
    public static Equipment kiteShield = new Equipment(Color.ITEM + "Kite Shield" + Color.RESET, 4, EquipmentType.Shield, 700, 1);

    //////
    /// Head Armor
    //////
    public static Equipment leatherHead = new Equipment(Color.ITEM + "Leather Helmet" + Color.RESET, 2,EquipmentType.Armor, 250, 1);
    public static Equipment chainHead = new Equipment(Color.ITEM + "Chain Coif" + Color.RESET, 3,       EquipmentType.Armor, 400, 2);
    public static Equipment plateHead = new Equipment(Color.ITEM + "Plate Helm" + Color.RESET, 4       , EquipmentType.Armor, 700, 3);

    //////
    /// Torso Armor
    //////
    public static Equipment leatherTorso = new Equipment(Color.ITEM + "Leather Torso" + Color.RESET, 2, EquipmentType.Armor, 250, 1);
    public static Equipment chainTorso = new Equipment(Color.ITEM + "Chain Chest" + Color.RESET, 3,     EquipmentType.Armor, 400, 2);
    public static Equipment plateTorso = new Equipment(Color.ITEM + "Plate Chest" + Color.RESET, 4,    EquipmentType.Armor, 700, 3);

    //////
    /// Leg Armor
    //////
    public static Equipment leatherLegs = new Equipment(Color.ITEM + "Leather Pants" + Color.RESET, 2, EquipmentType.Armor, 250, 1);
    public static Equipment chainLegs = new Equipment(Color.ITEM + "Chain Legs" + Color.RESET, 3, EquipmentType.Armor, 400, 2);
    public static Equipment plateLegs = new Equipment(Color.ITEM + "Plate Legs" + Color.RESET, 4, EquipmentType.Armor, 700, 3);

    public static List<Equipment> weapons = new List<Equipment> { noWeapon, shortSword, longSword, armingsword };
    public static List<Equipment> ambidextrous = new List<Equipment> { noWeapon, shortSword, longSword, armingsword };
    public static List<Equipment> shields = new List<Equipment> { noShield, buckler, smallShield, kiteShield };
    public static List<Equipment> heads = new List<Equipment> { noArmor, leatherHead, chainHead, plateHead };
    public static List<Equipment> torsos = new List<Equipment> { noArmor, leatherTorso, chainTorso, plateTorso };
    public static List<Equipment> legs = new List<Equipment> { noArmor, leatherLegs, chainLegs, plateLegs };

}