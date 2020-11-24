using System;
using System.IO;

namespace lab3_DES
{
    /// <summary>
    /// Класс заданных перестановок
    /// </summary>
    static class DESPreDefined
    {
        private static string _rootDirectory = @"D:\GitHub\bmstu_security\lab3_DES\permutations\";
        public static int[] encoding;
        public static int[] decoding;
        
        static DESPreDefined()
        {
            encoding = ReadIntArrayFromFile(_rootDirectory + "encoding.txt");
            decoding = ReadIntArrayFromFile(_rootDirectory + "decoding.txt");
        }

        /// <summary>
        /// Считывает массив чисел из файла, где числа разделены пробелом
        /// </summary>
        private static int[] ReadIntArrayFromFile(string filename)
        {
            string text = File.ReadAllText(filename);
            string[] splitedText = text.Split(' ');

            int[] res = new int[splitedText.Length];
            for (int i = 0; i < res.Length; i++)
                res[i] = Convert.ToInt32(splitedText[i]);

            return res;
        }
    }
}
