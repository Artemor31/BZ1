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
        string finalText;

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
            finalText = stringBuilder.ToString();
            Console.WriteLine("New message: " + finalText);
            PrintAnanlytics();
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
            finalText = stringBuilder.ToString();
            Console.WriteLine("New message: " + finalText);
            PrintAnanlytics();
        }
        /// <summary>
        /// Выводит в консоль пары ключей, пригодных для текущего алфавита
        /// </summary>
        public void PrintKeyPairs()
        {
            if (charsTable == null)
                CreateDictionary(SYMBOLS_FILE);
            int m = charsTable.Count;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if ((i*j) % m == 1)
                    {
                        Console.WriteLine(i + " : " j);
                    }
                }
            }
        }

        private void CreateDictionary(string inputFile)
        {
            string str = File.ReadAllText(inputFile);
            str = str.Replace(" ", "");
            alphabetVal = str.Length;

            charsTable = new Dictionary<int, char>(str.Length);

            for (int i = 0; i < str.Length; i++)
                charsTable.Add(i, str[i]);

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
        private void PrintAnanlytics()
        {
            List<Letter> letters = new List<Letter>();
            letters.Add(new Letter(finalText[0], 0));

            for (int i = 0; i < finalText.Length; i++)
            {
                for (int j = 0; j < letters.Count; j++)
                {
                    if (finalText[i] == letters[j].value)
                    {
                        letters[j].count++;
                        break;
                    }
                    else
                    {
                        letters.Add(new Letter(finalText[i], 0));
                    }
                }
            }

            foreach (var item in letters)
            {
                Console.WriteLine("Char: " + item.value + " Count: " + item.count);
            }
        }
    }

    public class Letter
    {
        public int count;
        public char value;

        public Letter(char c, int i)
        {
            value = c;
            count = i;
        }
    }
}
