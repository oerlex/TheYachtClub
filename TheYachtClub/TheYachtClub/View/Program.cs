using YachtClub.Controller;
using YachtClub.Model;
using System;
using System.Collections.Generic;
using TheYachtClub.View;

namespace TheYachtClub
{
    class Program
    {
        private static ConsoleHandler consoleHanlder = new ConsoleHandler();

        static void Main(string[] args)
        {
            System.Console.WriteLine("WELCOME TO THE YACHT CLUB");
            System.Console.WriteLine("PLease enter your first request");

            consoleHanlder.base_Loop();
        }
    }     
}