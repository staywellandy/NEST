﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NestedFilterDescriptor<T> : FilterBase where T : class
	{
		[JsonProperty("score_mode"), JsonConverter(typeof(StringEnumConverter))]
		internal NestedScore? _Score { get; set; }

		[JsonProperty("query")]
		internal BaseQuery _QueryDescriptor { get; set; }

		[JsonProperty("path")]
		internal string _Path { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set; }

		internal override bool IsConditionless
		{
			get
			{
				return this._QueryDescriptor == null || this._QueryDescriptor.IsConditionless;
			}
		}

		public NestedFilterDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			this._QueryDescriptor = querySelector(q);
			return this;
		}

		public NestedFilterDescriptor<T> Score(NestedScore score)
		{
			this._Score = score;
			return this;
		}
		public NestedFilterDescriptor<T> Path(string path)
		{
			this._Path = path;
			return this;
		}
		public NestedFilterDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			return this.Path(fieldName);
		}
		public NestedFilterDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
	}
}
