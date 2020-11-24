namespace lab3_DES
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader r = new Reader("test.txt");
            Writer w = new Writer("encoded.txt");
            Writer w2 = new Writer("decoded.txt");

            var read = r.ReadNextNBytes(8);
            var permutated = DataOperations.Permutate(read, DESPreDefined.encoding);
            var backToNormal = DataOperations.Permutate(permutated, DESPreDefined.decoding);

            w.WriteBits(permutated);
            w2.WriteBits(backToNormal);
        }
    }
}
