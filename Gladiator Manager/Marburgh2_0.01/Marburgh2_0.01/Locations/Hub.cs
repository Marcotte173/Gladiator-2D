using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public enum Display{ Stats, Injuries, Equipment}
public class Hub
{
    static Owner player = Create.player;
    static List<Owner> owners = new List<Owner> { };
    static List<Owner> schedule = new List<Owner> { };
    public static int day = 1;
    static bool audience;
    static bool fightsToday = true;
    static Display display;
    public static List<Gladiator> gladiatorsWhoFought = new List<Gladiator> { };

    public static void Start()
    {
        foreach (Owner o in Create.owners) owners.Add(o);
        Slaver.NewStock();
        for (int i = 0; i < 8; i++) Manage.managerNames.Add(Return.Name());
        Console.Clear();
        Write.Line("You have been recruited...\n\n");
        Write.Line("The emperor has decided to host a tournament to entertain the people of the mighty nation of Marburgh.");
        Write.Line("You have been selected, given a small sum of money and a gladiator, and told you have 10 days to prepare.\n\n");
        Write.Line("Win and you will be given ANYTHING you desire");
        Write.Line("That thing you're thinking of? Yeah, you can have that\n\n");
        Write.Line("Lose and you will be put to death\n");
        Write.Line("Now that's entertainment!");
        Write.KeyPress(0,28);
        Menu();
    }

    public static void Menu()
    {
        Console.Clear();
        Return.PlayerInfo();
        Return.Managers();
        Write.Line(90, 17, Color.SPEAK + "Toggle Information" + Color.RESET);
        Write.Line(90, 19, "[S]tats");
        Write.Line(90, 20, "[I]njuries");
        Write.Line(90, 21, "[E]quipment");
        if (display == Display.Stats) Return.Roster(player, 5);
        else if (display == Display.Injuries) Manage.Injuries();
        else if (display == Display.Equipment) Manage.Equipment();
        while (day < 11)
        {
            Write.Line(0, 18, "[1] " + Color.GOLD + "Hire Gladiators\n" + Color.RESET);
            Write.Line("[2] " + Color.ENERGY + "Manage Gladiators" + Color.RESET);
            Write.Line("[3] " + Color.ITEM + "Purchase Equipment" + Color.RESET);
            Write.Line("[4] " + Color.ENERGY + "Jobs" + Color.RESET);
            if (Graveyard.graveyard.Count > 0) Write.Line("[5] " + Color.DURABILITY + "Graveyard" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "The graveyard is empty" + Color.RESET);
            int gladiatorsWithAWin = 0;
            foreach (Owner o in Create.owners) foreach (Gladiator g in o.roster) if (g.wins > 0) gladiatorsWithAWin++;
            if(gladiatorsWithAWin >9)Write.Line("[6] " + Color.XP + "Top 10 Gladiators" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "Not enough gladiators have won a match" + Color.RESET);
            Write.Line("[7] " + Color.XP + "Owner Rankings" + Color.RESET);
            if (audience) Write.Line("[8] " + Color.XP + "Audience with the emperor" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "The emperor has no interest in you" + Color.RESET);
            if (fightsToday) Write.Line("[A] " + Color.STRENGTH + "The Arena!" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "There are no other fights today" + Color.RESET);
            Write.Line("[0] " + Color.TIME + "Next day" + Color.RESET);
            Write.Line("[Q] Quit");
            string choice = Return.Option();
            if (choice == "1") Slaver.Hire();
            else if (choice == "2") Manage.Gladiators();
            else if (choice == "3") Purchase.Equipment();
            else if (choice == "5" && Graveyard.graveyard.Count > 0) Graveyard.Visit();
            else if (choice == "6" && gladiatorsWithAWin > 9) Rankings.Gladiators();
            else if (choice == "7") Rankings.Owner();
            else if (choice == "a")
            {
                fightsToday = false;                
                for (int i = 0; i < 6; i++)
                {
                    Owner o = owners[Return.RandomInt(0, owners.Count)];
                    schedule.Add(o);
                    owners.Remove(o);
                }
                owners = schedule;
                ScheduleDisplay();
                gladiatorsWhoFought.Add(schedule[0].roster[0]);
                gladiatorsWhoFought.Add(schedule[1].roster[0]);
                gladiatorsWhoFought.Add(schedule[2].roster[0]); 
                gladiatorsWhoFought.Add(schedule[3].roster[0]);
                gladiatorsWhoFought.Add(schedule[4].roster[0]);
                gladiatorsWhoFought.Add(schedule[5].roster[0]);
                if (Return.CanHaveAMatch(schedule[0].roster[0], schedule[1].roster[0]))Combat.Start(schedule[0].roster[0], schedule[1].roster[0]);
                if (Return.CanHaveAMatch(schedule[2].roster[0], schedule[3].roster[0]))Combat.Start(schedule[2].roster[0], schedule[3].roster[0]);
                if (Return.CanHaveAMatch(schedule[4].roster[0], schedule[5].roster[0]))Combat.Start(schedule[4].roster[0], schedule[5].roster[0]);
            }
            else if (choice == "0")
            {
                if (fightsToday)
                {
                    Console.Clear();
                    Write.Line("Your Gladiators have not fought today\nAre you sure you want to move on to tomorrow?");
                    if (Return.Confirm())
                    {
                        day++;
                        Recap();
                        player.actions = 3;
                        if (day % 3 == 0) Slaver.NewStock();
                        // All the next day stuff
                        fightsToday = true;
                        gladiatorsWhoFought.Clear();
                    }
                }                
            }
            else if (choice == "s") display = Display.Stats;
            else if (choice == "e") display = Display.Equipment;
            else if (choice == "i") display = Display.Injuries; 
            else if (choice == "q") Environment.Exit(0);
            Menu();
        }
        Console.Clear();
        Write.Line("YOU ARE IN THE NEXT STAGE OF THE GAME! CONGRATS!");
        Write.KeyPress();
        Environment.Exit(0);
    }

