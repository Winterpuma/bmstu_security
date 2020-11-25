using System.Collections;

namespace lab3_DES
{
    static class KeyOperations
    {
        /// <summary>
        /// Генерация раундовых ключей
        /// </summary>
        /// <param name="key">56 бит</param>
        /// <returns>Массив рауновых ключей</returns>
        public static BitArray[] GenerateKeys(BitArray key)
        {
            BitArray[] result = new BitArray[16];

            // Расширение ключа до 64 бит
            var extendedKey = ExtendKey(key);

            // Начальная перестановка B (поиск C0, D0)
            BitArray ci = DataOperations.Permutate(extendedKey, DESPreDefined.keyC0);
            BitArray di = DataOperations.Permutate(extendedKey, DESPreDefined.keyD0);
            
            for (int iKey = 0; iKey < 16; iKey++)
            {
                // Сдвиг Si
                ci = ShiftLeft(ci, DESPreDefined.keyShift[iKey]);
                di = ShiftLeft(di, DESPreDefined.keyShift[iKey]);

                // Объединение ci и di
                var joined = DataOperations.Join(ci, di);

                // Сжимающая перестановка CP
                result[iKey] = DataOperations.Permutate(joined, DESPreDefined.keyCP);
            }
            
            return result;
        }

        /// <summary>
        ///  Добавляются биты в позиции 8, 16, 24, 32, 40, 48, 56, 64 ключа k 
        ///  таким образом, чтобы каждый байт содержал нечетное число единиц
        /// </summary>
        /// <param name="key">56 бит</param>
        /// <returns>Ключ 64 бит</returns>
        public static BitArray ExtendKey(BitArray key)
        {
            BitArray extendedKey = new BitArray(64);

            for (int iByte = 0; iByte < 8; iByte++)
            {
                int oneCount = 0;
                for (int iBit = 0; iBit < 7; iBit++)
                {
                    bool curBit = key[iByte * 7 + iBit];
                    extendedKey[iByte * 8 + iBit] = curBit;
                    if (curBit)
                        oneCount++;
                }

                // В текущем байте д.б. нечетное количество единиц
                extendedKey[iByte * 8 + 7] = oneCount % 2 == 0; 
            }

            return extendedKey;
        }
        
        /// <summary>
        /// Кольцевой сдвиг влево
        /// </summary>
        /// <param name="step">Шаг сдвига</param>
        public static BitArray ShiftLeft(BitArray data, int step)
        {
            step %= data.Count;
            BitArray shifted = new BitArray(data.Count);
            for (int i = 0; i < data.Count - step; i++)
                shifted[i] = data[i + step];
            for (int i = 0; i < step; i++)
                shifted[data.Count - (step - i)] = data[i];
            shifted[data.Count - 1] = data[0];
            return shifted;
        }
    }
}
