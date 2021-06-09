using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public enum Traits {None, Afraid, Fearless, }
public enum Actions { None , Resting, Repairing, Working, Strength, Offence, Defence, Endurance  }
public class Gladiator
{    
    public string name;
    public Body head;
    public Body torso;
    public Body leftArm;
    public Body rightArm;
    public Body legs;
    public bool dead;
    public int wins;
    public int kills;
    public int losses;
    public Traits trait1;
    public Traits trait2;
    public Actions action;
    public bool missingLeftArm;
    public bool missingRightArm;
    public bool missingLeg;
    public bool recovering;

    public Gladiator()
    {
        head = new Body("Head", 4, Part.Head, EquipmentList.noArmor.Copy(), 0, 3, 3, 3);
        torso = new Body("Torso", 6, Part.Torso, EquipmentList.noArmor.Copy(), 0, 1, 1, 3);
        leftArm = new Body("Left Arm", 3, Part.Arm, EquipmentList.noShield.Copy(), 3, 3, 3, 3);
        rightArm = new Body("Right Arm", 3, Part.Arm, EquipmentList.noWeapon.Copy(), 3, 5, 3, 3);        
        legs = new Body("Legs", 4, Part.Leg, EquipmentList.noArmor, 3, 3, 3, 4);
        name = Return.Name();
        trait1 = Traits.None;
        trait2 = Traits.None;
    }
    public Gladiator(int tier)
    {
        head = new Body("Head", 3 + Return.RandomInt(0,tier+1), Part.Head, EquipmentList.noArmor, 0, 2 + Return.RandomInt(0, tier + 1), 2 + Return.RandomInt(0, tier + 1), 3);
        torso = new Body("Torso", 5 + Return.RandomInt(0, tier + 1), Part.Torso, EquipmentList.noArmor, 1, 1, 1, 3);
        leftArm = new Body("Left Arm", 2 + Return.RandomInt(0, tier + 1), Part.Arm, EquipmentList.noArmor, 2 + Return.RandomInt(0, tier + 1) / 2, 2 + Return.RandomInt(0, tier + 1) / 2, 2 + Return.RandomInt(0, tier + 1) / 2, 3);
        rightArm = new Body("Right Arm", 3 + Return.RandomInt(0, tier + 1), Part.Arm, EquipmentList.noArmor, 3 + Return.RandomInt(0, tier + 1), 5 + Return.RandomInt(0, tier + 1), 3 + Return.RandomInt(0, tier + 1), 3);        
        legs = new Body("Legs", 3 + Return.RandomInt(0, tier + 1), Part.Leg, EquipmentList.noArmor, 3 + Return.RandomInt(0, tier + 1), 3 + Return.RandomInt(0, tier + 1), 3 + Return.RandomInt(0, tier + 1), 3);
        name = Return.Name();
        trait1 = Traits.None;
        trait2 = Traits.None;
    }

    public bool SurrenderCheck(Gladiator a, Gladiator b)
    {
        if (SeverelyInjured(head) || SeverelyInjured(torso) || Disabled(leftArm) || Disabled(rightArm) || Disabled(legs))
        {
            if (Disabled(leftArm) && Disabled(rightArm))
            {
                Combat.surrender = true;
                Console.Clear();
                Return.CombatDisplay(a, b);
                Write.Line(0, 24, $"With no means of defending themselves, {name} surrenders");
                return true;
            }
            if (Owner != null)
            {                
                int surrenderRoll = Return.RandomInt(0, Owner.prestige);
                if (SeverelyInjured(head)) surrenderRoll += Owner.prestige / 6;
                if (SeverelyInjured(torso)) surrenderRoll += Owner.prestige / 6;
                if (Disabled(leftArm)) surrenderRoll += Owner.prestige / 6;
                if (Disabled(rightArm)) surrenderRoll += Owner.prestige / 6;
                if (Disabled(legs)) surrenderRoll += Owner.prestige / 6;
                if (surrenderRoll > Owner.prestige)
                {
                    Combat.surrender = true;
                    Console.Clear();
                    Return.CombatDisplay(a, b);
                    Write.Line(0, 24, $"Displaying heartbreaking cowardice, {name} surrenders");
                    return true;                    
                }
            }
        }
        return false;
    }

    internal bool Disabled(Body body) => body.status == Status.Disabled;
    internal bool SeverelyInjured(Body body) => body.status == Status.SeverelyInjured;
    internal bool Injured(Body body) => body.status == Status.Injured;

