using System;
using System.Collections.Generic;
using System.Linq;

namespace IAMYonetim2.IsAkisiYonetim
{
	public class IsAkisiSurum : IIsAkisiSurum
	{
		public readonly string Surum;
		public string Aciklama;
		public DateTime OlusturmaTarihi;
		public DateTime UygulamaTarihi;
		public DateTime IptalTarihi;

		public IsAkisiSurum(string surum)
		{
			Surum = surum;
		}
		
		private readonly List<IFaaliyetTanim> _faaliyetTanimlar = new List<IFaaliyetTanim>();
		private readonly List<IIsAkisiTanimDegisken> _degiskenTanimlar = new List<IIsAkisiTanimDegisken>();
        
        public bool FaaliyetTanimEkle(IFaaliyetTanim faaliyetTanim)
        {
	        if (FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_faaliyetTanimlar.Add(faaliyetTanim);
	        return true;
		}

		public bool FaaliyetTanimIceriyor(IFaaliyetTanim faaliyetTanim)
		{
			return Enumerable.Contains(_faaliyetTanimlar, faaliyetTanim);
		}

		public bool DegiskenTanimEkle(IIsAkisiTanimDegisken degiskenTanim)
		{
			if (Enumerable.Contains(_degiskenTanimlar, degiskenTanim)) return false;
			_degiskenTanimlar.Add(degiskenTanim);
			return true;
		}

		//todo: yeni yazıldı, testini yazarak implement et
		public bool DegiskenTanimAl(string degiskenAd)
		{
			throw new System.NotImplementedException();
		}

		public bool FaaliyetTanimSil(IFaaliyetTanim faaliyetTanim)
		{
			if (!FaaliyetTanimIceriyor(faaliyetTanim)) return false;
			_faaliyetTanimlar.Remove(faaliyetTanim);
			return true;
		}

		public IFaaliyetTanim FaaliyetTanimAl(string faaliyetTanimAd)
		{
			return null;
		}

		public int FaaliyetTanimAdediAl()
		{
			return _faaliyetTanimlar.Count;
		}

		public int DegiskenAdediAl()
		{
			return _degiskenTanimlar.Count;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((IsAkisiSurum)obj);
		}

		protected bool Equals(IsAkisiSurum other)
		{
			return string.Equals(Surum, other.Surum);
		}

		public override int GetHashCode()
		{
			return (Surum != null ? Surum.GetHashCode() : 0);
		}
	}
}
