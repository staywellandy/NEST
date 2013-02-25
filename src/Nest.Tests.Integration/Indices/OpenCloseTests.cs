﻿using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class OpenCloseTests : IntegrationTests
	{
		[Test]
		public void CloseAndOpenIndex()
		{
			var r = this._client.CloseIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this._client.OpenIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndOpenIndexTyped()
		{
			var r = this._client.CloseIndex<ElasticSearchProject>();
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this._client.OpenIndex<ElasticSearchProject>();
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndSearchAndOpenIndex()
		{
			var r = this._client.CloseIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			var results = this._client.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						    ""match_all"" : { }
				} }"
			);
			Assert.False(results.IsValid);
			Assert.IsNotNull(results.ConnectionStatus.Error);
			Assert.True(results.ConnectionStatus.Error.HttpStatusCode == System.Net.HttpStatusCode.Forbidden, results.ConnectionStatus.Error.HttpStatusCode.ToString());
			Assert.True(results.ConnectionStatus.Error.ExceptionMessage.Contains("ClusterBlockException"));
			Assert.True(results.ConnectionStatus.Error.ExceptionMessage.Contains("index closed"));
			r = this._client.OpenIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.OK);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
	}
}