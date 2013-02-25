﻿using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class ConstantScoreQueryJson
	{
		[Test]
		public void ConstantScoreQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(q=>q
					.ConstantScore(cs=>cs
						.Query(qq=>qq.MatchAll())
						.Boost(1.2)
					)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						constant_score : { 
							query : { match_all : {} },
							boost: 1.2
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void ConstantScoreQueryWithFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(q => q
					.ConstantScore(cs => cs
						.Filter(qq => qq.MatchAll())
						.Boost(1.2)
					)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						constant_score : { 
							filter : { match_all : {} },
							boost: 1.2
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