    internal void TakeDamage(Gladiator g, int momentum, int level)
    {
        int dam = (g.Damage / level < 1) ? 1 : g.Damage / level;
        string flavor1 = "";
        string flavor2 = "";
        string flavor3 = "";
        while (true)
        {
            int target = Return.RandomInt(0, 20);
            if (target == 0 || target == 1)
            {
                int flav1Roll = Return.RandomInt(0, 3);
                flavor1 = (flav1Roll ==0) ? $"{g.name} stabs at {name}'s head." : (flav1Roll == 1) ? $"{g.name} cuts at {name}'s face." : $"{g.name} swings at {name}'s head."; 
                if (head.equipment.hp > 0)
                {
                    if (head.equipment.hp >= dam)
                    {
                        flavor2 = $"{name}'s armor absorbs the blow";
                        head.equipment.hp-= dam;
                    }
                    else if (head.equipment.hp > 0 )
                    {
                        flavor2 = $"{name}'s armor absorbs some of the blow";
                        head.equipment.hp = 0;
                        dam -= head.equipment.hp;                        
                    }                    
                }
                if (dam > 0)
                {
                    head.TakeDamage(dam);
                    if (head.Endurance > 0) head.Endurance -= 1;
                    if (rightArm.Endurance > 0) rightArm.Endurance -= 1;
                    if (legs.Endurance > 0) legs.Endurance -= 1;
                    flavor3 = (dam >3)?"THE BLADE BITES DEEP! Serious damage has been done!":(dam>0)? $"There is blood everywhere, making it difficult to tell how badly {name} is wounded" : $"{name} avoids the worst of it, coming away with a small cut";
                }                
                break;
            }
            else if (target == 2 || target == 3 || target == 4 || target == 5 || target == 6 || target == 7 || target == 8)
            {
                int flav1Roll = Return.RandomInt(0, 3);
                flavor1 = (flav1Roll == 0) ? $"{g.name} stabs at {name}'s chest." : (flav1Roll == 1) ? $"{g.name} cuts at {name}'s chest." : $"{g.name} swings at {name}'s chest.";
                if (torso.equipment.hp > 0)
                {
                    if (torso.equipment.hp >= dam)
                    {
                        flavor2 = $"{name}'s armor absorbs the blow";
                        torso.equipment.hp -= dam;
                    }
                    else if (torso.equipment.hp > 0)
                    {
                        flavor2 = $"{name}'s armor absorbs some of the blow";
                        torso.equipment.hp = 0;
                        dam -= torso.equipment.hp;
                    }
                }
                if (dam > 0)
                {
                    torso.TakeDamage(dam);
                    if (torso.Endurance > 0) torso.Endurance -= 1;
                    if (legs.Endurance > 0) legs.Endurance -= 1;
                    flavor3 = (dam > 3) ? "THE BLADE BITES DEEP! Serious damage has been done!" : (dam > 0) ? $"There is blood everywhere, making it difficult to tell how badly {name} is wounded" : $"{name} avoids the worst of it, coming away with a small cut";
                }
                break;
            }
            else if ((target == 9 || target == 10 || target == 11) && Disabled(leftArm) == false)
            {
                int flav1Roll = Return.RandomInt(0, 3);
                flavor1 = (flav1Roll == 0) ? $"{g.name} stabs at {name}'s arm." : (flav1Roll == 1) ? $"{g.name} cuts at {name}'s arm." : $"{g.name} swings at {name}'s arm.";
                if (leftArm.equipment.hp > 0)
                {
                    if (leftArm.equipment.hp >= dam)
                    {
                        flavor2 = $"{name}'s armor absorbs the blow";
                        leftArm.equipment.hp -= dam;
                    }
                    else if (leftArm.equipment.hp > 0)
                    {
                        flavor2 = $"{name}'s armor absorbs some of the blow";
                        leftArm.equipment.hp = 0;
                        dam -= leftArm.equipment.hp;
                    }
                }
                if (dam > 0)
                {
                    leftArm.TakeDamage(dam);
                    if (leftArm.Endurance > 0) leftArm.Endurance -= 1;
                    flavor3 = (dam > 3) ? "THE BLADE BITES DEEP! Serious damage has been done!" : (dam > 0) ? $"There is blood everywhere, making it difficult to tell how badly {name} is wounded" : $"{name} avoids the worst of it, coming away with a small cut";
                }
                break;
            }
            else if ((target == 12 || target == 13 || target == 14) && Disabled(rightArm) == false)
            {
                int flav1Roll = Return.RandomInt(0, 3);
                flavor1 = (flav1Roll == 0) ? $"{g.name} stabs at {name}'s arm." : (flav1Roll == 1) ? $"{g.name} cuts at {name}'s arm." : $"{g.name} swings at {name}'s arm.";
                if (rightArm.equipment.hp > 0)
                {
                    if (rightArm.equipment.hp >= dam)
                    {
                        flavor2 = $"{name}'s armor absorbs the blow";
                        rightArm.equipment.hp -= dam;
                    }
                    else if (rightArm.equipment.hp > 0)
                    {
                        flavor2 = $"{name}'s armor absorbs some of the blow";
                        rightArm.equipment.hp = 0;
                        dam -= rightArm.equipment.hp;
                    }
                }
                if (dam > 0)
                {
                    rightArm.TakeDamage(dam);
                    if (rightArm.Endurance > 0) rightArm.Endurance -= 1;
                    flavor3 = (dam > 3) ? "THE BLADE BITES DEEP! Serious damage has been done!" : (dam > 0) ? $"There is blood everywhere, making it difficult to tell how badly {name} is wounded" : $"{name} avoids the worst of it, coming away with a small cut";
                }
                break;
            }
            else if ((target == 15 || target == 16 || target == 17 || target == 18 || target == 19) && Disabled(legs) == false)
            {
                int flav1Roll = Return.RandomInt(0, 3);
                flavor1 = (flav1Roll == 0) ? $"{g.name} stabs at {name}'s legs." : (flav1Roll == 1) ? $"{g.name} cuts at {name}'s legs." : $"{g.name} swings at {name}'s legs.";
                if (legs.equipment.hp > 0)
                {
                    if (legs.equipment.hp >= dam)
                    {
                        flavor2 = $"{name}'s armor absorbs the blow";
                        legs.equipment.hp -= dam;
                    }
                    else if (legs.equipment.hp > 0)
                    {
                        flavor2 = $"{name}'s armor absorbs some of the blow";
                        legs.equipment.hp = 0;
                        dam -= legs.equipment.hp;
                    }
                }
                if (dam > 0)
                {
                    legs.TakeDamage(dam);
                    if (legs.Endurance > 0) legs.Endurance -= 1;
                    flavor3 = (dam > 3) ? "THE BLADE BITES DEEP! Serious damage has been done!" : (dam > 0) ? $"There is blood everywhere, making it difficult to tell how badly {name} is wounded" : $"{name} avoids the worst of it, coming away with a small cut";
                }
                break;
            }
        }
        if (Owner.player || g.Owner.player)
        {
            Write.Line(0, 23, flavor1);
            if (flavor2 != "")
            {
                Write.Line(0, 24, flavor2);
                if (flavor3 != "") Write.Line(0, 25, flavor3);
            }
            else Write.Line(0, 24, flavor3);
        }        
    }

