using System;

namespace IAMYonetim2.IsAkisiYonetim
{
	public class IsAkisiTanimNesneFabrika : IIsAkisiTanimNesneFabrika
	{
		public FaaliyetTanimYaratIstekSonuc FaaliyetTanimYarat(string ad, string aciklama, string tip, bool baslatan, bool bitiren, string bitirmeKosulu, string faaliyetBitirmeKosulu)
		{
			if (ad == "")
				return new FaaliyetTanimYaratIstekSonuc {Basarili = false, Mesaj = "Faaliyet tanım yaratılması için ad girilmesi zorunludur"};
			FaaliyetTanimTip tipEnum;
			bool tipCevrimiBasarili = Enum.TryParse(tip, out tipEnum);
			if (!tipCevrimiBasarili)
				return new FaaliyetTanimYaratIstekSonuc {Basarili = false, Mesaj = "Faaliyet tanım tipi hatalı gönderildi (" + tip + ")"};
			var nesne = new FaaliyetTanim {Ad = ad, Aciklama = aciklama, Tip = tipEnum, Baslatan = baslatan, Bitiren = bitiren, BitirmeKosulu = bitirmeKosulu, FaaliyetBitirmeKosulu = faaliyetBitirmeKosulu};
			return new FaaliyetTanimYaratIstekSonuc {Basarili = true, YaratilanNesne = nesne};
		}

		public IsAkisiTanimDegiskenYaratIstekSonuc IsAkisiTanimDegiskenYarat(string gosterilecekAd, string kisaAd, string tip, string ekBilgi, int siraNo, bool sadeceSoru, bool cacheli, string sql, string ekSecenekler)
		{
			if (gosterilecekAd == "" || kisaAd == "")
				return new IsAkisiTanimDegiskenYaratIstekSonuc {Basarili = false, Mesaj = "İş akışı tanım değişkeni yaratılması için gösterilecek ad ve kısa ad girilmesi zorunludur"};
			IsAkisiTanimDegiskenTipi tipEnum;
			bool tipCevrimiBasarili = Enum.TryParse(tip, out tipEnum);
			if (!tipCevrimiBasarili)
				return new IsAkisiTanimDegiskenYaratIstekSonuc {Basarili = false, Mesaj = "İş akışı tanım değişken tipi hatalı gönderildi (" + tip + ")"};
			var nesne = new IsAkisiTanimDegisken {GosterilecekAd = gosterilecekAd, KisaAd = kisaAd, Tip = tipEnum, EkBilgi = ekBilgi, SiraNo = siraNo, SadeceSoru = sadeceSoru, Cacheli = cacheli, Sql = sql, EkSecenekler = ekSecenekler};
			return new IsAkisiTanimDegiskenYaratIstekSonuc {Basarili = true, YaratilanNesne = nesne};
		}

		public FaaliyetTanimSorumluYaratIstekSonuc FaaliyetTanimSorumluYarat(int sorumluKullaniciId, string sorumluRolAdi, string sorumluOzel, string kosul)
		{
			if (sorumluKullaniciId <= 0 && sorumluRolAdi == "" && sorumluOzel == "")
				return new FaaliyetTanimSorumluYaratIstekSonuc {Basarili = false, Mesaj = "Faaliyet tanım sorumlu yaratılması için kullanıcı, rol veya özel alanlarından biri dolu olmalıdır"};
			var sorumlu = new FaaliyetTanimSorumlu {KullaniciId = sorumluKullaniciId, RolAd = sorumluRolAdi, OzelSorumluluk = sorumluOzel, Kosul = kosul};
			return new FaaliyetTanimSorumluYaratIstekSonuc {Basarili = true, YaratilanNesne = sorumlu};
		}

		public FaaliyetTanimDegiskenYaratIstekSonuc FaaliyetTanimDegiskenYarat(string gosterilecekAd, string kisaAd, string tip, string ekBilgi, int siraNo, bool sadeceSoru, bool cacheli, string sql, string ekSecenekler, string ozellikler, string stiller)
		{
			if (gosterilecekAd == "" || kisaAd == "")
				return new FaaliyetTanimDegiskenYaratIstekSonuc {Basarili = false, Mesaj = "Faaliyet tanım değilken yaratılması için gösterilen ad ve kısa ad girilmesi zorunludur"};
			FaaliyetTanimDegiskenTipi tipEnum;
			bool tipCevrimiBasarili = Enum.TryParse(tip, out tipEnum);
			if (!tipCevrimiBasarili)
				return new FaaliyetTanimDegiskenYaratIstekSonuc {Basarili = false, Mesaj = "Faaliyet tanım değişken tipi hatalı gönderildi (" + tip + ")"};
			var degisken = new FaaliyetTanimDegisken {GosterilecekAd = gosterilecekAd, KisaAd = kisaAd, Tip = tipEnum, EkBilgi = ekBilgi, SiraNo = siraNo, SadeceSoru = sadeceSoru, Cacheli = cacheli, SqlKomut = sql, EkSecenekler = ekSecenekler, Ozellikler = ozellikler, Stiller = stiller};
			return new FaaliyetTanimDegiskenYaratIstekSonuc {Basarili = true, YaratilanNesne = degisken};
		}
	}
}