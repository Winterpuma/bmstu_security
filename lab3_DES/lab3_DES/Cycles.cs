using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace lab3_DES
{
    static class Cycles
    {
        public static BitArray EncryptionСycles(BitArray input, BitArray key)
        {
            var curArray = DataOperations.Permutate(input, DESPreDefined.IPnormal);
            var left = DataOperations.GetLeftPart(curArray);
            var right = DataOperations.GetRightPart(curArray);

            for (int iCycle = 0; iCycle < 16; iCycle++)
            {
                var rightPrev = right;
                right = left.Xor(DataOperations.FeistelFunction(right, key));
                left = rightPrev;
            }

            curArray = DataOperations.Join(left, right);
            curArray = DataOperations.Permutate(curArray, DESPreDefined.IPconverse);
            return curArray;
        }

        public static BitArray DecryptionСycles(BitArray input, BitArray key)
        {
            var curArray = DataOperations.Permutate(input, DESPreDefined.IPnormal);
            var left = DataOperations.GetLeftPart(curArray);
            var right = DataOperations.GetRightPart(curArray);

            for (int iCycle = 0; iCycle < 16; iCycle++)
            {
                var leftPrev = left;
                left = right.Xor(DataOperations.FeistelFunction(left, key));
                right = leftPrev;
            }

            curArray = DataOperations.Join(left, right);
            curArray = DataOperations.Permutate(curArray, DESPreDefined.IPconverse);
            return curArray;
        }
    }
}
