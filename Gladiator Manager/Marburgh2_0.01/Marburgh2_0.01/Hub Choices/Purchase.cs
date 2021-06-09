using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

public class Purchase
{
    static Owner player = Create.player;
    public static void Equipment()
    {
        Console.Clear();
        Return.PlayerInfo();
        Return.Roster(player,2);
        Write.Line(0, 20, "Who would your like to equip?");
        for (int i = 0; i < player.roster.Count; i++)
        {
            Write.Line(0,22+i,$"[{i + 1}] "+Color.NAME + $"{player.roster[i].name}"+Color.RESET);
        }
        Write.Line(0,Console.WindowHeight-2,"[0] to Return");
        Gladiator g = new Gladiator(1);
        int glad = Return.Int();
        if (glad > 0 && glad <= player.roster.Count)
        {
            g = player.roster[glad - 1];
            Equip(g);
        }
        else if (glad == 0) Hub.Menu();
    }

    static void Equip(Gladiator g)
    {
        Console.Clear();
        Return.PlayerInfo();        
        Return.GladiatorInfo(g,50,3);
        Write.Line(0, 13, $"Head Armor       ");
        Write.Line(0, 14, $"Torso Armor      ");
        Write.Line(0, 15, $"Leg Armor        ");
        Write.Line(0, 17, $"Main Hand Weapon ");
        Write.Line(0, 18, $"Off Hand Weapon  ");
        Return.GladiatorCurrentEquipment(g);
        Return.GladiatorUpgradeEquipment(g);
        Write.Line(40, 24, "Please select the item you would like to Upgrade");
        Write.Line(40, 26,"[0] Return to compound      " + Color.RESET);

        string choice = Return.Option();
        if (choice == "1" && Return.FindUpgrade(EquipmentList.heads, g.head.equipment) != null)
        {
            if (Return.Afford(Return.FindUpgrade(EquipmentList.heads, g.head.equipment).value))
            {
                player.gold -= Return.FindUpgrade(EquipmentList.heads, g.head.equipment).value;
                g.head.equipment = Return.FindUpgrade(EquipmentList.heads, g.head.equipment).Copy();
            }
        }
        else if (choice == "2" && Return.FindUpgrade(EquipmentList.torsos, g.torso.equipment) != null)
        {
            if (Return.Afford(Return.FindUpgrade(EquipmentList.torsos, g.torso.equipment).value))
            {
                player.gold -= Return.FindUpgrade(EquipmentList.torsos, g.torso.equipment).value;
                g.torso.equipment = Return.FindUpgrade(EquipmentList.torsos, g.torso.equipment).Copy();
            }
        }
        else if (choice == "3" && Return.FindUpgrade(EquipmentList.legs, g.legs.equipment) != null)
        {
            if (Return.Afford(Return.FindUpgrade(EquipmentList.legs, g.legs.equipment).value))
            {
                player.gold -= Return.FindUpgrade(EquipmentList.legs, g.legs.equipment).value;
                g.legs.equipment = Return.FindUpgrade(EquipmentList.legs, g.legs.equipment).Copy();
            }
        }
        else if (choice == "m" && Return.FindUpgrade(EquipmentList.weapons, g.rightArm.equipment) != null)
        {
            if (Return.Afford(Return.FindUpgrade(EquipmentList.weapons, g.rightArm.equipment).value))
            {
                player.gold -= Return.FindUpgrade(EquipmentList.weapons, g.rightArm.equipment).value;
                g.rightArm.equipment = Return.FindUpgrade(EquipmentList.weapons, g.rightArm.equipment).Copy();
            }
        }
        else if (choice == "o" && Return.FindUpgrade(EquipmentList.shields, g.leftArm.equipment) != null)
        {
            if (Return.Afford(Return.FindUpgrade(EquipmentList.shields, g.leftArm.equipment).value))
            {
                player.gold -= Return.FindUpgrade(EquipmentList.shields, g.leftArm.equipment).value;
                g.leftArm.equipment = Return.FindUpgrade(EquipmentList.shields, g.leftArm.equipment).Copy();
            }
        }
        else if (choice == "0") Equipment();
        Equip(g);
    }
}