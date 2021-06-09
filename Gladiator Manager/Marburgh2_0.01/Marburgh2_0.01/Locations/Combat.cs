using System;
using System.Threading;

public class Combat
{
    static int momentum;
    public static Gladiator winner;
    public static Gladiator loser;
    public static bool surrender;
    public static void Start(Gladiator a, Gladiator b)
    {
        Console.Clear();
        if (a.Owner.player || b.Owner.player)
        {
            Write.Line(50, 12, "Welcome to the " + Color.BLOOD + "Arena" + Color.RESET + "!");
            Write.Line(30, 14, $"The next match to take place will be between {Color.NAME + a.name + Color.RESET} and {Color.NAME + b.name + Color.RESET}");
            Write.KeyPress(0, 28);
        }
        int round = 0;
        while (winner == null)
        {
            Console.Clear();
            Event();
            for (int i = 0; i < 6; i++)
            {
                Console.Clear();
                Attack(a, b);
                if (a.Owner.player || b.Owner.player) Return.CombatDisplay(a, b);
                if (a.Disabled(a.head) || a.Disabled(a.torso)) break;
                if (b.Disabled(b.head) || b.Disabled(b.torso)) break;
                if (a.Owner.player || b.Owner.player) Thread.Sleep(1500);
                round++;
            }
            if (a.SurrenderCheck(a,b))
            {
                winner = b;
                loser = a;
                surrender = true;                
            }
            if (b.SurrenderCheck(a,b))
            {
                winner = a;
                loser = b;
                surrender = true;
            }
            if (a.Disabled(a.head) || a.Disabled(a.torso))
            {
                winner = b;
                loser = a;
            }
            if (b.Disabled(b.head) || b.Disabled(b.torso))
            {
                loser = b;
                winner = a;
            }            
        }        
        if (a.Owner.player || b.Owner.player) Write.KeyPress(0, 26);
        Recap(winner, loser);
        winner = null;
        loser = null;
        surrender = false;
    }

    private static void Attack(Gladiator a, Gladiator b)
    {
        int attackRoll = Return.RandomInt(1, 101);
        //A crits
        if (attackRoll < 6)
        {
            if (momentum < 0) b.TakeDamage(a, momentum, 1);
            else b.TakeDamage(a, Math.Abs(momentum), 1);
            momentum -= 3;
        }
        //B Crits
        else if (attackRoll > 95)
        {
            if (momentum > 0) a.TakeDamage(b, momentum, 1);
            else a.TakeDamage(b, -momentum, 1);
            momentum += 3;
        }
        //Up for grabs!
        else
        {
            Write.Line(0,20,attackRoll.ToString());
            //Modify based on offence
            attackRoll -= a.Offence;
            attackRoll += b.Offence;
            //If less than 50, a wins, but b's defence can mitigate
            if (attackRoll < 50) attackRoll = (attackRoll + b.Defence/2 > 50) ? 50 : attackRoll + b.Defence/2;
            //If not, b wins, but a's defence can mitigate
            else attackRoll = (attackRoll - a.Defence/2 < 50) ? 50 : attackRoll - a.Defence/2;


            if (attackRoll < 45 && attackRoll >= 25)
            {
                if (momentum < 0) b.TakeDamage(a, momentum, 3);
                else b.TakeDamage(a, Math.Abs(momentum), 3);
                momentum -= 1;
            }
            else if (attackRoll < 25 && attackRoll >= 11)
            {
                if (momentum < 0) b.TakeDamage(a, momentum, 2);
                else b.TakeDamage(a, Math.Abs(momentum), 2);
                momentum -= 2;
            }
            else if (attackRoll < 11)
            {
                if (momentum < 0) b.TakeDamage(a, momentum, 1);
                else b.TakeDamage(a, Math.Abs(momentum), 1);
                momentum -= 3;
            }
            else if (attackRoll > 55 && attackRoll <= 75)
            {
                if (momentum > 0) a.TakeDamage(b, momentum, 3);
                else a.TakeDamage(b, -momentum, 3);
                momentum += 1;
            }
            else if (attackRoll > 75 && attackRoll <= 90)
            {
                if (momentum > 0) a.TakeDamage(b, momentum, 2);
                else a.TakeDamage(b, -momentum, 2);
                momentum += 2;
            }
            else if (attackRoll > 90)
            {
                if (momentum > 0) a.TakeDamage(b, momentum, 1);
                else a.TakeDamage(b, -momentum, 1);
                momentum += 3;
            }
            else
            {
                int r = Return.RandomInt(0, 3);
                if (a.Owner.player || b.Owner.player)
                {
                    string x = (r == 0) ? "The two gladiators circle each other, neither gaining an advantage" : (r == 1) ? $"{a.name} makes a feint, but {b.name} doesn't fall for it" : $"{b.name} makes a feint, but {a.name} doesn't fall for it";
                    Write.Line(0, 24, x);
                }
            }
        }
    }

    private static void Event()
    {

    }

    private static void Recap(Gladiator winner, Gladiator loser)
    {
        Console.Clear();
        if (surrender) Write.Line(40, 1, $"{loser.name} has surrendered! {winner.name} has defeated {loser.name}");
        else Write.Line(45, 1, $"{winner.name} has defeated {loser.name}");
        winner.lastFightOutcome = 1;
        loser.lastFightOutcome = 2;
        winner.wins++;
        loser.losses++;
        Winner();
        Loser();
        Write.KeyPress(0, 28);
    }

