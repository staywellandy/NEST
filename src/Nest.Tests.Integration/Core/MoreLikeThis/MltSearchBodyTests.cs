﻿using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Integration.Core.MoreLikeThis
{
	[TestFixture]
	public class MltSearchBodyTests : IntegrationTests
	{
		[Test]
		public void SearchBodyEndsUpInPost()
		{
			var result = this._client.MoreLikeThis<ElasticSearchProject>(mlt => mlt
				.Id(1)
				.Options(o => o
					.OnFields(p => p.Country, p => p.Content)
					.MinDocumentFrequency(1)
				)
				.Search(s=>s
					.From(0)
					.Take(20)
				)
			);
			
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Total.Should().BeGreaterOrEqualTo(10);
			result.Documents.Should().NotBeEmpty();
		}
	}
}
