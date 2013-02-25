﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Domain;

namespace Nest
{
	public interface IMultiGetHit<out T> where T : class
	{
		 string Index { get; }

		 bool Exists { get; }

		 string Type { get; }

		 string Version { get; }

		 string Id { get; }
	}

    [JsonObject]
    public class MultiGetHit<T> : IMultiGetHit<T>
		where T : class
    {
		//[JsonProperty(PropertyName = "fields")]
		public FieldSelection<T> FieldSelection { get; internal set; }
		
		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }
		
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }
		
		[JsonProperty(PropertyName = "exists")]
		public bool Exists{ get; internal set; }
		
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }
		
		[JsonProperty(PropertyName = "_version")]
		public string Version { get; internal set; }
		
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }
    }
}
