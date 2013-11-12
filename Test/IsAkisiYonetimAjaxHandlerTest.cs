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
		private IIsAkisiYonetimVeriAdaptoru _veriAdaptoru;
		private List<IsAkisi> _ikiAkisIcerenListe;
		private IsAkisiYonetimAjaxHandler _ajaxHandler;
		private string _ikiIsAkisiJSONString = @"asdf asf asdf as ""  ";		
		
		[SetUp]
		public void TestOncesi()
		{
			_veriAdaptoru = Substitute.For<IIsAkisiYonetimVeriAdaptoru>();
			_ajaxHandler = new IsAkisiYonetimAjaxHandler();
			_ikiIsAkisiJSONString = "[{'Ad':'Teklif Talep','Aciklama':'Acentelerin teklif talepleri için kullanılır','OlusturmaTarihi':'2013-10-28T00:00:00'},{'Ad':'Otorizasyon Talep','Aciklama':'Acentelerin otorizasyon talepleri için kullanılır','OlusturmaTarihi':'2013-11-20T00:00:00'}]".Replace("'", "\"");
			_ikiAkisIcerenListe = new List<IsAkisi> { new IsAkisi("Teklif Talep") { Aciklama = "Acentelerin teklif talepleri için kullanılır", OlusturmaTarihi = new DateTime(2013, 10, 28) }, new IsAkisi("Otorizasyon Talep") { Aciklama = "Acentelerin otorizasyon talepleri için kullanılır", OlusturmaTarihi = new DateTime(2013, 11, 20) } };
		}
		
		//mevcut iş akışlarının listesi görülmek istendiğinde iş akışlarının listesi json formatında gelir
		[Test]
		public void UcIsAkisiVar_IsAkislarininListesiIsteniyor_UcAkisiIcerenJSONDonmeli()
		{
			//given
			_veriAdaptoru.TumIsAkislariniAl().Returns(_ikiAkisIcerenListe);
			//when
			var donus = _ajaxHandler.TumIsAkislariniAlInternal(_veriAdaptoru);
			//then
			Assert.That(donus, Is.EqualTo(_ikiIsAkisiJSONString));
		}
	}

	public class IsAkisiYonetimAjaxHandler
	{
		public string TumIsAkislariniAlInternal(IIsAkisiYonetimVeriAdaptoru veriAdaptoru)
		{
			return JsonConvert.SerializeObject(veriAdaptoru.TumIsAkislariniAl());
		}

		public string TumIsAkislariniAl()
		{
			return TumIsAkislariniAlInternal(VeriAdaptorFabrikasiYarat().IsAkisiYonetimVeriAdaptoruYarat());
		}

		private VeriAdaptoruFabrikasi VeriAdaptorFabrikasiYarat()
		{
			return null;
		}
	}

	public class IsAkisi
	{
		protected bool Equals(IsAkisi other)
		{
			return string.Equals(Ad, other.Ad);
		}

		public override int GetHashCode()
		{
			return (Ad != null ? Ad.GetHashCode() : 0);
		}

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
			return obj.GetType() == this.GetType() && Equals((IsAkisi)obj);
		}
	}
}
