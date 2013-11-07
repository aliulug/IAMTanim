using IAMYonetim2.Test;

namespace IAMYonetim2
{
	public interface IIsAkisiSurumYoneticisi
	{
		bool YeniFaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim);
		bool YeniFaaliyetTanimIliskisiEkle(IFaaliyetTanim kimden, IFaaliyetTanim kime);
		bool YeniFaaliyetTanimDegiskeniEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken);
		bool YeniFaaliyetTanimSorumlusuEkle(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu);
		void DegiskenTanimEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken);
		bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim);
		bool FaaliyetTanimIliskisiSil(IFaaliyetTanim kimden, IFaaliyetTanim kime);
		bool FaaliyetTanimDegiskenSil(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimDegisken faaliyetTanimDegisken);
		bool FaaliyetTanimSorumluSil(IFaaliyetTanim faaliyetTanim, IFaaliyetTanimSorumlu faaliyetTanimSorumlu);
		void Kaydet(IIsAkisiSurumKaydeden isAkisiSurumKaydeden);
		IFaaliyetTanim FaaliyetTanimAl(string faaliyetTanimAd);
	}
}