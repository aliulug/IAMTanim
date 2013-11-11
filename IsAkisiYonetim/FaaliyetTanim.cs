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
		public string Aciklama;
		public FaaliyetTanimTip Tip;
		public bool Baslatan;
		public bool Bitiren;
		public string BitirmeKosulu;
		public string FaaliyetBitirmeKosulu;

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

		//TODO: yeni eklendi test yaz
		public IFaaliyetTanimDegisken DegiskenAl(string degiskenAdi)
		{
			return null;
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

		//todo: yeni eklendi, testleri ile birlikte yaz
		public IFaaliyetTanimSorumlu SorumluKullaniciAl(int sorumluKullaniciId)
		{
			throw new System.NotImplementedException();
		}

		//todo: yeni eklendi, testleri ile birlikte yaz
		public IFaaliyetTanimSorumlu SorumluRolAl(int sorumluKullaniciId)
		{
			throw new System.NotImplementedException();
		}

		//todo: yeni eklendi, testleri ile birlikte yaz
		public IFaaliyetTanimSorumlu SorumluOzelAl(int sorumluKullaniciId)
		{
			throw new System.NotImplementedException();
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
