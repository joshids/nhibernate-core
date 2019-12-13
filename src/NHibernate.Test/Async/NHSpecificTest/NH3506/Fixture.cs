﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.SqlCommand;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3506
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		private Department _department;

		private Employee _employee1;

		private Employee _employee2;

		protected override void OnSetUp()
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				_department = new Department();

				_employee1 = new Employee
				{
					Department = _department
				};

				_employee2 = new Employee();

				session.Save(_department);
				session.Save(_employee1);
				session.Save(_employee2);

				tx.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				session.Delete(_department);
				session.Delete(_employee1);
				session.Delete(_employee2);

				tx.Commit();
			}
		}

		[Theory]
		public async Task Querying_Employees_LeftJoining_On_Departments_Should_Return_All_Employees_HQLAsync(bool enableFilter)
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				if (enableFilter)
					session.EnableFilter("ValidAtFilter").SetParameter("CurrentDate", DateTime.UtcNow);

				var employees = await (session.CreateQuery("select e from Employee e left join e.Department d")
					.ListAsync<Employee>());

				Assert.That(employees.Count, Is.EqualTo(2));

				await (tx.CommitAsync());
			}
		}

		[Theory]
		public async Task Querying_Employees_LeftJoining_On_Departments_Should_Return_All_Employees_ICriteriaAsync(bool enableFilter)
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				if (enableFilter)
					session.EnableFilter("ValidAtFilter").SetParameter("CurrentDate", DateTime.UtcNow);

				var employees = await (session.CreateCriteria<Employee>()
					.CreateCriteria("Department", JoinType.LeftOuterJoin)
					.ListAsync<Employee>());

				Assert.That(employees.Count, Is.EqualTo(2));

				await (tx.CommitAsync());
			}
		}

		[Theory]
		public async Task Querying_Employees_LeftJoining_On_Departments_Should_Return_All_Employees_QueryOverAsync(bool enableFilter)
		{
			using (var session = OpenSession())
			using (var tx = session.BeginTransaction())
			{
				if (enableFilter)
					session.EnableFilter("ValidAtFilter").SetParameter("CurrentDate", DateTime.UtcNow);

				var employees = await (session.QueryOver<Employee>()
					.JoinQueryOver(x => x.Department, JoinType.LeftOuterJoin)
					.ListAsync());

				Assert.That(employees.Count, Is.EqualTo(2));

				await (tx.CommitAsync());
			}
		}
	}
}
