using IAMYonetim2.IsAkisiYonetim;
using NSubstitute;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class IsAkisiSurumTest
	{
		private IsAkisiSurum _isAkisiSurum;
		private IFaaliyetTanim _faaliyetTanim1;
		private IFaaliyetTanim _faaliyetTanim2;
		private IIsAkisiTanimDegisken _isAkisiTanimDegisken1;

		[SetUp]
		public void TestOncesi()
		{
			_isAkisiSurum = new IsAkisiSurum();
			_faaliyetTanim1 = Substitute.For<IFaaliyetTanim>();
			_faaliyetTanim2 = Substitute.For<IFaaliyetTanim>();
			_isAkisiTanimDegisken1 = Substitute.For<IIsAkisiTanimDegisken>();
		}

		[Test]
		public void BosBirIASIcinde_IASFTAdediCagirildiginda_0Donmeli()
		{
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(0));
		}

	
		[Test]
        public void BosBirIASIcine_YeniBirFTEklendiginde_IASFTAdedi1Olsun()
        {
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(1));
        }

		[Test]
		public void BirFTIcerenIASIcinde_MevcutFTIcinFaaliyetTanimIceriyormuCagirildiginda_TrueDonmeli()
		{
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimIceriyor(_faaliyetTanim1), Is.True);
		}

		[Test]
		public void BosBirIASIcinde_FaaliyetTanimIceriyormuCagirildiginda_FalseDonmeli()
		{
			Assert.That(_isAkisiSurum.FaaliyetTanimIceriyor(_faaliyetTanim1), Is.False);
		}

		[Test]
		public void BirFTIcerenIASIcinde_MevcutOlmayanBirFTIcinFaaliyetTanimIceriyormuCagirildiginda_FalseDonmeli()
		{
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimIceriyor(_faaliyetTanim2), Is.False);
		}

		[Test]
		public void BirFTIcerenIASIcine_AyniFTEklenmekIstendiginde_FalseDonmeliVeToplamFaaliyetAdedi1Olmali()
		{
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1), Is.False);
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BosBirIASIcinde_DegiskenAdediAlindiginda_0Donmeli()
		{
			Assert.That(_isAkisiSurum.DegiskenAdediAl(), Is.EqualTo(0));
		}

		[Test]
		public void BosBirIASIcinde_DegiskenTanimEkleCagirildiginda_DegiskenAdediBirArtmaliVeTrueDonmeli()
		{
			Assert.That(_isAkisiSurum.DegiskenTanimEkle(_isAkisiTanimDegisken1), Is.True);
			Assert.That(_isAkisiSurum.DegiskenAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BirIATDIcerenBirIASIcinde_MevcutDegiskenIcinDegiskenTanimEkleCagirildiginda_DegiskenAdediAyniKalmaliVeFalseDonmeli()
		{
			_isAkisiSurum.DegiskenTanimEkle(_isAkisiTanimDegisken1);
			Assert.That(_isAkisiSurum.DegiskenTanimEkle(_isAkisiTanimDegisken1), Is.False);
			Assert.That(_isAkisiSurum.DegiskenAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BosBirIASIcinde_FaaliyetTanimSilCagirildiginda_FalseDonmeli()
		{
			Assert.That(_isAkisiSurum.FaaliyetTanimSil(_faaliyetTanim1), Is.False);
		}

		[Test]
		public void BirFTIcerenIASIcinde_MevcutFTIcinFaaliyetTanimSilCagirildiginda_TrueDonmeliVeFTAdedi0Olmali()
		{
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimSil(_faaliyetTanim1), Is.True);
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(0));
		}
		
		[Test]
		public void IkiFTIcerenIASIcinde_MevcutFTIcinFaaliyetTanimSilCagirildiginda_TrueDonmeliVeFTAdedi1Olmali()
		{
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim1);
			_isAkisiSurum.FaaliyetTanimEkle(_faaliyetTanim2);
			Assert.That(_isAkisiSurum.FaaliyetTanimSil(_faaliyetTanim1), Is.True);
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(1));
		}
	}
}
// ReSharper restore InconsistentNaming