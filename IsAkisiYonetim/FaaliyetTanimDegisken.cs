namespace IAMYonetim2.IsAkisiYonetim
{
	public enum FaaliyetTanimDegiskenTipi { Bool, Liste, String }
	
	public class FaaliyetTanimDegisken : IFaaliyetTanimDegisken
	{
		public string GosterilecekAd { get; set; }
		public string KisaAd { get; set; }
		public FaaliyetTanimDegiskenTipi Tip { get; set; }
		public string EkBilgi { get; set; }
		public int SiraNo { get; set; }
		public bool SadeceSoru { get; set; }
		public bool Cacheli { get; set; }
		public string SqlKomut { get; set; }
		public string EkSecenekler { get; set; }
		public string Ozellikler { get; set; }
		public string Stiller { get; set; }
	}
}
