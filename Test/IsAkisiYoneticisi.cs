using System;
using System.Collections.Generic;
using System.Linq;
using IAMYonetim2.IsAkisiYonetim;
using Newtonsoft.Json;

namespace IAMYonetim2.Test
{
	public class IsAkisiYoneticisi
	{
		private readonly IIsAkisiYonetimVeriAdaptoru _veriAdaptoru;

		public IsAkisiYoneticisi(IIsAkisiYonetimVeriAdaptoru veriAdaptoru)
		{
			_veriAdaptoru = veriAdaptoru;
		}

		public string TumIsAkislariniAl()
		{
			return JsonConvert.SerializeObject(_veriAdaptoru.TumIsAkislariniAl());
		}

		public int IsAkisiInstanceAdediAl(string isAkisAd)
		{
			return _veriAdaptoru.IsAkisiInstanceAdediAl(isAkisAd);
		}

		public IstekSonuc IsAkisSil(string isAkisAd)
		{
			if (IsAkisAl(isAkisAd) == null)
				return IstekSonuc.Hata(isAkisAd + " ismi ile tanımlı bir iş akışı bulunamadı");
			_veriAdaptoru.IsAkisiSil(isAkisAd);
			return IstekSonuc.Basari();
		}

		public IsAkisi IsAkisAl(string isAkisiAdi)
		{
			return _veriAdaptoru.IsAkisAl(isAkisiAdi);
		}

		public IstekSonuc IsAkisiYenidenAdlandir(string eskiAd, string yeniAd)
		{
			if (IsAkisAl(yeniAd) != null)
				return IstekSonuc.Hata(yeniAd + " ismi ile tanımlı bir iş akışı var");
			if (IsAkisAl(eskiAd) == null)
				return IstekSonuc.Hata(yeniAd + " ismi ile tanımlı bir iş akışı bulunamadı");
			_veriAdaptoru.IsAkisYenidenAdlandir(eskiAd, yeniAd);
			return IstekSonuc.Basari();
		}

		public string IsAkisSurumleriniAl(string isAkisAd)
		{
			return JsonConvert.SerializeObject(_veriAdaptoru.TumIsAkisSurumleriniAl(isAkisAd));
		}

		public IstekSonuc IsAkisSurumEkle(string isAkisAd, string surum, string aciklama, DateTime olusturmaTarihi)
		{
			if (_veriAdaptoru.TumIsAkisSurumleriniAl(isAkisAd).Contains(new IsAkisiSurum(surum)))
				return IstekSonuc.Hata(surum + " ismi ile tanımlı bir sürüm var");
			_veriAdaptoru.SurumEkle(isAkisAd, surum, aciklama, olusturmaTarihi);
			return IstekSonuc.Basari();
		}

		public IstekSonuc IsAkisSurumSil(string isAkisAd, string surum)
		{
			if (_veriAdaptoru.IsAkisSurumAl(isAkisAd, surum) == null)
				return IstekSonuc.Hata(surum + " adı ile tanımlı bir sürüm bulunamadı");
			_veriAdaptoru.SurumSil(isAkisAd, surum);
			return IstekSonuc.Basari();
		}

		public int IsAkisSurumInstanceAdediAl(string isAkisAd, string surum)
		{
			return _veriAdaptoru.IsAkisSurumInstanceAdediAl(isAkisAd, surum);
		}

		public IstekSonuc IsAkisSurumTarihleriGuncelle(string isAkisiAd1, string surum, DateTime uygulamaTarihi, DateTime iptalTarihi)
		{
			List<IsAkisiSurum> surumler = _veriAdaptoru.IsAkisSurumAl(isAkisiAd1, uygulamaTarihi, iptalTarihi);
			if (surumler.Count > 0)
				return IstekSonuc.Hata("Verilen tarih aralığında geçerli olan başka sürümler var (" + surumler.Aggregate("", (current, surumNesnesi) => current + (surumNesnesi.Surum + ", ")).TrimEnd(new[] {',', ' '}) + ")");
			_veriAdaptoru.SurumTarihGuncelle(isAkisiAd1, surum, uygulamaTarihi, iptalTarihi);
			return IstekSonuc.Basari();
		}
	}
}