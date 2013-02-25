﻿using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration.Query
{
	/// <summary>
	/// Integrated tests of NumericRangeFilter with elasticsearch.
	/// </summary>
	[TestFixture]
	public class TextPhrasePrefixQueryTests : IntegrationTests
	{
		/// <summary>
		/// Document used in test.
		/// </summary>
		private ElasticSearchProject _LookFor;

		[TestFixtureSetUp]
		public void Initialize()
		{
			_client.IsValid.Should().BeTrue();

			_LookFor = NestTestData.Session.Single<ElasticSearchProject>().Get();
			_LookFor.Name = "one two three four";
			var status = this._client.Index(_LookFor, new IndexParameters { Refresh = true }).ConnectionStatus;
			Assert.True(status.Success, status.Result);
		}


		/// <summary>
		/// Test control. If this test fail, the problem not in TextPhrasePrefixQuery.
		/// </summary>
		[Test]
		public void TestControl()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());

			var results = this._client.Search<ElasticSearchProject>(
				s => s.Filter(filterId && filterId)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(1, results.Total);
		}

		/// <summary>
		/// Set of query that should not filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestNotFiltered()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());
			var querySlop0 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one two")
					.Slop(0));
			var querySlop1 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one three")
					.Slop(1));
			var queryPrefix1 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one t")
					.Slop(0)
					.PrefixLength(1));
			var queryPrefix2 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one tw")
					.Slop(0)
					.PrefixLength(2));

			var results = this._client.Search<ElasticSearchProject>(
				s => s.Filter(filterId)
					.Query(querySlop0 && querySlop1 && queryPrefix1 && queryPrefix2)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(1, results.Total);
		}

		/// <summary>
		/// Set of query that should filter de documento _LookFor.
		/// </summary>
		[Test]
		public void TestFiltered()
		{
			var id = _LookFor.Id;
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, id.ToString());
			var querySlop0 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one three")
					.Slop(0));
			var querySlop1 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one four")
					.Slop(1));
			var queryPrefix2 = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one th")
					.Slop(0)
					.PrefixLength(2));
			var queryFail = Query<ElasticSearchProject>.TextPhrasePrefix(
				textPhrasePrefix => textPhrasePrefix
					.OnField(p => p.Name)
					.QueryString("one fail"));

			var results = this._client.Search<ElasticSearchProject>(
				s => s.Filter(filterId)
					.Query(querySlop0 || querySlop1 || queryPrefix2 || queryFail)
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(0, results.Total);
		}
	}
}
