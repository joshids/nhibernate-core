﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections;
using NHibernate.DomainModel.NHSpecific;
using NUnit.Framework;
using NExp = NHibernate.Criterion;

namespace NHibernate.Test.NHSpecificTest
{
	using System.Threading.Tasks;
	using System.Threading;
	[TestFixture]
	public class NH47FixtureAsync : TestCase
	{
		protected override string[] Mappings
		{
			get { return new string[] {"NHSpecific.UnsavedType.hbm.xml"}; }
		}

		public async Task<TimeSpan> BatchInsertAsync(object[] objs, CancellationToken cancellationToken = default(CancellationToken))
		{
			TimeSpan tspan = TimeSpan.Zero;

			if (objs != null && objs.Length > 0)
			{
				ISession s = OpenSession();
				ITransaction t = s.BeginTransaction();

				int count = objs.Length;

				Console.WriteLine();
				Console.WriteLine("Start batch insert " + count.ToString() + " objects");

				DateTime startTime = DateTime.Now;

				for (int i = 0; i < count; ++i)
				{
					await (s.SaveAsync(objs[i], cancellationToken));
				}
				await (t.CommitAsync(cancellationToken));
				s.Close();

				tspan = DateTime.Now.Subtract(startTime);

				Console.WriteLine("Finish in " + tspan.TotalMilliseconds.ToString() + " milliseconds");
			}

			return tspan;
		}

		[Test, Explicit]
		public async Task TestNH47Async()
		{
			int testCount = 100;

			object[] al = new object[testCount];

			TimeSpan tspan = TimeSpan.Zero;

			int times = 1000;

			for (int i = 0; i < times; ++i)
			{
				for (int j = 0; j < testCount; ++j)
				{
					UnsavedType ut = new UnsavedType();
					ut.Id = j + 1 + testCount * (i + 1);
					ut.TypeName = Guid.NewGuid().ToString();
					al[j] = ut;
				}

				tspan = tspan.Add(await (BatchInsertAsync(al)));
			}

			Console.WriteLine("Finish average in " + (tspan.TotalMilliseconds / times).ToString() + " milliseconds for " +
			                  times.ToString() + " times");
			Console.Read();
		}
	}
}