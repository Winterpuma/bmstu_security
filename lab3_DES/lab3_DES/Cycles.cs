using System.Collections;

namespace lab3_DES
{
    static class Cycles
    {
        /// <summary>
        /// Циклы шифрования
        /// </summary>
        /// <param name="input">Данные для шифрования</param>
        /// <param name="roundKeys">Раундовые ключи</param>
        /// <returns>Зашифрованные данные</returns>
        public static BitArray EncryptionСycles(BitArray input, BitArray[] roundKeys)
        {
            var curArray = DataOperations.Permutate(input, DESPreDefined.IPnormal);
            var left = DataOperations.GetLeftPart(curArray);
            var right = DataOperations.GetRightPart(curArray);

            for (int iCycle = 0; iCycle < 16; iCycle++)
            {
                var rightPrev = right;
                right = left.Xor(DataOperations.FeistelFunction(right, roundKeys[iCycle]));
                left = rightPrev;
            }

            curArray = DataOperations.Join(left, right);
            curArray = DataOperations.Permutate(curArray, DESPreDefined.IPconverse);
            return curArray;
        }

        /// <summary>
        /// Циклы дешифрования
        /// </summary>
        /// <param name="input">Данные для дешифрации</param>
        /// <param name="roundKeys">Раундовые ключи</param>
        /// <returns>Дешифрованные данные</returns>
        public static BitArray DecryptionСycles(BitArray input, BitArray[] roundKeys)
        {
            var curArray = DataOperations.Permutate(input, DESPreDefined.IPnormal);
            var left = DataOperations.GetLeftPart(curArray);
            var right = DataOperations.GetRightPart(curArray);

            for (int iCycle = 0; iCycle < 16; iCycle++)
            {
                var leftPrev = left;
                left = right.Xor(DataOperations.FeistelFunction(left, roundKeys[16 - iCycle - 1]));
                right = leftPrev;
            }

            curArray = DataOperations.Join(left, right);
            curArray = DataOperations.Permutate(curArray, DESPreDefined.IPconverse);
            return curArray;
        }
    }
}
