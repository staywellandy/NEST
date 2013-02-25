﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	/// <summary>
	/// A Query that matches documents containing a particular sequence of terms. A PhraseQuery is built by QueryParser for input like "new york".
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	public class TextPhraseQueryDescriptor<T> : TextQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "type")]
		internal override string _Type { get { return "phrase"; } }
	}
}
