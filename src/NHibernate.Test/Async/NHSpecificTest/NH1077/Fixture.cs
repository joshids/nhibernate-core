﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NHibernate.Dialect;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1077
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override bool AppliesTo(Dialect.Dialect dialect)
		{
			// Specific to MsSql2000Dialect and MsSql2005Dialect
			return dialect is MsSql2000Dialect;
		}

		[Test]
		public async Task LokingAsync()
		{
			object savedId;
			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				A a = new A("hunabKu");
				savedId = await (s.SaveAsync(a));
				await (t.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				A a = await (s.GetAsync<A>(savedId));
				using (SqlLogSpy sqlLogSpy = new SqlLogSpy())
				{
					await (s.LockAsync(a, LockMode.Upgrade));
					string sql = sqlLogSpy.Appender.GetEvents()[0].RenderedMessage;
					Assert.Less(0, sql.IndexOf("with (updlock"));
				}
				await (t.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction t = s.BeginTransaction())
			{
				await (s.DeleteAsync("from A"));
				await (t.CommitAsync());
			}
		}
	}
}