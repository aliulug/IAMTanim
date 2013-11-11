namespace IAMYonetim2
{
	public interface IIsAkisiSurum
	{
		bool FaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim);
		bool FaaliyetTanimIceriyor(IFaaliyetTanim faaliyetTanim);
		bool DegiskenTanimEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken);
		bool DegiskenTanimAl(string degiskenAd);
		bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim);
		IFaaliyetTanim FaaliyetTanimAl(string faaliyetTanimAd);
	}
}