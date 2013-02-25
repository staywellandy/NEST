﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration
{
	[TestFixture]
	public class BaseTests
	{
		//static initializer to really run initialization once
		static BaseTests()
		{
			var client = CreateClient();
			var cloneIndex = Test.Default.DefaultIndex + "_clone";
			if (client.IsValid)
			{
				var projects = NestTestData.Data;
				var people = NestTestData.People;

				client.DeleteMapping<ElasticSearchProject>();
				client.DeleteMapping<ElasticSearchProject>(cloneIndex);
				client.OpenIndex<ElasticSearchProject>();
				client.OpenIndex(cloneIndex);

				ResetType<ElasticSearchProject>(client, projects);
				ResetType<Person>(client, people);
			}
		}


		[TestFixtureSetUp]
		public virtual void Initialize()
		{
			
		}

		private static IConnectionSettings _settings;
		protected static IConnectionSettings Settings
		{
			get
			{
				if (_settings != null)
					return _settings;

				_settings = new ConnectionSettings(Test.Default.Host, Test.Default.Port)
								.SetDefaultIndex(Test.Default.DefaultIndex)
								.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
								.UsePrettyResponses();

				return _settings;
			}
		}
		
		private ElasticClient _connectedClient;
		protected ElasticClient _client
		{
			get 
			{
				if (this._connectedClient != null)
					return this._connectedClient;

				var client = new ElasticClient(Settings);
				if (client.IsValid)
				{ 
					this._connectedClient = client;
					return this._connectedClient;
				}
				return null;
			}
		}
		
		protected static ElasticClient CreateClient()
		{
			return new ElasticClient(Settings);
		}

		protected virtual void ResetIndexes()
		{
			
		}

		private static void ResetType<T>(IElasticClient client, IEnumerable<T> objects) where T : class
		{
			var cloneIndex = Test.Default.DefaultIndex + "_clone";
			var bulkParameters = new SimpleBulkParameters() { Refresh = true };

			client.MapFromAttributes<T>();
			client.MapFromAttributes<T>(cloneIndex);

			client.IndexMany(objects, bulkParameters);
			client.IndexMany(objects, cloneIndex, bulkParameters);
		}

		protected void DeleteIndices()
		{
			var client = CreateClient();
			if (client.IsValid)
			{
				var cloneIndex = Test.Default.DefaultIndex + "_clone";
				client.DeleteMapping<ElasticSearchProject>();
				client.DeleteMapping<ElasticSearchProject>(cloneIndex);
			}
		}
	
		protected void BulkIndexData()
		{
			var client = CreateClient();
			if (client.IsValid)
			{
				var projects = NestTestData.Data;
				var cloneIndex = Test.Default.DefaultIndex + "_clone";
				var bulkParameters = new SimpleBulkParameters() { Refresh = true };
				client.IndexMany(projects, bulkParameters);
				client.IndexMany(projects, cloneIndex, bulkParameters);

			}
		}

		/// <summary>
		/// Execute a filter test and assert the results.
		/// </summary>
		/// <param name="project">Document to be search</param>
		/// <param name="filter">Filter to be test</param>
		/// <param name="queryMustHaveResults">If the execution of search must return results</param>
		public void DoFilterTest(Func<FilterDescriptor<ElasticSearchProject>, Nest.BaseFilter>  filter, ElasticSearchProject project, bool queryMustHaveResults)
		{
			var filterId = Filter<ElasticSearchProject>.Term(e => e.Id, project.Id.ToString());

			var results = this._client.Search<ElasticSearchProject>(
				s => s.Filter(ff => ff.And(
						f => f.Term(e => e.Id, project.Id.ToString()),
						filter
					))
				);

			Assert.True(results.IsValid, results.ConnectionStatus.Result);
			Assert.True(results.ConnectionStatus.Success, results.ConnectionStatus.Result);
			Assert.AreEqual(queryMustHaveResults ? 1 : 0, results.Total);
		}
	}
}
