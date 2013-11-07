using IAMYonetim2.Test;

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
			FaaliyetTanimYaratIstekSonuc yaratimSonucu = _fabrika.FaaliyetTanimYarat(ad, aciklama, tip, baslatan, bitiren, isAkisiBitirmeKosulu, faaliyetBitirmeKosulu);
			if (!yaratimSonucu.Basarili)
				return new IstekSonuc {Basarili = false, Mesaj = yaratimSonucu.Mesaj};
			_yonetici.YeniFaaliyetTanimEkle(yaratimSonucu.YaratilanNesne);
			return new IstekSonuc {Basarili = true};
		}

		public IstekSonuc FaaliyetTanimIliskiEkle(string kaynakFaaliyetAd, string hedefFaaliyetAd)
		{
			IFaaliyetTanim tanim1 = _yonetici.FaaliyetTanimAl(kaynakFaaliyetAd);
			IFaaliyetTanim tanim2 = _yonetici.FaaliyetTanimAl(hedefFaaliyetAd);
			if (tanim1 == null || tanim2 == null)
				return new IstekSonuc {Basarili = false, Mesaj = "Tanımsız faaliyet"};
			_yonetici.YeniFaaliyetTanimIliskisiEkle(tanim1, tanim2);
			return new IstekSonuc {Basarili = true};
		}
	}
}