    private static void Recap()
    {
        Console.Clear();
        //You gain Prestige        
        foreach(Gladiator g in gladiatorsWhoFought)
        {
            if (g.lastFightOutcome == 0)
            {
                if(g.Owner == player)Write.Line("Your Gladiators did not fight today. You are gaining a reputation as a coward");
                g.Owner.prestige -= 10;
            }
            if (g.lastFightOutcome == 1)
            {
                if (g.Owner == player) Write.Line("Your gladiator won today. The emperor is pleased");
                g.Owner.prestige += 5;
            }
            else if (g.lastFightOutcome == 2)
            {
                if (g.Owner == player) Write.Line("Your gladiator lost today. The emperor is not pleased");
                g.Owner.prestige -= 5;
            }
            g.lastFightOutcome = 0;
        }
        //Gladiators Recover
        foreach(Owner o in owners)
            foreach(Gladiator g in o.roster)
            {
                List<Body> injured = new List<Body> { };               
                if(g.head.status != Status.Uninjured) injured.Add(g.head);
                if(g.torso.status != Status.Uninjured) injured.Add(g.torso);
                if(g.rightArm.status != Status.Uninjured) injured.Add(g.rightArm);
                if(g.leftArm.status != Status.Uninjured) injured.Add(g.leftArm);
                if (g.legs.status != Status.Uninjured) injured.Add(g.legs);
                if (o.healthManager)
                {
                    if (g.Owner == player) Write.Line($"Thanks your Health Manager, {g.name} is recovering well");
                    foreach (Body b in injured) Heal(g,b, 2);
                }
                else Heal(g,injured[Return.RandomInt(0, injured.Count)], 2);
            }
        //Gear gets fixed        
        Write.KeyPress();
    }

