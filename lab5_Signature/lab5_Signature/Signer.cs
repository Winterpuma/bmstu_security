using System.Security.Cryptography;
using System.IO;

namespace lab5_Signature
{
    class Signer
    {
        HashAlgorithm hashAlgorithm;

        RSACryptoServiceProvider cryptoProvider;
        RSAPKCS1SignatureDeformatter deformatter;
        RSAPKCS1SignatureFormatter formatter;

        public Signer(string hashAlgorithmName)
        {
            hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);

            cryptoProvider = new RSACryptoServiceProvider();
            formatter = new RSAPKCS1SignatureFormatter(cryptoProvider);
            deformatter = new RSAPKCS1SignatureDeformatter(cryptoProvider);

            formatter.SetHashAlgorithm(hashAlgorithmName);
            deformatter.SetHashAlgorithm(hashAlgorithmName);
        }

        /// <summary>
        /// Подписывает документ
        /// </summary>
        /// <param name="filename">Путь к документу</param>
        /// <param name="signatureFilename">Путь для создания подписи</param>
        public void Sign(string filename, string signatureFilename)
        {
            using (var reader = new FileStream(filename, FileMode.Open)) 
            {
                using (var writer = new BinaryWriter(new FileStream(signatureFilename, FileMode.Create)))
                {
                    var key = cryptoProvider.ExportCspBlob(false);
                    writer.Write(key.Length);
                    writer.Write(key);

                    var hash = hashAlgorithm.ComputeHash(reader);
                    var signed_hash = formatter.CreateSignature(hash);
                    int hash_len = signed_hash.Length;

                    writer.Write(hash_len);
                    writer.Write(signed_hash);
                }
            }
        }

        /// <summary>
        /// Проверяет соответствие подписи и документа
        /// </summary>
        /// <param name="filename">Путь к документу</param>
        /// <param name="signatureFilename">Путь к подписи</param>
        /// <returns>true - подпись соответствует документу</returns>
        public bool CheckSignature(string filename, string signatureFilename)
        {
            bool isCorrect = false;
            using (var signature_reader = new BinaryReader(new FileStream(signatureFilename, FileMode.Open)))
            {
                using (var reader = new FileStream(filename, FileMode.Open))
                {
                    var key = signature_reader.ReadBytes(signature_reader.ReadInt32());
                    cryptoProvider.ImportCspBlob(key);

                    var tail = signature_reader.ReadInt32();

                    var hash = hashAlgorithm.ComputeHash(reader);
                    var signed_hash = signature_reader.ReadBytes(tail);

                    isCorrect = deformatter.VerifySignature(hash, signed_hash);
                }
            }
            return isCorrect;
        }
    }
}
