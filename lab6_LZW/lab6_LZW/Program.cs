using System;
using System.IO;

namespace lab6_LZW
{
    class Program
    {
        static void Main(string[] args)
        {
            string fType = "rar";
            LZWAlgo.Compress("test." + fType, "tmp.txt");
            Console.WriteLine();
            LZWAlgo.Decompress("tmp.txt", "res." + fType);
            //Console.ReadLine();

            /*
            MyBinaryWriter myBinaryWriter = new MyBinaryWriter(new FileStream("binar.txt", FileMode.Create));
            myBinaryWriter.WriteNBits(123, 9);
            myBinaryWriter.WriteNBits(456, 10);
            myBinaryWriter.Close();

            MyBinaryReader myBinaryReader = new MyBinaryReader(new FileStream("binar.txt", FileMode.Open));
            int m = myBinaryReader.ReadNBits(9);
            m = myBinaryReader.ReadNBits(10);*/
        }
    }
}
