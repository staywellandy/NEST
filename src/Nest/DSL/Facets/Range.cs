﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class Range<T> where T : struct
  {
    [JsonProperty(PropertyName = "from")]
    internal Nullable<T> _From { get; set; }

    [JsonProperty(PropertyName = "to")]
    internal Nullable<T> _To { get; set; }

    public Range<T> From(T value)
    {
      this._From = value;
      return this;
    }
    public Range<T> To(T value)
    {
      this._To = value;
      return this;
    }
  }
}
