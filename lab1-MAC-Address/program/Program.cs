using System;
using System.IO;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CheckAuthority())
                Console.WriteLine("Hello World!");

            Console.ReadKey();
        }

        static bool CheckAuthority()
        {
            string filename = "coolfile.txt";
            if (!File.Exists(filename))
            {
                Console.WriteLine("Please, run installer first.");
                return false;
            }

            byte[] dataHash = File.ReadAllBytes(filename);
            if (installer.Program.VerifyCurrentMachine(dataHash))
                return true;
            else
                Console.WriteLine("Error. Different machine.");
            return false;
        }


    }
}
