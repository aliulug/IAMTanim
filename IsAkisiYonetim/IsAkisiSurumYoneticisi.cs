using IAMYonetim2.Test;

namespace IAMYonetim2.IsAkisiYonetim
{
	public class IsAkisiSurumYoneticisi : IIsAkisiSurumYoneticisi
	{
		private readonly IIsAkisiSurum _isAkisiSurum;

		public IsAkisiSurumYoneticisi(IIsAkisiSurum isAkisiSurum)
		{
			_isAkisiSurum = isAkisiSurum;
		}

		public bool FaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim)
		{
			if (_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_isAkisiSurum.FaaliyetTanimEkle(faaliyetTanim);
			return true;
		}

		//TODO: TEST YENİDEN YAZ, KOŞUL PARAMETRESİ EKLEDİM
		public bool FaaliyetTanimIliskiEkle(IFaaliyetTanim kimden, IFaaliyetTanim kime, string kosul)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(kimden) || !_isAkisiSurum.FaaliyetTanimIceriyor(kime)) return false;
			kimden.IliskiEkle(kime);
			return true;
		}

		public bool FaaliyetTanimDegiskeniEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			faaliyetTanim.DegiskenTanimEkle(faaliyetTanimDegisken);
			return true;
		}

		//TODO: test yeniden yaz, koşul parametresi ekledim
		public bool FaaliyetTanimSorumluEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu, string kosul)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			faaliyetTanim.SorumluEkle(faaliyetTanimSorumlu);
			return true;
		}

		//todo revize
		public bool IsAkisiDegiskenEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken)
		{
			return _isAkisiSurum.DegiskenTanimEkle(isAkisiTanimDegisken);
		}

		public bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_isAkisiSurum.FaaliyetTanimSil(faaliyetTanim);
			return true;
		}

		public bool FaaliyetTanimIliskiSil(IFaaliyetTanim kimden, IFaaliyetTanim kime)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(kimden)) return false;
			if (!kimden.IliskiIceriyor(kime)) return false;
			kimden.IliskiSil(kime);
			return true;
		}

		public bool FaaliyetTanimDegiskenSil(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			if (!faaliyetTanim.DegiskenIceriyor(faaliyetTanimDegisken)) return false;
			faaliyetTanim.DegiskenTanimSil(faaliyetTanimDegisken);
			return true;
		}

		public bool FaaliyetTanimSorumluSil(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			if (!faaliyetTanim.SorumluIceriyor(faaliyetTanimSorumlu)) return false;
			faaliyetTanim.SorumluSil(faaliyetTanimSorumlu);
			return true;
		}

		public void Kaydet(IIsAkisiSurumKaydeden isAkisiSurumKaydeden)
		{
			isAkisiSurumKaydeden.Kaydet(_isAkisiSurum);
		}

		public IFaaliyetTanim FaaliyetTanimAl(string faaliyetTanimAd)
		{
			return _isAkisiSurum.FaaliyetTanimAl(faaliyetTanimAd);
		}

		//todo
		public IIsAkisiTanimDegisken IsAkisiDegiskenAl(string degiskenAd)
		{
			throw new System.NotImplementedException();
		}

		//todo
		public bool IsAkisiDegiskenSil(IIsAkisiTanimDegisken degisken)
		{
			throw new System.NotImplementedException();
		}
	}
}