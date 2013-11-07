using System;
using IAMYonetim2.IsAkisiYonetim;
// ReSharper disable InconsistentNaming
using NUnit.Framework;

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class IsAkisiTanimNesneFabrikaTest
	{
		private IsAkisiTanimNesneFabrika _fabrika;
		private string _stringDeger1;
		private string _stringDeger2;
		private string _stringDeger3;
		private string _stringDeger4;
		private bool _trueDeger;
		private bool _falseDeger;
		private string _kullaniciEnumStr;

		[SetUp]
		public void TestOncesi()
		{
			_fabrika = new IsAkisiTanimNesneFabrika();
			_stringDeger1 = "asdf asdf asf";
			_stringDeger2 = "asdf asfd as";
			_trueDeger = true;
			_falseDeger = false;
			_stringDeger3 = "asdfasd fasdf";
			_stringDeger4 = "qwer asdf asdf";
			_kullaniciEnumStr = "Kullanici";
		}

		[Test]
		public void GecerliDegerlerIleFTYaratilmakIstendiginde_PropertyDegerleriVerilenDegerlerOlanNesneYaratilmali()
		{
			var faaliyetTanimYaratmaSonucu = _fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _trueDeger, _falseDeger, _stringDeger3, _stringDeger4);
			Assert.That(faaliyetTanimYaratmaSonucu.Basarili, Is.True);
			var yaratilanNesne = (FaaliyetTanim)faaliyetTanimYaratmaSonucu.YaratilanNesne;
			Assert.That(yaratilanNesne.Ad, Is.EqualTo(_stringDeger1));
			Assert.That(yaratilanNesne.Aciklama, Is.EqualTo(_stringDeger2));
			Assert.That(yaratilanNesne.Tip, Is.EqualTo(FaaliyetTanimTip.Kullanici));
			Assert.That(yaratilanNesne.Baslatan, Is.True);
			Assert.That(yaratilanNesne.Bitiren, Is.False);
			Assert.That(yaratilanNesne.BitirmeKosulu, Is.EqualTo(_stringDeger3));
			Assert.That(yaratilanNesne.FaaliyetBitirmeKosulu, Is.EqualTo(_stringDeger4));
		}

		[Test]
		public void BosBirAdIleFTYaratilmakIstendiginde_FalseDonmeli()
		{
			var faaliyetTanimYaratmaSonucu = _fabrika.FaaliyetTanimYarat("", _stringDeger2, _kullaniciEnumStr, _trueDeger, _falseDeger, _stringDeger3, _stringDeger4);
			Assert.That(faaliyetTanimYaratmaSonucu.Basarili, Is.False);
		}

		[Test]
		public void HataliBirTipIleFTYaratilmakIstendiginde_FalseDonmeli()
		{
			var faaliyetTanimYaratmaSonucu = _fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, "asdfasdfasdfas", _trueDeger, _falseDeger, _stringDeger3, _stringDeger4);
			Assert.That(faaliyetTanimYaratmaSonucu.Basarili, Is.False);
		}
	}

	internal class IsAkisiTanimNesneFabrika
	{
		public FaaliyetTanimYaratIstekSonuc FaaliyetTanimYarat(string ad, string aciklama, string tip, bool baslatan, bool bitiren, string bitirmeKosulu, string faaliyetBitirmeKosulu)
		{
			if (ad == "")
				return new FaaliyetTanimYaratIstekSonuc() { Basarili = false, Mesaj = "Faaliyet tanım yaratılması için ad girilmesi zorunludur" };
			FaaliyetTanimTip tipEnum;
			bool tipCevrimiBasarili = Enum.TryParse(tip, out tipEnum);
			if (!tipCevrimiBasarili)
				return new FaaliyetTanimYaratIstekSonuc() { Basarili = false, Mesaj = "Faaliyet tanım tipi hatalı gönderildi (" + tip + ")" };
			FaaliyetTanim nesne = new FaaliyetTanim { Ad = ad, Aciklama = aciklama, Tip = tipEnum, Baslatan = baslatan, Bitiren = bitiren, BitirmeKosulu = bitirmeKosulu, FaaliyetBitirmeKosulu = faaliyetBitirmeKosulu };
			return new FaaliyetTanimYaratIstekSonuc() { Basarili = true, YaratilanNesne = nesne };
		}
	}
}
// ReSharper restore InconsistentNaming