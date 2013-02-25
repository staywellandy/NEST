﻿using System.Collections.Generic;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class ClearCacheTest : IntegrationTests
	{
		[Test]
		public void test_clear_cache()
		{
			var client = this._client;
			var status = client.ClearCache();
			Assert.True(status.IsValid);
			Assert.True(status.OK);
		}
		[Test]
		public void test_clear_cache_specific()
		{
			var client = this._client;
			var status = client.ClearCache(ClearCacheOptions.Filter | ClearCacheOptions.Bloom);
			Assert.True(status.IsValid);
			Assert.True(status.OK);
		}
		[Test]
		public void test_clear_cache_generic_specific()
		{
			var client = this._client;
			var status = client.ClearCache<ElasticSearchProject>(ClearCacheOptions.Filter | ClearCacheOptions.Bloom);
			Assert.True(status.IsValid);
			Assert.True(status.OK);
		}
		[Test]
		public void test_clear_cache_generic_specific_indices()
		{
			var client = this._client;
			var status = client.ClearCache(new List<string> { _settings.DefaultIndex, _settings.DefaultIndex }, ClearCacheOptions.Filter | ClearCacheOptions.Bloom);
			Assert.True(status.IsValid);
			Assert.True(status.OK);
		}
	}
}