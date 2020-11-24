using System.Collections;

namespace lab3_DES
{
    class DataOperations
    {
        /// <summary>
        /// Перестановка значений bitArray в зададанные indexes позиции
        /// </summary>
        /// <returns>Новый bitArray сформированный из старого</returns>
        public static BitArray Permutate(BitArray data, int[] indexes)
        {
            bool[] newData = new bool[indexes.Length];

            for (int i = 0; i < indexes.Length; i++)
            {
                newData[i] = data[indexes[i] - 1];
            }

            return new BitArray(newData);
        }
    }
}
