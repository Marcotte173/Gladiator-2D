using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

public class Menu
{
    internal static void Start()
    {
        Console.Clear();
        Write.Line(35,5,Color.DAMAGE + "  ____ _           _ _       _             ");
        Write.Line(35,6,Color.DAMAGE + " / ___| | __ _  __| (_) __ _| |_ ___  _ __ ");
        Write.Line(35,7,Color.DAMAGE + "| |  _| |/ _` |/ _` | |/ _` | __/ _ \\| '__|");
        Write.Line(35,8,Color.DAMAGE + "| |_| | | (_| | (_| | | (_| | || (_) | |   ");
        Write.Line(35,9,Color.DAMAGE + " \\____|_|\\__,_|\\__,_|_|\\__,_|\\__\\___/|_|   ");
        Write.Line(35,10,Color.DAMAGE + "                                           ");
        Write.Line(35,12, Color.HEALTH + " __  __                                    ");
        Write.Line(35,13, Color.HEALTH + "|  \\/  | __ _ _ __   __ _  __ _  ___ _ __  ");
        Write.Line(35,14, Color.HEALTH + "| |\\/| |/ _` | '_ \\ / _` |/ _` |/ _ \\ '__| ");
        Write.Line(35,15, Color.HEALTH + "| |  | | (_| | | | | (_| | (_| |  __/ |    ");
        Write.Line(35,16, Color.HEALTH + "|_|  |_|\\__,_|_| |_|\\__,_|\\__, |\\___|_|    ");
        Write.Line(35,17, Color.HEALTH + "                           |___/           ");
        Write.Line(0,Console.WindowHeight - 4, Color.RESET + "[1] New Game");
        //Write.Line(0, Console.WindowHeight - 3, "[2] Load Game");
        Write.Line(0, Console.WindowHeight - 2, "[0] Quit");
        Write.Line(100, Console.WindowHeight - 2, "Version 0.40");
        string choice = Return.Option();
        Console.Clear();
        if (choice == "1")
        {
            Create.Player();
            Create.Opponents();           
            Hub.Start();           
        }
        if (choice == "2")
        {
            Test.Combat();
        }
        if (choice == "0") Environment.Exit(0);
        Start();
    }
}