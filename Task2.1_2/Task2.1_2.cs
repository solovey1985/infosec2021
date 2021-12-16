using System;
using System.IO;
using System.Text;

namespace Task2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ініціалізація алфавіту для шифрування/розшифровування
              (повинен бути розміром 2^n для коректної роботи розшифровувача
              та мати усі символи, що задіяні у тексті для шифрування)*/
            string alph = "abcdefghijklmnopqrstuvwxyz12345 "; 
            string _key = "ohzyki"; //ініціалізація ключа для шифрування/розшифрування
            string path = @"C:\Users\User\Desktop\ПЗ №2\Task2.1_2\d.txt"; //шлях до файлу, що потрібно зашифрувати
            string pathEncr = @"C:\Users\User\Desktop\ПЗ №2\Task2.1_2\d.dat"; //шлях до файлу, який буде створений та в якому буде записано зашифрований текст з попереднього файлу
            string message;
            /*зчитування файлу d.txt*/
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    /*запис тексту з файлу до змінної message*/
                    message = sr.ReadToEnd();

                    var pad = new OnTimePad(alph);
                    /*шифрування message*/
                    string encrypt = pad.Encr(message, _key);
                    /*розшифрування зашифрованого тексту*/
                    string decrypt = pad.Encr(encrypt, _key);
                    /*вивід тексту, зчитаного з файлу та його зашифрованого вигляду*/
                    Console.WriteLine("Message: " + message);
                    Console.WriteLine("Encrypted: " + encrypt);
                    /*запис зашифрованого тексту до файлу d.dat*/
                    using (StreamWriter sw = new StreamWriter(pathEncr, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(encrypt);
                }
                    /*вивід повідомлення про успішний запис та розшифрованого тексту */
                    Console.WriteLine("File succesfully wtitten!");
                    Console.WriteLine("\nDecryption: "+ decrypt);
                }
            }
            /*вивід повідомлення про помилку*/
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey(true);
    }
}
/*клас, що виконує шифрування методом Вернама*/
class OnTimePad
{   /*ініціалізація словників для перебору*/
    Dictionary<char, int> alph = new Dictionary<char,int>();
    Dictionary<int, char> alph_r = new Dictionary<int, char>();
    /*метод для передачі відповідного алфавіту і створення двох нових для перебору*/
    public OnTimePad(IEnumerable<char> Alphabet)
    {
        int i = 0;
        foreach(char c in Alphabet)
        {
            alph.Add(c, i);
            alph_r.Add(i++, c);
        }
    }
    /*функція шифрування шифром Вернама*/
    public string Encr(string giv_text, string giv_key)
    {
        char[] key = giv_key.ToCharArray();
        char[] text = giv_text.ToCharArray();
        var sb = new StringBuilder();
 
        for (int i = 0; i < text.Length; i++)
        {
            int ind;
            if (alph.TryGetValue(text[i], out ind))
            {
                sb.Append(alph_r[(ind ^ alph[key[i % key.Length]]) % alph.Count]);
            }
        }
 
        return sb.ToString();
    }
}
}
