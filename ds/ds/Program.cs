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
    }
}
