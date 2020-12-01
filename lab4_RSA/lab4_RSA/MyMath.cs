namespace lab4_RSA
{
    static class MyMath
    {
        /// <summary>
        /// Находит НОД двух чисел
        /// </summary>
        public static int GCD(int a, int b)
        {
            int temp;
            while (b != 0)
            {
                temp = a;
                a = b;
                b = temp % b;
            }
            return a;
        }

        // функция проверяет - простое ли число n
        public static bool IsNumberPrimePrecise(int num)
        {
            if (num > 1)
            {
                for (int i = 2; i < num; i++)
                    if (num % i == 0)
                        return false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Расширенный алгоритм Евклида
        /// Находит такое t, что
        /// (a * t) mod n == 1
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Inverse(int a, int n)
        {
            int t = 0;
            int r = n;
            int newt = 1;
            int newr = a;

            while (newr != 0)
            {
                var quotient = r / newr;
                var tmpt = newt;
                newt = t - quotient * newt;
                t = tmpt;

                var tmpr = newr;
                newr = r - quotient * newr;
                r = tmpr;
            }

            if (r > 1)
                return -1; // a is not invertible
            if (t < 0)
                t = t + n;

            return t;
        }
    }
}
