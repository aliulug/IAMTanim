using IAMYonetim2.Test;

namespace IAMYonetim2
{
	public interface IIsAkisiSurumYoneticisi
	{
		bool FaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim);
		bool FaaliyetTanimIliskiEkle(IFaaliyetTanim kimden, IFaaliyetTanim kime, string kosul);
		bool FaaliyetTanimDegiskeniEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken);
		bool FaaliyetTanimSorumluEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu, string kosul);
		bool IsAkisiDegiskenEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken);
		bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim);
		bool FaaliyetTanimIliskiSil(IFaaliyetTanim kimden, IFaaliyetTanim kime);
		bool FaaliyetTanimDegiskenSil(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken);
		bool FaaliyetTanimSorumluSil(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu);
		void Kaydet(IIsAkisiSurumKaydeden isAkisiSurumKaydeden);
		IFaaliyetTanim FaaliyetTanimAl(string faaliyetTanimAd);
		IIsAkisiTanimDegisken IsAkisiDegiskenAl(string degiskenAd);
		bool IsAkisiDegiskenSil(IIsAkisiTanimDegisken degisken);
	}
}