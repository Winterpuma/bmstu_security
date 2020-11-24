namespace lab3_DES
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader r = new Reader("test.txt");
            Writer w = new Writer("res.txt");
            
            var read = r.ReadNextNBytes(6);
            w.WriteBits(read);

        }
    }
}
