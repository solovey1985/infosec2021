using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

/*Програма, яка виконує зашифровування та розшифровування даних з використанням 
  алгоритмів симетричного шифрування DES, Triple-DES, AES. 
  Секретний ключ та вектор ініціалізації генерується випадковим чином.*/
namespace Task6_1{
    /*клас алгоритму симетричного шифрування DES*/
    class DesEncryption{
        /*метод генерації випадкового числа для створення ключа та вектору ініціалізації*/
        public byte[] GenerateRandomNumber(int length){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        /*метод зашифровування тексту алгоритмом DES*/
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
        /*метод розшифровування тексту алгоритмом DES*/
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
        /*метод генерації випадкового числа для створення ключа та вектору ініціалізації*/
        public byte[] GenerateRandomNumber(int length){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        /*метод зашифровування тексту алгоритмом TripleDES*/
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
        /*метод розшифровування тексту алгоритмом TripleDES*/
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
        /*метод генерації випадкового числа для створення ключа та вектору ініціалізації*/
        public byte[] GenerateRandomNumber(int length){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        /*метод зашифровування тексту алгоритмом АES*/
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
        /*метод розшифровування тексту алгоритмом АES*/
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
        const string original = "Text to encrypt";
        /*шифрування тексту original алгоритмом DES*/
        var des = new DesEncryption();
        var key = des.GenerateRandomNumber(8);
        var iv = des.GenerateRandomNumber(8);
        var encryptedDES = des.EncryptDES(Encoding.UTF8.GetBytes(original), key, iv);
        var decryptedDES = des.DecryptDES(encryptedDES, key, iv);
        var decryptedMessage = Encoding.UTF8.GetString(decryptedDES);
        /*вивід*/
        Console.WriteLine("DES Demonstration");
        Console.WriteLine("-------------------------");
        Console.WriteLine("Original Text = " + original);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedDES));
        Console.WriteLine("Decrypted Text = " + decryptedMessage);
        Console.WriteLine();
        /*шифрування тексту original алгоритмом TripleDES*/
        var trdes = new TripleDesEncryption();
        var trkey = trdes.GenerateRandomNumber(24);
        var triv = trdes.GenerateRandomNumber(8);
        var encryptedtrDES = trdes.EncryptTrDES(Encoding.UTF8.GetBytes(original), trkey, triv);
        var decryptedtrDES = trdes.DecryptTrDES(encryptedtrDES, trkey, triv);
        var trdecryptedMessage = Encoding.UTF8.GetString(decryptedtrDES);
        /*вивід*/
        Console.WriteLine("TripleDES Demonstration");
        Console.WriteLine("-------------------------");
        Console.WriteLine("Original Text = " + original);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedtrDES));
        Console.WriteLine("Decrypted Text = " + trdecryptedMessage);
        Console.WriteLine();
        /*шифрування тексту original алгоритмом AES*/
        var aes = new AesEncryption();
        var akey = aes.GenerateRandomNumber(16);
        var aiv = aes.GenerateRandomNumber(16);
        var encryptedAES = aes.EncryptAES(Encoding.UTF8.GetBytes(original), akey, aiv);
        var decryptedAES = aes.DecryptAES(encryptedAES, akey, aiv);
        var adecryptedMessage = Encoding.UTF8.GetString(decryptedAES);
        /*вивід*/
        Console.WriteLine("AES Demonstration");
        Console.WriteLine("-------------------------");
        Console.WriteLine("Original Text = " + original);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encryptedAES));
        Console.WriteLine("Decrypted Text = " + adecryptedMessage);
        Console.ReadLine();
        } 
   
    }
}