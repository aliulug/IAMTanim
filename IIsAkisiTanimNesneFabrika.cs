using IAMYonetim2.IsAkisiYonetim;

namespace IAMYonetim2
{
	public interface IIsAkisiTanimNesneFabrika
	{
		FaaliyetTanimYaratIstekSonuc FaaliyetTanimYarat(string ad, string aciklama, string tip, bool baslatan, bool bitiren, string bitirmeKosulu, string faaliyetBitirmeKosulu);
		IsAkisiTanimDegiskenYaratIstekSonuc IsAkisiTanimDegiskenYarat(string gosterilecekAd, string kisaAd, string tip, string ekBilgi, int siraNo, bool sadeceSoru, bool cacheli, string sql, string ekSecenekler);
		FaaliyetTanimSorumluYaratIstekSonuc FaaliyetTanimSorumluYarat(int sorumluKullaniciId, string sorumluRolAdi, string sorumluOzel, string kosul);
		FaaliyetTanimDegiskenYaratIstekSonuc FaaliyetTanimDegiskenYarat(string gosterilecekAd, string kisaAd, string tip, string ekBilgi, int siraNo, bool sadeceSoru, bool cacheli, string sql, string ekSecenekler, string ozellikler, string stiller);
	}
}