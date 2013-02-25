﻿using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanNotQueryJson
	{
		[Test]
		public void SpanNotQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanNot(sf=>sf
						.Include(e =>e.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1))
						.Exclude(e=>e.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1))
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_not: { 
					include: { 
						span_term: { 
							name: {
								value: ""elasticsearch.pm"",
								boost: 1.1
							}
						}
					},
					exclude: { 
						span_term: { 
							name: {
								value: ""elasticsearch.pm"",
								boost: 1.1
							}
						}
					}
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
