using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Task5_3{
    public class PBKDF2{
        /*метод генерації "солі"*/
        public static byte[] GenerateSalt(){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        /*метод конкатенації повідомлення та "солі"*/
         private static byte[] Comb(byte[] first, byte[] second){
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
        /*методи алгоритмів хешування*/
        public static byte[] HPMD5(byte[] toBeHashed, byte[] salt, int numberOfRounds){
             for(int i = 0; i < numberOfRounds; i++){
                 using (var md5 = MD5.Create()){
                    toBeHashed = md5.ComputeHash(Comb(toBeHashed, salt));
                }
             }
             return toBeHashed;
        }
        public static byte[] HPSHA1(byte[] toBeHashed, byte[] salt, int numberOfRounds){
             for(int i = 0; i < numberOfRounds; i++){
                 using (var sha1 = SHA1.Create()){
                    toBeHashed = sha1.ComputeHash(Comb(toBeHashed, salt));
                }
             }
             return toBeHashed;
        }
        public static byte[] HPSHA256(byte[] toBeHashed, byte[] salt, int numberOfRounds){
             for(int i = 0; i < numberOfRounds; i++){
                 using (var sha256 = SHA256.Create()){
                    toBeHashed = sha256.ComputeHash(Comb(toBeHashed, salt));
                }
             }
             return toBeHashed;
        }
        public static byte[] HPSHA384(byte[] toBeHashed, byte[] salt, int numberOfRounds){
             for(int i = 0; i < numberOfRounds; i++){
                 using (var sha384 = SHA1.Create()){
                    toBeHashed = sha384.ComputeHash(Comb(toBeHashed, salt));
                }
             }
             return toBeHashed;
        }
        public static byte[] HPSHA512(byte[] toBeHashed, byte[] salt, int numberOfRounds){
             for(int i = 0; i < numberOfRounds; i++){
                 using (var sha512 = SHA512.Create()){
                    toBeHashed = sha512.ComputeHash(Comb(toBeHashed, salt));
                }
             }
             return toBeHashed;
        }
       
    }

    class Program{
        static void Main(){
            const string passwordToHash = "TheStrongestPassword";
            int iter = 16000;
            /*меню для вибору алгоритму хешування*/
            Console.WriteLine("-----ALGORYTHM-----\n1. MD5\n2. SHA1\n3. SHA256\n4. SHA384\n5. SHA512");
            Console.WriteLine("Choose the algorythm: ");
            int ans = Convert.ToInt32(Console.ReadLine());
            switch(ans){
                case 1:
                {
                    Console.WriteLine("You chose MD5.");
                    for(int i = 0; i < 10; i++){
                        HashMD5(passwordToHash, iter);
                        iter+=50000;
                    }
                    break;
                }
                case 2:
                {
                    Console.WriteLine("You chose SHA1.");
                    for(int i = 0; i < 10; i++){
                        HashSHA1(passwordToHash, iter);
                        iter+=50000;
                    }
                    break;
                }
                case 3:
                {
                    Console.WriteLine("You chose SHA256.");
                    for(int i = 0; i < 10; i++){
                        HashSHA256(passwordToHash, iter);
                        iter+=50000;
                    }
                    break;
                }
                case 4:
                {
                    Console.WriteLine("You chose SHA384.");
                    for(int i = 0; i < 10; i++){
                        HashSHA384(passwordToHash, iter);
                        iter+=50000;
                    }
                    break;
                }
                case 5:
                {
                    Console.WriteLine("You chose SHA512.");
                    for(int i = 0; i < 10; i++){
                        HashSHA512(passwordToHash, iter);
                        iter+=50000;
                    }
                    break;
                }

            }
            

            Console.ReadLine();
        }

    /*методи хешування та обчислення витраченого на нього часу в мс*/
        private static void HashMD5(string passwordToHash, int numberOfRounds){
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HPMD5(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
        }
        private static void HashSHA1(string passwordToHash, int numberOfRounds){
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HPSHA1(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
        }
        private static void HashSHA256(string passwordToHash, int numberOfRounds){
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HPSHA256(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
        }
        private static void HashSHA384(string passwordToHash, int numberOfRounds){
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HPSHA384(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
        }
        private static void HashSHA512(string passwordToHash, int numberOfRounds){
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HPSHA512(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
        }
    }
}