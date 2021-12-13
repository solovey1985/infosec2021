using System;

//генерація та виведення на екран послідовності псевдовипадкових чисел
namespace Task1_1{
class PRNG {
  static void Main() {
      Console.WriteLine("The same PRNG\n");

      /*ініціалізуємо змінну класу Random з заданим seed,
       на основі якого щоразу при запуску програми генерується
       однакова послідовність рандомних чисел*/
      Random rnd = new Random(1984);
      
      for (int i = 0; i < 13; i++){
          Console.WriteLine(rnd.Next());
      }
      
      /*в циклі з кожною ітерацією ініціалізується нова змінна класу Random
      у якої не заданий явний seed, тому при запуску програми послідовність щоразу інша*/
      Console.WriteLine("\nDifferent PRNG\n");
      
      for (int i = 0; i < 13; i++){
          Random drnd = new Random();
          Console.WriteLine(drnd.Next());
      }
      Console.ReadKey(true);
  }
}}

