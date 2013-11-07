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
		private string _stringDeger5;
		private string _stringDeger6;
		private string _stringDeger7;
		private bool _trueDeger;
		private bool _falseDeger;
		private int _intDeger1;
		private string _kullaniciEnumStr;
		private string _boolEnumStr;

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
			_stringDeger5 = "as dfasdf asdf";
			_stringDeger6 = "afqwerasdfas dfasdf";
			_stringDeger7 = "qfasd fasdf asdf";
			_intDeger1 = 10;

			_kullaniciEnumStr = "Kullanici";
			_boolEnumStr = "Bool";
		}

		[Test]
		public void GecerliDegerlerIleFTYaratilmakIstendiginde_PropertyDegerleriVerilenDegerlerOlanNesneYaratilmali()
		{
			var faaliyetTanimYaratmaSonucu = _fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _trueDeger, _falseDeger, _stringDeger3, _stringDeger4);
			Assert.That(faaliyetTanimYaratmaSonucu.Basarili, Is.True);
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.Ad, Is.EqualTo(_stringDeger1));
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.Aciklama, Is.EqualTo(_stringDeger2));
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.Tip, Is.EqualTo(FaaliyetTanimTip.Kullanici));
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.Baslatan, Is.True);
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.Bitiren, Is.False);
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.BitirmeKosulu, Is.EqualTo(_stringDeger3));
			Assert.That(faaliyetTanimYaratmaSonucu.YaratilanNesne.FaaliyetBitirmeKosulu, Is.EqualTo(_stringDeger4));
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

		[Test]
		public void GecerliDegerlerIleIATDYaratilmakIstendiginde_PropertyDegerleriVerilenDegerlerOlanNesneYaratilmaliVeTrueDonmeli()
		{
			var isAkisiTanimDegiskenYaratmaSonucu = _fabrika.IsAkisiTanimDegiskenYarat(_stringDeger1, _stringDeger2, _boolEnumStr, _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.Basarili, Is.True);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.GosterilecekAd, Is.EqualTo(_stringDeger1));
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.KisaAd, Is.EqualTo(_stringDeger2));
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.Tip, Is.EqualTo(IsAkisiTanimDegiskenTipi.Bool));
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.EkBilgi, Is.EqualTo(_stringDeger3));
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.SiraNo, Is.EqualTo(_intDeger1));
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.SadeceSoru, Is.False);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.Cacheli, Is.True);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.Sql, Is.EqualTo(_stringDeger4));
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.YaratilanNesne.EkSecenekler, Is.EqualTo(_stringDeger5));
		}

		[Test]
		public void BosBirGosterilecekAdIleIATDYaratilmakIstendiginde_FalseDonmeli()
		{
			var isAkisiTanimDegiskenYaratmaSonucu = _fabrika.IsAkisiTanimDegiskenYarat("", _stringDeger2, _boolEnumStr, _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.Basarili, Is.False);
		}

		[Test]
		public void BosBirKisaAdIleIATDYaratilmakIstendiginde_FalseDonmeli()
		{
			var isAkisiTanimDegiskenYaratmaSonucu = _fabrika.IsAkisiTanimDegiskenYarat(_stringDeger1, "", _boolEnumStr, _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.Basarili, Is.False);
		}

		[Test]
		public void HataliBirTipIleIATDYaratilmakIstendiginde_FalseDonmeli()
		{
			var isAkisiTanimDegiskenYaratmaSonucu = _fabrika.IsAkisiTanimDegiskenYarat(_stringDeger1, _stringDeger2, "asdf asdfasdfasdfas", _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5);
			Assert.That(isAkisiTanimDegiskenYaratmaSonucu.Basarili, Is.False);
		}

		[Test]
		public void GecerliDegerlerIleFTSYaratilmakIstendiginde_PropertyDegerleriVerilenDegerlerOlanNesneYaratilmaliVeTrueDonmeli()
		{
			var faaliyetTanimSorumluEklemeSonucu = _fabrika.FaaliyetTanimSorumluYarat(_intDeger1, _stringDeger1, _stringDeger2, _stringDeger3);
			Assert.That(faaliyetTanimSorumluEklemeSonucu.Basarili, Is.True);
			Assert.That(faaliyetTanimSorumluEklemeSonucu.YaratilanNesne.KullaniciId, Is.EqualTo(_intDeger1));
			Assert.That(faaliyetTanimSorumluEklemeSonucu.YaratilanNesne.RolAd, Is.EqualTo(_stringDeger1));
			Assert.That(faaliyetTanimSorumluEklemeSonucu.YaratilanNesne.OzelSorumluluk, Is.EqualTo(_stringDeger2));
			Assert.That(faaliyetTanimSorumluEklemeSonucu.YaratilanNesne.Kosul, Is.EqualTo(_stringDeger3));
		}

		[Test]
		public void KullanicisiRoluVeOzelSorumluluguOlmayanBirFTSYaratilmakIstendiginde_FalseDonmeli()
		{
			var faaliyetTanimSorumluEklemeSonucu = _fabrika.FaaliyetTanimSorumluYarat(0, "", "", _stringDeger1);
			Assert.That(faaliyetTanimSorumluEklemeSonucu.Basarili, Is.False);
		}

		[Test]
		public void GecerliDegerlerIleFTDYaratilmakIstendiginde_PropertyDegerleriVerilenDegerlerOlanNesneYaratilmaliVeTrueDonmeli()
		{
			var ftdEklemeSonucu = _fabrika.FaaliyetTanimDegiskenYarat(_stringDeger1, _stringDeger2, _boolEnumStr, _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5, _stringDeger6, _stringDeger7);
			Assert.That(ftdEklemeSonucu.Basarili, Is.True);
			Assert.That(ftdEklemeSonucu.YaratilanNesne.GosterilecekAd, Is.EqualTo(_stringDeger1));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.KisaAd, Is.EqualTo(_stringDeger2));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.Tip, Is.EqualTo(FaaliyetTanimDegiskenTipi.Bool));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.EkBilgi, Is.EqualTo(_stringDeger3));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.SiraNo, Is.EqualTo(_intDeger1));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.SadeceSoru, Is.False);
			Assert.That(ftdEklemeSonucu.YaratilanNesne.Cacheli, Is.True);
			Assert.That(ftdEklemeSonucu.YaratilanNesne.SqlKomut, Is.EqualTo(_stringDeger4));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.EkSecenekler, Is.EqualTo(_stringDeger5));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.Ozellikler, Is.EqualTo(_stringDeger6));
			Assert.That(ftdEklemeSonucu.YaratilanNesne.Stiller, Is.EqualTo(_stringDeger7));
		}

		[Test]
		public void BosBirGosterilecekAdIleFTDYaratilmakIstendiginde_FalseDonmeli()
		{
			var ftdEklemeSonucu = _fabrika.FaaliyetTanimDegiskenYarat("", _stringDeger2, _boolEnumStr, _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5, _stringDeger6, _stringDeger7);
			Assert.That(ftdEklemeSonucu.Basarili, Is.False);
		}

		[Test]
		public void BosBirKisaAdIleFTDYaratilmakIstendiginde_FalseDonmeli()
		{
			var ftdEklemeSonucu = _fabrika.FaaliyetTanimDegiskenYarat(_stringDeger1, "", _boolEnumStr, _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5, _stringDeger6, _stringDeger7);
			Assert.That(ftdEklemeSonucu.Basarili, Is.False);
		}

		[Test]
		public void HataliBirTipIleFTDYaratilmakIstendiginde_FalseDonmeli()
		{
			var ftdEklemeSonucu = _fabrika.FaaliyetTanimDegiskenYarat(_stringDeger1, _stringDeger2, "asdfas dfa sdfasd", _stringDeger3, _intDeger1, _falseDeger, _trueDeger, _stringDeger4, _stringDeger5, _stringDeger6, _stringDeger7);
			Assert.That(ftdEklemeSonucu.Basarili, Is.False);
		}
	}
}
// ReSharper restore InconsistentNaming