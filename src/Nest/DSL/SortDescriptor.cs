﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest.DSL.Descriptors
{
  public class SortDescriptor<T> where T : class
  {
    internal string _Field { get; set; }

    [JsonProperty("missing")]
    internal string _Missing { get; set; }

    [JsonProperty("order")]
    internal string _Order { get; set; }

    public virtual SortDescriptor<T> OnField(string field)
    {
      this._Field = field;
      return this;
    }

    public virtual SortDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
        var resolver = new PropertyNameResolver();

        var fieldName = resolver.Resolve(objectPath);

        var fieldAttributes = resolver.ResolvePropertyAttributes(objectPath);
        if ((fieldAttributes.Where(x => x.AddSortField == true)).Any())
        {
            fieldName += ".sort";
        }

        return this.OnField(fieldName);
    }

    public virtual SortDescriptor<T> MissingLast()
    {
      this._Missing = "_last";
      return this;
    }
    public virtual SortDescriptor<T> MissingFirst()
    {
      this._Missing = "_first";
      return this;
    }
    public virtual SortDescriptor<T> MissingValue(string value)
    {
      this._Missing = value;
      return this;
    }
    public virtual SortDescriptor<T> Ascending()
    {
      this._Order = "asc";
      return this;
    }
    public virtual SortDescriptor<T> Descending()
    {
      this._Order = "desc";
      return this;
    }
    /// <summary>
    /// Pass true to sort ascending false to sort descending
    /// </summary>
    public virtual SortDescriptor<T> ToggleSort(bool ascending)
    {
      this._Order = ascending ? "asc" : "desc";
      return this;
    }
  }
}
