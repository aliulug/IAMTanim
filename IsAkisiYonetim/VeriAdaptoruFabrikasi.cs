using System.Collections.Generic;
using AdaVeriKatmani;
using IAMYonetim2.Test;

namespace IAMYonetim2.IsAkisiYonetim
{
	public class VeriAdaptoruFabrikasi
	{
		private readonly string _veritabaniBaglantiString;
		private readonly VeritabaniTipi _veritabaniTipi;

		public VeriAdaptoruFabrikasi(string veritabaniBaglantiString, VeritabaniTipi veritabaniTipi)
		{
			_veritabaniBaglantiString = veritabaniBaglantiString;
			_veritabaniTipi = veritabaniTipi;
		}

		//todo
		public IIsAkisiYonetimVeriAdaptoru IsAkisiYonetimVeriAdaptoruYarat()
		{
			return null;
		}
	}

	public interface IIsAkisiYonetimVeriAdaptoru
	{
		List<IsAkisi> TumIsAkislariniAl();
	}
}
