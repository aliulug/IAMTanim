using ClassLibrary2.Test;

namespace ClassLibrary2
{
	public class IsAkisiSurumYoneticisi : IIsAkisiSurumYoneticisi
	{
		private readonly IIsAkisiSurum _isAkisiSurum;

		public IsAkisiSurumYoneticisi(IIsAkisiSurum isAkisiSurum)
		{
			_isAkisiSurum = isAkisiSurum;
		}

		public bool YeniFaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim)
		{
			if (_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_isAkisiSurum.YeniFaaliyetTanimEkle(faaliyetTanim);
			return true;
		}

		public bool YeniFaaliyetTanimIliskisiEkle(IFaaliyetTanim kimden, IFaaliyetTanim kime)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(kimden) || !_isAkisiSurum.FaaliyetTanimIceriyor(kime)) return false;
			kimden.IliskiEkle(kime);
			return true;
		}

		public bool YeniFaaliyetTanimDegiskeniEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			faaliyetTanim.DegiskenTanimEkle(faaliyetTanimDegisken);
			return true;
		}

		public bool YeniFaaliyetTanimSorumlusuEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			faaliyetTanim.SorumluEkle(faaliyetTanimSorumlu);
			return true;
		}

		public void DegiskenTanimEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken)
		{
			_isAkisiSurum.DegiskenTanimEkle(isAkisiTanimDegisken);
		}

		public bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim)
		{
			if (!_isAkisiSurum.FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_isAkisiSurum.FaaliyetTanimSil(faaliyetTanim);
			return true;
		}

		public bool FaaliyetTanimIliskisiSil(IFaaliyetTanim kimden, IFaaliyetTanim kime)
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
			return null;
		}
	}
}