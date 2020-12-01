using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_RSA
{
    class RSA
    {
        static Random r = new Random(1);
        static int start = 1000;
        static int stop = 9000;

        int n;
        int publicKey, privateKey;
        
        public RSA()
        {
            int p = GetRandomPrimeNum();
            int q = GetRandomPrimeNum();
            int fi = (p - 1) * (q - 1);

            n = p * q; // длина алфавита
            publicKey = ComputePublicKey(fi);
            privateKey = ComputePrivateKey(publicKey, fi);
        }

        /// <summary>
        /// Шифрование
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int Encrypt(int m)
        {
            return (int)BigInteger.ModPow(m, publicKey, n);
        }

        /// <summary>
        /// Расшифровка
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int Decrypt(int c)
        {
            return (int)BigInteger.ModPow(c, privateKey, n);
        }

        static int GetRandomPrimeNum()
        {
            int num = r.Next(start, stop);

            while (!MyMath.IsNumberPrimePrecise(num))
                num = r.Next(start, stop);

            return num;
        }

        /// <summary>
        /// НОД(fi, e) == 1
        /// </summary>
        /// <param name="fi"></param>
        /// <returns>Взаимно простое с fi число</returns>
        static int ComputePublicKey(int fi)
        {
            int nod = 0;
            int num = -1;
            while (nod != 1)
            {
                num = r.Next(2, fi);//stop);
                nod = MyMath.GCD(fi, num);
            }
            return num;
        }

        /// <summary>
        /// (e * f) mod(fi) = 1
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fi"></param>
        /// <returns></returns>
        static int ComputePrivateKey(int e, int fi)
        {
            return MyMath.Inverse(e, fi);
        }
    }
}
