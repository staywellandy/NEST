﻿using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Integration.Query
{
	[TestFixture]
	public class TermQueryDynamic : IntegrationTests
	{
		[Test]
		public void TestTermQuery()
		{
			this._client.Search<ElasticSearchProject>(s=>s
			                            .AllIndices()
			                            .Query(q=>q.Term(f=>f.Name, ""))
			                            );
			var results = this._client.Search(s=>s
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.Type("elasticsearchprojects")
				.From(0)
				.Size(10)
				.Query(q => q
					.Term("name", "elasticsearch.pm")
				)
			);

			Assert.True(results.IsValid);
			Assert.Greater(results.Documents.Count(), 0);
			var first = results.Documents.First();
			Assert.IsNotNullOrEmpty((string)first.followers[0].firstName);
		}
	}
}
