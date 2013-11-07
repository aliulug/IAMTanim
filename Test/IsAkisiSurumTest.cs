using System.Collections.Generic;
using System.Linq;
using ClassLibrary2;
// ReSharper disable InconsistentNaming
using NSubstitute;
using NUnit.Framework;

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class IsAkisiSurumTest
	{
		private IsAkisiSurum _isAkisiSurum;
		private IFaaliyetTanim _faaliyetTanim1;
		private IFaaliyetTanim _faaliyetTanim2;

		[SetUp]
		public void TestOncesi()
		{
			_isAkisiSurum = new IsAkisiSurum();
			_faaliyetTanim1 = Substitute.For<IFaaliyetTanim>();
			_faaliyetTanim2 = Substitute.For<IFaaliyetTanim>();
		}

		[Test]
		public void BosBirIASIcinde_IASFTAdediCagirildiginda_0Donmeli()
		{
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(0));
		}


		[Test]
		public void BosBirIASIcine_YeniBirFTEklendiginde_IASFTAdedi1Olsun()
		{
			_isAkisiSurum.YeniFaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(1));
		}

		[Test]
		public void BirFTIcerenIASIcinde_MevcutFTIcinFaaliyetTanimIceriyormuCagirildiginda_TrueDonmeli()
		{
			_isAkisiSurum.YeniFaaliyetTanimEkle(_faaliyetTanim1);
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
			_isAkisiSurum.YeniFaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.FaaliyetTanimIceriyor(_faaliyetTanim2), Is.False);
		}

		[Test]
		public void BirFTIcerenIASIcine_AyniFTEklenmekIstendiginde_FalseDonmeliVeToplamFaaliyetAdedi1Olmali()
		{
			_isAkisiSurum.YeniFaaliyetTanimEkle(_faaliyetTanim1);
			Assert.That(_isAkisiSurum.YeniFaaliyetTanimEkle(_faaliyetTanim1), Is.False);
			Assert.That(_isAkisiSurum.FaaliyetTanimAdediAl(), Is.EqualTo(1));
		}
	}

	internal class IsAkisiSurum : IIsAkisiSurum
	{
		private readonly List<IFaaliyetTanim> _faaliyetler = new List<IFaaliyetTanim>();
		
		public bool YeniFaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim)
		{
			if (FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_faaliyetler.Add(faaliyetTanim);
			return true;
		}

		public bool FaaliyetTanimIceriyor(IFaaliyetTanim faaliyetTanim)
		{
			return Enumerable.Contains(_faaliyetler, faaliyetTanim);
		}

		public bool DegiskenTanimEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken)
		{
			throw new System.NotImplementedException();
		}

		public bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim)
		{
			throw new System.NotImplementedException();
		}

		public int FaaliyetTanimAdediAl()
		{
			return _faaliyetler.Count;
		}
	}
}
// ReSharper restore InconsistentNaming