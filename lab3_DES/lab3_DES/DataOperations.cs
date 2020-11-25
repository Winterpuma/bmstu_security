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
                newData[i] = data[indexes[i] - 1];

            return new BitArray(newData);
        }

        /// <summary>
        /// Функция Фейстеля
        /// </summary>
        /// <param name="right">Правый блок (32 бита)</param>
        /// <param name="key">Ключ (48 бит)</param>
        public static BitArray FeistelFunction(BitArray right, BitArray key)
        {
            // Расширение 
            var curBitArray = Permutate(right, DESPreDefined.expansion);

            // Сложение по модулю 2 с ключом
            curBitArray.Xor(key);

            // Преобразование S
            int[] sTransformRes = GetTransformedS(curBitArray);
            curBitArray = Convert4BitIntArrayToBitArray(sTransformRes);

            // Перестановка P
            curBitArray = Permutate(curBitArray, DESPreDefined.finishP);

            return curBitArray;
        }

        /// <summary>
        /// Получает преобразование для каждого (из восьми) 6 битового значения
        /// в 4х битовое значение при помощи преобразования S
        /// </summary>
        private static int[] GetTransformedS(BitArray right)
        {
            int[] sTransformRes = new int[8];
            for (int iBlock = 0; iBlock < 8; iBlock++)
            {
                int iCurBlockStart = iBlock * 6;

                int a = 0;
                a += right[iCurBlockStart] ? 1 : 0;
                a += right[iCurBlockStart + 5] ? 2 : 0;

                int b = 0;
                b += right[iCurBlockStart + 1] ? 1 : 0;
                b += right[iCurBlockStart + 2] ? 2 : 0;
                b += right[iCurBlockStart + 3] ? 4 : 0;
                b += right[iCurBlockStart + 4] ? 8 : 0;

                sTransformRes[iBlock] = DESPreDefined.sBlocks[iBlock][a][b];
            }
            return sTransformRes;
        }

        /// <summary>
        /// Преобразует массив чисел[0;15] в массив бит
        /// </summary>
        private static BitArray Convert4BitIntArrayToBitArray(int[] intArr)
        {
            BitArray res = new BitArray(intArr.Length * 4);

            for (int iBlock = 0, iCurBlockStart = 0; iBlock < intArr.Length; iBlock++, iCurBlockStart += 4)
            {
                int curNum = intArr[iBlock];
                if (curNum >= 16)
                    throw new System.Exception("Передано число занимающее более чем 4 бита");

                if (curNum >= 8)
                {
                    res[iCurBlockStart + 3] = true;
                    curNum -= 8;
                }
                if (curNum >= 4)
                {
                    res[iCurBlockStart + 2] = true;
                    curNum -= 4;
                }
                if (curNum >= 2)
                {
                    res[iCurBlockStart + 1] = true;
                    curNum -= 2;
                }
                if (curNum == 1)
                    res[iCurBlockStart] = true;
            }

            return res;
        }
        
        /// <summary>
        /// Возвращает левую половину data
        /// </summary>
        public static BitArray GetLeftPart(BitArray data)
        {
            BitArray res = new BitArray(data.Count / 2);

            for (int i = 0; i < res.Count; i++)
                res[i] = data[i];

            return res;
        }

        /// <summary>
        /// Возвращает правую половину data
        /// </summary>
        public static BitArray GetRightPart(BitArray data)
        {
            BitArray res = new BitArray(data.Count / 2);

            for (int iRes = 0, i = data.Count / 2; i < data.Count; i++, iRes++)
                res[iRes] = data[i];

            return res;
        }

        /// <summary>
        /// Объединяет левую и правую части данных
        /// </summary>
        public static BitArray Join(BitArray left, BitArray right)
        {
            BitArray res = new BitArray(left.Count + right.Count);
            bool[] resb = new bool[left.Count + right.Count];
            left.CopyTo(resb, 0);
            right.CopyTo(resb, left.Count);

            return new BitArray(resb);
        }
    }
}
