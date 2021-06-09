using System;
using System.Collections.Generic;
using System.Text;

public class Rankings
{ 
    public static List<Owner> ownerRankings = new List<Owner> { };
    public static List<Gladiator> gladiatorRankings = new List<Gladiator> { };
    public static void Owner()
    {
        ownerRankings = Create.owners;
        Return.SortByPrestige(ownerRankings);
        DisplayOwners();        
    }

    private static void DisplayOwners()
    {
        Console.Clear();
        Write.Line("Owner");
        Write.Line(40, 0, "Prestige");
        Write.Line(70, 0, "Gold");
        Write.Line(100, 0, "Roster Size");
        for (int i = 0; i < ownerRankings.Count; i++)
        {
            if (ownerRankings[i].player) Write.Line(0, 2 + i, Color.PLAYER + ownerRankings[i].name + Color.RESET);
            else Write.Line(0, 2 + i, Color.OWNER + ownerRankings[i].name + Color.RESET);
            Write.Line(40, 2 + i, Color.XP + ownerRankings[i].prestige.ToString() + Color.RESET);
            Write.Line(70, 2 + i, Color.GOLD + ownerRankings[i].gold.ToString() + Color.RESET);
            Write.Line(100, 2 + i, Color.NAME + ownerRankings[i].roster.Count.ToString() + Color.RESET);
        }
        Write.Line(0, 24,"[1] Display by " + Color.XP + "Prestige"    + Color.RESET);
        Write.Line(0, 25, "[2] Display by " + Color.GOLD + "Gold"        + Color.RESET);
        Write.Line(0, 26, "[3] Display by " + Color.NAME + "Roster Size" + Color.RESET);
        Write.Line(0, 28, "[0] To return");
        string choice = Return.Option();
        if (choice == "1") Return.SortByPrestige(ownerRankings);
        if (choice == "2") Return.SortByWealth(ownerRankings);
        if (choice == "3") Return.SortByRosterSize(ownerRankings);
        else if (choice == "0") Hub.Menu();
        DisplayOwners();
    }

    internal static void Gladiators()
    {
        gladiatorRankings.Clear();
        foreach (Owner o in Create.owners) foreach (Gladiator g in o.roster) gladiatorRankings.Add(g);
        Return.SortByWins(gladiatorRankings);
        DisplayGladiators();
    }

    private static void DisplayGladiators()
    {
        Console.Clear();
        Write.Line("Owner");
        Write.Line(40, 0, "Wins");
        Write.Line(70, 0, "Kills");
        Write.Line(100, 0, "Losses");
        for (int i = 0; i < gladiatorRankings.Count; i++)
        {
            if (gladiatorRankings[i].Owner == Create.player) Write.Line(0, 2 + i, Color.PLAYER + gladiatorRankings[i].name + Color.RESET);
            else Write.Line(0, 2 + i, Color.NAME + gladiatorRankings[i].name + Color.RESET);
            Write.Line(40, 2 + i, Color.HEALTH + gladiatorRankings[i].wins.ToString() + Color.RESET);
            Write.Line(70, 2 + i, Color.DAMAGE + gladiatorRankings[i].kills.ToString() + Color.RESET);
            Write.Line(100, 2 + i, Color.STUNNED + gladiatorRankings[i].losses.ToString() + Color.RESET);
        }
        Write.Line(0, 24, "[1] Display by " + Color.HEALTH + "Wins" + Color.RESET);
        Write.Line(0, 25, "[2] Display by " + Color.DAMAGE + "Kills" + Color.RESET);
        Write.Line(0, 26, "[3] Display by " + Color.STUNNED + "Losses" + Color.RESET);
        Write.Line(0, 28, "[0] To return");
        string choice = Return.Option();
        if (choice == "1") Return.SortByWins(gladiatorRankings);
        if (choice == "2") Return.SortByKills(gladiatorRankings);
        if (choice == "3") Return.SortByLosses(gladiatorRankings);
        else if (choice == "0") Hub.Menu();
        DisplayGladiators();
    }
}