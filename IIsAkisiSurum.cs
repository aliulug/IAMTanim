namespace IAMYonetim2
{
	public interface IIsAkisiSurum
	{
		bool YeniFaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim);
		bool FaaliyetTanimIceriyor(IFaaliyetTanim faaliyetTanim);
		bool DegiskenTanimEkle(IIsAkisiTanimDegisken isAkisiTanimDegisken);
		bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim);
	}
}