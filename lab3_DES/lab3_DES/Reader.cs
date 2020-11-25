using System.IO;
using System.Collections;

namespace lab3_DES
{
    class Reader
    {
        private FileStream _fs;

        public Reader(string filename)
        {
            _fs = new FileStream(filename, FileMode.Open);
        }

        /// <summary>
        /// Чтение последовательности байтов из файла
        /// в качестве массива битов
        /// </summary>
        /// <param name="n">Количество байт для считывания</param>
        /// <returns>Возвращает количество считанных байт</returns>
        public int ReadNextNBytes(int n, out BitArray readData)
        {
            byte[] bytes = new byte[n];

            int i;
            for (i = 0; i < n; i++)
            {
                int cur = _fs.ReadByte();
                if (cur == -1) // конец файла
                    break;
                else
                    bytes[i] = (byte)cur;
                
            }

            readData = new BitArray(bytes);
            return i;            
        }

        public void Close()
        {
            _fs.Close();
        }
        
        ~Reader()
        {
            Close();
        }
    }
}
