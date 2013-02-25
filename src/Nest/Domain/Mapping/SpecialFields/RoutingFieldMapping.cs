﻿using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class RoutingFieldMapping
	{
		public RoutingFieldMapping()
		{

		}

		[JsonProperty("required")]
		public bool Required { get; internal set; }

		[JsonProperty("path")]
		public string Path { get; internal set; }
	}


	public class RoutingFieldMapping<T> : RoutingFieldMapping
    {
		public RoutingFieldMapping<T> SetRequired(bool required = true)
		{
			this.Required = required;
			return this;
		}
		public RoutingFieldMapping<T> SetPath(string path)
		{
			this.Path = path;
			return this;
		}
		public RoutingFieldMapping<T> SetPath(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this.Path = new PropertyNameResolver().Resolve(objectPath);
			return this;	
		}
    }
}