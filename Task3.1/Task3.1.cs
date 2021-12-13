using System;
using System.Text;
using System.Security.Cryptography;

/*програма, яка обчислює хеш-коди за всіма відомими алгоритмами для заданих даних*/
namespace Task3_1{
    class Program{
         static void Main(string[] args){
             /*введення користувачем тексту для хешування*/
             Console.WriteLine("Enter the text: ");
             string? text = Console.ReadLine();
             /*хешування тексту*/
             var md5ForText = ComputeHashMd5(Encoding.Unicode.GetBytes(text));
             var sha1ForText = ComputeHashSha1(Encoding.Unicode.GetBytes(text));
             var sha256ForText = ComputeHashSha256(Encoding.Unicode.GetBytes(text));
             var sha384ForText = ComputeHashSha384(Encoding.Unicode.GetBytes(text));
             var sha512ForText = ComputeHashSha512(Encoding.Unicode.GetBytes(text));
            /*вивід введеного користувачем тексту та його хешу, створеного різними алгоритмами*/
             Console.WriteLine("Entered text: "+ text);
             Console.WriteLine("Hash MD5: " + Convert.ToBase64String(md5ForText) + " | Length: " + md5ForText.Length);
             Console.WriteLine("Hash SHA1: " + Convert.ToBase64String(sha1ForText) + " | Length: " + sha1ForText.Length);
             Console.WriteLine("Hash SHA256: " + Convert.ToBase64String(sha256ForText) + " | Length: " + sha256ForText.Length);
             Console.WriteLine("Hash SHA384: " + Convert.ToBase64String(sha384ForText) + " | Length: " + sha384ForText.Length);
             Console.WriteLine("Hash SHA512: " + Convert.ToBase64String(sha512ForText) + " | Length: " + sha512ForText.Length);

             Console.ReadKey(true);
         }
        /*хешування алгоритмом MD5*/
         static byte[] ComputeHashMd5(byte[] dataForHash){
             using (var md5 = MD5.Create()){
                return md5.ComputeHash(dataForHash);
             }
         }
         /*хешування алгоритмом SHA1*/
         public static byte[] ComputeHashSha1(byte[] toBeHashed){
             using (var sha1 = SHA1.Create()){
                return sha1.ComputeHash(toBeHashed);
             }
         }
         /*хешування алгоритмом SHA256*/
         public static byte[] ComputeHashSha256(byte[] toBeHashed){
             using (var sha256 = SHA256.Create()){
                return sha256.ComputeHash(toBeHashed);
             }
         }
         /*хешування алгоритмом SHA384*/
         public static byte[] ComputeHashSha384(byte[] toBeHashed){
             using (var sha384 = SHA384.Create()){
                return sha384.ComputeHash(toBeHashed);
             }
         } 
         /*хешування алгоритмом SHA512*/
         public static byte[] ComputeHashSha512(byte[] toBeHashed){
             using (var sha512 = SHA512.Create()){
                return sha512.ComputeHash(toBeHashed);
             }
         }       
    }
}