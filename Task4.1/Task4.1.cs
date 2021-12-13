using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
/*програма для обчислення хеш-коду автентифікації повідомлення, 
    а також реалізація можливості перевірки автентичності повідомлення.*/
namespace Task4_1{
    class Program{
        static void Main(string[] args){
            /*введення користувачем повідомлення*/
            Console.WriteLine("Enter the message: ");
            string? message = Console.ReadLine();
            /*генерація ключа*/
            string key = GetRandomString(32);
            /*хешування*/
            var md5Mess = ComputeHmacmd5(Encoding.Unicode.GetBytes(message),Encoding.Unicode.GetBytes(key));
            string md5Str = Convert.ToBase64String(md5Mess);
            Console.WriteLine($"HMAC MD5: {md5Str}");    

            //верифікація хешу 
            Console.WriteLine("Enter the message: ");
            string? newMess = Console.ReadLine();
            var md5NM = ComputeHmacmd5(Encoding.Unicode.GetBytes(newMess),Encoding.Unicode.GetBytes(key));
            string newHMAC = Convert.ToBase64String(md5NM);
            
                    //порівнювання хешів
                    if(String.Equals(md5Str,newHMAC)){
                        Console.WriteLine("Succesful verification!");
                    }
                    else{
                        Console.WriteLine("Verification wrong.");
                    }
            Console.ReadKey(true);
        }

        /*оголошення алфавіту*/
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        /*метод генерації випадкової строки(для генерації рандомного ключа)*/
        static string GetRandomString(int length){
            string s = "";
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider()){
                while (s.Length != length){
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (valid.Contains(character)){
                        s += character;
                    }
                }
            }
            return s;
        }
        /*метод хешування*/
        public static byte[] ComputeHmacmd5(byte[] toBeHashed, byte[] key){
            using (var hmac = new HMACMD5(key)){
                return hmac.ComputeHash(toBeHashed);
            }
        }
    }
}