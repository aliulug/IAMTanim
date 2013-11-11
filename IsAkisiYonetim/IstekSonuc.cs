namespace IAMYonetim2.IsAkisiYonetim
{
	public class IstekSonuc
	{
		public bool Basarili;
		public string Mesaj;

		public static IstekSonuc Hata(string mesaj)
		{
			return new IstekSonuc {Basarili = false, Mesaj = mesaj};
		}

		public static IstekSonuc Basari()
		{
			return new IstekSonuc {Basarili = true};
		}
	}
}