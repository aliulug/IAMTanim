namespace IAMYonetim2
{
	public interface IFaaliyetTanim
	{
		bool IliskiEkle(IFaaliyetTanim kime);
		bool IliskiSil(IFaaliyetTanim kime);
		bool IliskiIceriyor(IFaaliyetTanim kime);

		bool DegiskenTanimEkle(IFaaliyetTanimDegisken degisken);
		bool DegiskenTanimSil(IFaaliyetTanimDegisken degisken);
		bool DegiskenIceriyor(IFaaliyetTanimDegisken degisken);

		bool SorumluEkle(IFaaliyetTanimSorumlu faaliyetTanimSorumlu);
		bool SorumluSil(IFaaliyetTanimSorumlu faaliyetTanimSorumlu);
		bool SorumluIceriyor(IFaaliyetTanimSorumlu faaliyetTanimSorumlu);
	}
}