using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
/*Для програми з п.1. реалізувати можливість збереження відкритого ключа у файлі. 
Реалізувати можливість зашифровувати повідомлення за допомогою файлів відкритих ключів інших користувачів.*/
namespace Task7_2{
    class CodingRsa{
        /*оголошення відкритого і секретного ключів*/
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        
        /*присвоєння значень ключам*/
        public void AssignNewKey(string publicKeyPath){
            using (var rsa = new RSACryptoServiceProvider(2048)){
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                /*запис публічного ключа у файл*/
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                Console.WriteLine(">>>Public Key is written in file succesfully.");
                _privateKey = rsa.ExportParameters(true);
            }
        }
        /*метод шифрування*/
        public byte[] EncryptData(byte[] dataToEncrypt, string path){
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.PersistKeyInCsp = false;
                /*зчитування публічного ключа з файлу*/
                using (var sr = new StreamReader(path)){  
                    string keytxt = sr.ReadToEnd();
                    rsa.FromXmlString(keytxt);
                    rsa.PersistKeyInCsp = true;
                }
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
            /*замість наступної строки path можна зробити ввід шляху до файлу самим користувачем*/
            string path = @"C:\Users\User\Desktop\ПЗ №7-8\Task7.2\public.cer";
            rsaParams.AssignNewKey(path);
            
            var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original), path);
            var decrypted = rsaParams.DecryptData(encrypted);
            /*демонстрація шифр\розшифр*/
            Console.WriteLine("\nRSA Encryption Demonstration in .NET");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("\nEncrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("\nDecrypted Text = " + Encoding.Default.GetString(decrypted));
            Console.ReadKey(true);            
            
        }
    }
}