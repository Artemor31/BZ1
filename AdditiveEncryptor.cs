using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BZ1
{
    public class AdditiveEncryptor : IEncryptor 
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
            /////////////////////////////
            Console.WriteLine(alphabetVal);

            for (int i = 0; i < message.Length; i++)
            {
                int a = (charsTable.FirstOrDefault(x => x.Value == message[i]).Key - decryptKey) % alphabetVal;
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
                int a = (charsTable.FirstOrDefault(x => x.Value == message[i]).Key + encryptKey) % alphabetVal;
                char s = charsTable[a];
                stringBuilder.Append(s);
            }
            finalText = stringBuilder.ToString();
            Console.WriteLine("New message: " + finalText);
            PrintAnanlytics();
        }
        private void PrintAnanlytics()
        {
            List<Letter> letters = new List<Letter>();
            letters.Add(new Letter(finalText[0], 0));
            bool a = false;

            for (int i = 0; i < finalText.Length; i++)
            {
                a = true;
                for (int j = 0; j < letters.Count; j++)
                {
                    if (finalText[i] == letters[j].value)
                    {
                        a = false;
                        letters[j].count++;
                        break;
                    }
                }
                if (a)
                {
                    letters.Add(new Letter(finalText[i], 1));
                }
            }

            foreach (var item in letters)
            {
                Console.WriteLine("Char: " + item.value + " Count: " + item.count);
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
            while (!Int32.TryParse(Console.ReadLine(), out encryptKey))
                Console.Write("Enter encrypt key: ");

            decryptKey = encryptKey;
        }

        private void ReadDecryptKey()
        {
            if (charsTable == null)
                CreateDictionary(SYMBOLS_FILE);

            Console.Write("Enter decrypt key: ");
            while (!Int32.TryParse(Console.ReadLine(), out decryptKey))
                Console.Write("Enter decrypt key: ");

            encryptKey = decryptKey;
        }
    }
}
