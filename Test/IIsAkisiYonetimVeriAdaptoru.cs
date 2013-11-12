using System;
using System.Collections.Generic;
using IAMYonetim2.IsAkisiYonetim;

namespace IAMYonetim2.Test
{
	public interface IIsAkisiYonetimVeriAdaptoru
	{
		List<IsAkisi> TumIsAkislariniAl();
		void IsAkisiSil(string isAkisAd);
		int IsAkisiInstanceAdediAl(string isAkisAd);
		IsAkisi IsAkisAl(string isAkisAd);
		void IsAkisYenidenAdlandir(string eskiAd, string yeniAd);
		List<IsAkisiSurum> TumIsAkisSurumleriniAl(string isAkisAd);
		void SurumEkle(string isAkisAd, string surum, string aciklama, DateTime olusturmaTarihi);
		IsAkisiSurum IsAkisSurumAl(string isAkisiAd1, string surum);
		List<IsAkisiSurum> IsAkisSurumAl(string isAkisiAd1, DateTime uygulamaTarihi, DateTime iptalTarihi);
		void SurumSil(string isAkisAd, string surum);
		int IsAkisSurumInstanceAdediAl(string isAkisAd, string surum);
		void SurumTarihGuncelle(string isAkisAd, string surum, DateTime uygulamaTarihi, DateTime iptalTarihi);
	}
}