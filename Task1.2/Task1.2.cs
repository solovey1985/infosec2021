using System;
using System.Security.Cryptography;

namespace Task1_2{
class RNGCSP
{
    /*генерація та виведення на екран криптографічно стійкої послідовності випадкових чисел
     за допомогою класу RNGCryptoServiceProvider
     (Implements a cryptographic Random Number Generator (RNG) using the implementation
     provided by the cryptographic service provider (CSP))*/
    public static void Main()
    {
        var rng = new RNGCryptoServiceProvider();
        var bytes = new byte[8];
        for(int i = 0; i<13; i++){
            rng.GetBytes(bytes);
            Console.WriteLine(BitConverter.ToUInt64(bytes));
        }
    Console.ReadKey(true);
    }
    }
    }
