using System.Collections.Generic;
using System.IO;

namespace lab2_Enigma
{
    class Enigma
    {
        Reflector reflector;
        List<Rotor> rotors = new List<Rotor>(); // не больше 255


        /// <summary>
        /// Создание новой машины
        /// </summary>
        /// <param name="nRotors">Количество роторов [1; 255]</param>
        public Enigma(int nRotors)
        {
            reflector = new Reflector();

            for (int i = 0; i < nRotors; i++)
                rotors.Add(new Rotor());
        }


        /// <summary>
        /// Загрузка машины из файла конфигураций
        /// </summary>
        public Enigma(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);

            int nRotors = fs.ReadByte();

            reflector = new Reflector(fs);

            for (int i = 0; i < nRotors; i++)
                rotors.Add(new Rotor(fs));

            fs.Close();
        }


        /// <summary>
        /// Побайтная обработка файла
        /// </summary>
        /// <param name="src">Исходный файл</param>
        /// <param name="dst">Результирующий файл</param>
        public void ProcessFile(string src, string dst)
        {
            FileStream fsSrc = new FileStream(src, FileMode.Open);
            FileStream fsDst = new FileStream(dst, FileMode.Create);

            int cur;
            while (fsSrc.CanRead)
            {
                cur = fsSrc.ReadByte();
                if (cur == -1)
                    break;
                else;
                fsDst.WriteByte((byte)ProcessOneByte(cur));
            }

            fsSrc.Close();
            fsDst.Close();
        }


        /// <summary>
        /// Обработка одного байта
        /// </summary>
        /// <param name="a">Входной байт</param>
        /// <returns>Выходной байт</returns>
        private int ProcessOneByte(int a)
        {
            foreach (Rotor r in rotors)
                a = r.GetValue(a);

            a = reflector.GetValue(a);

            for (int i = rotors.Count - 1; i >= 0; i--)
                a = rotors[i].GetReversedValue(a);

            // поворачиваем роторы
            for (int i = rotors.Count - 1, turn = 0; i >= 0 && (turn == 0); i--)
                turn = rotors[i].TurnRotor();

            return a;
        }


        /// <summary>
        /// Сохранение состояния энигмы в файл конфигураций
        /// </summary>
        public void SaveEnigmaState(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);

            fs.WriteByte((byte)rotors.Count);

            reflector.SaveCommutations(fs);

            foreach (Rotor r in rotors)
                r.SaveCommutations(fs);

            fs.Close();
        }
    }
}
