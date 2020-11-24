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
        /// <param name="n">Количество байтов для считывания</param>
        public BitArray ReadNextNBytes(int n)
        {
            byte[] bytes = new byte[n];

            for (int i = 0; i < n; i++)
                bytes[i] = (byte)_fs.ReadByte();
            // TODO if EOF (not multiple of 64 for DES)

            return new BitArray(bytes);            
        }
    }
}
