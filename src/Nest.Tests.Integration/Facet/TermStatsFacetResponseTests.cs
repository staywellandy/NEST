﻿using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Facet
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class TermStatsFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleTermStatsFacet()
		{
			var queryResults = this._client.SearchRaw<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""nameStats"" : 
						{ 
							""terms_stats"" : {
								""key_field"" : ""name"",
								""value_field"" : ""loc""
							}
						}
					}
				}"
			);

		    var facet = queryResults.Facets["nameStats"];
			this.TestDefaultAssertions(queryResults);

            Assert.IsInstanceOf<TermStatsFacet>(facet);

		    var tsf = (TermStatsFacet) facet;
            
            Assert.GreaterOrEqual(tsf.Missing, 0);

		    foreach (var term in tsf.Items)
		    {
                Assert.Greater(term.Count, 0);
                Assert.Greater(term.Total, 0);
                Assert.Greater(term.Min, 0);
                Assert.Greater(term.Max, 0);
                Assert.Greater(term.Mean, 0);
                Assert.Greater(term.TotalCount, 0);
                Assert.IsNotNullOrEmpty(term.Term);
		    }
		}
	}
}
