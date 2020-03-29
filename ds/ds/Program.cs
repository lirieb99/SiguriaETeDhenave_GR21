using System;
using System.IO;
using System.Text;

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
            else if (args[0].Equals("playfair"))
            {
                if (args[1].Equals("encrypt")){
                    string keyword = args[2];
                    keyword = keyword.ToLower();
                    string plaintexti = args[3];
                    plaintexti = plaintexti.ToLower();
                    string ciphertexti = pfEncrypt(plaintexti, keyword);
                    Console.WriteLine("Teksti i enkriptuar eshte:" + ciphertexti);
                   
                }
                else if (args[1].Equals("decrypt"))
                {
                    string keyword = args[2];
                    keyword = keyword.ToLower();
                    string ciphertexti = args[3];
                    ciphertexti = ciphertexti.ToLower();
                    string DecryptedTexti = pfDecrypt(ciphertexti,keyword);
                    Console.WriteLine("Teksti i dekriptuar eshte:" + DecryptedTexti);
                
                }
                else if (args[1].Equals("table"))
                {
                    string keyword = args[2];
                    keyword = keyword.ToLower();
                    GenerateKey(keyword);
                    ShowKey();
                }
            }
            else if (args[0].Equals("beale"))
            {
                if (args[1].Equals("encrypt"))
                {
                    string libri = args[2];
                    string teksti = args[3];

                    bEncrypt(libri, teksti);
                }
                if (args[1].Equals("decrypt"))
                {
                    string libri = args[2];
                    string teksti = args[3];

                    bDecrypt(libri, teksti);
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
        static char[,] matricaCeles = new char[5, 5];
        static string pfEncrypt(string plaintext,string keyword)
        {
            GenerateKey(keyword);

            plaintext = plaintext.Replace(" ", "");
            if (plaintext.Length % 2 == 1)
                plaintext = plaintext + "w";
            plaintext = plaintext.Replace("j", "i");

            StringBuilder sbCiphertext = new StringBuilder(plaintext);
            for (int i = 0; i < sbCiphertext.Length; i = i + 2)
            {
                char ch1 = plaintext[i];
                char ch2 = plaintext[i + 1];
                if (ch1 == ch2)
                    ch2 = 'w';
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

        static string pfDecrypt(string ciphertext,string keyword)
        {
            GenerateKey(keyword);
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
            for (int i = 97; i < 123; i++)
            {
                if ((char)i == 'j')
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
        }
        static void ShowKey()
        {
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

        //beale chiper
        static void bEncrypt(string libri, string tekstiHyres)
        {
            StringBuilder teksti = new StringBuilder(tekstiHyres);
            int[] tekstiEncripted = new int[teksti.Length];
            string file = File.ReadAllText(libri).ToLower();

            for (int i = 0; i < teksti.Length; i++)
            {
                for (int j = 0; j < file.Length; j++)
                {
                    if (teksti[i] == file[j])
                    {
                        tekstiEncripted[i] = j + 1;
                        break;
                    }
                }
            }
            foreach (int i in tekstiEncripted)
            {
                Console.Write(i + " ");

            }
        }
        static void bDecrypt(string libri, string tekstiIEnkriptuar)
        {
            string[] stringu = tekstiIEnkriptuar.Split(' ');
            string file = File.ReadAllText(libri).ToLower();
            foreach (string numri in stringu)
            {
                int j = Convert.ToInt32(numri) - 1;
                Console.Write(file[j]);
            }


        }
    }
}
