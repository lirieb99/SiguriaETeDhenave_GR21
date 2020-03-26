using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0].Equals("frequency"))
            {
                string plaintexti = args[1];
                Frequency(plaintexti);

            }
            else if (args[0].Equals("playfairs"))
            {
                if (args[1].Equals("encrypt")){

                }
                else if (args[1].Equals("decrypt"))
                {

                }
                if (args[0].Equals("e"))
                {
                    string plaintexti = args[1];
                    string ciphertexti = Encrypt(plaintexti);
                    Console.WriteLine("Teksti i enkriptuar eshte:" + ciphertexti);

                }

                if (args[0].Equals("d"))
                {
                    string ciphertexti = args[1];
                    string DecryptedTexti = Decrypt(ciphertexti);
                    Console.WriteLine("Teksti i dekriptuar eshte:" + DecryptedTexti);

                }
            }
        }

        static void Frequency(string text)
        {
            string teksti = text.ToLower();
            int gjatesiaEtekstit = 0;
            for (int i = 0; i < teksti.Length; i++)
            {
                if (!(teksti[i] == 32))
                    gjatesiaEtekstit++;

            }
            int numriISimboleve = 0;
            Console.WriteLine("Total: " + gjatesiaEtekstit);
            int[] paraqitjaESimbolit = new int[127 - 33];
            int[] pStemp = new int[127 - 33];
            for (int b = 33; b < 127; b++)
            {
                int count = 0;
                for (int i = 0; i < teksti.Length; i++)
                {
                    if (teksti[i] == b)
                    {
                        count++;
                    }
                }
                paraqitjaESimbolit[b - 33] = count;
                pStemp[b - 33] = count;
                if (count > 0)
                    numriISimboleve++;
            }


            int[] poz = new int[127 - 33];


            for (int i = 0; i < numriISimboleve; i++)
            {
                int ind = i;
                int max = pStemp[i];
                for (int j = 0; j < 127 - 33; j++)
                {
                    if (pStemp[j] >= max)
                    {
                        max = pStemp[j];
                        ind = j;
                    }

                }
                poz[i] = ind;
                pStemp[ind] = 0;
            }

            for (int i = 0; i < numriISimboleve; i++)
            {
                double perqindja = paraqitjaESimbolit[poz[i]] / (gjatesiaEtekstit * 1.0) * 100;
                Console.WriteLine((char)(poz[i] + 33) + ": " + paraqitjaESimbolit[poz[i]] + " (" + perqindja + "%)");
            }
            Console.WriteLine("");
            for (int i = 0; i < numriISimboleve; i++)
            {
                double perqindja = paraqitjaESimbolit[poz[i]] / (gjatesiaEtekstit * 1.0) * 100;
                string t = "";
                string spaces = "";
                for (int j = 0; j < paraqitjaESimbolit[poz[i]]; j++)
                {
                    t += "#";
                }
                for (int j = 0; j < gjatesiaEtekstit - paraqitjaESimbolit[poz[i]]; j++)
                {
                    spaces += " ";
                }
                Console.WriteLine((char)(poz[i] + 33) + ": [" + t + spaces + "] " + perqindja + " %");
            }
        }
        //playfair cipher 
        static char[,] matricaCeles =
       {
            {'M','O','N','A','R'},
            {'C','H','Y','B','D'},
            {'E','F','G','I','K'},
            {'L','P','Q','S','T'},
            {'U','V','W','X','Z'}
        };
        static string Encrypt(string plaintext)
        {
            if (plaintext.Length % 2 == 1)
                plaintext = plaintext + "X";
            plaintext = plaintext.Replace("J", "I");

            StringBuilder sbCiphertext = new StringBuilder(plaintext);
            for (int i = 0; i < sbCiphertext.Length; i = i + 2)
            {
                char ch1 = plaintext[i];
                char ch2 = plaintext[i + 1];
                if (ch1 == ch2)
                    ch2 = 'X';
                int X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;
                for (int rr = 0; rr < 5; rr++)
                    for (int k = 0; k < 5; k++)
                    {
                        if (ch1 == matricaCeles[rr, k])
                        {
                            X1 = rr;
                            Y1 = k;

                        }
                        if (ch2 == matricaCeles[rr, k])
                        {
                            X2 = rr;
                            Y2 = k;

                        }

                    }
                char encCh1 = ' ', encCh2 = ' ';

                if (X1 == X2)
                {
                    encCh1 = matricaCeles[X1, (Y1 + 1) % 5];
                    encCh2 = matricaCeles[X2, (Y2 + 1) % 5];

                }
                else if (Y1 == Y2)
                {
                    encCh1 = matricaCeles[(X1 + 1) % 5, Y1];
                    encCh2 = matricaCeles[(X2 + 1) % 5, Y2];


                }
                else
                {
                    encCh1 = matricaCeles[X1, Y2];
                    encCh2 = matricaCeles[X2, Y1];

                }

                sbCiphertext[i] = encCh1;
                sbCiphertext[i + 1] = encCh2;



            }
            return sbCiphertext.ToString();
        }

        static string Decrypt(string ciphertext)
        {
            StringBuilder sbDecryptedText = new StringBuilder(ciphertext);

            for (int i = 0; i < sbDecryptedText.Length; i += 2)
            {
                char encCh1 = ciphertext[i];
                char encCh2 = ciphertext[i + 1];

                int X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;

                for (int rr = 0; rr < 5; rr++)
                    for (int k = 0; k < 5; k++)
                    {
                        if (encCh1 == matricaCeles[rr, k])
                        {
                            X1 = rr;
                            Y1 = k;

                        }

                        if (encCh2 == matricaCeles[rr, k])
                        {
                            X2 = rr;
                            Y2 = k;

                        }

                    }
                char decCh1 = ' ', decCh2 = ' ';
                if (X1 == X2)
                {
                    decCh1 = matricaCeles[X1, (Y1 - 1 + 5) % 5];
                    decCh2 = matricaCeles[X2, (Y2 - 1 + 5) % 5];

                }
                else if (Y1 == Y2)
                {
                    decCh1 = matricaCeles[(X1 - 1 + 5) % 5, Y1];
                    decCh2 = matricaCeles[(X2 - 1 + 5) % 5, Y2];


                }
                else
                {
                    decCh1 = matricaCeles[X1, Y2];
                    decCh2 = matricaCeles[X2, Y1];

                }
                sbDecryptedText[i] = decCh1;
                sbDecryptedText[i + 1] = decCh2;


            }
            return sbDecryptedText.ToString();

        }
        static void GenerateKey(string keyword)
        {
            StringBuilder sbAlfabeti = new StringBuilder();
            for (int i = 65; i < 91; i++)
            {
                if ((char)i == 'J')
                    continue;

                sbAlfabeti.Append((char)i);
            }

            for (int i = 0; i < keyword.Length; i++)
                sbAlfabeti = sbAlfabeti.Replace(keyword[i].ToString(), "");

            int k = 0; int l = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)

                {
                    while (k < keyword.Length && IsPresent(keyword, k))
                    {
                        k++;
                    }
                    if (k < keyword.Length)
                    {
                        matricaCeles[i, j] = keyword[k];
                        k++;
                    }
                    else
                    {
                        matricaCeles[i, j] = sbAlfabeti[l];
                        l++;

                    }
                }
            Console.WriteLine("matrica celes e formuar eshte: ");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {

                    Console.Write(matricaCeles[i, j] + "");
                }
                Console.WriteLine();
            }
        }

        static bool IsPresent(string keyword, int index)
        {
            char letter = keyword[index];
            bool result = false;
            for (int i = 0; i < index; i++)
            {
                if (keyword[i] == letter)
                {
                    result = true;
                    break;

                }
            }
            return result;

        }
    }
}
