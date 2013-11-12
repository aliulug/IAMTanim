using System;

namespace IAMYonetim2.Test
{
	public class IsAkisi
	{
		public readonly string Ad;
		public string Aciklama;
		public DateTime OlusturmaTarihi;

		public IsAkisi(string ad)
		{
			Ad = ad;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((IsAkisi)obj);
		}

		protected bool Equals(IsAkisi other)
		{
			return string.Equals(Ad, other.Ad);
		}

		public override int GetHashCode()
		{
			return (Ad != null ? Ad.GetHashCode() : 0);
		}
	}
}