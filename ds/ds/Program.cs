﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;


namespace ds
{
    class Program
    {
        static RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        static void Main(string[] args)
        {
            DeleteUser D = new DeleteUser();
            CreateUser C = new CreateUser();
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
                    Console.WriteLine(ciphertexti);
                   
                }
                else if (args[1].Equals("decrypt"))
                {
                    string keyword = args[2];
                    keyword = keyword.ToLower();
                    string ciphertexti = args[3];
                    ciphertexti = ciphertexti.ToLower();
                    string DecryptedTexti = pfDecrypt(ciphertexti,keyword);
                    Console.WriteLine(DecryptedTexti);
                
                }
                else if (args[1].Equals("table"))
                {
                    string keyword = args[2];
                    keyword = keyword.ToLower();
                    GenerateKey(keyword);
                    ShowKey();
                }
                else
                {
                    Console.WriteLine("Gabim");
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
            else if (args[0].Equals("create-user"))
            {
                string text = args[1];
                if (Regex.IsMatch(text, "^[A-Za-z0-9_.]+$"))
                {
                    //FilePath
                    string privateKeyfilePath = "keys/" + text + ".xml";
                    string publicKeyfilePath = "keys/" + text + ".pub.xml";

                    //Check nese egziston ndonje file me ate emer ne direktorin keys
                    bool privateKeyExist = File.Exists(privateKeyfilePath);
                    bool publicKeyExist = File.Exists(publicKeyfilePath);

                    if (!(privateKeyExist || publicKeyExist))
                    {
                        C.InsertIntoDB(text);
                        //Perdorimi i funksionit GenerateRsaKey per te krijuar qelesat privat dhe public me madhesi 1024(sipas deshires)
                        C.GenerateRsaKey(privateKeyfilePath, publicKeyfilePath, 1024);
                        //Trego qe u krijuan qelsat
                        Console.WriteLine("Eshte krijuar celesi privat " + "'keys/" + args[1] + ".xml'");
                        Console.WriteLine("Eshte krijuar celesi public " + "'keys/" + args[1] + ".pub.xml'");
                    }
                    else
                    {
                        Console.WriteLine("File me ate emer egziston ne folderin keys, provo tjeter emer!");
                    }
                }
                else
                {
                    Console.WriteLine("Username duhet te permbaj vetem shkronja, numra dhe _ dhe .");
                    Environment.Exit(1);
                }
            }
            else if (args[0].Equals("delete-user"))
            {
                string username = args[1];
                if (Regex.IsMatch(username, "^[A-Za-z0-9_.]+$"))
                {
                    //FilePath
                    string filePath = "keys/" + username + ".xml";
                    string filePath1 = "keys/" + username + ".pub.xml";

                    //Check nese egziston ndonje file me ate emer ne direktorin keys
                    bool privateKeyExist = File.Exists(filePath);
                    bool publicKeyExist = File.Exists(filePath1);

                    if ((privateKeyExist && publicKeyExist))
                    {
                        //Perdorimi i funksionit DeleteRsaKey per te fshire qelesat privat dhe public me madhesi 1024(sipas deshires)
                        D.DeletefromDB(username);
                        D.DeleteRsaKey(filePath, filePath1);

                        //Trego qe u fshin qelsat
                        Console.WriteLine("Eshte larguar celesi privat " + "'keys/" + username + ".xml'");
                        Console.WriteLine("Eshte larguar celesi publik " + "'keys/" + username + ".pub.xml'");
                    }
                    else if (publicKeyExist)
                    {
                        D.DeletefromDB(username);
                        D.DeleteRsaKey(filePath1);
                        Console.WriteLine("Eshte larguar celesi publik " + "'keys/" + username + ".pub.xml'");
                    }
                    else if (privateKeyExist)
                    {
                        D.DeletefromDB(username);
                        D.DeleteRsaKey(filePath);
                        Console.WriteLine("Eshte larguar celesi privat " + "'keys/" + username + ".xml'");
                    }
                    else
                    {
                        Console.WriteLine("Celesi '" + username + "' nuk ekziston.");
                    }
                }
                }
            else if (args[0].Equals("export-key"))
            {

                string ispublic = args[1];
                string username = args[2];
                string filePath = "";
                if (args.Length > 3)
                {
                    filePath = args[3];
                }
                ExportKey(ispublic, username, filePath);
            }
            else if (args[0].Equals("import-key"))
            {

                string username = args[1];
                string filePath = args[2];
                string xml = filePath.Substring(filePath.Length - 4);
                if (xml.Equals(".xml"))
                {
                    ImportKey(username, filePath);
                }
                else
                {
                    Console.WriteLine("Celesi i dhene nuk eshte valid");
                }
            }
            else
            {
                Console.WriteLine("Komanda e dhene nuk ekziston");
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
            ciphertext = ciphertext.Replace(" ", "");
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
        public static void Create(string username)
        {
            if (!DoesExists(username))
            {
                string strXmlParameters = objRSA.ToXmlString(true);
                string privateKey = "keys/" + username + ".xml";
                StreamWriter sw = new StreamWriter(privateKey);
                sw.Write(strXmlParameters);
                sw.Close();
                Console.WriteLine("Eshte krijuar celesi privat " + privateKey);

                string strXmlParameters1 = objRSA.ToXmlString(false);
                string publicKey = "keys/" + username + ".pub.xml";
                StreamWriter sw1 = new StreamWriter(publicKey);
                sw1.Write(strXmlParameters1);
                sw1.Close();
                Console.WriteLine("Eshte krijuar celesi publik " + publicKey);
            }
            else
                Console.WriteLine("Gabim: Celesi " + username + " ekziston paraprakisht");
        }
        public static void Delete(string username)
        {
            string filePath = "keys/" + username + ".xml";
            string filePath1 = "keys/" + username + ".pub.xml";
            if (File.Exists(filePath) && File.Exists(filePath1))
            {
                File.Delete(filePath);
                Console.WriteLine("Eshte larguar celesi privat " + filePath);
                File.Delete(filePath1);
                Console.WriteLine("Eshte larguar celesi publik " + filePath1);
            }
            else if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("Eshte larguar celesi privat " + filePath);
            }
            else if (File.Exists(filePath1))
            {
                File.Delete(filePath1);
                Console.WriteLine("Eshte larguar celesi publik " + filePath1);
            }
            else
            {
                Console.WriteLine("Gabim: Celesi " + username + " nuk ekziston.");
            }

        }
        public static void ExportKey(string ispublic, string username, string filepath)
        {
            string keyPath = "";
            if (ispublic == "private")
            {
                keyPath = "keys/" + username + ".xml";
                if (!File.Exists(keyPath))
                {
                    Console.WriteLine("Gabim: Celesi privat " + username + " nuk ekziston");
                    return;
                }
            }
            else if (ispublic == "public")
            {
                keyPath = "keys/" + username + ".pub.xml";
                if (!File.Exists(keyPath))
                {
                    Console.WriteLine("Gabim: Celesi publik " + username + " nuk ekziston");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Duhet te jet private ose public");
            }
            StreamReader sr = new StreamReader(keyPath);

            if (filepath == "")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(keyPath);
                doc.Save(Console.Out);
            }
            else
            {
                string line = "";
                StreamWriter sw = new StreamWriter(filepath);
                while ((line = sr.ReadLine()) != null)
                {
                    sw.Write(line);
                }
                sw.Close();
            }

        }

        public static void ImportKey(string username, string filepath)
        {
            if (!DoesExists(username))
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);
                XmlNodeList v = doc.GetElementsByTagName("P");
                string keypath = "";
                if (v.Count > 0)
                {
                    keypath = "keys/" + username + ".xml";
                    string keypath1 = "keys/" + username + ".pub.xml";
                    if (File.Exists(keypath))
                    {
                        Console.WriteLine("Gabim: Celesi " + username + " ekziston paraprakisht");
                        return;
                    }
                    else
                    {
                        StreamReader sr = new StreamReader(filepath);
                        StreamWriter sw = new StreamWriter(keypath);
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            sw.Write(line);
                        }
                        sw.Close();
                        Console.WriteLine("Celesi privat u ruajt ne fajllin " + keypath);

                        StreamWriter sw1 = new StreamWriter(keypath1);
                        string line1 = "";
                        while ((line1 = sr.ReadLine()) != null)
                        {
                            sw1.Write(line);
                        }
                        sw1.Close();
                        Console.WriteLine("Celesi publik u ruajt ne fajllin " + keypath1);
                    }

                }
                else
                {
                    keypath = "keys/" + username + ".pub.xml";
                    if (File.Exists(keypath))
                    {
                        Console.WriteLine("Gabim: Celesi " + username + " ekziston paraprakisht");
                        return;
                    }
                    else
                    {
                        StreamReader sr = new StreamReader(filepath);
                        StreamWriter sw = new StreamWriter(keypath);
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            sw.Write(line);
                        }
                        sw.Close();
                        Console.WriteLine("Celesi publik u ruajt ne fajllin " + keypath);
                    }

                }
            }

        }
        public static bool DoesExists(string username)
        {
            string filePath = "keys/" + username + ".xml";
            if (File.Exists(filePath))
            {
                return true;
            }
            else
                return false;
        }


        

    }
}
