using System;
using System.Linq;
using System.IO;

namespace lab2_Enigma
{
    class Reflector
    {
        const int _amount = 256; // byte

        /// <summary>
        /// Считаем, что коммутация по соседним парам
        /// 0 и 1 элементы массива, 2 и 3 итд.
        /// </summary>
        int[] commutations = new int[_amount];


        /// <summary>
        /// Генерация нового рефлектора
        /// </summary>
        public Reflector()
        {
            CreateRandomCommutations();
        }


        /// <summary>
        /// Загрузка ранее созданного рефлектора
        /// </summary>
        public Reflector (FileStream fs)
        {
            LoadCommutations(fs);
        }


        /// <summary>
        /// Получение связанного с input значения
        /// </summary>
        public int GetValue(int input)
        {
            int i = 0;
            // доходим до искомого значения
            for (; commutations[i] != input && i < commutations.Length; i++) ;
            
            // возвращаем его пару
            return (i % 2 == 0) ? commutations[i + 1] : commutations[i - 1];
        }


        /// <summary>
        /// Генерация и перемешивание массива коммутации
        /// </summary>
        private void CreateRandomCommutations()
        {
            for (int i = 0; i < commutations.Length; i++)
                commutations[i] = i;

            Random rnd = new Random();
            commutations = commutations.OrderBy(x => rnd.Next()).ToArray(); // случайно перемешиваем
        }


        /// <summary>
        /// Сохранение конфигурации рефлектора
        /// </summary>
        public void SaveCommutations(FileStream fs)
        {
            foreach (int i in commutations)
                fs.WriteByte((byte)i);
        }


        /// <summary>
        /// Загрузка конфигурации рефлектора
        /// </summary>
        private void LoadCommutations(FileStream fs)
        {
            for (int i = 0; i < commutations.Length; i++)
                commutations[i] = fs.ReadByte();
        }
    }
}
