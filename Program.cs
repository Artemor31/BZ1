using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZ1
{
    class Program
    {
        const string SYMBOLS_FILE = "symbols.txt";

        static void Main(string[] args)
        {

            ChooseEncryptor(ReadEncryptorType());
            Console.ReadKey();
        }

        static int ReadEncryptorType()
        {
            bool isValid = false;
            int dis = 0;
            do
            {
                Console.WriteLine("Choose encryptor: \n<1> Additive\n<2> Multiplicative\n<3>Playfere");
                isValid = Int32.TryParse(Console.ReadLine(), out dis);
            } while (dis < 1 && dis > 3 && !isValid);
            return dis;
        }

        static void ChooseEncryptor(int encryptorType)
        {
            IEncryptor encryptor;
            switch (encryptorType)
            {
                case 1:
                    encryptor = new AdditiveEncryptor();
                    ChooseEncryptOrDecrypt(ReadEncryptorOrDecrypt(), encryptor);
                    break;
                case 2:
                    encryptor = new MultipticativeCrypter();
                    ChooseEncryptOrDecrypt(ReadEncryptorOrDecrypt(), encryptor);
                    break;
                case 3:
                    encryptor = new PlayfereEncryptor();
                    ChooseEncryptOrDecrypt(ReadEncryptorOrDecrypt(), encryptor);
                    break;
                default:
                    Console.WriteLine("error");
                    return;
            }
        }
        static int ReadEncryptorOrDecrypt()
        {
            bool isValid = false;
            int dis = 0;
            do
            {
                Console.WriteLine("Choose <1> encrypt or <2> decrypt ");
                isValid = Int32.TryParse(Console.ReadLine(), out dis);
            } while (dis < 1 && dis > 2 && !isValid);
            return dis;
        }

        static void ChooseEncryptOrDecrypt(int op, IEncryptor encryptor)
        {
            switch (op)
            {
                case 1:
                    encryptor.Encrypt();
                    break;
                case 2:
                    encryptor.Decrypt();
                    break;
                default:
                    Console.WriteLine("error");
                    break;
            }
        }
    }
}
