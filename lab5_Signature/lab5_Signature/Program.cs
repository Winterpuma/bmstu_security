using System;

namespace lab5_Signature
{
    class Program
    {
        static void Main(string[] args)
        {
            var signer = new Signer("SHA512");

            string filenameData = "test.txt";
            string filenameSign = "signature.txt";

            Sign(signer, filenameData, filenameSign);
            //Check(signer, filenameData, filenameSign);
        }


        static void Sign(Signer signer, string filename, string signatureFilename)
        {
            signer.Sign(filename, signatureFilename);
        }

        static void Check(Signer signer, string filename, string signatureFilename)
        {
            if (signer.CheckSignature(filename, signatureFilename))
                Console.WriteLine("Signature is OK");
            else
                Console.WriteLine("Signature Failed");
            Console.Read();
        }
    }
}