    public void Death()
    {        
        if (Owner.player)
        {
            Graveyard.dayOfDeath.Add(Hub.day);
            Graveyard.graveyard.Add(this);
            Graveyard.killedBy.Add(this);
        }
        Owner.roster.Remove(this);
    }
    public void Death(Gladiator g)
    {        
        if (Owner.player)
        {
            Graveyard.dayOfDeath.Add(Hub.day);
            Graveyard.killedBy.Add(g);
            Graveyard.graveyard.Add(this);
        }
        Owner.roster.Remove(this);
    }


    public Owner Owner
    {
        get
        {
            foreach (Owner o in Create.owners) if (o.roster.Contains(this)) return o;
            return null;
        }
    }
    public int Damage
    {
        get
        {
            int damage = ((Strength - 5) / 2 > 0) ? (Strength - 5) / 2 : 0;
            if (rightArm.hp>0)damage += rightArm.equipment.damage;
            if (leftArm.equipment.type == EquipmentType.Weapon && leftArm.hp >0) damage += leftArm.equipment.damage / 2;
            return damage;
        }
    }
    public int TotalWeight { get { return head.equipment.weight + torso.equipment.weight + leftArm.equipment.weight + rightArm.equipment.weight; }}
    public int Offence { get { return head.Offence + torso.Offence + leftArm.Offence + rightArm.Offence + legs.Offence; } }
    public int Strength { get { return head.Strength + torso.Strength + leftArm.Strength + rightArm.Strength + legs.Strength; } }
    public int Defence { get { return head.Defence + torso.Defence + leftArm.Defence + rightArm.Defence + legs.Defence + leftArm.equipment.Defence(this); } }
    public int Endurance { get { return head.Endurance + torso.Endurance + leftArm.Endurance + rightArm.Endurance + legs.Endurance; } }
    public string Trait1 
    { 
        get 
        {
            if (trait1 == Traits.Afraid) return "AFRAID";
            if (trait1 == Traits.Fearless) return "FEARLESS";
            return "NONE";
        } 
    }
    public string Trait2
    {
        get
        {
            if (trait2 == Traits.Afraid) return "AFRAID";
            if (trait2 == Traits.Fearless) return "FEARLESS";
            return "NONE";
        }
    }

    public int Price
    {
        get
        {
            return head.price + torso.price + rightArm.price + leftArm.price + legs.price ;
        }
    }

    public string Action
    {
        get
        {
            if (action == Actions.Repairing) return Color.ENERGY + "REPAIRING" + Color.RESET;
            if (action == Actions.Resting) return Color.ENERGY + "RESTING" + Color.RESET;
            if (action == Actions.Working) return Color.ENERGY + "WORKING" + Color.RESET;
            if (action == Actions.Strength) return Color.ENERGY + "TRAINING STRENGTH" + Color.RESET;
            if (action == Actions.Offence) return Color.ENERGY + "TRAINING OFFENCE" + Color.RESET;
            if (action == Actions.Defence) return Color.ENERGY + "TRAINING DEFENCE" + Color.RESET;
            if (action == Actions.Endurance) return Color.ENERGY + "TRAINING ENDURANCE" + Color.RESET;
            return "";
        }
    }
}