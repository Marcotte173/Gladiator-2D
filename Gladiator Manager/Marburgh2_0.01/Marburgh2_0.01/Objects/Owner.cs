using System.Collections.Generic;

public class Owner
{
    public string name;
    public bool player;
    public int gold;
    public int prestige;
    public int actions;
    public bool equipmentManager;
    public bool healthManager;
    public bool strengthManager;
    public bool offenceManager;
    public bool defenceManager;
    public bool enduranceManager;
    public bool prestigeManager;
    public bool moneyManager;
    public List<Gladiator> roster = new List<Gladiator> { };

    public Owner()
    {
        name = Return.Name();
        gold = 2000;
        prestige = 10;
        actions = 3;
        roster.Add(new Gladiator());
    }
}