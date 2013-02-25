﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Domain;

namespace Nest
{
	public class MultiSearchDescriptor
	{
		internal string _FixedIndex { get; set; }
		internal string _FixedType { get; set; }

		internal IDictionary<string, SearchDescriptorBase> _Operations = new Dictionary<string, SearchDescriptorBase>();

		public MultiSearchDescriptor Search<T>(string name, Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			name.ThrowIfNull("name");
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(name, descriptor);
			return this;
		}

		public MultiSearchDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			return this.Search(Guid.NewGuid().ToString(), searchSelector);
		}

		/// <summary>
		/// Allows you to perform the multi search on a fixed path. 
		/// Each operation that doesn't specify an index or type will use this fixed index/type
		/// over the default infered index and type.
		/// </summary>
		public MultiSearchDescriptor FixedPath(string index, string type = null)
		{
			index.ThrowIfNullOrEmpty("index");
			this._FixedIndex = index;
			this._FixedType = type;
			return this;
		}
	}
}
