﻿using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;

namespace Nest.Tests.Integration.Core.MultiSearch
{
	[TestFixture]
	public class MultiSearchTests : IntegrationTests
	{
		[Test]
		public void SimpleSearch()
		{
			var result = this._client.MultiSearch(b => b
				.Search<ElasticSearchProject>(s=>s.MatchAll())
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result._Responses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);

			var queryResponses = result.GetResponses<ElasticSearchProject>();
			queryResponses.Should().NotBeNull().And.HaveCount(1);

			var queryResponse = queryResponses.First();
			queryResponse.Documents.Should().NotBeNull()
				.And.HaveCount(10)
				.And.OnlyContain(f => !f.Name.IsNullOrEmpty());
		}

		[Test]
		public void SimpleNamedSearch()
		{
			var result = this._client.MultiSearch(b => b
				.Search<ElasticSearchProject>("elasticsearchprojects", s => s.MatchAll())
				.Search<Person>("persons", s => s.MatchAll())
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result._Responses.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);

			var queryResponse = result.GetResponse<ElasticSearchProject>("elasticsearchprojects");
			queryResponse.Documents.Should().NotBeNull()
				.And.HaveCount(10)
				.And.OnlyContain(f => !f.Name.IsNullOrEmpty());

			var personQueryResponse = result.GetResponse<Person>("persons");
			personQueryResponse.Documents.Should().NotBeNull()
				.And.HaveCount(10)
				.And.OnlyContain(f => !f.FirstName.IsNullOrEmpty());
		}


		[Test]
		public void MultipleComplexSearches()
		{
			var result = this._client.MultiSearch(b => b
				.Search<ElasticSearchProject>(s => s
					.Query(q=>q.Term(p=>p.Name, "NEST"))
					.Filter(f => f.Term(p => p.Name, "NEST"))
					.FacetTerm(tf=>tf.OnField(p=>p.Name).Global())
					.SortDescending(p=>p.LongValue)
				)
				.Search<Person>(s => s
					.Query(q => q.Term(p => p.FirstName, "Ellie") || q.Term(p => p.FirstName, "Jessica"))
					.Filter(f => f.Term(p => p.FirstName, "Jake") || f.Term(p => p.FirstName, "Lewis"))
					.FacetTerm(tf => tf.OnField(p => p.FirstName).Global())
					.SortDescending(p => p.FirstName)
				)
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.GetResponses<ElasticSearchProject>().Should().NotBeEmpty();
			result.GetResponses<Person>().Should().NotBeEmpty();
		}
		
	}
}
