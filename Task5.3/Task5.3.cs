using System;
using System.Text;
using System.Security.Cryptography;

/*Клас SaltedHash, що реалізує хешування паролів із додаванням додаткової ентропії. 
  Обчислення хешу для заданого паролю та "солі".*/
namespace Task5_3{
    class SaltedHash{
        /*метод генерації "солі"*/
        public static byte[] GenerateSalt(){
            const int saltLength = 32;
            using(var randGen = new RNGCryptoServiceProvider()){
                var randNumb = new byte[saltLength];
                randGen.GetBytes(randNumb);
                return randNumb;
            }
        }
        /*метод конкатенації тексту повідомлення та "солі"*/
        private static byte[] Comb(byte[] first, byte[] second){
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
        /*метод хешування повідомлення з "сіллю"*/
        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt){
            using (var sha256 = SHA256.Create()){
                return sha256.ComputeHash(Comb(toBeHashed, salt));
            }
        }
    }

    class Program{
        static void Main(){
            /*перевірка роботи класу SaltedHash*/
            const string password = "V3ryC0mpl3xP455w0rd";
            byte[] salt = SaltedHash.GenerateSalt();
            Console.WriteLine("Password : " + password);
            Console.WriteLine("Salt = " + Convert.ToBase64String(salt));
            var hashedPassword1 = SaltedHash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password),salt);
            Console.WriteLine("Hashed Password = " + Convert.ToBase64String(hashedPassword1));
            Console.ReadLine();
        }
    }
}
