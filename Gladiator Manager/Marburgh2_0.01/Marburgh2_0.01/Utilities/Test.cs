using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

internal class Test
{
    public static void Combat()
    {
        Gladiator a = new Gladiator();
        Gladiator b = new Gladiator();
        Owner one = new Owner();
        Owner two = new Owner();
        Create.owners.Add(one);
        Create.owners.Add(two);
        one.player = true;
        one.roster.Add(a);
        two.roster.Add(b);
        global::Combat.Start(a,b);
    }
}