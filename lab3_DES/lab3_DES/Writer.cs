using System.IO;
using System.Collections;

namespace lab3_DES
{
    class Writer
    {
        private FileStream _fs;

        public Writer(string filename)
        {
            _fs = new FileStream(filename, FileMode.Create);
        }

        /// <summary>
        /// Запись последовальности битов в файл
        /// </summary>
        public void WriteBits(BitArray bitsToWrite)
        {
            byte[] byteArr = new byte[(bitsToWrite.Length - 1) / 8 + 1];
            bitsToWrite.CopyTo(byteArr, 0);

            foreach (byte curByte in byteArr)
                _fs.WriteByte(curByte);
        }

        public void WriteToStartOfFile(byte b)
        {
            _fs.Seek(0, SeekOrigin.Begin);
            _fs.WriteByte(b);
        }

        public void Close()
        {
            _fs.Close();
        }

        ~Writer()
        {
            Close();
        }
    }
}
