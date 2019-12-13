﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Mapping;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH2293
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : TestCase
	{
		protected override string[] Mappings => System.Array.Empty<string>();

		[Test]
		public void WhenQueryHasJustAfromThenThrowQuerySyntaxExceptionAsync()
		{
			using (var session = OpenSession())
			{
				Assert.That(() => session.CreateQuery("from").ListAsync(), Throws.TypeOf<QuerySyntaxException>());
			}
		}
	}
}
