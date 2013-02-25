﻿using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class ExplainTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleExplain()
		{
			var queryResults = this._client.SearchRaw<ElasticSearchProject>(
					@" {
						""explain"": true,
						""query"" : {
							""match_all"" : { }
					} }"
				);
			Assert.True(queryResults.DocumentsWithMetaData.All(h=>h.Explanation != null));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.Any()));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.All(d=>!d.Description.IsNullOrEmpty())));
		}
		[Test]
		public void ComplexExplain()
		{
			var queryResults = this._client.SearchRaw<ElasticSearchProject>(
					@" { ""explain"": true, 
						""query"" : {
						  ""fuzzy"" : { 
							""followers.firstName"" : {
								""value"" : """ + this._LookFor.ToLower() + @"x"",
								""boost"" : 1.0,
								""min_similarity"" : 0.5,
								""prefix_length"" : 0
							}
						}
					} }"
				);

			Assert.True(queryResults.DocumentsWithMetaData.All(h=>h.Explanation != null));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.Any()));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.All(d=>!d.Description.IsNullOrEmpty())));
		}

		
	}
}
;