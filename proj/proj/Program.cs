using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace proj
{
    class Disease
    {
        public static List<string> diss = new List<string>();
        public Disease(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                string a;
                while ((a = sr.ReadLine()) != null)
                {
                    diss.Add(a);
                }
            }
            else
            {
                Console.WriteLine("address is wrong");
            }
        }
        public void Search(string search)
        {
            if (!diss.Contains(search))
            {
                Console.WriteLine("this Disease is not exist");
            }
            else
            {
                Console.WriteLine("Disease is exist");
            }
        }
        public void add(string sre)
        {
            diss.Add(sre);
            Console.WriteLine("added");
        }
        public void delete(string dis, Alergies a)
        {
            if (!diss.Contains(dis))
            {
                Console.WriteLine("this Disease is not exist in Disease database");
            }
            else
            {
                diss.Remove(dis);
            }
            a.Delete(dis);
            Console.WriteLine("successfully");
        }
    }
    public class Drugs
    {
        public Hashtable drugs = new Hashtable();
        public Drugs(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                string a;
                while ((a = sr.ReadLine()) != null)
                {
                    string[] s = a.Split(' ', ':');
                    drugs.Add(s[0], int.Parse(s[3]));
                }
            }
            else
            {
                Console.WriteLine("address is wrong");
            }
        }
        public void add(string sre, int np)
        {
            drugs.Add(sre, np);
            Console.WriteLine("added");
        }
        public void delete(string del, Alergies a, Effects b)
        {
            if (!drugs.ContainsKey(del))
            {
                Console.WriteLine("this drug is not exist in drugs database");
            }
            else
            {
                drugs.Remove(del);
                Console.WriteLine("deleted drugs");
            }
            b.Delete(del);
            a.Delete2(del);
            Console.WriteLine("successfully");
        }
        public void Search(string sre)
        {
            if (!drugs.ContainsKey(sre))
                Console.WriteLine("this drug is not exist");
            else
            {
                Console.WriteLine("drug is exist with price :" + drugs[sre]);
            }
            Console.WriteLine();
        }

        public void cost(int n)
        {
            int costt = 0;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("enter your " + (i+1) + "st drug");
                string drgg = Console.ReadLine();
                if (!drugs.ContainsKey(drgg))
                {
                    Console.WriteLine("this drug is not exist");
                    i--;
                }
                else
                {
                    costt = costt + (int)drugs[drgg];
                }

            }
            Console.WriteLine("your cost for " + n + " drug(s) is :" + costt + "\n");
        }
        public void incr_price(int percentt)
        {
            List<int> inn = new List<int>();
            List<string> inn2 = new List<string>();
            foreach (DictionaryEntry dic in drugs)
            {
                int n = (int)dic.Value + ((int)dic.Value * percentt / 100);
                inn.Add(n);
                inn2.Add(dic.Key.ToString());
            }
            drugs.Clear();
            for (int i = 0; i < inn.Count; i++)
            {
                drugs.Add(inn2[i], inn[i]);
            }
            Console.WriteLine("finished");
        }
    }
    public class Effects
    {
        public static Hashtable eff = new Hashtable();
        public Effects(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);

                string a;
                Hashtable effcopy;
                while ((a = sr.ReadLine()) != null)
                {
                    effcopy = new Hashtable();
                    string[] l = a.Split(' ', ':');
                    if (l.Length == 4)//1 effect
                    {
                        string[] ll = l[3].Split('(', ')', ',');
                        effcopy.Add(ll[1], ll[2]);
                    }
                    else if (l.Length == 6)//2 effect
                    {
                        string[] ll = l[3].Split('(', ')', ',');
                        string[] lll = l[5].Split('(', ')', ',');
                        effcopy.Add(ll[1], ll[2]);
                        effcopy.Add(lll[1], lll[2]);
                    }
                    eff.Add(l[0], effcopy);
                }
            }
            else
            {
                Console.WriteLine("address is wrong");
            }
        }
        public void Search1(string search, Alergies a)
        {
            if (!eff.ContainsKey(search))
                Console.WriteLine("this drug is not exist");
            else
            {
                Console.WriteLine("effects to another drugs:");
                foreach (DictionaryEntry dic in eff)
                {
                    if (dic.Key.ToString() == search)
                    {
                        Console.Write(search + " ");
                        foreach (DictionaryEntry d in dic.Value as Hashtable)
                        {
                            Console.Write("( " + $"{d.Key}" + " with efect " + $"{d.Value}" + "and )");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        public void Delete(string delete)
        {
            if (!eff.ContainsKey(delete))
            {
                Console.WriteLine("this drug is not exist in effects database");
            }
            else
            {
                eff.Remove(delete);
                Console.WriteLine("deleted effects");
            }
        }
    }
    public class Alergies
    {
        public static Hashtable alg = new Hashtable();
        public Alergies(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = File.OpenText(path);
                string a;
                Hashtable algcopy;
                while ((a = sr.ReadLine()) != null)
                {
                    algcopy = new Hashtable();
                    string[] l = a.Split(' ', ':');
                    for (int j = 0; j < (l.Length - 2) / 2; j++)
                    {
                        string[] ll = l[j * 2 + 3].Split('(', ')', ',');
                        algcopy.Add(ll[1], ll[2]);
                    }
                    alg.Add(l[0], algcopy);
                }
            }
            else
            {
                Console.WriteLine("address is wrong");
            }
        }
        public void Searchforalg(string sre, int n)
        {
            if (!alg.ContainsKey(sre))
                Console.WriteLine("this drug is not exist");
            else
            {
                if (n == 2)
                {
                    Console.Write("Alergies for " + sre + " : ");
                    foreach (DictionaryEntry dic in alg[sre] as Hashtable)
                    {
                        if (dic.Value.ToString() == "+")
                            Console.Write(dic.Key + " and ");
                    }
                    Console.Write(" ...");
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Alergies for " + sre + " : ");
                    foreach (DictionaryEntry dic in alg[sre] as Hashtable)
                    {
                        if (dic.Value.ToString() == "-")
                            Console.Write(dic.Key + " and ");
                    }
                    Console.Write(" ...");
                    Console.WriteLine();
                }
            }
        }
        public void Delete(string dis)
        {
            if (!alg.ContainsKey(dis))
            {
                Console.WriteLine("this Disease is not exist in Alergies database");
            }

            else
            {
                alg.Remove(dis);
                Console.WriteLine("deleted Alergies");
            }
        }
        public void Delete2(string dis)
        {
            int q = 0;
            foreach (DictionaryEntry dic in alg)
            {
                foreach (DictionaryEntry dic2 in alg[dic.Key.ToString()] as Hashtable)
                {
                    if (dic2.Key.ToString() == dis)
                    {
                        alg.Remove(dic);
                        q++;
                    }
                }
            }
            if (q == 0)
            {
                Console.WriteLine("this drug is not exist in effects database");
            }
            else
            {
                Console.WriteLine("delete " + q + " Alergies");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            string pathdrug;
            string pathdis;
            string pathale;
            string patheff;
            Console.WriteLine(" 1-defult address or 2-enter addreess \n Please enter number of your option");
            Console.WriteLine("*******************************************************\n*******************************************************");
            int inp1 = int.Parse(Console.ReadLine());
            if (inp1 == 1)
            {
                pathdrug = @"C:\Users\ACER\Desktop\ds proj\DS-Final-Project\datasets\drugs.txt";
                pathdis = @"C:\Users\ACER\Desktop\ds proj\DS-Final-Project\datasets\diseases.txt";
                pathale = @"C:\Users\ACER\Desktop\ds proj\DS-Final-Project\datasets\alergies.txt";
                patheff = @"C:\Users\ACER\Desktop\ds proj\DS-Final-Project\datasets\effects.txt";
            }
            else
            {
                Console.WriteLine("please enter address of data(drugs)");
                pathdrug = Console.ReadLine();
                Console.WriteLine("please enter address of data(diseases)");
                pathdis = Console.ReadLine();
                Console.WriteLine("please enter address of data(alergies)");
                pathale = Console.ReadLine();
                Console.WriteLine("please enter address of data(effects)");
                patheff = Console.ReadLine();
            }
            Drugs a = new Drugs(pathdrug);
            Effects b = new Effects(patheff);
            Alergies c = new Alergies(pathale);
            Disease d = new Disease(pathdis);
            Console.WriteLine("Please enter number of your option");
            Console.WriteLine(" 1-  Drugs(create and search and delete{with Alergies and Effects})\n 2-  Disease(create and search and delete{with Alergies})\n 3-  Search for effects of drugs and delete effects\n 4-  Search for alergies of disease and delete alergies \n 5-  price of drugs \n 6-  inflation rate(increase price) \n 7-  End");
            Console.WriteLine("*******************************************************\n*******************************************************");
            int inp = 0;
            inp = int.Parse(Console.ReadLine());
            while (inp != 7)
            {
                if (inp == 1)
                {
                    Console.WriteLine("Please enter number of your option");
                    Console.WriteLine(" 1- create\n 2- Search\n 3- delete{with Alergies and Effects}\n 4- Exit");
                    Console.WriteLine("*******************************************************\n*******************************************************");
                    int inp2 = int.Parse(Console.ReadLine());
                    if (inp2 == 1)
                    {
                        Console.WriteLine("please enter drug name \n format : drug_example");
                        string atach = Console.ReadLine();
                        Console.WriteLine("please enter drug price \n format : drug_example");
                        int p = int.Parse(Console.ReadLine());
                        a.add(atach, p);
                    }
                    else if (inp2 == 2)
                    {
                        Console.WriteLine("please enter drug name \n format : drug_example");
                        string atach = Console.ReadLine();
                        a.Search(atach);
                    }
                    else if (inp2 == 3)
                    {
                        Console.WriteLine("please enter drug name \n format : drug_example");
                        string atach = Console.ReadLine();

                        a.delete(atach, c, b);

                    }

                }
                else if (inp == 2)
                {
                    Console.WriteLine("Please enter number of your option");
                    Console.WriteLine(" 1- create\n 2- Search\n 3- delete{with Alergies}\n 4- Exit");
                    Console.WriteLine("*******************************************************\n*******************************************************");
                    int inp2 = int.Parse(Console.ReadLine());
                    if (inp2 == 1)
                    {
                        Console.WriteLine("please enter Disease name \n format : Dis_example");
                        string atach = Console.ReadLine();
                        d.add(atach);
                    }
                    else if (inp2 == 2)
                    {
                        Console.WriteLine("please enter Disease name \n format : Dis_example");
                        string atach = Console.ReadLine();
                        d.Search(atach);
                    }
                    else if (inp2 == 3)
                    {
                        Console.WriteLine("please enter Disease name \n format : Dis_example");
                        string atach = Console.ReadLine();
                        d.delete(atach, c);
                    }
                }
                else if (inp == 3)
                {
                    Console.WriteLine("Please enter number of your option");
                    Console.WriteLine(" 1- Search for Effects of drugs\n 2- delete effects\n 3- Exit");
                    Console.WriteLine("*******************************************************\n*******************************************************");
                    int inp2 = int.Parse(Console.ReadLine());
                    if (inp2 == 1)
                    {
                        Console.WriteLine("please enter drug name \n format : drug_example");
                        string atach = Console.ReadLine();
                        b.Search1(atach, c);
                    }
                    else if (inp2 == 2)
                    {
                        Console.WriteLine("please enter drug name \n format : drug_example");
                        string atach = Console.ReadLine();
                        b.Delete(atach);
                    }
                }
                else if (inp == 4)
                {
                    Console.WriteLine("Please enter number of your option");
                    Console.WriteLine(" 1- Search for alergies of disease\n 2- Delete disease alergies\n 3- Exit");
                    Console.WriteLine("*******************************************************\n*******************************************************");
                    int inp2 = int.Parse(Console.ReadLine());
                    if (inp2 == 1)
                    {
                        Console.WriteLine("please enter Disease name \n format : Dis_example");
                        string atach = Console.ReadLine();
                        Console.WriteLine("please enter number 1( - negetive efect) or 2( + positive efect)");
                        int inp3 = int.Parse(Console.ReadLine());
                        c.Searchforalg(atach, inp3);
                    }
                    else if (inp2 == 2)
                    {
                        Console.WriteLine("please enter Disease name \n format : Dis_example");
                        string atach = Console.ReadLine();
                        c.Delete(atach);
                    }
                }
                else if (inp == 5)
                {
                    Console.WriteLine("*******************************************************\n*******************************************************");
                    Console.WriteLine("Please enter number of your drugs to buy");
                    int inp2 = int.Parse(Console.ReadLine());
                    a.cost(inp2);
                }
                else if (inp == 6)
                {
                    Console.WriteLine("*******************************************************\n*******************************************************");
                    Console.WriteLine("Please enter your inflation rate (percent):");
                    int inp2 = int.Parse(Console.ReadLine());
                    a.incr_price(inp2);
                }
                inp = 0;
                Console.WriteLine("Please enter number of your option");
                Console.WriteLine(" 1-  Drugs(create and search and delete{with Alergies and Effects})\n 2-  Disease(create and search and delete{with Alergies})\n 3-  Search for effects of drugs and delete effects\n 4-  Search for alergies of disease and delete alergies \n 5-  price of drugs \n 6-  inflation rate(increase price) \n 7-  End");
                Console.WriteLine("*******************************************************\n*******************************************************");
                inp = int.Parse(Console.ReadLine());
            }
            sw1.Stop();
            Console.WriteLine("time : " + (double)sw1.Elapsed.TotalSeconds + "  S  ***  memory :  " + (double)GC.GetTotalMemory(true) / 1000000 + " mB");
            Console.WriteLine("*******************************************************\n*******************************************************");
            GC.GetTotalMemory(true);
            Console.ReadKey();
        }
    }
}
