// ReSharper disable InconsistentNaming
using IAMYonetim2.IsAkisiYonetim;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class IsAkisiSurumYoneticisiTest
	{
		private Mockery _mockery;
		private IsAkisiSurumYoneticisi _isAkisiSurumYoneticisi;
		private IIsAkisiSurum _isAkisiSurum1;
		private IFaaliyetTanim _faaliyetTanim1;
		private IFaaliyetTanim _faaliyetTanim2;
		private IFaaliyetTanimDegisken _faaliyetTanimDegisken1;
		private IFaaliyetTanimSorumlu _faaliyetTanimSorumlu1;
		private IIsAkisiTanimDegisken _isAkisiTanimDegisken1;

		[SetUp]
		public void TestOncesi()
		{
			_mockery = new Mockery();
			_isAkisiSurum1 = _mockery.NewMock<IIsAkisiSurum>();
			_faaliyetTanim1 = _mockery.NewMock<IFaaliyetTanim>();
			_faaliyetTanim2 = _mockery.NewMock<IFaaliyetTanim>();
			_faaliyetTanimDegisken1 = _mockery.NewMock<IFaaliyetTanimDegisken>();
			_faaliyetTanimSorumlu1 = _mockery.NewMock<IFaaliyetTanimSorumlu>();
			_isAkisiTanimDegisken1 = _mockery.NewMock<IIsAkisiTanimDegisken>();
			_isAkisiSurumYoneticisi = new IsAkisiSurumYoneticisi(_isAkisiSurum1);
		}

		[Test]
		public void YeniBirIASIcine_YeniBirFTEklendiginde_YeniFaaliyetEkleCagirilmali()
		{
			Expect.Once.On(_isAkisiSurum1).Method("FaaliyetTanimEkle").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(false));
			_isAkisiSurumYoneticisi.FaaliyetTanimEkle(_faaliyetTanim1);
		}

		[Test]
		public void BirFTIcerenBirIASIcine_AyniIsimliBirFTDahaEklendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Expect.Never.On(_isAkisiSurum1);
			_isAkisiSurumYoneticisi.FaaliyetTanimEkle(_faaliyetTanim1);
		}

		[Test]
		public void BirFTIcerenBirIASIcine_YeniBirFTEklendiginde_YeniFaaliyetEkleCagirilmali()
		{
			Expect.Once.On(_isAkisiSurum1).Method("FaaliyetTanimEkle").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(false));
			_isAkisiSurumYoneticisi.FaaliyetTanimEkle(_faaliyetTanim1);
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(false));
			Expect.Once.On(_isAkisiSurum1).Method("FaaliyetTanimEkle").With(_faaliyetTanim2).Will(Return.Value(true));
			_isAkisiSurumYoneticisi.FaaliyetTanimEkle(_faaliyetTanim2);
		}

		//TODO: REVİZE ET KOŞUL EKLENDİ, FAALİYET TANIM İLİŞKİ EKLE METODUNA DA BAK
		[Test]
		public void IkiFTIcerenBirIASIcine_YeniBirFTIEklendiginde_FTYeniIliskiEkleCagirilmali()
		{
			Expect.Once.On(_faaliyetTanim1).Method("IliskiEkle").With(_faaliyetTanim2).Will(Return.Value(true));
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(true));
			_isAkisiSurumYoneticisi.FaaliyetTanimIliskiEkle(_faaliyetTanim1, _faaliyetTanim2, "");
		}
		
		//TODO: REVİZE ET KOŞUL EKLENDİ, FAALİYET TANIM İLİŞKİ EKLE METODUNA DA BAK
		[Test]
		public void YeniBirIASIcine_YeniBirFTIEklendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			yeniBirIASIcinStubAyarla();
			Expect.Never.On(_faaliyetTanim1);
			Expect.Never.On(_faaliyetTanim2);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimIliskiEkle(_faaliyetTanim1, _faaliyetTanim2, ""), Is.False);
		}

		[Test]
		public void YeniBirIASIcine_YeniBirFTDEklendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			yeniBirIASIcinStubAyarla();
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimDegiskeniEkle(_faaliyetTanim1, _faaliyetTanimDegisken1), Is.False);
		}

		[Test]
		public void BirFTIcerenBirIASIcine_YeniBirFTDEklendiginde_DegiskenTanimEkleCagirilmaliVeTrueDonmeli()
		{
			birFTIcerenIASIcinStubAyarla();
			Expect.Once.On(_faaliyetTanim1).Method("DegiskenTanimEkle").With(_faaliyetTanimDegisken1).Will(Return.Value(true));
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimDegiskeniEkle(_faaliyetTanim1, _faaliyetTanimDegisken1), Is.True);
		}

		//TODO: REVİZE ET KOŞUL EKLENDİ
		[Test]
		public void YeniBirIASIcine_FTSOlarakBirRolEklendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			yeniBirIASIcinStubAyarla();
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimSorumluEkle(_faaliyetTanim1, _faaliyetTanimSorumlu1, ""), Is.False);
		}

		[Test]
		public void BirFTIcerenBirIASIcine_FTSOlarakBirRolEklendiginde_SorumluRolEkleCagirilmaliVeTrueDonmeli()
		{
			birFTIcerenIASIcinStubAyarla();
			Expect.Once.On(_faaliyetTanim1).Method("SorumluEkle").With(_faaliyetTanimSorumlu1).Will(Return.Value(true));
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimSorumluEkle(_faaliyetTanim1, _faaliyetTanimSorumlu1, ""), Is.True);
		}

		[Test]
		public void YeniBirIASIcine_BirIADEklendiginde_DegiskenEkleCagirilmali()
		{
			Expect.Once.On(_isAkisiSurum1).Method("DegiskenTanimEkle").With(_isAkisiTanimDegisken1).Will(Return.Value(true));
			_isAkisiSurumYoneticisi.IsAkisiDegiskenEkle(_isAkisiTanimDegisken1);
		}

		[Test]
		public void YeniBirIASIcinde_BirFTSilinmekIstendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			yeniBirIASIcinStubAyarla();
			Expect.Never.On(_isAkisiSurum1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimSil(_faaliyetTanim1), Is.False);
		}

		[Test]
		public void BirFTIcerenBirIASIcinde_MevcutFTSilinmekIstendiginde_FaaliyetTanimSilCagirilmaliVeTrueDonmeli()
		{
			birFTIcerenIASIcinStubAyarla();
			Expect.Once.On(_isAkisiSurum1).Method("FaaliyetTanimSil").With(_faaliyetTanim1).Will(Return.Value(true));
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimSil(_faaliyetTanim1), Is.True);
		}

		[Test]
		public void YeniBirIASIcinde_BirFTISilinmekIstendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			yeniBirIASIcinStubAyarla();
			Expect.Never.On(_isAkisiSurum1);
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimIliskiSil(_faaliyetTanim1, _faaliyetTanim2), Is.False);
		}

		[Test]
		public void BirFTVeSifirIliskiIcerenBirIASIcinde_BirFTISilinmekIstendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			birFTVeSifirIliskiIcerenIASIcinStubAyarla();
			Expect.Never.On(_isAkisiSurum1);
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimIliskiSil(_faaliyetTanim1, _faaliyetTanim2), Is.False);
		}

		[Test]
		public void BirFTVeBirIliskiIcerenBirIASIcinde_BirFTISilinmekIstendiginde_IliskiSilCagirilmaliVeTrueDonmeli()
		{
			birFTVeBirIliskiIcerenIASIcinStubAyarla();
			Expect.Once.On(_faaliyetTanim1).Method("IliskiSil").With(_faaliyetTanim1).Will(Return.Value(false));
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimIliskiSil(_faaliyetTanim1, _faaliyetTanim1), Is.True);
		}

		[Test]
		public void YeniBirIASIcinde_BirFTDSilinmekIstendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			yeniBirIASIcinStubAyarla();
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimDegiskenSil(_faaliyetTanim1, _faaliyetTanimDegisken1), Is.False);
		}

		[Test]
		public void SifirDegiskeniOlanBirFTIcerenBirIASIcinde_BirFTDSilinmekIstendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			sifirDegiskenliBirFTOlanIASIcinStubAyarla();
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimDegiskenSil(_faaliyetTanim1, _faaliyetTanimDegisken1), Is.False);
		}

		[Test]
		public void BirDegiskeniOlanBirFTIcerenBirIASIcinde_BirFTDSilinmekIstendiginde_DegiskenSilCagirilmaliVeTrueDonmeli()
		{
			birDegiskenliBirFTOlanIASIcinStubAyarla();
			Expect.Once.On(_faaliyetTanim1).Method("DegiskenTanimSil").With(_faaliyetTanimDegisken1).Will(Return.Value(true));
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimDegiskenSil(_faaliyetTanim1, _faaliyetTanimDegisken1), Is.True);
		}

		[Test]
		public void SifirSorumlusuOlanBirFTIcerenBirIASIcinde_BirFTSSilinmekIstendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			sifirSorumlusuOlanBirFTOlanIASIcinStubAyarla();
			Expect.Never.On(_faaliyetTanim1);
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimSorumluSil(_faaliyetTanim1, _faaliyetTanimSorumlu1), Is.False);
		}

		[Test]
		public void BirSorumlusuOlanBirFTIcerenBirIASIcinde_BirFTSSilinmekIstendiginde_SorumluSilCagirilmaliVeTrueDonmeli()
		{
			birSorumlusuOlanBirFTOlanIASIcinStubAyarla();
			Expect.Once.On(_faaliyetTanim1).Method("SorumluSil").With(_faaliyetTanimSorumlu1).Will(Return.Value(true));
			Assert.That(_isAkisiSurumYoneticisi.FaaliyetTanimSorumluSil(_faaliyetTanim1, _faaliyetTanimSorumlu1), Is.True);
		}

		[Test]
		public void HerhangiBirIASYIcinde_KaydetCagirildiginda_KaydedicininKaydetMetoduCagirilmali()
		{
			var isAkisiSurumKaydeden = _mockery.NewMock<IIsAkisiSurumKaydeden>();
			Expect.Once.On(isAkisiSurumKaydeden).Method("Kaydet").With(_isAkisiSurum1).Will(Return.Value(true));
			_isAkisiSurumYoneticisi.Kaydet(isAkisiSurumKaydeden);
		}

		[Test]
		public void HerhangiBirIASIcinde_MevcutBirFTIcinFaaliyetTanimAlCagirildiginda_FTDonmeli()
		{	
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimAl").With("mevcut faaliyet").Will(Return.Value(_faaliyetTanim1));
			var faaliyetTanim = _isAkisiSurumYoneticisi.FaaliyetTanimAl("mevcut faaliyet");
			Assert.That(faaliyetTanim, Is.Not.Null);
		}

		private void sifirSorumlusuOlanBirFTOlanIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_faaliyetTanim1).Method("SorumluIceriyor").WithAnyArguments().Will(Return.Value(false));
		}
		
		private void birSorumlusuOlanBirFTOlanIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_faaliyetTanim1).Method("SorumluIceriyor").WithAnyArguments().Will(Return.Value(true));
		}

		private void birDegiskenliBirFTOlanIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_faaliyetTanim1).Method("DegiskenIceriyor").WithAnyArguments().Will(Return.Value(true));
		}

		private void sifirDegiskenliBirFTOlanIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_faaliyetTanim1).Method("DegiskenIceriyor").WithAnyArguments().Will(Return.Value(false));
		}

		private void birFTVeBirIliskiIcerenIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_faaliyetTanim1).Method("IliskiIceriyor").WithAnyArguments().Will(Return.Value(true));
		}

		private void birFTVeSifirIliskiIcerenIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
			Stub.On(_faaliyetTanim1).Method("IliskiIceriyor").WithAnyArguments().Will(Return.Value(false));
		}

		private void yeniBirIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(false));
		}

		private void birFTIcerenIASIcinStubAyarla()
		{
			Stub.On(_isAkisiSurum1).Method("FaaliyetTanimIceriyor").With(_faaliyetTanim1).Will(Return.Value(true));
		}

		[TearDown]
		public void TestSonrasi()
		{
			_mockery.VerifyAllExpectationsHaveBeenMet();
		}
	}

	public interface IIsAkisiSurumKaydeden
	{
		bool Kaydet(IIsAkisiSurum surum);
	}
}
// ReSharper restore InconsistentNaming