    public static void Heal(Gladiator g,Body b, int heal)
    {        
        b.SetHealth(heal);
        if (g.Owner == player) Write.Line($"{g.name}'s {b.name} is healing and is now {b.status}");
    }

    private static void ScheduleDisplay()
    {
        Console.Clear();
        int n = 10;
        Write.Line(52, 4, Color.DAMAGE + "Matches today" + Color.RESET);
        Write.Line(10, 8, "Owner"); 
        Write.Line(35, 8, "Gladiator");
        Write.Line(70, 8, "Gladiator");
        Write.Line(95, 8, "Owner");

        Write.Line(0,  n, "[1]");
        Write.Line(58, n, "Vs.");
        if (schedule[0].player) Write.Line(10, n, Color.PLAYER + schedule[0].name + Color.RESET);
        else Write.Line(10, n, Color.OWNER + schedule[0].name + Color.RESET);        
        if(Return.GladiatorUnavailable(schedule[0].roster[0])) Write.Line(35, n, Color.DAMAGE + "Unavailable" + Color.RESET);
        else Write.Line(35, n, Color.NAME + schedule[0].roster[0].name + Color.RESET);
        if (Return.GladiatorUnavailable(schedule[1].roster[0])) Write.Line(70, n, Color.DAMAGE + "Unavailable" + Color.RESET);
        else Write.Line(70, n, Color.NAME + schedule[1].roster[0].name + Color.RESET);
        if (schedule[1].player) Write.Line(95, n, Color.PLAYER + schedule[1].name + Color.RESET);
        else Write.Line(95, n, Color.OWNER + schedule[1].name + Color.RESET);        

        Write.Line(0,  n+2, "[2]");
        Write.Line(58, n+2, "Vs.");
        if (schedule[2].player) Write.Line(10, n + 2, Color.PLAYER + schedule[2].name + Color.RESET);
        else Write.Line(10, n + 2, Color.OWNER + schedule[2].name + Color.RESET);
        if (Return.GladiatorUnavailable(schedule[2].roster[0])) Write.Line(35, n+2, Color.DAMAGE + "Unavailable" + Color.RESET);
        else Write.Line(35, n+2, Color.NAME + schedule[2].roster[0].name + Color.RESET);
        if (Return.GladiatorUnavailable(schedule[3].roster[0])) Write.Line(70, n+2, Color.DAMAGE + "Unavailable" + Color.RESET);
        else Write.Line(70, n+2, Color.NAME + schedule[3].roster[0].name + Color.RESET);
        if (schedule[3].player) Write.Line(95, n + 2, Color.PLAYER + schedule[3].name + Color.RESET);
        else Write.Line(95, n + 2, Color.OWNER + schedule[3].name + Color.RESET);
        

        Write.Line(0, n + 4, "[3]");
        Write.Line(58, n + 4, "Vs.");
        if (schedule[4].player) Write.Line(10, n + 4, Color.PLAYER + schedule[4].name + Color.RESET);
        else Write.Line(10, n + 4, Color.OWNER + schedule[4].name + Color.RESET);
        if (Return.GladiatorUnavailable(schedule[4].roster[0])) Write.Line(35, n + 4, Color.DAMAGE + "Unavailable" + Color.RESET);
        else Write.Line(35, n + 4, Color.NAME + schedule[4].roster[0].name + Color.RESET);
        if (Return.GladiatorUnavailable(schedule[5].roster[0])) Write.Line(70, n + 4, Color.DAMAGE + "Unavailable" + Color.RESET);
        else Write.Line(70, n + 4, Color.NAME + schedule[5].roster[0].name + Color.RESET);
        if (schedule[5].player) Write.Line(95, n + 4, Color.PLAYER + schedule[5].name + Color.RESET);
        else Write.Line(95, n + 4, Color.OWNER + schedule[5].name + Color.RESET);
        

        Write.Line(0, 28, "[0] To continue");
        string choice = Return.Option();
        if (choice != "0") ScheduleDisplay();
    }
}