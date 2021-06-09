using System;
using System.Collections.Generic;
using System.Text;

public class Create
{
    public static List<Owner> owners = new List<Owner> { };
    public static List<string> names = new List<string> { };
    public static Owner player;
    internal static void Opponents()
    {
        //Rich
        Owner opp1 = new Owner();
        opp1.gold = Return.RandomInt(4000,5000);
        opp1.prestige = Return.RandomInt(10,25);
        opp1.roster.Add(new Gladiator(1));
        opp1.roster.Add(new Gladiator(2));
        owners.Add(opp1);

        //Prestige
        Owner opp2 = new Owner();
        opp2.gold = Return.RandomInt(2000,3000);
        opp2.prestige = Return.RandomInt(30,50);
        opp2.roster.Add(new Gladiator(1));
        opp2.roster.Add(new Gladiator(2));
        owners.Add(opp2);

        //Both
        Owner opp3 = new Owner();
        opp3.gold = Return.RandomInt(4000,7000);
        opp3.prestige = Return.RandomInt(30,60);
        opp3.roster.Add(new Gladiator(2));
        opp3.roster.Add(new Gladiator(2));
        owners.Add(opp3);

        //Neither
        Owner opp4 = new Owner();
        opp4.gold = Return.RandomInt(1500,2500);
        opp4.prestige = Return.RandomInt(7,15);
        opp4.roster.Add(new Gladiator());
        owners.Add(opp4);

        //Gladiator
        Owner opp5 = new Owner();
        opp5.gold = Return.RandomInt(1500,2500);
        opp5.prestige = Return.RandomInt(7,15);
        opp5.roster.Add(new Gladiator(2));
        opp5.roster.Add(new Gladiator(2));
        opp5.roster.Add(new Gladiator(3));
        owners.Add(opp5);
    }

    internal static void Player()
    {
        owners.Add(new Owner());
        player = owners[0];
        player.player = true;              
        NamePlayer();        
    }

    private static void NamePlayer()
    {
        Console.Clear();
        Write.Line("Please enter your name\n");
        owners[0].name = Return.String();
        Write.Line("\n"+owners[0].name + ", is that correct?");
        if (Return.Confirm() == false) NamePlayer();
    }
}