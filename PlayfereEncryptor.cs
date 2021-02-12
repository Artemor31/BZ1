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
        char[] keyWord = new char[] { 's' };
        char[] alphabet = File.ReadAllText(SYMBOLS_FILE).Replace(" ", "").ToCharArray();
        char[,] table;
        int firstI, firstJ, secondI, secondJ;
        int rows = 0, columns = 0;
        string text = "";

        public void Decrypt()
        {
            return;
        }

        public void Encrypt()
        {
            ReadKeyWordFormConsole();
            ReadColumnCountFromConsole();
            PutKeyWordInTable();
            EncryptPairs(FormateNewArray());
        }

        private void PutKeyWordInTable()
        {
            table = new char[rows, columns];
            for (int i = 0; i < keyWord.Length; i++)
                table[i / columns, i % columns] = keyWord[i];

            alphabet = alphabet.Except(keyWord).ToArray();
            for (int i = 0; i < alphabet.Length; i++)
            {
                int position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(table[i, j] + " ");
                Console.WriteLine();
            }

        }
        private void ReadKeyWordFormConsole()
        {
            while (keyWord.Except(alphabet).Any())
            {
                Console.WriteLine("Write keyWord");
                keyWord = Console.ReadLine().Replace(" ", "").ToCharArray().Distinct().ToArray();
            }
        }
        private void ReadColumnCountFromConsole()
        {
            Console.WriteLine("Enter column count: ");
            while (!(Int32.TryParse(Console.ReadLine(), out columns)))// && isValid))
            {
                Console.WriteLine("Enter valid column count");
            }
            rows = alphabet.Length / columns;
        }
        private void GetEncryptableText()
        {
            Console.WriteLine("Enter text:");
            text = Console.ReadLine();

            if (text.Length % 2 != 0)
                text = text.PadRight((text.Length + 1), 'я');
        }
        private string[] FormateNewArray()
        {
            GetEncryptableText();
            int len = text.Length / 2;
            string[] str = new string[len];
            int l = -1;

            for (int i = 0; i < text.Length; i += 2)
            {
                l++;
                if (l < len)
                {
                    //new_array[i] = old_array[i] + old_array[i+1]
                    str[l] = Convert.ToString(text[i]) + Convert.ToString(text[i + 1]);
                }
            }
            return str;
        }
        private void EncryptPairs(string[] str)
        {
            string encodetString = "";
            string s1 = null, s2 = null;
            foreach (string both in str)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        //first symb
                        if (both[0] == (table[i, j]))
                        {
                            firstI = i;
                            firstJ = j;

                        }
                        //second symb
                        if (both[1] == (table[i, j]))
                        {
                            secondI = i;
                            secondJ = j;

                        }
                    }
                }
                if (firstI == secondI)
                    PairInSameRow(out s1, out s2);

                if (firstJ == secondJ)
                    PairInSameColumn(out s1, out s2);

                if (firstI != secondI && firstJ != secondJ)
                    PairInDifferentColAndRow(out s1, out s2);


                if (s1 == s2)
                    encodetString = encodetString + s1 + "я" + s2;
                else
                    encodetString = encodetString + s1 + s2;
            }
            Console.WriteLine(encodetString);
        }
        private void PairInSameRow(out string s1, out string s2)
        {
            if (firstJ == columns - 1) /*если символ последний в строке, кодируем его первым символом из матрицы*/
                s1 = Convert.ToString(table[firstI, 0]);
            else //если символ не последний, кодируем его стоящим справа от него
                s1 = Convert.ToString(table[firstI, firstJ + 1]);

            if (secondJ == columns - 1) /*если символ последний в строке кодируем его первым символом из матрицы*/
                s2 = Convert.ToString(table[secondI, 0]);
            else //если символ не последний, кодируем его стоящим справа от него
                s2 = Convert.ToString(table[secondI, secondJ + 1]);
        }
        private void PairInSameColumn(out string s1, out string s2)
        {
            if (firstI == rows - 1)
                s1 = Convert.ToString(table[0, firstJ]);
            else
                s1 = Convert.ToString(table[firstI + 1, firstJ]);

            if (secondI == rows - 1)
                s2 = Convert.ToString(table[0, secondJ]);
            else
                s2 = Convert.ToString(table[secondI + 1, secondJ]);
        }
        private void PairInDifferentColAndRow(out string s1, out string s2)
        {
            s1 = Convert.ToString(table[firstI, secondJ]);
            s2 = Convert.ToString(table[secondI, firstJ]);
        }
    }
}