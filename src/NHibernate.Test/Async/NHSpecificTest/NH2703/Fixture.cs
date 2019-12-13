﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH2703
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		Parent RootElement = null;

		protected override bool AppliesTo(Dialect.Dialect dialect)
		{
			return TestDialect.SupportsEmptyInsertsOrHasNonIdentityNativeGenerator;
		}

		protected override void OnSetUp()
		{
			using (ISession session = Sfi.OpenSession())
			{
				var parent = new Parent();
				parent.A.Add(new A() { PropA = "Child" });
				parent.B.Add(new B() { PropB = "Child" });
				parent.C.Add(new C() { PropC = "Child" });
				session.Persist(parent);
				session.Flush();
			}
		}

		protected override void OnTearDown()
		{
			using (ISession session = Sfi.OpenSession())
			{
				session.CreateQuery("delete from A").ExecuteUpdate();
				session.CreateQuery("delete from B").ExecuteUpdate();
				session.CreateQuery("delete from C").ExecuteUpdate();
				session.CreateQuery("delete from Parent").ExecuteUpdate();
				session.Flush();
			}
			base.OnTearDown();
		}

		[Test]
		public async Task CanOuterJoinMultipleTablesWithSimpleWithClauseAsync()
		{
			using (ISession session = Sfi.OpenSession())
			{
				IQueryOver<Parent, Parent> query = session.QueryOver(() => RootElement);

				A A_Alias = null;
				B B_Alias = null;
				C C_Alias = null;
				query.Left.JoinQueryOver(parent => parent.C, () => C_Alias, c => c.PropC == A_Alias.PropA);
				query.Left.JoinQueryOver(parent => parent.A, () => A_Alias);
				query.Left.JoinQueryOver(parent => parent.B, () => B_Alias, b => b.PropB == C_Alias.PropC);
				// Expected join order: a --> c --> b

				// This query should not throw
				await (query.ListAsync());
			}
		}

		[Test]
		public async Task CanOuterJoinMultipleTablesWithComplexWithClauseAsync()
		{
			using (ISession session = Sfi.OpenSession())
			{
				IQueryOver<Parent, Parent> query = session.QueryOver(() => RootElement);

				A A_Alias = null;
				B B_Alias = null;
				C C_Alias = null;
				query.Left.JoinQueryOver(parent => parent.C, () => C_Alias, c => c.PropC == A_Alias.PropA && c.PropC == B_Alias.PropB);
				query.Left.JoinQueryOver(parent => parent.A, () => A_Alias);
				query.Left.JoinQueryOver(parent => parent.B, () => B_Alias, b => b.PropB == A_Alias.PropA);
				// Expected join order: a --> b --> c

				// This query should not throw
				await (query.ListAsync());
			}
		}
	}
}
