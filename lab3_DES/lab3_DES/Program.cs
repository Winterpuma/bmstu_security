using System;
using System.Collections;

namespace lab3_DES
{
    class Program
    {
        static void Main(string[] args)
        {
            BitArray key = new BitArray(new byte[7] { 89, 6, 2, 52, 2, 25, 165 });
            BitArray[] roundKeys = KeyOperations.GenerateKeys(key);

            Encrypt("test.txt", "encoded.txt", roundKeys);
            Decrypt("encoded.txt", "decoded.txt", roundKeys);
        }

        static void Encrypt(string filenameIn, string filenameOut, BitArray[] roundKeys)
        {
            BitArray read;
            Reader r = new Reader(filenameIn);
            Writer w = new Writer(filenameOut);

            w.WriteToStartOfFile(0); // место для размера последнего блока

            int readNBytes = 8; // количество считанных байт
            while (readNBytes == 8) // пока не конец файла
            {
                readNBytes = r.ReadNextNBytes(8, out read);
                var encr = Cycles.EncryptionСycles(read, roundKeys);
                w.WriteBits(encr);
            }

            w.WriteToStartOfFile((byte)readNBytes); // записываем размер последнего блока
            r.Close();
            w.Close();
        }

        static void Decrypt(string filenameIn, string filenameOut, BitArray[] roundKeys)
        {
            BitArray read, decrypted = new BitArray(0);
            Reader r = new Reader(filenameIn);
            Writer w = new Writer(filenameOut);

            r.ReadNextNBytes(1, out read);
            int lastBlockBytes = ConvertToByte(read); // размер последнего блока

            int readNBytes = r.ReadNextNBytes(8, out read); ; // cчитывается первый блок
            while (readNBytes == 8) // пока не конец файла
            {
                decrypted = Cycles.DecryptionСycles(read, roundKeys);
                PrintValues("decr", decrypted, 8);

                readNBytes = r.ReadNextNBytes(8, out read);
                if (readNBytes != 0) // этот блок не последний
                    w.WriteBits(decrypted);
            }

            // обработка последнего блока
            if (lastBlockBytes != 0)
                w.WriteBits(GetFirstNBits(decrypted, lastBlockBytes * 8));

            r.Close();
            w.Close();
        }
        

        public static void PrintValues(string title, IEnumerable myList, int myWidth)
        {
            Console.WriteLine(title);
            int i = myWidth;
            foreach (Object obj in myList)
            {
                if (i <= 0)
                {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                if ((bool)obj)
                    Console.Write("1", obj);
                else
                    Console.Write("0", obj);
            }
            Console.WriteLine();
        }

        public static byte ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        public static BitArray GetFirstNBits(BitArray src, int n)
        {
            BitArray dst = new BitArray(n);
            for (int i = 0; i < n; i++)
                dst[i] = src[i];
            return dst;
        }
    }
}
