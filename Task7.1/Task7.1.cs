using System;
using System.Text;
using System.Security.Cryptography;

/*Програма, яка виконує зашифровування та розшифровування даних 
  з використанням алгоритмів асиметричного шифрування RSA.*/
namespace Task7_1{
    class CodingRsa{
        /*оголошення відкритого і секретного ключів*/
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;
        /*присвоєння значень ключам*/
        public void AssignNewKey(){
            using (var rsa = new RSACryptoServiceProvider(2048)){
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        /*метод шифрування*/
        public byte[] EncryptData(byte[] dataToEncrypt){
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);
                cipherbytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipherbytes;
        }
        /*метод розшифрування*/
        public byte[] DecryptData(byte[] dataToEncrypt){
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                plain = rsa.Decrypt(dataToEncrypt, true);
            }
            return plain;
        }
        
    }

    class Program{
        static void Main(string[] args){
            var rsaParams = new CodingRsa();
            const string original = "Text for encryption";
            rsaParams.AssignNewKey();
            var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine("RSA Encryption Demonstration in .NET");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("\nEncrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("\nDecrypted Text = " + Encoding.Default.GetString(decrypted));
            Console.ReadKey(true);            
            
        }
    }
}