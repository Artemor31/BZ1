using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BZ1
{
    class MultipticativeCrypter : IEncryptor
    {
        const string SYMBOLS_FILE = "symbols.txt";

        private Dictionary<int, char> charsTable = null;
        private int encryptKey, decryptKey, alphabetVal;

        public void Decrypt()
        {
            ReadDecryptKey();

            Console.WriteLine("Enter massage to decrypt: ");
            string message = Console.ReadLine();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < message.Length; i++)
            {
                int a = (charsTable.FirstOrDefault(x => x.Value == message[i]).Key * decryptKey) % alphabetVal;
                char s = charsTable[a];
                stringBuilder.Append(s);
            }
            Console.WriteLine("New message: " + stringBuilder);
        }
        public void Encrypt()
        {
            ReadEncryptKey();
            Console.WriteLine("Enter massage to encrypt: ");
            string message = Console.ReadLine();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < message.Length; i++)
            {
                int a = (charsTable.FirstOrDefault(x => x.Value == message[i]).Key * encryptKey) % alphabetVal;
                char s = charsTable[a];
                stringBuilder.Append(s);
            }
            Console.WriteLine("New message: " + stringBuilder);
        }

        private void CreateDictionary(string inputFile)
        {
            string str = File.ReadAllText(inputFile);
            str = str.Replace(" ", "");
            alphabetVal = str.Length;

            charsTable = new Dictionary<int, char>(str.Length);

            for (int i = 0; i < str.Length; i++)
                charsTable.Add(i, str[i]);

            foreach (var item in charsTable)
                Console.WriteLine(item.Key + " : " + item.Value);
        }

        private void ReadEncryptKey()
        {
            if (charsTable == null)
                CreateDictionary(SYMBOLS_FILE);
            Console.Write("Enter encrypt key: ");

            while (!Int32.TryParse(Console.ReadLine(),out encryptKey))
                Console.Write("Enter encrypt key: ");

            for (int i = 2; i < alphabetVal; i++)
            {
                if ((encryptKey * i) % alphabetVal == 1)
                {
                    decryptKey = i;
                    return;
                }
            }
            Console.WriteLine("can't create key");
        }

        private void ReadDecryptKey()
        {
            if (charsTable == null)
                CreateDictionary(SYMBOLS_FILE);

            Console.Write("Enter decrypt key: ");
            while (!Int32.TryParse(Console.ReadLine(), out decryptKey))
                Console.Write("Enter decrypt key: ");

            for (int i = 2; i < alphabetVal; i++)
            {
                if ((decryptKey * i) % alphabetVal == 1)
                {
                    encryptKey = i;
                    return;
                }
            }
            Console.WriteLine("can't create key");
        }
    }
}
