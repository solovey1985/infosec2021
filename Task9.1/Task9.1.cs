using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Task9_1{
    class DigitalSignature{
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;
        public void AssignNewKey(string publicKeyPath){
            using (var rsa = new RSACryptoServiceProvider(2048)){
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                _privateKey = rsa.ExportParameters(true);
}
}

        public byte[] SignData(byte[] hashOfDataToSign){
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature, string publicKeyPath){
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }

        static void Main(string[] args){
            string path = @"C:\Users\User\Desktop\ПЗ №9-10\Task9.1\public.cer";
            var document = Encoding.UTF8.GetBytes("Some important info!!!");
            byte[] hashedDocument;
            using (var sha256 = SHA256.Create()){
                hashedDocument = sha256.ComputeHash(document);
            }
            var digitalSignature = new DigitalSignature();
            digitalSignature.AssignNewKey(path);
            var signature = digitalSignature.SignData(hashedDocument);
            var verified = digitalSignature.VerifySignature(hashedDocument, signature, path);
            Console.WriteLine("Digital Signature Demonstration in .NET");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" Original Text = " +
            Encoding.Default.GetString(document));
            Console.WriteLine();
            Console.WriteLine(" Digital Signature = " +
            Convert.ToBase64String(signature));
            Console.WriteLine(verified
                ? "The digital signature has been correctly verified."
                : "The digital signature has NOT been correctly verified.");
            Console.ReadKey(true);
            }
            
    }
}
        