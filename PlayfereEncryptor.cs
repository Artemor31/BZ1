using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZ1
{
    class PlayfereEncryptor : IEncryptor
    {
        const string SYMBOLS_FILE = "symbols.txt";
        char[] message = new char[] { 's' };
        string alphabet = File.ReadAllText(SYMBOLS_FILE).Replace(" ", "");

        int firstI, firstJ;
        int seconsI, secondJ;



        string s1 = "", s2 = ""; //строки для хранения зашифрованного символа 
        string encodetString = ""; 
        int rows = 0, columns = 0;
        int i, j;
        string text = "";

        bool isValid = false;
        
        public void Decrypt()
        {

        }

        public void Encrypt()
        {
            ReadColumnCountFromConsole();
            ReadMessageFormConsole();

            var table = new char[rows, columns];

            // Вписываем в нее ключевое слово
            for (i = 0; i < message.Length; i++)
            {
                table[i / columns, i % columns] = message[i];
            }
            // Исключаем уникальные символы ключевого слова из алфавита
            char[] alphabetChar = alphabet.ToCharArray().Except(message).ToArray();

            // Вписываем алфавит
            for (i = 0; i < alphabet.Length; i++)
            {
                int position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    Console.Write(table[i, j] + " ");
                }
                Console.WriteLine();
            }





            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
                sb.Append(message[i]);
            Console.WriteLine("message: " + sb);
            Console.WriteLine("col and row : " + columns + " : " + rows);
        }

        private void ReadMessageFormConsole()
        {
            while (message.Except(alphabet).Any())
            {
                Console.WriteLine("Write message to encrypt");
                message = Console.ReadLine().Replace(" ", "").ToCharArray().Distinct().ToArray();
            }
        }

        private void ReadColumnCountFromConsole()
        {
            Console.WriteLine("Enter column count: ");
            while (!(Int32.TryParse(Console.ReadLine(), out columns)))// && isValid))
            {
            //    if(isValid)
            //    if(columns != 0)
            //        rows = alphabet.Length / columns;
            //    isValid = (rows > 1) && (rows * columns == alphabet.Length) && (columns > 1);
                Console.WriteLine("Enter valid column count");
            }
            rows = alphabet.Length / columns;
        }


    }
}
