﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Xml;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Engine;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3518
{
	using System.Threading.Tasks;
	[TestFixture]
	public class XmlColumnTestAsync : BugTestCase
	{
		protected override void OnTearDown()
		{
			using (var session = Sfi.OpenSession())
			using (var t = session.BeginTransaction())
			{
				session.Delete("from ClassWithXmlMember");
				t.Commit();
			}
		}

		protected override bool AppliesTo(Dialect.Dialect dialect)
		{
			return dialect is MsSql2005Dialect;
		}

		protected override bool AppliesTo(ISessionFactoryImplementor factory)
		{
			return factory.ConnectionProvider.Driver is SqlClientDriver;
		}

		protected override void Configure(Configuration configuration)
		{
			configuration.SetProperty(Environment.PrepareSql, "true");
		}

		[Test]
		public async Task FilteredQueryAsync()
		{
			var xmlDocument = new XmlDocument();
			var xmlElement = xmlDocument.CreateElement("testXml");
			xmlDocument.AppendChild(xmlElement);
			var parentA = new ClassWithXmlMember("A", xmlDocument);

			using (var s = Sfi.OpenSession())
			using (var t = s.BeginTransaction())
			{
				await (s.SaveAsync(parentA));
				await (t.CommitAsync());
			}
		}
	}
}
