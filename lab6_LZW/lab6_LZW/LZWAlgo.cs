using System;
using System.Collections.Generic;
using System.IO;

namespace lab6_LZW
{
    static class LZWAlgo
    {
        public static List<string> InitDictionary()
        {
            List<string>  dictionary = new List<string>();
            /*
            dictionary.Add("a");
            dictionary.Add("b");
            dictionary.Add("w");
            */
            
            for (int i = 0; i < 256; i++)
                dictionary.Add(((char)i).ToString());
            /*
            for (char c = 'a'; c <= 'z'; c++)
                dictionary.Add(c.ToString());*/

            return dictionary;
        }
        
        /// <summary>
        /// Сжатие
        /// </summary>
        /// <param name="src">Путь к файлу с данными</param>
        /// <param name="dst">Путь для создания сжатого файла</param>
        public static void Compress(string src, string dst)
        {
            List<string> dictionary = InitDictionary();

            using (var input = new FileStream(src, FileMode.Open))
            {
                using (var output = new BinaryWriter(new FileStream(dst, FileMode.Create)))
                {
                    string p = "";
                    int last = input.ReadByte();
                    while (last != -1)
                    {
                        string pPlusC = p + (char)last;
                        while (dictionary.FindIndex(x => x == pPlusC) != -1)
                        {
                            p = pPlusC;
                            last = input.ReadByte();
                            pPlusC += (char)last;
                        }
                        
                        dictionary.Add(pPlusC);
                        int indexToAdd = dictionary.FindIndex(x => x == p);
                        output.Write((ushort)indexToAdd);
                        p = "";
                    }
                }
            }
        }

        /// <summary>
        /// Распаковка
        /// </summary>
        /// <param name="src">Путь к сжатому файлу</param>
        /// <param name="dst">Путь для создания распакованного файла</param>
        public static void Decompress(string src, string dst)
        {
            List<string> dictionary = InitDictionary();

            using (var input = new FileStream(src, FileMode.Open))
            {
                using (var output = new BinaryWriter(new FileStream(dst, FileMode.Create)))
                {
                    ushort cW;
                    string w = "";

                    var r1 = input.ReadByte();

                    while (r1 != -1)
                    {
                        var r2 = input.ReadByte() << 8;
                        cW = (ushort)(r2 | r1);

                        string entry = null;
                        if (cW < dictionary.Count)
                            entry = dictionary[cW];
                        else if (cW == dictionary.Count)
                            entry = w + w[0];
                        
                        foreach (char c in entry)
                            output.Write(c);

                        dictionary.Add(w + entry[0]);

                        w = entry;

                        r1 = input.ReadByte();
                    }
                }
            }
        }
        
    }
}