    private static void Winner()
    {
        int n = 5;
        Write.Line(20, n - 2, Color.NAME + winner.name + Color.HEALTH + " recovery" + Color.RESET);
        if (winner.Disabled(winner.head))
        {
            int chance = Return.RandomInt(0, 7);
            if (chance < 4)
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " succumbs to his head injuries");
                winner.dead = true;
            }
            else
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " will recover from his head wounds but this experience will scar him forever");
                Write.Line(25, n+1, "TRAIT GAINED: AFRAID");
                winner.trait1 = Traits.Afraid;
                winner.head.SetHealth(2);
            }
            n++;
        }
        if (winner.Disabled(winner.torso) && winner.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " succumbs to his chest injuries");
                winner.dead = true;
            }
            else
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " will recover from his chest wounds in time");
                winner.torso.SetHealth(2);
            }
            n++;
        }
        if (winner.Disabled(winner.leftArm) && winner.missingLeftArm == false && winner.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " loses his left arm");
                if (winner.missingRightArm)
                {
                    Write.Line(0, n + 1, "Doctors can't stop the bleeding," + Color.NAME + winner.name + Color.RESET + " succumbs to his wounds");
                    winner.dead = true;
                }
                else winner.missingLeftArm = true;
            }
            else
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " will recover from his arm wounds in time");
                winner.leftArm.SetHealth(2);
            }
            n++;
        }
        if (winner.Disabled(winner.rightArm) && winner.missingRightArm == false && winner.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " loses his right arm");
                if (winner.missingLeftArm)
                {
                    Write.Line(0, n + 1, "Doctors can't stop the bleeding," + Color.NAME + winner.name + Color.RESET + " succumbs to his wounds");
                    winner.dead = true;
                }
                else winner.missingRightArm = true;
            }
            else
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " will recover from his arm wounds in time");
                winner.rightArm.SetHealth(2);
            }
            n++;
        }
        if (winner.Disabled(winner.legs) && winner.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " loses his leg");
                if (winner.missingLeg)
                {
                    Write.Line(0, n + 1, "Doctors can't stop the bleeding," + Color.NAME + winner.name + Color.RESET + " succumbs to his wounds");
                    winner.dead = true;
                }
                else
                {
                    winner.legs.hp = winner.legs.maxHp;
                    winner.missingLeg = true;
                }
            }
            else
            {
                Write.Line(0, n, Color.NAME + winner.name + Color.RESET + " will recover from his arm wounds in time");
                winner.legs.SetHealth(2);
            }
            n++;
        }
        if (n == 17) Write.Line(0, 19, Color.NAME + winner.name + Color.RESET + " has no serious injuries");
        if (winner.dead)
        {
            if (winner.Owner.player)
            {
                Graveyard.graveyard.Add(winner);
                Graveyard.killedBy.Add(loser);
                Graveyard.dayOfDeath.Add(Hub.day);
            }
            Write.Line(0, n + 2, Color.NAME + loser.name + Color.DAMAGE + " has died" + Color.RESET);
            winner.Owner.roster.Remove(winner);
        }
    }

    private static void Loser()
    {
        int n = 17;
        Write.Line(20, n-2, Color.NAME + loser.name + Color.HEALTH + " recovery" + Color.RESET);
        if (loser.Disabled(loser.head))
        {
            int chance = Return.RandomInt(0, 7);
            if (chance < 4)
            {
                Write.Line(0,n ,Color.NAME + loser.name + Color.RESET + " succumbs to his head injuries");
                loser.dead = true;                
            }
            else
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " will recover from his head wounds but this experience will scar him forever");
                Write.Line(25, n+1, "TRAIT GAINED: AFRAID");
                loser.trait1 = Traits.Afraid;
                loser.head.SetHealth(2);
            }
            n++;
        }
        if (loser.Disabled(loser.torso) && loser.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " succumbs to his chest injuries");
                loser.dead = true;                
            }
            else
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " will recover from his chest wounds in time");
                loser.torso.SetHealth(2);
            }
            n++;
        }
        if (loser.Disabled(loser.leftArm) && loser.missingLeftArm == false && loser.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " loses his left arm");
                if (loser.missingRightArm)
                {
                    Write.Line(0, n + 1, "Doctors can't stop the bleeding," + Color.NAME + loser.name + Color.RESET + " succumbs to his wounds");
                    loser.dead = true;
                }
                else loser.missingLeftArm = true;
            }
            else
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " will recover from his arm wounds in time");
                loser.leftArm.SetHealth(2);
            }
            n++;
        }
        if (loser.Disabled(loser.rightArm) && loser.missingRightArm == false && loser.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " loses his right arm");
                if (loser.missingLeftArm)
                {
                    Write.Line(0, n + 1, "Doctors can't stop the bleeding," + Color.NAME + loser.name + Color.RESET + " succumbs to his wounds");
                    loser.dead = true;
                }
                else loser.missingRightArm = true;
            }
            else
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " will recover from his arm wounds in time");
                loser.rightArm.SetHealth(2);
            }
            n++;
        }
        if (loser.Disabled(loser.legs) && loser.dead == false)
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " loses his leg");
                if (loser.missingLeg)
                {
                    Write.Line(0, n + 1, "Doctors can't stop the bleeding," + Color.NAME + loser.name + Color.RESET + " succumbs to his wounds");
                    loser.dead = true;
                }
                else
                {
                    loser.legs.hp = loser.legs.maxHp;
                    loser.missingLeg = true;
                }
            }
            else
            {
                Write.Line(0, n, Color.NAME + loser.name + Color.RESET + " will recover from his arm wounds in time");
                loser.legs.SetHealth(2);
            }
            n++;
        }
        if (n == 17) Write.Line(0,19,Color.NAME + loser.name + Color.RESET +" has no serious injuries");
        if (loser.dead)
        {
            loser.Death(winner);
            Write.Line(0,n+2,Color.NAME + loser.name + Color.DAMAGE + " has died" + Color.RESET);
        }
    }
}