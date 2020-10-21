using System;
using System.Linq;
using System.IO;

namespace lab2_Enigma
{
    class Rotor
    {
        const int _amount = 256; // byte

        /// <summary>
        /// Считаем что коммутация индекс - значение
        /// </summary>
        int[] commutations = new int[_amount];

        int rotationsMade = 0;


        /// <summary>
        /// Генерация нового ротора
        /// </summary>
        public Rotor()
        {
            CreateRandomCommutations();
        }


        /// <summary>
        /// Загрузка ранее созданного ротора
        /// </summary>
        /// <param name="fs"></param>
        public Rotor(FileStream fs)
        {
            LoadCommutations(fs);
        }


        /// <summary>
        /// Поворот ротора
        /// </summary>
        /// <returns>0 если сделал полный оборот</returns>
        public int TurnRotor()
        {
            int tmp = commutations[commutations.Length - 1];
            Array.Copy(commutations, 0, commutations, 1, commutations.Length - 1);
            commutations[0] = tmp;

            rotationsMade++;
            rotationsMade %= _amount;
            return rotationsMade;
        }


        /// <summary>
        /// Получение выходного байта по входному
        /// </summary>
        public int GetValue(int input)
        {
            return commutations[input];
        }


        /// <summary>
        /// Получение входного байта по выходному
        /// </summary>
        public int GetReversedValue(int input)
        {
            int i = 0;
            // доходим до искомого значения и возвращаем его индекс
            for (; commutations[i] != input && i < commutations.Length; i++) ;

            return i;
        }


        /// <summary>
        /// Генерация и перемешивание массива коммутаций
        /// </summary>
        private void CreateRandomCommutations()
        {
            for (int i = 0; i < commutations.Length; i++)
                commutations[i] = i;

            Random rnd = new Random();
            commutations = commutations.OrderBy(x => rnd.Next()).ToArray(); // случайно перемешиваем
        }


        /// <summary>
        /// Сохранение конфигурации ротора
        /// </summary>
        public void SaveCommutations(FileStream fs)
        {
            foreach (int i in commutations)
                fs.WriteByte((byte)i);
        }


        /// <summary>
        /// Загрузка конфигурации ротора
        /// </summary>
        private void LoadCommutations(FileStream fs)
        {
            for (int i = 0; i < commutations.Length; i++)
                commutations[i] = fs.ReadByte();
        }
    }
}
