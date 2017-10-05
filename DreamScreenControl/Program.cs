using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScreenControl
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DreamScreenControl.exe <ip> <mode>");
                Console.WriteLine("Modes are:");
                Console.WriteLine("0 - Off");
                Console.WriteLine("1 - Video");
                Console.WriteLine("2 - Music");
                Console.WriteLine("3 - Ambient");
            }
            else
            {
                int m = 0;
                if (int.TryParse(args[1], out m))
                {
                    if (m >= 0 && m <= 3)
                    {
                        var d = new DreamScreen(args[0]);
                        d.setMode(m);
                    }
                    else
                    {
                        Console.WriteLine("Please specify a Mode from 0 to 3");
                    }
                }
                else
                {
                    Console.WriteLine("Please specify a Mode from 0 to 3");
                }


            }

        }
    }
}
