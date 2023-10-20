using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; //Language in Query
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdatfeldolgozasiFeladatok
{
    struct Zene
    {
        int ado;
        int perc;
        int mperc;
        string eloado;
        string cim;
        public int Ado
        {
            get
            {
                return ado;
            }
        }
        public int Perc
        {
            get
            {
                return perc;
            }
        }
        public int Mperc
        {
            get
            {
                return mperc;
            }
        }
        public string Eloado
        {
            get
            {
                return eloado;
            }
        }
        public string Cim
        {
            get
            {
                return cim;
            }
        }
        public Zene(string sor)
        {
            string[] r1 = sor.Split(':');
            cim = r1[1];
            string[] r2 = r1[0].Split(' ');
            ado = int.Parse(r2[0]);
            perc = int.Parse(r2[1]);
            mperc = int.Parse(r2[2]);
            //eloado = r2[3];
            eloado = "";
            for(int i=3; i < r2.Length; i++)
            {
                eloado += (r2[i] + " ");
            }
            eloado = eloado.Trim();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //A zenek.txt file-t a felhasználói profilmappa source\repos\AdatfeldolgozasiFeladatok\AdatfeldolgozasiFeladatok\bin\Debug mappába kell tenni, mert így egyszerűen a filenév+kiterjesztéssel tudunk hivatkozni rá
            List<Zene> zenek = new List<Zene>();
            StreamReader file = new StreamReader("zenek.txt");
            while (!file.EndOfStream)
            {
                //Console.WriteLine(file.ReadLine());
                Zene z = new Zene(file.ReadLine());
                zenek.Add(z);
            }
            //Console.ReadKey();
            file.Close();
            //Kíváncsiak vagyunk arra, hogy az egyes rádióadók műsorideje mennyi is volt
            foreach (Zene z in zenek)
            { //így tudnák "bejárni" a zenek elnevezésű listát elemenként}
            }
                var linq1 = from zene in zenek
                            select new
                            {
                                Ado = zene.Ado,
                                Perc = zene.Perc,
                                Mperc = zene.Mperc
                            };
            foreach(var z in linq1)
            {
                Console.WriteLine($"{z.Ado}. rádióadó: {z.Perc,2:00}:{z.Mperc,2:00}");
            }
            int[] a = { 1, 2, 4, 7 };
            int[] b = { 1, 3, 5, 6, 7 };
            //Console.WriteLine(a.Union(b));
            Console.Write("{");
            foreach (var elem in a.Union(b).OrderBy(x => x*-1)) //lambda kifejezés
            {
                Console.Write(elem+" ");
            }
            Console.WriteLine("}");

            var linq2 = from adat in linq1
                        group adat by adat.Ado
                      into csoport
                        select new
                        {
                            Ado = csoport.Key,
                            Ora = csoport.Sum(sor => sor.Perc) / 60,
                            Perc = (csoport.Sum(sor => sor.Mperc) / 60 + csoport.Sum(sor => sor.Perc)) % 60,
                            Mperc = csoport.Sum(sor => sor.Mperc) % 60
                        };
            foreach(var adat in linq2.OrderByDescending(w=>DateTime.Parse(w.Ora+":"+w.Perc+":"+w.Mperc)))
            {
                Console.WriteLine($"{adat.Ado}. rádióadó: {adat.Ora,2:00}:{adat.Perc,2:00}:{adat.Mperc,2:00}");
            }
            Console.ReadKey();
        }
    }
}
