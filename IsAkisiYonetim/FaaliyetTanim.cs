using System.Collections.Generic;
using System.Linq;

namespace IAMYonetim2.IsAkisiYonetim
{
	public enum FaaliyetTanimTip { Kullanici, Basit }
	
	public class FaaliyetTanim : IFaaliyetTanim
	{	
		private readonly List<IFaaliyetTanim> _iliskiliFaaliyetler = new List<IFaaliyetTanim>();
		private readonly List<IFaaliyetTanimDegisken> _degiskenTanimlar = new List<IFaaliyetTanimDegisken>();
		private readonly List<IFaaliyetTanimSorumlu> _sorumlular = new List<IFaaliyetTanimSorumlu>();

		public string Ad;

		public bool IliskiEkle(IFaaliyetTanim kime)
		{
			if (Enumerable.Contains(_iliskiliFaaliyetler, kime)) return false;
			_iliskiliFaaliyetler.Add(kime);
			return true;
		}

		public bool IliskiIceriyor(IFaaliyetTanim kime)
		{
			return Enumerable.Contains(_iliskiliFaaliyetler, kime);
		}


		public bool IliskiSil(IFaaliyetTanim kime)
		{
			if (!IliskiIceriyor(kime)) return false;
			_iliskiliFaaliyetler.Remove(kime);
			return true;
		}

		public bool DegiskenTanimEkle(IFaaliyetTanimDegisken degisken)
		{
			if (DegiskenIceriyor(degisken)) return false;
			_degiskenTanimlar.Add(degisken);
			return true;
		}

		public bool DegiskenIceriyor(IFaaliyetTanimDegisken degisken)
		{
			return Enumerable.Contains(_degiskenTanimlar, degisken);
		}

		public bool DegiskenTanimSil(IFaaliyetTanimDegisken degisken)
		{
			if (!DegiskenIceriyor(degisken)) 
				return false;
			_degiskenTanimlar.Remove(degisken);
			return true;
		}

		public bool SorumluEkle(IFaaliyetTanimSorumlu faaliyetTanimSorumlu)
		{
			if (SorumluIceriyor(faaliyetTanimSorumlu)) return false;
			_sorumlular.Add(faaliyetTanimSorumlu);
			return true;
		}

		public bool SorumluSil(IFaaliyetTanimSorumlu faaliyetTanimSorumlu)
		{
			if (!SorumluIceriyor(faaliyetTanimSorumlu)) return false;
			_sorumlular.Remove(faaliyetTanimSorumlu);
			return true;
		}

		public bool SorumluIceriyor(IFaaliyetTanimSorumlu faaliyetTanimSorumlu)
		{
			return Enumerable.Contains(_sorumlular, faaliyetTanimSorumlu);
		}

		public int IliskiliFaaliyetAdediAl()
		{
			return _iliskiliFaaliyetler.Count;
		}

		public int DegiskenAdediAl()
		{
			return _degiskenTanimlar.Count;
		}

		public int SorumluAdediAl()
		{
			return _sorumlular.Count;
		}

		public override bool Equals(object obj)
		{
			var other = obj as FaaliyetTanim;
			if (other == null) return false;
			return Ad == other.Ad;
		}

		public override int GetHashCode()
		{
			return Ad.GetHashCode();
		}
	}
}
