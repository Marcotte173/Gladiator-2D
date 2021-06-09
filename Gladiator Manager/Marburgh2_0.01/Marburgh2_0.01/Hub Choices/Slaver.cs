using System;
using System.Collections.Generic;
using System.Text;

public class Slaver
{
    new public static List<Gladiator> list = new List<Gladiator> { };

    internal static void NewStock()
    {
        list.Clear();
        //Create 9 new Gladiators
        for (int i = 0; i < 9; i++) 
        {
            list.Add(new Gladiator(Return.RandomInt(-1,9)));
        }
        Return.SortByPrice(list);
    }
    public static void Hire()
    {
        Console.Clear();
        Write.Line("You walk into the slaver's compound");
        Write.Line(0,2, Color.NPC+"Rizzo" + Color.RESET+" walks up to you");
        Write.Line(0,4, Color.SPEAK +"'Greetings my friend! You're here for new gladiators, yes? Come take a look!'"+ Color.RESET);
        Write.KeyPress(0,28);
        HireNext();
    }

    private static void HireNext()
    {        
        DisplayGladiator();
        Write.Line(0, 16, Color.SPEAK + "'Well? Does anyone catch your eye?'" + Color.RESET);
        Write.Line(0, 28, "[0] to return");
        Gladiator g = new Gladiator(1);
        int choice = Return.Int();
        if (choice > 0 && choice <= list.Count) g = list[choice - 1];
        else if (choice == 0) Hub.Menu();
        else HireNext();
        Console.Clear();
        Return.PlayerInfo();
        Write.Line(0, 2, Color.NAME + g.name + Color.RESET + "?");
        Write.Line(0, 4, Color.STRENGTH +   "Strength "  + Color.RESET + $"  {g.Strength}");
        Write.Line(0, 5, Color.OFFENCE +   "Offence  "   + Color.RESET +$"  {g.Offence}");
        Write.Line(0, 6, Color.DEFENCE +   "Defence  "   + Color.RESET +$"  {g.Defence}");
        Write.Line(0, 7, Color.ENDURANCE + "Endurance" + Color.RESET +  $"  {g.Endurance}");
        Write.Line(0, 9, Color.GOLD +   "Price      " + Color.RESET+ g.Price );
        if (Return.Afford(g.Price))
        {
            if (Return.Confirm())
            {
                Console.Clear();
                Write.Line(0, 1, Color.SPEAK+"'Wonderful!'"+ Color.NPC+"\n\nRizzo" + Color.RESET +" takes your money, and "+Color.NAME + $"{g.name}" + Color.RESET+" joins your team\n");
                Create.player.gold -= g.Price;
                list.Remove(g);
                rosterAdd(g);
                Write.KeyPress(0,28);
                Hub.Menu();
            }
            else HireNext();
        }
        else
        {
            Write.Line(0, 25, "You can't afford it\n");
            Write.KeyPress();
            Hub.Menu();
        }
    }

    private static void DisplayGladiator()
    {
        Console.Clear();
        Return.PlayerInfo();
        Write.Line(20, 2, Color.STRENGTH +"Strength");
        Write.Line(35, 2, Color.OFFENCE +"Offence");
        Write.Line(50, 2, Color.DEFENCE + "Defence");
        Write.Line(65, 2, Color.ENDURANCE + "Endurance");
        Write.Line(80, 2, Color.GOLD + "Price" + Color.RESET);
        for (int i = 0; i < list.Count; i++)
        {
            Write.Line(1,  i + 4, (i + 1).ToString());
            Write.Line(5,  i + 4, Color.NAME +   list[i].name + Color.RESET);
            Write.Line(23, i + 4, list[i].Strength.ToString());
            Write.Line(38, i + 4, list[i].Offence.ToString());
            Write.Line(53, i + 4, list[i].Defence.ToString());
            Write.Line(69, i + 4, list[i].Endurance.ToString());
            Write.Line(80, i + 4, list[i].Price.ToString());
        }
    }

    static void rosterAdd(Gladiator g)
    {
        if (Create.player.roster.Count < 5)
        {
            Create.player.roster.Add(g);
        }
        else
        {
            Write.Line("Your roster is full! Release a gladiator!");
            Write.KeyPress();
        }
    }
}