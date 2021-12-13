using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Task5_3{
    public class PBKDF2{
        /*метод генерації солі*/
        public static byte[] GenerateSalt(){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        /*мето хешування повідомлення та солі*/
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds){
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds)){
                return rfc2898.GetBytes(20);
            }
        }
    }
class Program{
        static void Main(string[] args){
            int numberOfRounds = 16000;/*кількість ітерацій*/
            var genSalt = PBKDF2.GenerateSalt();/*генерація солі*/
            /*реєстрація користувача*/
            Console.WriteLine("===REGISTER===\nPlease, make your login: ");
            string? log = Console.ReadLine();            
            Console.WriteLine("Now, make your password: ");
            string? pass = Console.ReadLine();
            /*хешування логіну та паролю*/
            var hLog = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(log), genSalt, numberOfRounds);
            string hLstr = Convert.ToBase64String(hLog);
            var hPass = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(pass), genSalt, numberOfRounds);
            string hPstr = Convert.ToBase64String(hPass);

            Console.WriteLine("Congrats! You`re succesfully registered.\n Now, let`s make the authorization.");
            Console.WriteLine("Your salt: " + Convert.ToBase64String(genSalt));
            bool helper = false;
            /*цикл, який запитує у користувача логін і пароль,
              порівнює хеші введених значень з "зареєстрованими" і, якщо вони співпадають,
              завершує роботу. Якщо ні - повторює запит.*/
            while(helper!=true){
                Console.WriteLine("Login: ");
                string? newLog = Console.ReadLine();
                var nhl = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(newLog), genSalt, numberOfRounds);
                string nhLstr = Convert.ToBase64String(nhl);
                Console.WriteLine("Password: ");
                string? newPass = Console.ReadLine();
                var nhp = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(newPass), genSalt, numberOfRounds);
                string nhPstr = Convert.ToBase64String(nhp);
                if(String.Equals(hLstr, nhLstr)&&String.Equals(hPstr,nhPstr)){
                    Console.WriteLine("Succesful authorization!");
                    helper = true;
                }
                else{
                    Console.WriteLine("You entered wrong login or password. Try again!\n");
                }
            }
            Console.ReadKey(true);
        }
    }
            
}
