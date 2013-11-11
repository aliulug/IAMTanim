using NMock2;

namespace IAMYonetim2.IsAkisiYonetim
{
	public class IsAkisiSurumYonetimAjaxHandler
	{
		private readonly IIsAkisiSurumYoneticisi _yonetici;
		private readonly IIsAkisiTanimNesneFabrika _fabrika;

		public IsAkisiSurumYonetimAjaxHandler(IIsAkisiSurumYoneticisi yonetici, IIsAkisiTanimNesneFabrika fabrika)
		{
			_yonetici = yonetici;
			_fabrika = fabrika;
		}

		public IstekSonuc FaaliyetTanimEkle(string ad, string aciklama, string tip, bool baslatan, bool bitiren, string isAkisiBitirmeKosulu, string faaliyetBitirmeKosulu)
		{
			var faaliyetYaratIstekSonuc = _fabrika.FaaliyetTanimYarat(ad, aciklama, tip, baslatan, bitiren, isAkisiBitirmeKosulu, faaliyetBitirmeKosulu);
			if (!faaliyetYaratIstekSonuc.Basarili) return IstekSonuc.Hata(faaliyetYaratIstekSonuc.Mesaj);
			var islemBasari = _yonetici.FaaliyetTanimEkle(faaliyetYaratIstekSonuc.YaratilanNesne);
			return new IstekSonuc {Basarili = islemBasari};
		}

		public IstekSonuc FaaliyetTanimIliskiEkle(string kaynakFaaliyetAd, string hedefFaaliyetAd, string kosul)
		{
			var faaliyet1 = _yonetici.FaaliyetTanimAl(kaynakFaaliyetAd);
			var faaliyet2 = _yonetici.FaaliyetTanimAl(hedefFaaliyetAd);
			if (faaliyet1 == null || faaliyet2 == null) return IstekSonuc.Hata("Tanımsız faaliyet");
			_yonetici.FaaliyetTanimIliskiEkle(faaliyet1, faaliyet2, kosul);
			return new IstekSonuc {Basarili = true};
		}

		public IstekSonuc FaaliyetTanimSil(string faaliyetAdi)
		{
			var tanim = _yonetici.FaaliyetTanimAl(faaliyetAdi);
			return new IstekSonuc {Basarili = _yonetici.FaaliyetTanimSil(tanim)};
		}

		public IstekSonuc IliskiSil(string kaynakFaaliyetAd, string hedefFaaliyetAd)
		{
			var faaliyet1 = _yonetici.FaaliyetTanimAl(kaynakFaaliyetAd);
			var faaliyet2 = _yonetici.FaaliyetTanimAl(hedefFaaliyetAd);
			if (faaliyet1 == null || faaliyet2 == null) return IstekSonuc.Hata("Tanımsız faaliyet");
			return faaliyet1.IliskiSil(faaliyet2) ? new IstekSonuc { Basarili = true } : IstekSonuc.Hata(kaynakFaaliyetAd + " faaliyeti " + hedefFaaliyetAd + " faaliyetine bir ilişki içermiyor");
		}

		public IstekSonuc FaaliyetTanimDegiskenEkle(string faaliyetTanimAd, string gosterilecekAd, string kisaAd, string tip, string ekBilgi, int siraNo, bool sadeceSoru, bool cacheli, string sql, string ekSecenekler, string ozellikler, string stiller)
		{
			var faaliyet = _yonetici.FaaliyetTanimAl(faaliyetTanimAd);
			if (faaliyet == null) return IstekSonuc.Hata(faaliyetTanimAd + " isimli faaliyet bulunamnadı");
			var degiskenYaratIstekSonuc = _fabrika.FaaliyetTanimDegiskenYarat(gosterilecekAd, kisaAd, tip, ekBilgi, siraNo, sadeceSoru, cacheli, sql, ekSecenekler, ozellikler, stiller);
			if (!degiskenYaratIstekSonuc.Basarili) return new IstekSonuc {Basarili = false, Mesaj = degiskenYaratIstekSonuc.Mesaj};
			return !_yonetici.FaaliyetTanimDegiskeniEkle(faaliyet, degiskenYaratIstekSonuc.YaratilanNesne) ? IstekSonuc.Hata("Değişken eklenemedi") : IstekSonuc.Basari();
		}

