using IAMYonetim2.IsAkisiYonetim;
using NSubstitute;
using NUnit.Framework;

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class IsAkisiSurumYonetimAjaxHandlerTest
	{
		private IIsAkisiSurumYoneticisi _yonetici;
		private IIsAkisiTanimNesneFabrika _fabrika;
		private IsAkisiSurumYonetimAjaxHandler _ajaxHandler;

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
			_yonetici = Substitute.For<IIsAkisiSurumYoneticisi>();
			_fabrika = Substitute.For<IIsAkisiTanimNesneFabrika>();
			_ajaxHandler = new IsAkisiSurumYonetimAjaxHandler(_yonetici, _fabrika);

			_stringDeger1 = "asdf asdf asf";
			_stringDeger2 = "asdf asfd as";
			_trueDeger = true;
			_falseDeger = false;
			_stringDeger3 = "asdfasd fasdf";
			_stringDeger4 = "qwer asdf asdf";

			_kullaniciEnumStr = "Kullanici";
		}
		
		[Test]
		public void HerhangiBirEkranda_GecerliBilgilerIleFaaliyetTanimEkleCagirildiginda_FabrikaninYaratmaMetoduCagirilmaliVeYoneticininFTEklemeMetoduCagirilmaliVeTrueDonmeli()
		{
			//başarılı yaratım için stub
			_fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4).ReturnsForAnyArgs(new FaaliyetTanimYaratIstekSonuc { Basarili = true });
			//test ettiğimiz metodu çağır
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimEkle(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			//assert
			Assert.That(sonuc.Basarili, Is.True);
			_fabrika.Received(1).FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			_yonetici.ReceivedWithAnyArgs(1).YeniFaaliyetTanimEkle(null);
		}

		[Test]
		public void HerhangiBirEkranda_GecersizBilgilerIleFaaliyetTanimEkleCagirildiginda_FabrikaninYaratmaMetoduCagirilmaliVeYoneticininHicBirMetoduCagirilmamaliVeFalseDonmeli()
		{
			//başarılı yaratım için stub
			_fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4).ReturnsForAnyArgs(new FaaliyetTanimYaratIstekSonuc { Basarili = false });
			//test ettiğimiz metodu çağır
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimEkle(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			//assert
			Assert.That(sonuc.Basarili, Is.False);
			_fabrika.Received(1).FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			_yonetici.DidNotReceiveWithAnyArgs().YeniFaaliyetTanimEkle(null);
		}

		[Test]
		public void IkiFTIcerenBirIASEkraninda_VarolanFTlarArasindaBirIliskiYaratilmakIstendiginde_YoneticidenTanimNesneleriAlinmaliVeYoneticininIliskiEkleMetoduCagirilmaliVeTrueDonmeli()
		{
			//stub
			IFaaliyetTanim tanim1 = Substitute.For<IFaaliyetTanim>();
			IFaaliyetTanim tanim2 = Substitute.For<IFaaliyetTanim>();
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(tanim1);
			_yonetici.FaaliyetTanimAl(_stringDeger2).Returns(tanim2);

			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimIliskiEkle(_stringDeger1, _stringDeger2);
			//assert
			Assert.That(sonuc.Basarili, Is.True);
			_yonetici.Received(1).YeniFaaliyetTanimIliskisiEkle(tanim1, tanim2);
		}

	}
}
