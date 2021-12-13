using System;
using System.Text;
using System.Security.Cryptography;

//hash based on SHA256
/*програма для реєстрації користувача за логіном/паролем та 
  авторизації шляхом співставлення відповідних логінів і паролів.*/
namespace Task4_2{
    class Program{
        static void Main(string[] args){
            /*ініціалізація реєстрації*/
            Console.WriteLine("===REGISTER===\nPlease, make your login: ");
            string? log = Console.ReadLine();
            Console.WriteLine("Now, make your password: ");
            string? pass = Console.ReadLine();
            string key = GetRandomString(32);
            /*хешування логіну та паролю*/
            var s2L = ComputeHmacsha256(Encoding.Unicode.GetBytes(log),Encoding.Unicode.GetBytes(key));
            string slog = Convert.ToBase64String(s2L);
            var s2P = ComputeHmacsha256(Encoding.Unicode.GetBytes(pass),Encoding.Unicode.GetBytes(key));
            string spass = Convert.ToBase64String(s2P);

            Console.WriteLine("Congrats! You`re succesfully registered.\n Now, let`s make the authorization.");
            bool helper = false;
            /*ініціалізація циклу в якому програма запитує користувача логін та пароль.
              Якщо хеші введених користувачем логіну та паролю співпадать з "зареєстрованими",
              то цикл завершується. Якщо ні - програма повторює запит.*/
            while(helper!=true){
                Console.WriteLine("Login: ");
                string? newLog = Console.ReadLine();
                var s2NL = ComputeHmacsha256(Encoding.Unicode.GetBytes(newLog),Encoding.Unicode.GetBytes(key));
                string s2Nlog = Convert.ToBase64String(s2NL);
                Console.WriteLine("Password: ");
                string? newPass = Console.ReadLine();
                var s2NP = ComputeHmacsha256(Encoding.Unicode.GetBytes(newPass),Encoding.Unicode.GetBytes(key));
                string s2Npass = Convert.ToBase64String(s2NP);
                if(String.Equals(slog, s2Nlog)&&String.Equals(spass,s2Npass)){
                    Console.WriteLine("Succesful authorization!");
                    helper = true;
                }
                else{
                    Console.WriteLine("You entered wrong login or password. Try again!\n");
                }
            }
            Console.ReadKey(true);
        }
        /*метод хешування*/
        public static byte[] ComputeHmacsha256(byte[] toBeHashed, byte[] key){
            using (var hmac = new HMACSHA256(key)){
                return hmac.ComputeHash(toBeHashed);
            }
        }
        /*оголошення алфавіту, а після нього і методу генерації
          рандомної строки для створення ключа*/
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

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
    }
}