		public IstekSonuc FaaliyetDegiskenSil(string faaliyetAd, string degiskenAd)
		{
			var faaliyet = _yonetici.FaaliyetTanimAl(faaliyetAd);
			if (faaliyet == null) return IstekSonuc.Hata(faaliyetAd + " isimli faaliyet bulunamadı");
			var degisken = faaliyet.DegiskenAl(degiskenAd);
			if (degisken == null) return IstekSonuc.Hata(degiskenAd + " isimli değişken bulunamadı");
			return !faaliyet.DegiskenTanimSil(degisken) ? IstekSonuc.Hata("Değişken silinemedi") : IstekSonuc.Basari();
		}

		public IstekSonuc FaaliyetTanimSorumluRolEkle(string faaliyetAd, string rolAdi, string kosul)
		{
			return faaliyetTanimSorumluEkle(faaliyetAd, 0, rolAdi, "", kosul);
		}

		public IstekSonuc FaaliyetTanimSorumluOzelEkle(string faaliyetAd, string ozelSorumluluk, string kosul)
		{
			return faaliyetTanimSorumluEkle(faaliyetAd, 0, "", ozelSorumluluk, kosul);
		}

		public IstekSonuc FaaliyetTanimSorumluKullaniciEkle(string faaliyetAd, int kullaniciId, string kosul)
		{
			return faaliyetTanimSorumluEkle(faaliyetAd, kullaniciId, "", "", kosul);
		}

		private IstekSonuc faaliyetTanimSorumluEkle(string faaliyetAd, int sorumluKullaniciId, string sorumluRolAdi, string sorumluOzel, string kosul)
		{
			var faaliyet = _yonetici.FaaliyetTanimAl(faaliyetAd);
			if (faaliyet == null) return IstekSonuc.Hata(faaliyetAd + " isimli faaliyet bulunamadı");
			var sorumluYaratIstekSonuc = _fabrika.FaaliyetTanimSorumluYarat(sorumluKullaniciId, sorumluRolAdi, sorumluOzel, kosul);
			if (!sorumluYaratIstekSonuc.Basarili) IstekSonuc.Hata(sorumluYaratIstekSonuc.Mesaj);
			return !_yonetici.FaaliyetTanimSorumluEkle(faaliyet, sorumluYaratIstekSonuc.YaratilanNesne, kosul) ? IstekSonuc.Hata("Sorumlu eklenemedi") : IstekSonuc.Basari();
		}

		public IstekSonuc FaaliyetTanimSorumluKullaniciSil(string faaliyetAd, int sorumluKullaniciId)
		{
			var faaliyet = _yonetici.FaaliyetTanimAl(faaliyetAd);
			if (faaliyet == null) return IstekSonuc.Hata(faaliyetAd + " isimli faaliyet bulunamadı");
			IFaaliyetTanimSorumlu sorumlu = faaliyet.SorumluKullaniciAl(sorumluKullaniciId);
			if (sorumlu == null) return IstekSonuc.Hata(sorumluKullaniciId + " ID numarasına sahip kullanıcı faaliyet sorumlusu olarak bulunamadı");
			return !_yonetici.FaaliyetTanimSorumluSil(faaliyet, sorumlu) ? IstekSonuc.Hata("Sorumlu silinemedi") : IstekSonuc.Basari();
		}

		public IstekSonuc IsAkisiSurumDegiskeniEkle(string gosterilecekAd, string kisaAd, string tip, string ekBilgi, int siraNo, bool sadeceSoru, bool cacheli, string sql, string ekSecenekler)
		{
			var degisken = _yonetici.IsAkisiDegiskenAl(kisaAd);
			if (degisken != null) return IstekSonuc.Hata(kisaAd + " adına sahip bir değişken zaten tanımlı");
			var degiskenYaratIstekSonuc =_fabrika.IsAkisiTanimDegiskenYarat(gosterilecekAd, kisaAd, tip, ekBilgi, siraNo, sadeceSoru, cacheli, sql, ekSecenekler);
			if (!degiskenYaratIstekSonuc.Basarili) return IstekSonuc.Hata(degiskenYaratIstekSonuc.Mesaj);
			return !_yonetici.IsAkisiDegiskenEkle(degiskenYaratIstekSonuc.YaratilanNesne) ? IstekSonuc.Hata("Değişken eklenemedi") : IstekSonuc.Basari();
		}

		public IstekSonuc IsAkisiSurumDegiskeniSil(string degiskenKisaAd)
		{
			var degisken = _yonetici.IsAkisiDegiskenAl(degiskenKisaAd);
			if (degisken == null) return IstekSonuc.Hata(degiskenKisaAd + " adlı değişken bulunamadı");
			return !_yonetici.IsAkisiDegiskenSil(degisken) ? IstekSonuc.Hata("Değişken silinemedi") : IstekSonuc.Basari();
		}
	}
}