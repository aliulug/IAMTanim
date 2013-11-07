using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			_yonetici = Substitute.For<IIsAkisiSurumYoneticisi>();
			_fabrika = Substitute.For<IIsAkisiTanimNesneFabrika>();
			_ajaxHandler = new IsAkisiSurumYonetimAjaxHandler(_yonetici, _fabrika);

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

	public class FaaliyetTanimYaratIstekSonuc : IstekSonuc
	{
		public IFaaliyetTanim YaratilanNesne;
	}

	public class IstekSonuc
	{
		public bool Basarili;
		public string Mesaj;
	}

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

	public interface IIsAkisiTanimNesneFabrika
	{
		FaaliyetTanimYaratIstekSonuc FaaliyetTanimYarat(string ad, string aciklama, string tip, bool baslatan, bool bitiren, string isAkisiBitirmeKosulu, string faaliyetBitirmeKosulu);
	}
}
