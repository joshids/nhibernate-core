﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3124
{
	using System.Threading.Tasks;
	[TestFixture]
	public class ByCodeFixtureAsync : TestCaseMappingByCode
	{
		protected override HbmMapping GetMappings()
		{
			var mapper = new ModelMapper();
			mapper.Class<Person>(ca =>
			{
				ca.Id(x => x.Id, map => map.Generator(Generators.Assigned));
				ca.Property(x => x.Name, map => map.Length(150));
				ca.Property(x => x.Type, map => map.Length(1));
			});

			return mapper.CompileMappingForAllExplicitlyAddedEntities();
		}

		protected override void OnSetUp()
		{
			using (ISession session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				session.Save(new Person {Id = 1000, Name = "Test", Type = 'A'});
				transaction.Commit();
			}
		}

		[Test]
		public async Task LinqStatementGeneratesIncorrectCastToIntegerAsync()
		{
			using (ISession session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				await (session.Query<Person>().Where(x => x.Type == 'A').ToListAsync());
				await (transaction.CommitAsync());
			}
		}

		protected override void OnTearDown()
		{
			using (ISession session = OpenSession())
			using (ITransaction transaction = session.BeginTransaction())
			{
				session.Delete("from Person");
				transaction.Commit();
			}
		}
	}
}
