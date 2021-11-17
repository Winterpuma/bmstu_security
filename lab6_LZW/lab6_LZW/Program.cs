using System;

namespace lab6_LZW
{
    class Program
    {
        static void Main(string[] args)
        {
            LZWAlgo.Compress("test.rar", "tmp.txt");
            Console.WriteLine();
            LZWAlgo.Decompress("tmp.txt", "res.rar");
            Console.ReadLine();
        }
    }
}
