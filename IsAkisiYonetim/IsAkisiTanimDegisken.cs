namespace IAMYonetim2.IsAkisiYonetim
{
	public enum IsAkisiTanimDegiskenTipi { Bool, Liste, String }
	
	public class IsAkisiTanimDegisken : IIsAkisiTanimDegisken
	{
		public string GosterilecekAd;
		public string KisaAd;
		public IsAkisiTanimDegiskenTipi Tip;
		public string EkBilgi;
		public int SiraNo;
		public bool SadeceSoru;
		public bool Cacheli;
		public string Sql;
		public string EkSecenekler;
	}
}
