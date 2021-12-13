using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;
/*Відновлення пароль користувача за відомим хешем на хеш-кодом*/
namespace Task3_2{
    class Program{
        static void Main(string[] args){
            /*ініціалізація хеш та хеш-коду*/
            string md5Text = "po1MVkAE7IjUUwu61XxgNg==";
            string md5guid = "564c8da6-0440-88ec-d453-0bbad57c6036";
            /*перетворення string зі значенням guid в тип Guid*/
            Guid newGuid = Guid.Parse(md5guid);
            /*запуск циклу для брут-форсу паролю*/
            var sw = new Stopwatch();
            sw.Start();
            for(int i=0; i<100000000; i++){
                /*перетворення числового значення в строковий*/
                string s = Convert.ToString(i);
                /*обрахування для новоствореної строки хешу*/
                var md5ForStr = ComputeHashMd5(Encoding.Unicode.GetBytes(s));
                /*обрахування для новоствореної строки значення Guid*/
                Guid guid1 = new Guid(md5ForStr);
                /*перетворення значення Guid в строковий формат*/
                string md5ForI = Convert.ToBase64String(md5ForStr);
                /*порівняння заданого хешу та хеш-коду з новоствореним.
                    Якщо значення співпадають - виводить пароль*/
                if(String.Equals(md5Text, md5ForI)&&(newGuid==guid1)){
                    Console.WriteLine();
                    Console.WriteLine("Hacked password: " + i);
                    break;
                }
                /*імітація брут-форсу*/
                if(i%1000000==0){
                    Console.Write("_");
                }
                
            }
            sw.Stop();
            Console.WriteLine("Elapsed Time : " + sw.ElapsedMilliseconds+ "ms");
            Console.ReadKey(true);
        }
        /*метод обрахування хешу алгоритмом MD5*/
        static byte[] ComputeHashMd5(byte[] dataForHash){
            using (var md5 = MD5.Create()){
                return md5.ComputeHash(dataForHash);
            }
        }
    }
}
