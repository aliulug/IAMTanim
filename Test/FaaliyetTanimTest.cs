// ReSharper disable InconsistentNaming
using IAMYonetim2.IsAkisiYonetim;
using NSubstitute;
using NUnit.Framework;

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class FaaliyetTanimTest
	{
		private FaaliyetTanim _faaliyetTanim;
		private FaaliyetTanim _faaliyetTanim2; 
		private FaaliyetTanim _faaliyetTanim3;
		private IFaaliyetTanimDegisken _degisken1;
		private IFaaliyetTanimSorumlu _sorumlu1;
		
		[SetUp]
		public void TestOncesi()
		{
			_faaliyetTanim = new FaaliyetTanim { Ad = "ft1"};
			_faaliyetTanim2 = new FaaliyetTanim { Ad = "ft12" };
			_faaliyetTanim3 = new FaaliyetTanim { Ad = "ft3" };
			_degisken1 = Substitute.For<IFaaliyetTanimDegisken>();
			_sorumlu1 = Substitute.For<IFaaliyetTanimSorumlu>();
		}

		[Test]
		public void BosBirFTIcinde_SonrakiFaaliyetAdediAlCagirildiginda_0Donmeli()
		{
			Assert.That(_faaliyetTanim.IliskiliFaaliyetAdediAl(), Is.EqualTo(0));
		}
		
		[Test]
		public void BosBirFTIcine_IliskiEklendiginde_SonrakiFaaliyetAdedi1Olmali()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiliFaaliyetAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BirIliskisiOlanBirFTIcine_YeniBirFTIEklendiginde_SonrakiFaaliyetAdedi2OlmaliVeTrueDonmeli()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiEkle(_faaliyetTanim3), Is.True);
			Assert.That(_faaliyetTanim.IliskiliFaaliyetAdediAl(), Is.EqualTo(2));
		}

		[Test]
		public void BirIliskisiOlanBirFTIcine_AyniFTIleIliskiEklenmekIstendiginde_SonrakiFaaliyetAdedi1KalmaliVeFalseDonmeli()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiEkle(_faaliyetTanim2), Is.False);
			Assert.That(_faaliyetTanim.IliskiliFaaliyetAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BosBirFTIcinde_IliskiIceriyorCagirildiginda_FalseDonmeli()
		{
			Assert.That(_faaliyetTanim.IliskiIceriyor(_faaliyetTanim2), Is.False);
		}

		[Test]
		public void BirIliskisiOlanBirFTIcine_MevcutFTIIcinIliskiIceriyorCagirildiginda_TrueDonmeli()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiIceriyor(_faaliyetTanim2), Is.True);
		}

		[Test]
		public void BirIliskisiOlanBirFTIcine_MevcutOlmayanBirFTIIcinIliskiIceriyorCagirildiginda_FalseDonmeli()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiIceriyor(_faaliyetTanim3), Is.False);
		}

		[Test]
		public void BosBirFTIcinde_IliskiSilCagirildiginda_FalseDonmeli()
		{
			Assert.That(_faaliyetTanim.IliskiSil(_faaliyetTanim2), Is.False);
		}

		[Test]
		public void BirIliskisiOlanBirFTIcine_MevcutFTIIcinIliskiSilCagirildiginda_TrueDonmeliVeIliskiAdedi0Olmali()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiSil(_faaliyetTanim2), Is.True);
			Assert.That(_faaliyetTanim.IliskiliFaaliyetAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BirIliskisiOlanBirFTIcine_MevcutOlmayanBirFTIIcinIliskiSilCagirildiginda_FalseDonmeliVeIliskiAdedi1Olmali()
		{
			_faaliyetTanim.IliskiEkle(_faaliyetTanim2);
			Assert.That(_faaliyetTanim.IliskiSil(_faaliyetTanim3), Is.False);
			Assert.That(_faaliyetTanim.IliskiliFaaliyetAdediAl(), Is.EqualTo(1));
		}
		
		[Test]
		public void BosBirFTIcinde_DegiskenAdediAlCagirildiginda_0Donmeli()
		{
			Assert.That(_faaliyetTanim.DegiskenAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BosBirFTIcinde_DegiskenTanimEkleCagirildiginda_DegiskenAdedi1OlmaliVeTrueDonmeli()
		{
			Assert.That(_faaliyetTanim.DegiskenTanimEkle(_degisken1), Is.True);
			Assert.That(_faaliyetTanim.DegiskenAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BirFTDIcerenFTIcinde_AyniDegiskenEklenmekIstendiginde_DegiskenAdedi1OlmaliVeFalseDonmeli()
		{
			Assert.That(_faaliyetTanim.DegiskenTanimEkle(_degisken1), Is.True);
			Assert.That(_faaliyetTanim.DegiskenAdediAl(), Is.EqualTo(1));
			Assert.That(_faaliyetTanim.DegiskenTanimEkle(_degisken1), Is.False);
			Assert.That(_faaliyetTanim.DegiskenAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BosBirFTIcinde_DegiskenIceriyorCagirildiginda_FalseDonmeli()
		{
			Assert.That(_faaliyetTanim.DegiskenIceriyor(_degisken1), Is.False);
		}

		[Test]
		public void BirFTDIcerenFTIcinde_MevcutFTDIcinDegiskenIceriyorCagirildiginda_TrueDonmeli()
		{
			Assert.That(_faaliyetTanim.DegiskenTanimEkle(_degisken1), Is.True);
			Assert.That(_faaliyetTanim.DegiskenIceriyor(_degisken1), Is.True);
		}

		[Test]
		public void BosBirFTIcinde_DegiskenTanimSilCagirildiginda_FalseDonmeliVeDegiskenAdedi0Olmali()
		{
			Assert.That(_faaliyetTanim.DegiskenTanimSil(_degisken1), Is.False);
			Assert.That(_faaliyetTanim.DegiskenAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BirFTDIcerenFTIcinde_MevcutDegiskenTanimSilCagirildiginda_TrueDonmeliVeDegiskenAdedi0Olmali()
		{
			Assert.That(_faaliyetTanim.DegiskenTanimEkle(_degisken1), Is.True);
			Assert.That(_faaliyetTanim.DegiskenTanimSil(_degisken1), Is.True);
			Assert.That(_faaliyetTanim.DegiskenAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BosBirFTIcinde_SorumluAdediAlCagirildiginda_0Donmeli()
		{
			Assert.That(_faaliyetTanim.SorumluAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BosBirFTIcinde_SorumluEkleCagirildiginda_SorumluAdedi1OlmaliVeTrueDonmeli()
		{
			Assert.That(_faaliyetTanim.SorumluEkle(_sorumlu1), Is.True);
			Assert.That(_faaliyetTanim.SorumluAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BirSorumluIcerenFTIcinde_AyniSorumluEklenmekIstendiginde_DegiskenAdedi1OlmaliVeFalseDonmeli()
		{
			Assert.That(_faaliyetTanim.SorumluEkle(_sorumlu1), Is.True);
			Assert.That(_faaliyetTanim.SorumluAdediAl(), Is.EqualTo(1));
			Assert.That(_faaliyetTanim.SorumluEkle(_sorumlu1), Is.False);
			Assert.That(_faaliyetTanim.SorumluAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BosBirFTIcinde_SorumluIceriyorCagirildiginda_FalseDonmeli()
		{
			Assert.That(_faaliyetTanim.SorumluIceriyor(_sorumlu1), Is.False);
		}

		[Test]
		public void BirSorumluIcerenFTIcinde_MevcutSorumluIcinSorumluIceriyorCagirildiginda_TrueDonmeli()
		{
			Assert.That(_faaliyetTanim.SorumluEkle(_sorumlu1), Is.True);
			Assert.That(_faaliyetTanim.SorumluIceriyor(_sorumlu1), Is.True);
		}

		[Test]
		public void BosBirFTIcinde_SorumluSilCagirildiginda_FalseDonmeliVeSorumluAdedi0Olmali()
		{
			Assert.That(_faaliyetTanim.SorumluSil(_sorumlu1), Is.False);
			Assert.That(_faaliyetTanim.SorumluAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BirSorumluIcerenFTIcinde_MevcutSorumluIcinSilCagirildiginda_TrueDonmeliVeSorumluAdedi0Olmali()
		{
			Assert.That(_faaliyetTanim.SorumluEkle(_sorumlu1), Is.True);
			Assert.That(_faaliyetTanim.SorumluSil(_sorumlu1), Is.True);
			Assert.That(_faaliyetTanim.SorumluAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void ElimizdeAyniAdliIkiFTVarsa_EqualsMetoduCagirildiginda_TrueDonmeli()
		{
			FaaliyetTanim tanim1 = new FaaliyetTanim {Ad = "tanım adı"};
			FaaliyetTanim tanim2 = new FaaliyetTanim { Ad = "tanım adı" };
			Assert.That(tanim1.Equals(tanim2));
			Assert.That(tanim2.Equals(tanim1));
		}
	}
}
// ReSharper restore InconsistentNaming