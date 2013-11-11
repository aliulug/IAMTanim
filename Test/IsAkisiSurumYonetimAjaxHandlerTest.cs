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
		private IFaaliyetTanim _faaliyet1;
		private IFaaliyetTanim _faaliyet2;
		private IFaaliyetTanim _nullFaaliyet;
		private IFaaliyetTanimDegisken _faaliyetDegisken;
		private IFaaliyetTanimDegisken _nullFaaliyetDegisken;
		private IFaaliyetTanimSorumlu _sorumlu;
		private IFaaliyetTanimSorumlu _nullSorumlu;
		private IIsAkisiTanimDegisken _surumDegisken;
		private IIsAkisiTanimDegisken _nullSurumDegisken;

		//aşağıdakiler interface değil ama bunlar sadece data object
		private FaaliyetTanimDegisken _concreteFaaliyetDegisken;
		private FaaliyetTanimSorumlu _concreteSorumlu;
		private IsAkisiTanimDegisken _concreteIsAkisiDegisken;

		private IsAkisiSurumYonetimAjaxHandler _ajaxHandler;

		private string _stringDeger1;
		private string _stringDeger2;
		private string _stringDeger3;
		private string _stringDeger4;
		private string _stringDeger5;
		private string _stringDeger6;
		private string _stringDeger7;
		private string _stringDeger8;
		private bool _trueDeger;
		private bool _falseDeger;
		private string _kullaniciEnumStr;
		private string _boolEnumStr;
		private int _intDeger1;

		[SetUp]
		public void TestOncesi()
		{
			_yonetici = Substitute.For<IIsAkisiSurumYoneticisi>();
			_fabrika = Substitute.For<IIsAkisiTanimNesneFabrika>();
			_faaliyet1 = Substitute.For<IFaaliyetTanim>();
			_faaliyet2 = Substitute.For<IFaaliyetTanim>();
			_nullFaaliyet = null;
			_nullFaaliyetDegisken = null;
			_nullSorumlu = null;
			_nullSurumDegisken = null;
			_faaliyetDegisken = Substitute.For<IFaaliyetTanimDegisken>();
			_sorumlu = Substitute.For<IFaaliyetTanimSorumlu>();
			_surumDegisken = Substitute.For<IIsAkisiTanimDegisken>();
			_ajaxHandler = new IsAkisiSurumYonetimAjaxHandler(_yonetici, _fabrika);

			_concreteFaaliyetDegisken = Substitute.For<FaaliyetTanimDegisken>();
			_concreteSorumlu = Substitute.For<FaaliyetTanimSorumlu>();
			_concreteIsAkisiDegisken = Substitute.For<IsAkisiTanimDegisken>();

			_stringDeger1 = "asdf asdf asf";
			_stringDeger2 = "asdf asfd as";
			_trueDeger = true;
			_falseDeger = false;
			_stringDeger3 = "asdfasd fasdf";
			_stringDeger4 = "qwer asdf asdf";
			_stringDeger5 = "qw asf asfdaer asdf asdf";
			_stringDeger6 = "qwfqwer qwaer asdf asdf";
			_stringDeger7 = "qwer asd v  v vvvf asdf";
			_stringDeger8 = "qwer aqrtqt 423 sdf asdf";

			_kullaniciEnumStr = "Kullanici";
			_boolEnumStr = "Bool";
			_intDeger1 = 1432;
		}
		
		//Mevcut olmayan bir FT eklenmek isteniyor
		[Test]
		public void BosBirIASIcinde_GecerliBilgilerIleFaaliyetTanimEkleCagirildiginda_FabrikaninYaratmaMetoduCagirilmaliVeYoneticininFTEklemeMetoduCagirilmaliVeTrueDonmeli()
		{	
			//given
			_fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4).ReturnsForAnyArgs(new FaaliyetTanimYaratIstekSonuc { Basarili = true });
			_yonetici.FaaliyetTanimEkle(null).ReturnsForAnyArgs(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimEkle(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			//that
			Assert.That(sonuc.Basarili, Is.True);
			_fabrika.Received(1).FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			_yonetici.ReceivedWithAnyArgs(1).FaaliyetTanimEkle(null);
		}

		//geçersiz bilgiler ile bir FT yaratılmak isteniyor
		[Test]
		public void HerhangiBirEkranda_GecersizBilgilerIleFaaliyetTanimEkleCagirildiginda_FabrikaninYaratmaMetoduCagirilmaliVeYoneticininHicBirMetoduCagirilmamaliVeFalseDonmeli()
		{
			//given
			_fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4).ReturnsForAnyArgs(new FaaliyetTanimYaratIstekSonuc { Basarili = false });
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimEkle(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			//then
			Assert.That(sonuc.Basarili, Is.False);
			_fabrika.Received(1).FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			_yonetici.DidNotReceiveWithAnyArgs().FaaliyetTanimEkle(null);
		}

		//bir FT içeren bir IAS içine aynı adlı bir FT eklemek isteniyor
		[Test]
		public void BirFTIcerenBirIASIcinde_AyniAdaSahipBirFTEklenmekİstendiginde_FalseDonmeli()
		{
			//given
			_fabrika.FaaliyetTanimYarat(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4).ReturnsForAnyArgs(new FaaliyetTanimYaratIstekSonuc { Basarili = true });
			_yonetici.FaaliyetTanimEkle(null).ReturnsForAnyArgs(false);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimEkle(_stringDeger1, _stringDeger2, _kullaniciEnumStr, _falseDeger, _trueDeger, _stringDeger3, _stringDeger4);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut bir FT silinmek isteniyor
		[Test]
		public void BirFTIcerenBirIASIcinde_MevcutFTSilinmekIstendiginde_YoneticininFaaliyetSilMetoduCagirilmaliVeTrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimSil(null).ReturnsForAnyArgs(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSil(_stringDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.True);
			_yonetici.ReceivedWithAnyArgs(1).FaaliyetTanimSil(null);
		}

		//Mevcut olmayan bir FT silinmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutOlmayanBirFTSilinmekIstendiginde_YoneticininFaaliyetSilMetoduCagirilmaliVeFalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimSil(null).ReturnsForAnyArgs(false);
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSil(_stringDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.False);
			_yonetici.ReceivedWithAnyArgs(1).FaaliyetTanimSil(null);
		}

		//iki FT içeren bir IAS içinde mevcut iki faaliyet için ilişki eklenmek isteniyor
		[Test]
		public void IkiFTIcerenBirIASEkraninda_VarolanFTlarArasindaBirIliskiYaratilmakIstendiginde_YoneticidenTanimNesneleriAlinmaliVeYoneticininIliskiEkleMetoduCagirilmaliVeTrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_yonetici.FaaliyetTanimAl(_stringDeger2).Returns(_faaliyet2);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimIliskiEkle(_stringDeger1, _stringDeger2, _stringDeger3);
			//then
			Assert.That(sonuc.Basarili, Is.True);
			_yonetici.Received(1).FaaliyetTanimIliskiEkle(_faaliyet1, _faaliyet2, _stringDeger3);
		}

		//IAS sürüm içinde olmayan bir FT için ilişki eklenmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_IASIcindeOlmayanBirFTIcinIliskiEklenmekIstendiginde_FalseDonmeliVeYoneticininIliskiEkleMetoduCagirilmamali()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_yonetici.FaaliyetTanimAl(_stringDeger2).Returns(_nullFaaliyet);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimIliskiEkle(_stringDeger1, _stringDeger2, _stringDeger3);
			//then
			Assert.That(sonuc.Basarili, Is.False);
			_yonetici.DidNotReceive().FaaliyetTanimIliskiEkle(_faaliyet1, _nullFaaliyet, _stringDeger3);

		}
		
		//IAS sürüm içinde mevcut bir ilişki silinmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutBirFTISilinmekIstendiginde_TrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_yonetici.FaaliyetTanimAl(_stringDeger2).Returns(_faaliyet2);
			_faaliyet1.IliskiSil(_faaliyet2).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.IliskiSil(_stringDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}

		//IAS sürüm içinde mevcut olmayan bir ilişki silinmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutOlmayanBirFTISilinmekIstendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_yonetici.FaaliyetTanimAl(_stringDeger2).Returns(_faaliyet2);
			_faaliyet1.IliskiSil(_faaliyet2).Returns(false);
			//when
			IstekSonuc sonuc = _ajaxHandler.IliskiSil(_stringDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}
		
		
		//Mevcut bir FT içine geçerli bilgilerle değişken eklenmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutOlanBirFTIcineMevcutOlmayanBirDegiskenEklendiginde_TrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_fabrika.FaaliyetTanimDegiskenYarat(_stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8).Returns(new FaaliyetTanimDegiskenYaratIstekSonuc {Basarili = true, YaratilanNesne = _concreteFaaliyetDegisken});
			_yonetici.FaaliyetTanimDegiskeniEkle(_faaliyet1, _concreteFaaliyetDegisken).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimDegiskenEkle(_stringDeger1, _stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}

		//Mevcut bir FT içine geçersiz bilgilerle değişken eklenmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutOlanBirFTIcineMevcutOlmayanBirDegiskenGecersizBilgilerleEklendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_fabrika.FaaliyetTanimDegiskenYarat(_stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8).Returns(new FaaliyetTanimDegiskenYaratIstekSonuc { Basarili = false });
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimDegiskenEkle(_stringDeger1, _stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olmayan bir FT içine değişken eklenmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutOlmayanBirFTIcineDegiskenEklendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_nullFaaliyet);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimDegiskenEkle(_stringDeger1, _stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olan bir FT içine zaten varolan isimle bir değişken eklenmek isteniyor
		[Test]
		public void HerhangiBirIASIcinde_MevcutOlanBirFTIcineZatenVarolanBirFTDEklendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_fabrika.FaaliyetTanimDegiskenYarat(_stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8).Returns(new FaaliyetTanimDegiskenYaratIstekSonuc { Basarili = true });
			_yonetici.FaaliyetTanimDegiskeniEkle(_faaliyet1, null).Returns(false);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimDegiskenEkle(_stringDeger1, _stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _trueDeger, _falseDeger, _stringDeger5, _stringDeger6, _stringDeger7, _stringDeger8);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olmayan bir FT içinden değişken silinmek isteniyor
		[Test]
		public void XIsimliBirFTYok_XIcindenDegiskenSilinmekIstendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_nullFaaliyet);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetDegiskenSil(_stringDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olan bir FT içinden olmayan bir değişken silinmek isteniyor
		[Test]
		public void XIsimliBirFTVarVeYIsimliBirDegiskenYok_XIcindenYSilinmekIstendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_faaliyet1.DegiskenAl(_stringDeger2).Returns(_nullFaaliyetDegisken);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetDegiskenSil(_stringDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olan bir FT içinden mevcut bir değişken silinmek isteniyor
		[Test]
		public void XIsimliBirFTVarVeYIsimliBirDegiskenVar_XIcindenYSilinmekIstendiginde_TrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_faaliyet1.DegiskenAl(_stringDeger2).Returns(_faaliyetDegisken);
			_faaliyet1.DegiskenTanimSil(_faaliyetDegisken).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetDegiskenSil(_stringDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}

		//Mevcut FT içine sorumlu kullanıcı eklenmek isteniyor
		[Test]
		public void XIsimliBirFTVarVeYIDliBirSorumluKullaniciYok_YIDliSorumluKullaniciEklendiginde_TrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_fabrika.FaaliyetTanimSorumluYarat(_intDeger1, "", "", _stringDeger2).Returns(new FaaliyetTanimSorumluYaratIstekSonuc{Basarili = true, YaratilanNesne = _concreteSorumlu});
			_yonetici.FaaliyetTanimSorumluEkle(_faaliyet1, _concreteSorumlu, _stringDeger2).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSorumluKullaniciEkle(_stringDeger1, _intDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}
		
		//Mevcut olmayan bir FT içine sorumlu kullanıcı eklenmek isteniyor
		[Test]
		public void XIsimliBirFTYok_XIsimliFTaYIDliKullaniciEklendiginde_FalseDonmeli()
		{			
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_nullFaaliyet);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSorumluKullaniciEkle(_stringDeger1, _intDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut FT içine zaten varolan bir sorumlu kullanıcı eklenmek isteniyor
		[Test]
		public void XIsimliBirFTVarVeYIDliBirSorumluKullaniciVar_YIDIleSorumluKullaniciEklendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_fabrika.FaaliyetTanimSorumluYarat(_intDeger1, "", "", _stringDeger2).Returns(new FaaliyetTanimSorumluYaratIstekSonuc { Basarili = true, YaratilanNesne = _concreteSorumlu });
			_yonetici.FaaliyetTanimSorumluEkle(_faaliyet1, _concreteSorumlu, _stringDeger2).Returns(false);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSorumluKullaniciEkle(_stringDeger1, _intDeger1, _stringDeger2);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olan bir FT içinden mevcut bir sorumlu kullanıcı silinmek isteniyor
		[Test]
		public void XIsimliBirFTVarVeYIDliBirSorumluKullaniciVar_YIDliSorumluKullaniciSilinmekIstendiginde_TrueDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_faaliyet1.SorumluKullaniciAl(_intDeger1).Returns(_sorumlu);
			_yonetici.FaaliyetTanimSorumluSil(_faaliyet1, _sorumlu).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSorumluKullaniciSil(_stringDeger1, _intDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}

		//Mevcut olmayan bir FT içinden sorumlu kullanıcı silinmek isteniyor
		[Test]
		public void XIsimliBirFTYok_YIDliSorumluKullaniciSilinmekIstendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_nullFaaliyet);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSorumluKullaniciSil(_stringDeger1, _intDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//Mevcut olan bir FT içinden mevcut olmayan bir sorumlu kullanıcı silinmek isteniyor
		[Test]
		public void XIsimliBirFTVarVeYIDliBirSorumluKullaniciYok_YIDliSorumluKullaniciSilinmekIstendiginde_FalseDonmeli()
		{
			//given
			_yonetici.FaaliyetTanimAl(_stringDeger1).Returns(_faaliyet1);
			_faaliyet1.SorumluKullaniciAl(_intDeger1).Returns(_nullSorumlu);
			//when
			IstekSonuc sonuc = _ajaxHandler.FaaliyetTanimSorumluKullaniciSil(_stringDeger1, _intDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//IAS içine mevcut olmayan isimli bir IASD eklenmek isteniyor
		[Test]
		public void XIsimliBirIASDYok_XIsimliIASDEklendiginde_TrueDonmeli()
		{
			//given
			_yonetici.IsAkisiDegiskenAl(_stringDeger3).Returns(_nullSurumDegisken);
			_fabrika.IsAkisiTanimDegiskenYarat(_stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _falseDeger, _trueDeger, _stringDeger5, _stringDeger6).Returns(new IsAkisiTanimDegiskenYaratIstekSonuc {Basarili = true, YaratilanNesne = _concreteIsAkisiDegisken});
			_yonetici.IsAkisiDegiskenEkle(_concreteIsAkisiDegisken).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.IsAkisiSurumDegiskeniEkle(_stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _falseDeger, _trueDeger, _stringDeger5, _stringDeger6);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}

		//IAS içine mevcut olan isimli bir IASD değişkeni eklenmek isteniyor
		[Test]
		public void XIsimliBirIASDVar_XIsimliIASDEklendiginde_TrueDonmeli()
		{
			//given
			_yonetici.IsAkisiDegiskenAl(_stringDeger3).Returns(_surumDegisken);
			//when
			IstekSonuc sonuc = _ajaxHandler.IsAkisiSurumDegiskeniEkle(_stringDeger2, _stringDeger3, _boolEnumStr, _stringDeger4, _intDeger1, _falseDeger, _trueDeger, _stringDeger5, _stringDeger6);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}

		//IAS içinde mevcut olan bir IASD silinmek isteniyor
		[Test]
		public void XIsimliBirIASDVar_XIsimliIASDSilindiginde_TrueDonmeli()
		{
			//given
			_yonetici.IsAkisiDegiskenAl(_stringDeger1).Returns(_surumDegisken);
			_yonetici.IsAkisiDegiskenSil(_surumDegisken).Returns(true);
			//when
			IstekSonuc sonuc = _ajaxHandler.IsAkisiSurumDegiskeniSil(_stringDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.True);
		}

		//IAS içinde mevcut olmayan bir IASD silinmek isteniyor
		[Test]
		public void XIsimliBirIASDVar_XIsimliIASDSilindiginde_FalseDonmeli()
		{
			//given
			_yonetici.IsAkisiDegiskenAl(_stringDeger1).Returns(_nullSurumDegisken);
			//when
			IstekSonuc sonuc = _ajaxHandler.IsAkisiSurumDegiskeniSil(_stringDeger1);
			//then
			Assert.That(sonuc.Basarili, Is.False);
		}
	}
}
