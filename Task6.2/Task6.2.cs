using System;
using System.Text;
using System.Security.Cryptography;

/*Реалізація можливісті задання секретного ключа та вектора ініціалізації 
  за допомогою псевдовипадкової послідовності із використанням пароля.
  «Сіль» генерується як випадкова послідовність байтів.*/
namespace Task6_1{
    public class PBKDF2{
        /*генерація солі*/
        public static byte[] GenerateSalt(){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        /*хешування солі та паролю, для генерації псевдовипадкового ключа та вектора ініціалізації*/
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, int numbBytes){
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds)){
                return rfc2898.GetBytes(numbBytes);
            }
        }
    }
    class DesEncryption{
        /*зашифровування алгоритмом DES*/
        public byte[] EncryptDES(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new DESCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        /*розшифровування алгоритмом DES*/
        public byte[] DecryptDES(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new DESCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }              
    }

    class TripleDesEncryption{
        /*зашифровування алгоритмом TripleDES*/
        public byte[] EncryptTrDES(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new TripleDESCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        /*розшифровування алгоритмом TripleDES*/
        public byte[] DecryptTrDES(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new TripleDESCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }              
    }
    class AesEncryption{
        /*зашифровування алгоритмом AES*/
        public byte[] EncryptAES(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new AesCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        /*розшифровування алгоритмом AES*/
        public byte[] DecryptAES(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new AesCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }              
    }
    class Program{
        static void Main(string[] args){
            /*ініціалізація початкових значень*/
            int iter = 16000;
            const string original = "Text to encrypt";
            const string pass = "somepa$8w0rd";
            /*створення солі для ключа та вектору ініціалізації*/
            var saltyP = PBKDF2.GenerateSalt();
            var saltyI = PBKDF2.GenerateSalt();
            /*шифрування, розшифрування та вивід DES*/
            var des = new DesEncryption();
            var key = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(pass), saltyP, iter, 8);
            var iv = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(pass), saltyI, iter, 8);
            var encryptedDES = des.EncryptDES(Encoding.UTF8.GetBytes(original), key, iv);
            var decryptedDES = des.DecryptDES(encryptedDES, key, iv);
            var decryptedMessage = Encoding.UTF8.GetString(decryptedDES);
            Console.WriteLine("DES Demonstration");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Password = " + pass);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedDES));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);
            Console.WriteLine();
            /*шифрування, розшифрування та вивід TripleDES*/
            var trdes = new TripleDesEncryption();
            var trkey = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(pass), saltyP, iter, 24);
            var triv = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(pass), saltyI, iter, 8);
            var encryptedtrDES = trdes.EncryptTrDES(Encoding.UTF8.GetBytes(original), trkey, triv);
            var decryptedtrDES = trdes.DecryptTrDES(encryptedtrDES, trkey, triv);
            var trdecryptedMessage = Encoding.UTF8.GetString(decryptedtrDES);
            Console.WriteLine("TripleDES Demonstration");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Password = " + pass);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedtrDES));
            Console.WriteLine("Decrypted Text = " + trdecryptedMessage);
            Console.WriteLine();
            /*шифрування, розшифрування та вивід AES*/
            var aes = new AesEncryption();
            var akey = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(pass), saltyP, iter, 16);
            var aiv = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(pass), saltyI, iter, 16);
            var encryptedAES = aes.EncryptAES(Encoding.UTF8.GetBytes(original), akey, aiv);
            var decryptedAES = aes.DecryptAES(encryptedAES, akey, aiv);
            var adecryptedMessage = Encoding.UTF8.GetString(decryptedAES);
            Console.WriteLine("AES Demonstration");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Password = " + pass);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedAES));
            Console.WriteLine("Decrypted Text = " + adecryptedMessage);
            Console.ReadLine();
        }      
    }
}