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
       Menüleri olan ve güncel tarih ile çalýþan bir otel rezervasyon uygulamasý 


 **Her rezervasyondan sonra oda temizlik icin bir gun bos birakilacak

 */

namespace ButikOtelRezervasyonu3
{
    /*
     * Rezervasyon yapma isleri bir class'a toplandi, hatalar duzeltildi
     */
    class Rezervasyon
    {
        const int odaSayisi = 10;//const field deðerini sabitlemeye yarar.
        const int gunSayisi = 30;
       
        enum RezervasyonEnum
        {
            Bos = 0,
            Dolu = 1,
            Temizlik = 2
        };
        private RezervasyonEnum[,] rezervasyonDurumu = new RezervasyonEnum[odaSayisi, gunSayisi];
        

        public void RasgeleDoldur()
        {//Odalarýn ay içinde hangi günlerde dolu ve temizlikte olduðunu burada belirtiyoruz
            
            rezervasyonDurumu[0, 0] = RezervasyonEnum.Dolu;//1 numaralý oda güncel tarihte 1. günde dolu
            rezervasyonDurumu[0, 1] = RezervasyonEnum.Temizlik;//1 numaralý oda güncel tarihte 2. günde temizlikte
            rezervasyonDurumu[0, 5] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 6] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[1, 7] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[1, 8] = RezervasyonEnum.Temizlik;//2 numaralý oda güncel tarihten itibaren 9. günde temizlikte
            rezervasyonDurumu[2, 9] = RezervasyonEnum.Dolu;//3 numaralý oda 10. günde dolu
            rezervasyonDurumu[2, 10] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[5, 15] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 16] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[8, 20] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[8, 21] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[0, 27] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 28] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[9, 29] = RezervasyonEnum.Dolu;

            rezervasyonDurumu[4, 0] = RezervasyonEnum.Temizlik;// 5 numaralý oda güncel tarihte  1. günde Temizlikte
            rezervasyonDurumu[5, 1] = RezervasyonEnum.Dolu;// 6 numaralý oda güncel tarihte 2. günde dolu
            rezervasyonDurumu[5, 2] = RezervasyonEnum.Temizlik;// 6 numaralý oda güncel tarihte 3. günde temizlikte
        }
        public void BugunkuBosOdalar() 
        {
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                if (rezervasyonDurumu[i, 0] == RezervasyonEnum.Bos//i. oda 0. günde ve bir sonraki günde boþsa boþ oda var.
                    && rezervasyonDurumu[i, 1] == RezervasyonEnum.Bos)
                {
                    bosOdaYok = false;//boþ oda var 
                    Console.WriteLine(i + 1);//numaralý odayý yazdýrýr.
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
                Console.Write(" {0:00}", DateTime.Today.AddDays(j).Day);//Döngüde güncel tarihten itibaren yan yana ayýn günlerini yazdýrýr.
            }
            Console.WriteLine();
            for (int i = 0; i < odaSayisi; i++)
            {
                Console.Write("Oda {0:00}", i + 1);//Bu for döngüsünde odalarý alt alta yazdýrýr.
                for (int j = 0; j < gunSayisi; j++)//Burada hemen üstte yazdýrdýðýmýz oda için aylýk doluluk durumunu çýkartýyoruz.
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
            IkiTarihArasiRezervasyon(DateTime.Today, DateTime.Today);//Baþlangýç ve bitiþ tarihini bugün alýyor.
        }
        public void IkiTarihArasiRezervasyon(DateTime date1, DateTime date2)
        {
            if (date1 < DateTime.Today)//rezervasyonun baþlangýç tarihi bugünden küçük olamaz.
            {
                Console.WriteLine("Baslangic tarihi bugunden küçük olamaz");
                Console.WriteLine("Lütfen rezervasyon istediginiz tarihi kontrol ediniz.");
                return;
            }
            if (date2 < date1)//Rezervasyonun bitiþ tarihi baþlangýç tarihinden küçük olamaz
            {
                Console.WriteLine("Bitis tarihi baslangic tarihinden kucuk olamaz");
                Console.WriteLine("Lütfen rezervasyon istediginiz tarihi kontrol ediniz.");
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
            int gun1 = (date1 - DateTime.Today).Days;// burda gün1 ve gün2 indexler
            int gun2 = (date2 - DateTime.Today).Days;
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)//en dýþta teker teker odalara bakacak.
            {
                bool odaMusait = true;
                for (int j = gun1; j <= gun2; j++) // burda belirtilen tarihler arasýndaki indeksler arasýnda dönecek 
                {
                    if (rezervasyonDurumu[i, j] != RezervasyonEnum.Bos)//Öncelikle girilen tarih aralýðýnda odalarýn boþ olmasýna bakýyor
                    {
                        odaMusait = false;
                        break;
                    }
                }
                if ((gun2 + 1) < gunSayisi)
                {
                    if (rezervasyonDurumu[i, gun2 + 1] != RezervasyonEnum.Bos)//bitiþ tarihinden bir gün sonra oda temizliðe ayrýlacaðý için 
                                                                              // o günün de boþ olmasý lazým.
                        odaMusait = false;
                }
                if (odaMusait)//girilen tarihler arasýnda müsait bir oda bulduðunda 
                {
                    bosOdaYok = false; 
                    for (int j = gun1; j <= gun2; j++) //önce girilen tarih aralýklarýný doluluk durumuna DOLU olarak iþliyor.
                    {
                        rezervasyonDurumu[i, j] = RezervasyonEnum.Dolu;
                    }
                    if ((gun2 + 1) < gunSayisi)
                        rezervasyonDurumu[i, gun2 + 1] = RezervasyonEnum.Temizlik;//Sonra da girilen rezervasyon bitiþ tarihinden bir sonraki 
                                                                                  //günü TEMÝZLÝK için doluluk durumuna TEMÝZLÝK olarak iþliyor.
                    Console.WriteLine("{0} numarali oda sizin icin ayrildi", i + 1);
                    break;
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Istediginiz tarihte bos oda yok");
        }
        public void GunSonuIslemi()// gün sonu iþleminde aylýk doluluk durumunda odalara göre D,T ve - olan günleri bir önceki indekse aktaracak.
                                    //Yani bir sonraki güne geçiyoruz.
                                    //Sürekli güncel tarih alýndýðý için gün sonu iþlemi gece yarýsý yapýlmalý********************
        {
            for (int i = 0; i < odaSayisi; i++)
            {
                for (int j = 0; j < gunSayisi - 1; j++)
                {
                    rezervasyonDurumu[i, j] = rezervasyonDurumu[i, j + 1];
                }
                if (rezervasyonDurumu[i, gunSayisi - 2] == RezervasyonEnum.Dolu)
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Temizlik;//Bir önceki gün doluysa bir sonra gününe temizlik yapýlmalý
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
