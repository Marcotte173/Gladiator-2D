using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Design;

namespace Marburgh2_0._01
{
   
    class Program
    {
        public static List<Owner> owners = new List<Owner> { };
        static void Main(string[] args)
        {
            Color.SetupConsole();
            string[] names = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/Names.txt");
            for (int i = 0; i < names.Length; i++) { Create.names.Add(names[i]); }
            foreach (string name in Create.names.ToList()) Create.names.Add($"{name} II");
            Console.CursorVisible = false;
            //ColorTest();
            Menu.Start();
        }

        private static void ColorTest()
        {
            Write.Line(Color.ABILITY + "ABILITY");
            Write.Line(Color.DEFENCE + "ACTION");
            Write.Line(Color.BLOOD + "BLOOD");
            Write.Line(Color.BOLD + "BOLD"); 
            Write.Line(Color.DAMAGE + "BOSS");
            Write.Line(Color.BURNING + "BURNING");
            Write.Line(Color.CLASS + "CLASS");
            Write.Line(Color.OFFENCE + "CRIT");
            Write.Line(Color.STRENGTH + "DAMAGE");
            Write.Line(Color.DURABILITY + "DEFENCE");
            Write.Line(Color.DROP + "DROP");
            Write.Line(Color.ENERGY + "ENERGY");
            Write.Line(Color.ENHANCEMENT + "ENHANCEMENT");
            Write.Line(Color.GOLD + "GOLD");
            Write.Line(Color.HEALTH + "HEALTH");
            Write.Line(Color.NPC + "HIT");
            Write.Line(Color.ITEM + "ITEM");
            Write.Line(Color.MITIGATION + "MITIGATION");
            Write.Line(Color.MONSTER + "MONSTER");
            Write.Line(Color.NAME + "NAME");
            Write.Line(Color.RAREDROP + "RAREDROP");
            Write.Line(Color.RESET + "RESET");
            Write.Line(Color.ENDURANCE + "SHIELD");
            Write.Line(Color.SPEAK + "SPEAK");
            Write.Line(Color.SPECIAL + "SPECIAL");
            Write.Line(Color.STUNNED + "STUNNED");
            Write.Line(Color.TIME + "TIME");
            Write.Line(Color.XP + "XP");
            Write.KeyPress();
        }
    }
}
