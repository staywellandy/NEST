﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoPolygonFilterJson
	{
		[Test]
		public void GeoPolygonFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.GeoPolygon("origin", "drn5x1g8cu2y", "30, -80", "20, -90")
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_polygon: {
						origin : {
							points: [
								""drn5x1g8cu2y"", ""30, -80"", ""20, -90""
							]
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void GeoPolygonFilterWithTuples()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.GeoPolygon(f => f.Origin, new List<Tuple<double, double>> { Tuple.Create(30.0, -80.0), Tuple.Create(20.0, -90.0) })
				);
			
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_polygon: {
						origin : {
							points: [
								""30, -80"", ""20, -90""
							]
						}
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
