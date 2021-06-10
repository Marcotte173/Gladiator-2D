using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class Return
{
    internal static Random rand = new Random();
    internal static int RandomInt(int min, int max) => rand.Next(min, max);

    internal static bool Afford(int price) => (price <= Create.player.gold);
    internal static int Integer()
    {
        int sellChoice;
        do
        {

        } while (!int.TryParse(Console.ReadLine(), out sellChoice));
        return sellChoice;
    }
    internal static int Int()
    {
        int sellChoice;
        do
        {

        } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out sellChoice));
        return sellChoice;
    }

    internal static void Managers()
    {
        int n = 19;
        int x = 50;        
        if (Create.player.equipmentManager)
        {
            Write.Line(x, n, Color.ITEM + Manage.managerNames[0] + Color.RESET + " the" + Color.ITEM + " equipment manager" + Color.RESET);
            n++;
        }
        if (Create.player.healthManager)
        {
            Write.Line(x, n, Color.HEALTH + Manage.managerNames[1] + Color.RESET + " the" + Color.HEALTH + " health manager" + Color.RESET);
            n++;
        }
        if (Create.player.strengthManager)
        {
            Write.Line(x, n, Color.STRENGTH + Manage.managerNames[2] + Color.RESET + " the" + Color.STRENGTH + " fitness instructor" + Color.RESET);
            n++;
        }
        if (Create.player.offenceManager)
        {
            Write.Line(x, n, Color.OFFENCE + Manage.managerNames[3] + Color.RESET + " the" + Color.OFFENCE + " offence coordinator" + Color.RESET);
            n++;
        }
        if (Create.player.defenceManager)
        {
            Write.Line(x, n, Color.DEFENCE + Manage.managerNames[4] + Color.RESET + " the" + Color.DEFENCE + " defence coordinator" + Color.RESET);
            n++;
        }
        if (Create.player.enduranceManager)
        {
            Write.Line(x, n, Color.ENDURANCE + Manage.managerNames[5] + Color.RESET + " the" + Color.ENDURANCE + " conditioning coach" + Color.RESET);
            n++;
        }
        if (Create.player.prestigeManager)
        {
            Write.Line(x, n, Color.XP + Manage.managerNames[6] + Color.RESET + " the" + Color.XP + " publicity manager" + Color.RESET);
            n++;
        }
        if (Create.player.moneyManager)
        {
            Write.Line(x, n, Color.GOLD + Manage.managerNames[7] + Color.RESET + " the" + Color.GOLD + " fiscal planner" + Color.RESET);
            n++;
        }
        if (n == 19) Write.Line(60, 17, Color.SPEAK + "No Managers" + Color.RESET);
        else Write.Line(60, 17, Color.SPEAK + "Managers" + Color.RESET);
    }

    public static void SortByPrice(List<Gladiator> list)
    {
        Gladiator temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].Price < list[i + 1].Price)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    public static void SortByWins(List<Gladiator> list)
    {
        Gladiator temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].wins < list[i + 1].wins)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    public static void SortByKills(List<Gladiator> list)
    {
        Gladiator temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].kills < list[i + 1].kills)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }

    public static void SortByLosses(List<Gladiator> list)
    {
        Gladiator temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].losses < list[i + 1].losses)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }

    public static void SortByPrestige(List<Owner> list)
    {
        Owner temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].prestige < list[i + 1].prestige)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }

    public static void SortByWealth(List<Owner> list)
    {
        Owner temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].gold < list[i + 1].gold)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    public static void SortByRosterSize(List<Owner> list)
    {
        Owner temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].roster.Count < list[i + 1].roster.Count)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }

    internal static bool Confirm()
    {
        Write.Line(0, Console.WindowHeight - 3, "[1]" + Color.HEALTH + " Yes" + Color.RESET);
        Write.Line(0, Console.WindowHeight - 2, "[2]" + Color.STRENGTH + " No" + Color.RESET);
        string choice = Return.Option();
        if (choice == "1") return true;
        else if (choice == "2") return false;
        else Confirm();
        return false;
    }

    internal static bool Confirm(int x, int y)
    {
        Write.Line(x, y, "[1]" + Color.HEALTH + " Yes" + Color.RESET);
        Write.Line(x, y, "[2]" + Color.STRENGTH + " No" + Color.RESET);
        string choice = Return.Option();
        if (choice == "1") return true;
        else if (choice == "2") return false;
        else Confirm();
        return false;
    }   

    internal static bool CanHaveAMatch(Gladiator a, Gladiator b)
    {
        if (GladiatorUnavailable(a) ||GladiatorUnavailable(b)) return false;
        return true;
    }

    internal static bool GladiatorUnavailable(Gladiator g) => g == null || g.hurt;

    public static void GladiatorCurrentStatus(Gladiator gladiator, int x)
    {
        //Character Info
        Write.Line(0,  x + 0, $"Name: " + Color.NAME + $"{gladiator.name}" + Color.RESET);
        Write.Line(25, x + 0, $"Trait 1:  " + Color.ABILITY + "None" + Color.RESET);
        Write.Line(65, x + 0, $"Trait 2:  " + Color.ABILITY + "None" + Color.RESET);
        if (gladiator.Owner == null) Write.Line(100, x + 0, $"Owner: " + Color.NPC + "None" + Color.RESET);
        else if (gladiator.Owner == Create.player) Write.Line(100, x + 0, $"Owner: " + Color.PLAYER + $"{gladiator.Owner.name}" + Color.RESET);
        else Write.Line(100, x + 0, $"Owner: " + Color.NPC + $"{gladiator.Owner.name}" + Color.RESET);

        //MainHand, OffHand, Offence, Defence
        if (gladiator.Disabled(gladiator.rightArm) == false) Write.Line(0, x + 2, $"Weapon:   {gladiator.rightArm.equipment.name}");
        else Write.Line(0, x + 2, $"Weapon:   " + Color.STRENGTH + "Arm Disabled"+ Color.RESET);
        if (gladiator.Disabled(gladiator.leftArm) == false) Write.Line(0, x + 3, $"OffHand:  {gladiator.leftArm.equipment.name}");
        else Write.Line(0, x + 3, $"OffHand:  " + Color.STRENGTH + "Arm Disabled" + Color.RESET);
        Write.Line(0, x + 5, "Strength: " + Color.STRENGTH + $"{gladiator.Strength}" + Color.RESET);
        Write.Line(0, x + 6, "Offence:  " + Color.OFFENCE + $"{gladiator.Offence}" + Color.RESET);
        Write.Line(0, x + 7, "Defence:  " + Color.DEFENCE + $"{gladiator.Defence}" + Color.RESET);

        //Body Status                
        Write.Line(25, x + 2, $"Head:       {gladiator.head.StatusString()}");
        Write.Line(25, x + 3, $"Torso:      {gladiator.torso.StatusString()}");
        Write.Line(25, x + 4, $"Left Arm:   {gladiator.leftArm.StatusString()}");
        Write.Line(25, x + 5, $"Right Arm:  {gladiator.rightArm.StatusString()}");
        Write.Line(25, x + 6, $"Legs:       {gladiator.legs.StatusString()}");
        Write.Line(25,  x + 7, "Endurance:  " + Color.ENDURANCE + $"{gladiator.Endurance}" + Color.RESET);

        //Armor Status
        if (gladiator.head.equipment.maxHp == 0) Write.Line(65, x + 2, $"Head Armor:      {gladiator.head.equipment.name}");
        else Write.Line(65, x + 2, $"Head Armor:      {gladiator.head.equipment.name}  - {gladiator.head.equipment.CheckStatus()}");
        if (gladiator.torso.equipment.maxHp == 0) Write.Line(65, x + 3, $"Torso Armor:     {gladiator.torso.equipment.name}");
        else Write.Line(65, x + 3, $"Torso Armor:     {gladiator.torso.equipment.name}  - {gladiator.torso.equipment.CheckStatus()}");
        if (gladiator.legs.equipment.maxHp == 0) Write.Line(65, x + 4, $"Legs Armor:      {gladiator.legs.equipment.name}\n\n\n\n");
        else Write.Line(65, x + 4, $"Legs Armor:      {gladiator.legs.equipment.name}  - {gladiator.legs.equipment.CheckStatus()}\n\n\n\n");
    }    

    public static void GladiatorInfo(Gladiator g)
    {
        Write.Line(0, 6, Color.NAME + g.name + Color.RESET);
        Write.Line(0, 8, Color.HEALTH + "Wins" + Color.RESET + $"       { g.wins}");
        Write.Line(0, 9, Color.STRENGTH + "Strength" + Color.RESET + $"   {g.Strength}");
        Write.Line(0, 10, Color.OFFENCE + "Offence" + Color.RESET + $"    {g.Offence}");
        Write.Line(0, 11, Color.DEFENCE + "Defence" + Color.RESET + $"    {g.Defence}");
        Write.Line(0, 12, Color.ENDURANCE + "Endurance" + Color.RESET + $"  {g.Endurance}");
        if (g.Trait1 != "NONE") Write.Line(0, 14, Color.ABILITY + $"{g.Trait1}" + Color.RESET);
        if (g.Trait2 != "NONE") Write.Line(0, 15, Color.ABILITY + $"{g.Trait2}" + Color.RESET);       
    }
    public static void GladiatorInfo(Gladiator g, int x, int y)
    {
        Write.Line(x, y, Color.NAME + g.name + Color.RESET);
        Write.Line(x, y+2, Color.HEALTH + "Wins" + Color.RESET + $"       { g.wins}");
        Write.Line(x, y+3, Color.STRENGTH + "Strength" + Color.RESET + $"   {g.Strength}");
        Write.Line(x, y+4, Color.OFFENCE + "Offence" + Color.RESET + $"    {g.Offence}");
        Write.Line(x, y+5, Color.DEFENCE + "Defence" + Color.RESET + $"    {g.Defence}");
        Write.Line(x, y+6, Color.ENDURANCE + "Endurance" + Color.RESET + $"  {g.Endurance}");
        if (g.Trait1 != "NONE") Write.Line(x, y+8, Color.ABILITY + $"{g.Trait1}" + Color.RESET);
        if (g.Trait2 != "NONE") Write.Line(x, y+9, Color.ABILITY + $"{g.Trait2}" + Color.RESET);
    }

    public static void GladiatorCurrentEquipment(Gladiator g)
    {
        Write.Line(20, 11, Color.HEALTH + "Current" + Color.RESET);
        Write.Line(20, 13,$"{g.head.equipment.name}");
        Write.Line(20, 14,$"{g.torso.equipment.name}");
        Write.Line(20, 15,$"{g.legs.equipment.name}");
        Write.Line(20, 17,$"{g.rightArm.equipment.name}");
        Write.Line(20, 18,$"{g.leftArm.equipment.name} ");
        if (g.head.equipment.hp >0)Write.Line(35, 13,  Color.DURABILITY +  g.head.equipment.hp + " HP");
        if (g.torso.equipment.hp >0)Write.Line(35, 14,  Color.DURABILITY + g.torso.equipment.hp + " HP");
        if (g.legs.equipment.hp > 0) Write.Line(35, 15,  Color.DURABILITY + g.legs.equipment.hp + " HP");
        if (g.rightArm.equipment.effect1 >0)Write.Line(35, 17, Color.DAMAGE+ g.rightArm.equipment.effect1  + " DAM");
        if (g.leftArm.equipment.effect1 > 0) Write.Line(35, 18, Color.DEFENCE+ g.leftArm.equipment.effect1 + " DEF");
    }

    internal static void GladiatorUpgradeEquipment(Gladiator g)
    {
        int x = 50;
        int y = 13;
        Write.Line(x, y -2, Color.HEALTH + "Upgrade" + Color.RESET);
        if (FindUpgrade(EquipmentList.heads, g.head.equipment) != null)
        {
            Write.Line(x, y, "[1] " + FindUpgrade(EquipmentList.heads, g.head.equipment).name);
            Write.Line(x + 25, y, Color.DURABILITY + FindUpgrade(EquipmentList.heads, g.head.equipment).hp + " HP" + Color.GOLD + $"    {FindUpgrade(EquipmentList.heads, g.head.equipment).value} GOLD"+ Color.RESET);
        }
        else Write.Line(x, y, $"No Upgrade");
        if (FindUpgrade(EquipmentList.torsos, g.torso.equipment) != null)
        {
            Write.Line(x, y + 1, "[2] " + FindUpgrade(EquipmentList.torsos, g.torso.equipment).name);
            Write.Line(x + 25, y + 1, Color.DURABILITY + FindUpgrade(EquipmentList.torsos, g.torso.equipment).hp + " HP" + Color.GOLD + $"    {FindUpgrade(EquipmentList.torsos, g.torso.equipment).value} GOLD" + Color.RESET);
        }
        else Write.Line(x, y + 1, $"No Upgrade");
        if (FindUpgrade(EquipmentList.legs, g.legs.equipment) != null)
        {
            Write.Line(x, y + 2, "[3] " + FindUpgrade(EquipmentList.legs, g.legs.equipment).name);
            Write.Line(x + 25, y + 2, Color.DURABILITY + FindUpgrade(EquipmentList.legs, g.legs.equipment).hp + " HP" + Color.GOLD + $"    {FindUpgrade(EquipmentList.legs, g.legs.equipment).value} GOLD" +Color.RESET);
        }
        else Write.Line(x, y + 2, $"No Upgrade");
        if (FindUpgrade(EquipmentList.weapons, g.rightArm.equipment) != null)
        {
            Write.Line(x, y + 4, "[M] " +FindUpgrade(EquipmentList.weapons, g.rightArm.equipment).name);
            Write.Line(x + 25, y + 4, Color.DAMAGE + FindUpgrade(EquipmentList.weapons, g.rightArm.equipment).effect1  + " DAM" + Color.GOLD + $"   {FindUpgrade(EquipmentList.weapons, g.rightArm.equipment).value} GOLD" + Color.RESET);
        }
        else Write.Line(x, y + 4, $"No Upgrade");
        if (FindUpgrade(EquipmentList.shields, g.leftArm.equipment) != null)
        {
            Write.Line(x, y + 5, "[O] " + FindUpgrade(EquipmentList.shields, g.leftArm.equipment).name);
            Write.Line(x + 25, y + 5, Color.DEFENCE + FindUpgrade(EquipmentList.shields, g.leftArm.equipment).effect1 + " DEF" + Color.GOLD + $"   {FindUpgrade(EquipmentList.shields, g.leftArm.equipment).value} GOLD" + Color.RESET);
        }
        else Write.Line(x, y + 5, $"No Upgrade");
    }

    internal static Equipment FindUpgrade(List<Equipment> list, Equipment item)
    {
        int i = 0;
        foreach (Equipment equip in list) if (equip.name == item.name) i = list.IndexOf(equip);
        if (i < list.Count - 1) return list[i + 1];
        return null;
    }

    internal static void PlayerInfo()
    {
        Write.Line(105 , 0, "It is Day " + Color.TIME + $"{Hub.day}" + Color.RESET);
        Write.Line(0, 0, Color.PLAYER + Create.player.name + Color.RESET);
        Write.Line(70, 0, $"You have " + Color.GOLD + $"{Create.player.gold}" + Color.RESET + " Gold");
        Write.Line(35, 0, $"You have " + Color.XP + $"{Create.player.prestige}" + Color.RESET + " Prestige");
        Write.Line(110,28, Color.SPEAK + "[?] Help"+ Color.RESET);
    }

    public static void Roster(Owner p, int x)
    {
        Write.Line(x, 2, Color.ITEM + "MAIN" + Color.RESET);
        
        for (int i = 0; i < Create.player.roster.Count; i++)
        {
            Write.Line(x, 4, Create.player.roster[i].Action);
            Write.Line(x, 6, Color.NAME + Create.player.roster[i].name + Color.RESET);
            Write.Line(x, 8, Color.HEALTH + "Wins" + Color.RESET + $"       { Create.player.roster[i].wins}");
            Write.Line(x, 9, Color.STRENGTH + "Strength" + Color.RESET + $"   {Create.player.roster[i].Strength}");
            Write.Line(x, 10, Color.OFFENCE + "Offence" + Color.RESET + $"    {Create.player.roster[i].Offence}");
            Write.Line(x, 11, Color.DEFENCE + "Defence" + Color.RESET + $"    {Create.player.roster[i].Defence}");
            Write.Line(x, 12, Color.ENDURANCE + "Endurance" + Color.RESET + $"  {Create.player.roster[i].Endurance}");
            if (Create.player.roster[i].Trait1 != "NONE") Write.Line(x, 14, Color.ABILITY + $"{Create.player.roster[i].Trait1}" + Color.RESET);
            if (Create.player.roster[i].Trait2 != "NONE") Write.Line(x, 15, Color.ABILITY + $"{Create.player.roster[i].Trait2}" + Color.RESET);
            x += 25;
        }
    }

    public static void CombatDisplay(Gladiator one, Gladiator two)
    {
        Return.GladiatorCurrentStatus(one, 0);
        for (int i = 0; i < 120; i++) Console.Write("-");
        Write.Line(50, Console.CursorTop+1, Color.STRENGTH + "VS.\n" + Color.RESET);
        for (int i = 0; i < 120; i++) Console.Write("-");
        Return.GladiatorCurrentStatus(two, 11);
        for (int i = 0; i < 120; i++) Console.Write("-");
    }  

    public static string Name()
    {
        int choice = RandomInt(0, Create.names.Count);
        string name = Create.names[choice];
        Create.names.RemoveAt(choice);
        return name;
    }

    internal static string Option() =>  Console.ReadKey(true).KeyChar.ToString().ToLower();
    internal static string String() => Console.ReadLine();
}