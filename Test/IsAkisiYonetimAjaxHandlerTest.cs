// ReSharper disable InconsistentNaming
using System.Linq;
using System;
using System.Collections.Generic;
using IAMYonetim2.IsAkisiYonetim;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace IAMYonetim2.Test
{
	[TestFixture]
	public class IsAkisiYonetimAjaxHandlerTest
	{
		private IsAkisiYoneticisi _yonetici;
		
		private IIsAkisiYonetimVeriAdaptoru _veriAdaptoru;
		private List<IsAkisi> _ikiIsAkisiIcerenListe;
		private string _ikiIsAkisiJSONString;
		private List<IsAkisiSurum> _ikiSurumIcerenListe;
		private List<IsAkisiSurum> _sifirSurumIcerenListe;
		private string _ikiIsAkisSurumuJSONString;
		private string _ikiSurumIcerenListedeOlanSurumAdi;
		private string _isAkisiAd1;
		private string _isAkisiAd2;
		private string _surum;
		private string _aciklama;
		private DateTime _tarih1;
		private DateTime _tarih2;
		private int _int1;
		private IsAkisi _isAkisi;
		private IsAkisi _nullIsAkisi;
		private IsAkisiSurum _isAkisiSurum;
		private IsAkisiSurum _nullIsAkisiSurum;

		[SetUp]
		public void TestOncesi()
		{
			_veriAdaptoru = Substitute.For<IIsAkisiYonetimVeriAdaptoru>();
			_yonetici = new IsAkisiYoneticisi(_veriAdaptoru);
			_ikiIsAkisiJSONString = "[{'Ad':'Teklif Talep','Aciklama':'Acentelerin teklif talepleri için kullanılır','OlusturmaTarihi':'2013-10-28T00:00:00'},{'Ad':'Otorizasyon Talep','Aciklama':'Acentelerin otorizasyon talepleri için kullanılır','OlusturmaTarihi':'2013-11-20T00:00:00'}]".Replace("'", "\"");
			_ikiIsAkisiIcerenListe = new List<IsAkisi> {new IsAkisi("Teklif Talep") {Aciklama = "Acentelerin teklif talepleri için kullanılır", OlusturmaTarihi = new DateTime(2013, 10, 28)}, new IsAkisi("Otorizasyon Talep") {Aciklama = "Acentelerin otorizasyon talepleri için kullanılır", OlusturmaTarihi = new DateTime(2013, 11, 20)}};
			_ikiIsAkisSurumuJSONString = "[{'Surum':'v1','Aciklama':'açıklama','OlusturmaTarihi':'2013-10-28T00:00:00','UygulamaTarihi':'2013-10-29T00:00:00','IptalTarihi':'2013-10-30T00:00:00'},{'Surum':'v2','Aciklama':'açıklama2','OlusturmaTarihi':'2013-11-28T00:00:00','UygulamaTarihi':'2013-11-29T00:00:00','IptalTarihi':'2013-11-30T00:00:00'}]".Replace("'", "\"");
			_ikiSurumIcerenListe = new List<IsAkisiSurum> {new IsAkisiSurum("v1") {Aciklama = "açıklama", OlusturmaTarihi = new DateTime(2013, 10, 28), UygulamaTarihi = new DateTime(2013, 10, 29), IptalTarihi = new DateTime(2013, 10, 30)}, new IsAkisiSurum("v2") {Aciklama = "açıklama2", OlusturmaTarihi = new DateTime(2013, 11, 28), UygulamaTarihi = new DateTime(2013, 11, 29), IptalTarihi = new DateTime(2013, 11, 30)}};
			_sifirSurumIcerenListe = new List<IsAkisiSurum>();
			_ikiSurumIcerenListedeOlanSurumAdi = "v1";
			_isAkisiAd1 = " asdf asdf qqqq";
			_isAkisiAd2 = " ag asfvzxb asgfas df";
			_surum = "g qhyqg afgsdf";
			_aciklama = "kokltnvsmö gasndfmög n";
			_tarih1 = DateTime.Now;
			_tarih2 = DateTime.Now;
			_int1 = 4211;
			_isAkisi = new IsAkisi("iş akış ad") {Aciklama = " aqfas dfas fasdf", OlusturmaTarihi = new DateTime(2013, 8, 7)};
			_nullIsAkisi = null;
			_isAkisiSurum = new IsAkisiSurum("v1");
			_nullIsAkisiSurum = null;
		}
		
		//mevcut iş akışlarının listesi görülmek istendiğinde iş akışlarının listesi json formatında gelir
		[Test]
		public void IkiIsAkisiVar_IsAkislarininListesiIsteniyor_IkiAkisiIcerenJSONDonmeli()
		{
			//given
			_veriAdaptoru.TumIsAkislariniAl().Returns(_ikiIsAkisiIcerenListe);
			//when
			var donus = _yonetici.TumIsAkislariniAl();
			//then
			Assert.That(donus, Is.EqualTo(_ikiIsAkisiJSONString));
		}

		//instance'ı bulunan bir iş akışının kaç instance'ı olduğu bilgisinin sorgulanması
		[Test]
		public void BirIsAkisininXInstanceVar_IsAkisininInstanceAdediSorgulandiginda_XDonmeli()
		{
			//given
			_veriAdaptoru.IsAkisiInstanceAdediAl(_isAkisiAd1).Returns(_int1);
			//when
			var donus = _yonetici.IsAkisiInstanceAdediAl(_isAkisiAd1);
			//then
			Assert.That(donus, Is.EqualTo(_int1));
		}

		//varolan ve bir iş akışının silinmesi
		[Test]
		public void IkiIsAkisiVar_VarolanBirIsAkisiSilinmekIstendiginde_TrueDonmeli()
		{
			//given
			_veriAdaptoru.IsAkisAl(_isAkisiAd1).Returns(_isAkisi);
			//when
			var donus = _yonetici.IsAkisSil(_isAkisiAd1);
			//then
			Assert.That(donus.Basarili, Is.True);
		}

		//varolmayan bir iş akışının silinmesi
		[Test]
		public void IkiIsAkisiVar_VarolmayanIsimliBirIsAkisiSilinmekIstendiginde_FalseDonmeli()
		{
			//given
			_veriAdaptoru.IsAkisAl(_isAkisiAd1).Returns(_nullIsAkisi);
			//when
			var donus = _yonetici.IsAkisSil(_isAkisiAd1);
			//then
			Assert.That(donus.Basarili, Is.False);
		}

		//mevcut bir iş akışının alınması
		[Test]
		public void IkiIsAkisiVar_VarolanIsimliBirIsAkisiAlinmakIstendiginde_IsAkisiDoner()
		{
			//given
			_veriAdaptoru.IsAkisAl(_isAkisiAd1).Returns(_isAkisi);
			//when
			var donus = _yonetici.IsAkisAl(_isAkisiAd1);
			//then
			Assert.That(donus, Is.EqualTo(_isAkisi));
		}

		//bir iş akışının adının değiştirilmesi - yeni ad müsait
		[Test]
		public void YeniIsAkisiAdiIleBaskaBirIsAkisiYok_VerolanBirIsAkisininAdiDegistirilmekIstendiginde_TrueDonmeliVeAdaptorunYenidenAdlandirMetoduCagirilmali()
		{
			//given
			_veriAdaptoru.IsAkisAl(_isAkisiAd2).Returns(_nullIsAkisi);
			_veriAdaptoru.IsAkisAl(_isAkisiAd1).Returns(_isAkisi);
			//when
			var donus = _yonetici.IsAkisiYenidenAdlandir(_isAkisiAd1, _isAkisiAd2);
			//then
			Assert.That(donus.Basarili, Is.True);
			_veriAdaptoru.Received(1).IsAkisYenidenAdlandir(_isAkisiAd1, _isAkisiAd2);
		}

		//bir iş akışının adının değiştirilmesi - yeni ad müsait değil
		[Test]
		public void YeniIsAkisiAdiIleBaskaBirIsAkisiVar_VerolanBirIsAkisininAdiDegistirilmekIstendiginde_FalseDonmeliVeAdaptorunHicBirMetoduCagirilmamali()
		{
			//given
			_veriAdaptoru.IsAkisAl(_isAkisiAd2).Returns(_isAkisi);
			//when
			var donus = _yonetici.IsAkisiYenidenAdlandir(_isAkisiAd1, _isAkisiAd2);
			//then
			Assert.That(donus.Basarili, Is.False);
			_veriAdaptoru.DidNotReceiveWithAnyArgs().IsAkisYenidenAdlandir(_isAkisiAd1, _isAkisiAd2);
		}

		//bir iş akışının tüm sürümlerinin görüntülenmesi
		[Test]
		public void IsAkisininIkiSurumuVar_SurumlerinListesiIsteniyor_IkiSurumunBilgileriJSONFormatindaDonmeli()
		{
			//given
			_veriAdaptoru.TumIsAkisSurumleriniAl(_isAkisiAd1).Returns(_ikiSurumIcerenListe);
			//when
			var donus = _yonetici.IsAkisSurumleriniAl(_isAkisiAd1);
			//then
			Assert.That(donus, Is.EqualTo(_ikiIsAkisSurumuJSONString));
		}

		//bir iş akışına yeni sürüm ekleme - sürüm adı mevcut değil
		[Test]
		public void IkiSurumIcerenBirIsAkisina_VarolmayanBirSurumAdiIleSurumEklenmekIstendiginde_TrueDon()
		{
			//given
			_veriAdaptoru.TumIsAkisSurumleriniAl(_isAkisiAd1).Returns(_ikiSurumIcerenListe);
			//when
			var donus = _yonetici.IsAkisSurumEkle(_isAkisiAd1, _surum, _aciklama, _tarih1);
			//then
			Assert.That(donus.Basarili, Is.True);
		}

		//bir iş akışına yeni sürüm ekleme - sürüm adı mevcut
		[Test]
		public void IkiSurumIcerenBirIsAkisina_VarolanBirSurumAdiIleSurumEklenmekIstendiginde_FalseDon()
		{
			//given
			_veriAdaptoru.TumIsAkisSurumleriniAl(_isAkisiAd1).Returns(_ikiSurumIcerenListe);
			//when
			var donus = _yonetici.IsAkisSurumEkle(_isAkisiAd1, _ikiSurumIcerenListedeOlanSurumAdi, _aciklama, _tarih1);
			//then
			Assert.That(donus.Basarili, Is.False);
		}

		//instance'ı bulunan bir iş akışının sürümünün kaç instance'ı olduğu bilgisinin sorgulanması
		[Test]
		public void BirIsAkisininSurumununXInstanceVar_IsAkisininSurumununInstanceAdediSorgulandiginda_XDonmeli()
		{
			//given
			_veriAdaptoru.IsAkisSurumInstanceAdediAl(_isAkisiAd1, _surum).Returns(_int1);
			//when
			var donus = _yonetici.IsAkisSurumInstanceAdediAl(_isAkisiAd1, _surum);
			//then
			Assert.That(donus, Is.EqualTo(_int1));
		}
	
		//bir iş akışından sürüm silme - sürüm mevcut
		[Test]
		public void IkiSurumIcerenBirIsAkisindan_VerolanBirSurumSilinmekIstendiginde_TrueDonVeVeriAdaptoruSilCagir()
		{
			//given
			_veriAdaptoru.IsAkisSurumAl(_isAkisiAd1, _surum).Returns(_isAkisiSurum);
			//when
			var donus = _yonetici.IsAkisSurumSil(_isAkisiAd1, _surum);
			//then
			Assert.That(donus.Basarili, Is.True);
			_veriAdaptoru.Received(1).SurumSil(_isAkisiAd1, _surum);
		}

		//bir iş akışından sürüm silme - sürüm mevcut değil
		[Test]
		public void IkiSurumIcerenBirIsAkisindan_VerolmayanBirSurumSilinmekIstendiginde_FalseDonVeVeriAdaptoruCagirilmamali()
		{
			//given
			_veriAdaptoru.IsAkisSurumAl(_isAkisiAd1, _surum).Returns(_nullIsAkisiSurum);
			//when
			var donus = _yonetici.IsAkisSurumSil(_isAkisiAd1, _surum);
			//then
			Assert.That(donus.Basarili, Is.False);
			_veriAdaptoru.DidNotReceive().SurumSil(_isAkisiAd1, _surum);
		}
		
		//bir iş akışının tarihlerinin değiştirilmesi - tarihler ile çakışan başka bir sürüm yok
		[Test]
		public void DegistirilmekIstenenTarihlerleCakisanBaskaBirSurumYokken_BirSurumunTarihleriDegistirildiginde_TrueDonmeliVeVeriAdaptorununTarihGuncelleMetoduCagirilmali()
		{
			//given
			_veriAdaptoru.IsAkisSurumAl(_isAkisiAd1, _tarih1, _tarih2).Returns(_sifirSurumIcerenListe);
			//when
			var donus = _yonetici.IsAkisSurumTarihleriGuncelle(_isAkisiAd1, _surum, _tarih1, _tarih2);
			//then
			Assert.That(donus.Basarili, Is.True);
			_veriAdaptoru.Received(1).SurumTarihGuncelle(_isAkisiAd1, _surum, _tarih1, _tarih2);
		}

		//bir iş akışının tarihlerinin değiştirilmesi - tarihler ile çakışan başka bir sürüm var
		//bir iş akışının tarihlerinin değiştirilmesi - tarihler ile çakışan başka bir sürüm yok
		[Test]
		public void DegistirilmekIstenenTarihlerleCakisanBaskaBirSurumVarken_BirSurumunTarihleriDegistirildiginde_FalseDonmeliVeVeriAdaptorununTarihGuncelleMetoduCagirilmamali()
		{
			//given
			_veriAdaptoru.IsAkisSurumAl(_isAkisiAd1, _tarih1, _tarih2).Returns(_ikiSurumIcerenListe);
			//when
			var donus = _yonetici.IsAkisSurumTarihleriGuncelle(_isAkisiAd1, _surum, _tarih1, _tarih2);
			//then
			Assert.That(donus.Basarili, Is.False);
			_veriAdaptoru.DidNotReceive().SurumTarihGuncelle(_isAkisiAd1, _surum, _tarih1, _tarih2);
		}
	}

	public class IsAkisiYoneticisi
	{
		private readonly IIsAkisiYonetimVeriAdaptoru _veriAdaptoru;

		public IsAkisiYoneticisi(IIsAkisiYonetimVeriAdaptoru veriAdaptoru)
		{
			_veriAdaptoru = veriAdaptoru;
		}

		public string TumIsAkislariniAl()
		{
			return JsonConvert.SerializeObject(_veriAdaptoru.TumIsAkislariniAl());
		}

		public int IsAkisiInstanceAdediAl(string isAkisAd)
		{
			return _veriAdaptoru.IsAkisiInstanceAdediAl(isAkisAd);
		}

		public IstekSonuc IsAkisSil(string isAkisAd)
		{
			if (IsAkisAl(isAkisAd) == null)
				return IstekSonuc.Hata(isAkisAd + " ismi ile tanımlı bir iş akışı bulunamadı");
			_veriAdaptoru.IsAkisiSil(isAkisAd);
			return IstekSonuc.Basari();
		}

		public IsAkisi IsAkisAl(string isAkisiAdi)
		{
			return _veriAdaptoru.IsAkisAl(isAkisiAdi);
		}

		public IstekSonuc IsAkisiYenidenAdlandir(string eskiAd, string yeniAd)
		{
			if (IsAkisAl(yeniAd) != null)
				return IstekSonuc.Hata(yeniAd + " ismi ile tanımlı bir iş akışı var");
			if (IsAkisAl(eskiAd) == null)
				return IstekSonuc.Hata(yeniAd + " ismi ile tanımlı bir iş akışı bulunamadı");
			_veriAdaptoru.IsAkisYenidenAdlandir(eskiAd, yeniAd);
			return IstekSonuc.Basari();
		}

		public string IsAkisSurumleriniAl(string isAkisAd)
		{
			return JsonConvert.SerializeObject(_veriAdaptoru.TumIsAkisSurumleriniAl(isAkisAd));
		}

		public IstekSonuc IsAkisSurumEkle(string isAkisAd, string surum, string aciklama, DateTime olusturmaTarihi)
		{
			if (_veriAdaptoru.TumIsAkisSurumleriniAl(isAkisAd).Contains(new IsAkisiSurum(surum)))
				return IstekSonuc.Hata(surum + " ismi ile tanımlı bir sürüm var");
			_veriAdaptoru.SurumEkle(isAkisAd, surum, aciklama, olusturmaTarihi);
			return IstekSonuc.Basari();
		}

		public IstekSonuc IsAkisSurumSil(string isAkisAd, string surum)
		{
			if (_veriAdaptoru.IsAkisSurumAl(isAkisAd, surum) == null)
				return IstekSonuc.Hata(surum + " adı ile tanımlı bir sürüm bulunamadı");
			_veriAdaptoru.SurumSil(isAkisAd, surum);
			return IstekSonuc.Basari();
		}

		public int IsAkisSurumInstanceAdediAl(string isAkisAd, string surum)
		{
			return _veriAdaptoru.IsAkisSurumInstanceAdediAl(isAkisAd, surum);
		}

		public IstekSonuc IsAkisSurumTarihleriGuncelle(string isAkisiAd1, string surum, DateTime uygulamaTarihi, DateTime iptalTarihi)
		{
			List<IsAkisiSurum> surumler = _veriAdaptoru.IsAkisSurumAl(isAkisiAd1, uygulamaTarihi, iptalTarihi);
			if (surumler.Count > 0)
				return IstekSonuc.Hata("Verilen tarih aralığında geçerli olan başka sürümler var (" + surumler.Aggregate("", (current, surumNesnesi) => current + (surumNesnesi.Surum + ", ")).TrimEnd(new[] {',', ' '}) + ")");
			_veriAdaptoru.SurumTarihGuncelle(isAkisiAd1, surum, uygulamaTarihi, iptalTarihi);
			return IstekSonuc.Basari();
		}
	}

	public interface IIsAkisiYonetimVeriAdaptoru
	{
		List<IsAkisi> TumIsAkislariniAl();
		void IsAkisiSil(string isAkisAd);
		int IsAkisiInstanceAdediAl(string isAkisAd);
		IsAkisi IsAkisAl(string isAkisAd);
		void IsAkisYenidenAdlandir(string eskiAd, string yeniAd);
		List<IsAkisiSurum> TumIsAkisSurumleriniAl(string isAkisAd);
		void SurumEkle(string isAkisAd, string surum, string aciklama, DateTime olusturmaTarihi);
		IsAkisiSurum IsAkisSurumAl(string isAkisiAd1, string surum);
		List<IsAkisiSurum> IsAkisSurumAl(string isAkisiAd1, DateTime uygulamaTarihi, DateTime iptalTarihi);
		void SurumSil(string isAkisAd, string surum);
		int IsAkisSurumInstanceAdediAl(string isAkisAd, string surum);
		void SurumTarihGuncelle(string isAkisAd, string surum, DateTime uygulamaTarihi, DateTime iptalTarihi);
	}

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
// ReSharper restore InconsistentNaming