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
            MultipticativeCrypter muliCry = new MultipticativeCrypter();
            AdditiveEncryptor additiveEncryptor = new AdditiveEncryptor();
            PlayfereEncryptor playfereEncryptor = new PlayfereEncryptor();
            playfereEncryptor.Encrypt();
            Console.ReadKey();
        }
    }
}
