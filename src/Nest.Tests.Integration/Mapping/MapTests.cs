﻿using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class MapTests : IntegrationTests
	{
		private void TestMapping(RootObjectMapping typeMapping)
		{
			Assert.NotNull(typeMapping);
			Assert.AreEqual("string", typeMapping.Properties["content"].Type);
			Assert.AreEqual("string", typeMapping.Properties["country"].Type);
			Assert.AreEqual("double", typeMapping.Properties["doubleValue"].Type);
			Assert.AreEqual("long", typeMapping.Properties["longValue"].Type);
			Assert.AreEqual("boolean", typeMapping.Properties["boolValue"].Type);
			Assert.AreEqual("integer", typeMapping.Properties["intValues"].Type);
			Assert.AreEqual("float", typeMapping.Properties["floatValues"].Type);
			Assert.AreEqual("multi_field", typeMapping.Properties["name"].Type);
			Assert.AreEqual("date", typeMapping.Properties["startedOn"].Type);
			Assert.AreEqual("long", typeMapping.Properties["stupidIntIWantAsLong"].Type);
			Assert.AreEqual("float", typeMapping.Properties["floatValue"].Type);
			Assert.AreEqual("integer", typeMapping.Properties["id"].Type);
			Assert.AreEqual("multi_field", typeMapping.Properties["loc"].Type);
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			Assert.AreEqual(FieldIndexOption.not_analyzed, mapping.Index);
			//Assert.AreEqual("elasticsearchprojects", typeMapping.Parent.Type);

			Assert.AreEqual("geo_point", typeMapping.Properties["origin"].Type);

			//Assert.IsTrue(typeMapping.Properties["origin"].Dynamic);
			//Assert.AreEqual("double", typeMapping.Properties["origin"].Properties["lat"].Type);
			//Assert.AreEqual("double", typeMapping.Properties["origin"].Properties["lon"].Type);

			//Assert.IsTrue(typeMapping.Properties["followers"].Dynamic);
			//Assert.AreEqual("long", typeMapping.Properties["followers"].Properties["age"].Type);
			//Assert.AreEqual("date", typeMapping.Properties["followers"].Properties["dateOfBirth"].Type);
			//Assert.AreEqual("string", typeMapping.Properties["followers"].Properties["email"].Type);
			//Assert.AreEqual("string", typeMapping.Properties["followers"].Properties["firstName"].Type);
			//Assert.AreEqual("long", typeMapping.Properties["followers"].Properties["id"].Type);
			//Assert.AreEqual("string", typeMapping.Properties["followers"].Properties["lastName"].Type);

			//Assert.IsTrue(typeMapping.Properties["followers"].Properties["placeOfBirth"].Dynamic);
			//Assert.AreEqual("double", typeMapping.Properties["followers"].Properties["placeOfBirth"].Properties["lat"].Type);
			//Assert.AreEqual("double", typeMapping.Properties["followers"].Properties["placeOfBirth"].Properties["lon"].Type);
		}

		[Test]
		public void SimpleMapByAttributes()
		{
			this._client.DeleteMapping<ElasticSearchProject>();
			this._client.DeleteMapping<ElasticSearchProject>(ElasticsearchConfiguration.DefaultIndex + "_clone");
			var x = this._client.MapFromAttributes<ElasticSearchProject>();
			Assert.IsTrue(x.OK);

			var typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects");
			TestMapping(typeMapping);
		}




		[Test]
		public void SimpleMapByAttributesUsingType()
		{
			var t = typeof(ElasticSearchProject);
			this._client.DeleteMapping(t);
			this._client.DeleteMapping(t, ElasticsearchConfiguration.DefaultIndex + "_clone");
			var x = this._client.MapFromAttributes(t);
			Assert.IsTrue(x.OK);

			var typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects");
			TestMapping(typeMapping);
		}

		[Test]
		public void GetMapping()
		{
			var typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects");
			this.TestMapping(typeMapping);
		}

		[Test]
		public void GetMappingOnNonExistingIndexType()
		{
			Assert.DoesNotThrow(() =>
			{
				var typeMapping = this._client.GetMapping("asfasfasfasfasf", "asdasdasdasdasdasdasdasd");
				Assert.Null(typeMapping);
			});

		}

		[Test]
		public void DynamicMap()
		{
			var typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects");
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			mapping.Boost = 3;
			typeMapping.Name = "elasticsearchprojects2";
			this._client.Map(typeMapping, ElasticsearchConfiguration.DefaultIndex + "_clone");

			typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex + "_clone",
												  "elasticsearchprojects2");
			var countryMapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(countryMapping);
			Assert.AreEqual(3, countryMapping.Boost);
		}
	}
}
