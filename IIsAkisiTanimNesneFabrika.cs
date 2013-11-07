using IAMYonetim2.IsAkisiYonetim;

namespace IAMYonetim2.Test
{
	public interface IIsAkisiTanimNesneFabrika
	{
		FaaliyetTanimYaratIstekSonuc FaaliyetTanimYarat(string ad, string aciklama, string tip, bool baslatan, bool bitiren, string isAkisiBitirmeKosulu, string faaliyetBitirmeKosulu);
	}
}