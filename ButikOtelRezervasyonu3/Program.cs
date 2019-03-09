using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*     1-Bugunku bos odalari goster
       2-30 gunluk doluluk durumu
       3-Bugun icin hizli rezervasyon
       4-Iki tarih arasi rezervasyon
       5-Gun sonu islemi
       Men�leri olan ve g�ncel tarih ile �al��an bir otel rezervasyon uygulamas� 


 **Her rezervasyondan sonra oda temizlik icin bir gun bos birakilacak

 */

namespace ButikOtelRezervasyonu3
{
    /*
     * Rezervasyon yapma isleri bir class'a toplandi, hatalar duzeltildi
     */
    class Rezervasyon
    {
        const int odaSayisi = 10;//const field de�erini sabitlemeye yarar.
        const int gunSayisi = 30;
       
        enum RezervasyonEnum
        {
            Bos = 0,
            Dolu = 1,
            Temizlik = 2
        };
        private RezervasyonEnum[,] rezervasyonDurumu = new RezervasyonEnum[odaSayisi, gunSayisi];
        

        public void RasgeleDoldur()
        {//Odalar�n ay i�inde hangi g�nlerde dolu ve temizlikte oldu�unu burada belirtiyoruz
            
            rezervasyonDurumu[0, 0] = RezervasyonEnum.Dolu;//1 numaral� oda g�ncel tarihte 1. g�nde dolu
            rezervasyonDurumu[0, 1] = RezervasyonEnum.Temizlik;//1 numaral� oda g�ncel tarihte 2. g�nde temizlikte
            rezervasyonDurumu[0, 5] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 6] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[1, 7] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[1, 8] = RezervasyonEnum.Temizlik;//2 numaral� oda g�ncel tarihten itibaren 9. g�nde temizlikte
            rezervasyonDurumu[2, 9] = RezervasyonEnum.Dolu;//3 numaral� oda 10. g�nde dolu
            rezervasyonDurumu[2, 10] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[5, 15] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 16] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[8, 20] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[8, 21] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[0, 27] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 28] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[9, 29] = RezervasyonEnum.Dolu;

            rezervasyonDurumu[4, 0] = RezervasyonEnum.Temizlik;// 5 numaral� oda g�ncel tarihte  1. g�nde Temizlikte
            rezervasyonDurumu[5, 1] = RezervasyonEnum.Dolu;// 6 numaral� oda g�ncel tarihte 2. g�nde dolu
            rezervasyonDurumu[5, 2] = RezervasyonEnum.Temizlik;// 6 numaral� oda g�ncel tarihte 3. g�nde temizlikte
        }
        public void BugunkuBosOdalar() 
        {
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                if (rezervasyonDurumu[i, 0] == RezervasyonEnum.Bos//i. oda 0. g�nde ve bir sonraki g�nde bo�sa bo� oda var.
                    && rezervasyonDurumu[i, 1] == RezervasyonEnum.Bos)
                {
                    bosOdaYok = false;//bo� oda var 
                    Console.WriteLine(i + 1);//numaral� oday� yazd�r�r.
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Bugun icin bos oda yok");
        }
        public void AylikDolulukDurumu()
        {
            Console.Write("      ");
            for (int j = 0; j < gunSayisi; j++)
            {
                Console.Write(" {0:00}", DateTime.Today.AddDays(j).Day);//D�ng�de g�ncel tarihten itibaren yan yana ay�n g�nlerini yazd�r�r.
            }
            Console.WriteLine();
            for (int i = 0; i < odaSayisi; i++)
            {
                Console.Write("Oda {0:00}", i + 1);//Bu for d�ng�s�nde odalar� alt alta yazd�r�r.
                for (int j = 0; j < gunSayisi; j++)//Burada hemen �stte yazd�rd���m�z oda i�in ayl�k doluluk durumunu ��kart�yoruz.
                {
                    if (rezervasyonDurumu[i, j] == RezervasyonEnum.Bos)
                        Console.Write(" - ");
                    else if (rezervasyonDurumu[i, j] == RezervasyonEnum.Dolu)
                        Console.Write(" D ");
                    else
                        Console.Write(" T ");
                }
                Console.WriteLine();
            }
        }
        public void BugunIcinHizliRezervasyon()
        {
            IkiTarihArasiRezervasyon(DateTime.Today, DateTime.Today);//Ba�lang�� ve biti� tarihini bug�n al�yor.
        }
        public void IkiTarihArasiRezervasyon(DateTime date1, DateTime date2)
        {
            if (date1 < DateTime.Today)//rezervasyonun ba�lang�� tarihi bug�nden k���k olamaz.
            {
                Console.WriteLine("Baslangic tarihi bugunden k���k olamaz");
                Console.WriteLine("L�tfen rezervasyon istediginiz tarihi kontrol ediniz.");
                return;
            }
            if (date2 < date1)//Rezervasyonun biti� tarihi ba�lang�� tarihinden k���k olamaz
            {
                Console.WriteLine("Bitis tarihi baslangic tarihinden kucuk olamaz");
                Console.WriteLine("L�tfen rezervasyon istediginiz tarihi kontrol ediniz.");
                return;
            }
            if ((date1 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Baslangic tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            if ((date2 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Bitis tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            int gun1 = (date1 - DateTime.Today).Days;// burda g�n1 ve g�n2 indexler
            int gun2 = (date2 - DateTime.Today).Days;
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)//en d��ta teker teker odalara bakacak.
            {
                bool odaMusait = true;
                for (int j = gun1; j <= gun2; j++) // burda belirtilen tarihler aras�ndaki indeksler aras�nda d�necek 
                {
                    if (rezervasyonDurumu[i, j] != RezervasyonEnum.Bos)//�ncelikle girilen tarih aral���nda odalar�n bo� olmas�na bak�yor
                    {
                        odaMusait = false;
                        break;
                    }
                }
                if ((gun2 + 1) < gunSayisi)
                {
                    if (rezervasyonDurumu[i, gun2 + 1] != RezervasyonEnum.Bos)//biti� tarihinden bir g�n sonra oda temizli�e ayr�laca�� i�in 
                                                                              // o g�n�n de bo� olmas� laz�m.
                        odaMusait = false;
                }
                if (odaMusait)//girilen tarihler aras�nda m�sait bir oda buldu�unda 
                {
                    bosOdaYok = false; 
                    for (int j = gun1; j <= gun2; j++) //�nce girilen tarih aral�klar�n� doluluk durumuna DOLU olarak i�liyor.
                    {
                        rezervasyonDurumu[i, j] = RezervasyonEnum.Dolu;
                    }
                    if ((gun2 + 1) < gunSayisi)
                        rezervasyonDurumu[i, gun2 + 1] = RezervasyonEnum.Temizlik;//Sonra da girilen rezervasyon biti� tarihinden bir sonraki 
                                                                                  //g�n� TEM�ZL�K i�in doluluk durumuna TEM�ZL�K olarak i�liyor.
                    Console.WriteLine("{0} numarali oda sizin icin ayrildi", i + 1);
                    break;
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Istediginiz tarihte bos oda yok");
        }
        public void GunSonuIslemi()// g�n sonu i�leminde ayl�k doluluk durumunda odalara g�re D,T ve - olan g�nleri bir �nceki indekse aktaracak.
                                    //Yani bir sonraki g�ne ge�iyoruz.
                                    //S�rekli g�ncel tarih al�nd��� i�in g�n sonu i�lemi gece yar�s� yap�lmal�********************
        {
            for (int i = 0; i < odaSayisi; i++)
            {
                for (int j = 0; j < gunSayisi - 1; j++)
                {
                    rezervasyonDurumu[i, j] = rezervasyonDurumu[i, j + 1];
                }
                if (rezervasyonDurumu[i, gunSayisi - 2] == RezervasyonEnum.Dolu)
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Temizlik;//Bir �nceki g�n doluysa bir sonra g�n�ne temizlik yap�lmal�
                else
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Bos;
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Rezervasyon rezervasyon = new Rezervasyon();
            rezervasyon.RasgeleDoldur();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("        Butik Otel Rezervasyonu");
                Console.WriteLine("1-Bugunku bos odalari goster");
                Console.WriteLine("2-30 gunluk doluluk durumu");
                Console.WriteLine("3-Bugun icin hizli rezervasyon");
                Console.WriteLine("4-Iki tarih arasi rezervasyon");
                Console.WriteLine("5-Gun sonu islemi");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugunku bos odalar");
                            rezervasyon.BugunkuBosOdalar();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine();
                            Console.WriteLine("30 gunluk doluluk durumu");
                            rezervasyon.AylikDolulukDurumu();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugun icin hizli rezervasyon");
                            rezervasyon.BugunIcinHizliRezervasyon();
                            break;
                        }
                    case '4':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon");
                            DateTime date1 = DateTime.Today;
                            DateTime date2 = DateTime.Today;
                            try
                            {
                                Console.Write("Rezervasyon baslangic tarihi (gg/aa/yyyy): ");
                                string baslangicTarihi = Console.ReadLine();
                                date1 = Convert.ToDateTime(baslangicTarihi);

                                Console.Write("Rezervasyon bitis tarihi (gg/aa/yyyy): ");
                                string bitisTarihi = Console.ReadLine();
                                date2 = Convert.ToDateTime(bitisTarihi);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Tarih formatina dikkat ediniz");
                            }
                            rezervasyon.IkiTarihArasiRezervasyon(date1, date2);
                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Gun sonu islemi");
                            rezervasyon.GunSonuIslemi();
                            break;
                        }
                }
            }
        }
    }
}
