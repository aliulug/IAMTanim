using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace IAMYonetim
{
	[TestFixture]
	public class IsAkisiSurumYoneticisiTest
	{
		private Mockery _mockery;
		private IIsAkisiSurum _surum;
		private IsAkisiSurumYoneticisi _yonetici;
		private IFaaliyetTanim _faaliyetTanim;
		private IFaaliyetTanim _faaliyetTanim2;

		[SetUp]
		public void TestOncesi()
		{
			_mockery = new Mockery();
			_surum = _mockery.NewMock<IIsAkisiSurum>();
			_yonetici = new IsAkisiSurumYoneticisi(_surum);
			_faaliyetTanim = _mockery.NewMock<IFaaliyetTanim>();
			_faaliyetTanim2 = _mockery.NewMock<IFaaliyetTanim>();
		}

		[Test]
		public void YeniBirIASIcine_YeniBirFTEklendiginde_YeniFaaliyetEkleCagirilmaliVeTrueDonmeli()
		{
			Stub.On(_surum).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(false));
			Expect.Once.On(_surum).Method("YeniFaaliyetTanimEkle").With(_faaliyetTanim).Will(Return.Value(true));
			_yonetici.YeniFaaliyetTanimEkle(_faaliyetTanim);
		}

		[Test]
		public void BirFTIcerenBirIASIcine_AyniIsimliBirFTDahaEklendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			Stub.On(_surum).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(true));
			Expect.Never.On(_surum);
			_yonetici.YeniFaaliyetTanimEkle(_faaliyetTanim);
		}

		[Test]
		public void IkiFTIcerenBirIASIcine_YeniBirFTIEklendiginde_FTYeniIliskiEkleCagirilmaliVeTrueDonmeli()
		{
			Expect.Once.On(_faaliyetTanim).Method("IliskiEkle").With(_faaliyetTanim2).Will(Return.Value(true));
			Stub.On(_surum).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(true));
			Assert.That(_yonetici.YeniFaaliyetTanimIliskisiEkle(_faaliyetTanim, _faaliyetTanim2), Is.True);
		}

		[Test]
		public void YeniBirIASIcinde_YeniBirFTIEklendiginde_HicBirSeyCagirilmamaliVeFalseDonmeli()
		{
			Expect.Never.On(_faaliyetTanim);
			Stub.On(_surum).Method("FaaliyetTanimIceriyor").WithAnyArguments().Will(Return.Value(false));
			Assert.That(_yonetici.YeniFaaliyetTanimIliskisiEkle(_faaliyetTanim, _faaliyetTanim2), Is.False);
		}

		[TearDown]
		public void TestSonrasi()
		{
			_mockery.VerifyAllExpectationsHaveBeenMet();
		}
	}

	public interface IFaaliyetTanim
	{
		bool IliskiEkle(IFaaliyetTanim hedefFaaliyet);
	}

	public interface IIsAkisiSurum
	{
		bool YeniFaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim);
		bool FaaliyetTanimIceriyor(IFaaliyetTanim faaliyetTanim);
	}

	public class IsAkisiSurumYoneticisi
	{
		private readonly IIsAkisiSurum _surum;

		public IsAkisiSurumYoneticisi(IIsAkisiSurum surum)
		{
			_surum = surum;
		}

		public bool YeniFaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim)
		{
			if (_surum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_surum.YeniFaaliyetTanimEkle(faaliyetTanim);
			return true;
		}

		public bool YeniFaaliyetTanimIliskisiEkle(IFaaliyetTanim kaynakFaaliyet, IFaaliyetTanim hedefFaaliyet)
		{
			if (!_surum.FaaliyetTanimIceriyor(kaynakFaaliyet) || !_surum.FaaliyetTanimIceriyor(hedefFaaliyet)) return false;
			kaynakFaaliyet.IliskiEkle(hedefFaaliyet);
			return true;
		}
	}
}
