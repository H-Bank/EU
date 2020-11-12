using System;
using System.IO;

namespace EU
{
    class Program
    {
        public struct adat
        {
            public string orsz;
            public string date;
            public int ev;
            public int ho;
            public int nap;
        }
        static void Main(string[] args)
        {
            string[] sorok = File.ReadAllLines("EUcsatlakozas.txt"); //az ékezetes betűket ? jelre cseréli ki, mert a txt fájl ANSI-ba van, nem UTF-8-ban.
            adat[] eu = new adat[sorok.Length];
            for (int i=0;i<sorok.Length;i++)
            {
                string[] sor = sorok[i].Split(';');
                eu[i].orsz = sor[0];
                eu[i].date = sor[1];
                string[] tord = sor[1].Split('.');
                eu[i].ev = Convert.ToInt32(tord[0]);
                eu[i].ho = Convert.ToInt32(tord[1]);
                eu[i].nap = Convert.ToInt32(tord[2]);
            }

            Console.WriteLine("3. feladat: EU tagállamainak száma: "+ sorok.Length+" db");
            
            Console.Write("4. feladat: ");
            int db = 0;
            for (int i=0;i<sorok.Length;i++)
            {
                if (eu[i].ev==2007)
                {
                    db = db + 1;
                }
            }
            Console.WriteLine("2007-ben " + db + " ország csatlakozott.");

            Console.Write("5. feladat: ");
            string be;
            for (int i = 0; i < sorok.Length; i++)
            {
                try
                {
                    be = eu[i].orsz.Substring(0, 6);
                }
                catch
                {
                    be = "";
                }
                if (be== "Magyar")
                {
                    Console.WriteLine("Magyarország csatlakozásának dátuma: " + eu[i].date);
                }
            }

            Console.Write("6. feladat: ");
            int j = 0;
            bool van = true;
            while (j<sorok.Length && van)
            {
                if (eu[j].ho==5)
                {
                    van = false;
                }
                j++;
            }
            if (!van)
            {
                Console.WriteLine("Májusban volt csatlakozás!");
            }
            else
            {
                Console.WriteLine("Májusban nem volt csatlakozás!");
            }

            Console.Write("7. feladat: ");
            int max = 0, maxi=0;
            for (int i=0;i<sorok.Length;i++)
            {
                if (eu[i].ev>max)
                {
                    max = eu[i].ev;
                    maxi = i;
                }
            }
            db = 0;
            for (int i = 0; i < sorok.Length; i++)
            {
                if (eu[i].ev == max)
                {
                    db = db + 1;
                }
            }
            if (db==1)
            {
                Console.WriteLine("Legutoljára csatlakozott ország: " + eu[maxi].orsz);
            }
            else
            {
                Console.WriteLine("Legutoljára csatlakozott ország: Több is volt");
            }

            Console.WriteLine("8. feladat: Statisztika");
            int x = 0;
            int[,] evek = new int[sorok.Length, 2];
            van = true;
            int d = 0;
            for (int i=0;i<sorok.Length;i++)
            {
                van = true;
                d = 0;
                while (d<x && van)
                {
                    if (evek[d,0]==eu[i].ev)
                    {
                        van = false;
                    }
                    d++;
                }
                if (van)
                {
                    evek[x, 0] = eu[i].ev;
                    x++;
                }
            }

            for (int i=0;i<x;i++)
            {
                db = 0;
                for (int a=0;a<sorok.Length;a++)
                {
                    if (evek[i,0]==eu[a].ev)
                    {
                        db++;
                    }
                }
                evek[i, 1] = db;
            }


            for (int i=0;i<x;i++)
            {
                Console.WriteLine("\t"+evek[i, 0]+" - "+evek[i,1]+" ország");
            }

            Console.ReadLine();
        }
    }
}
