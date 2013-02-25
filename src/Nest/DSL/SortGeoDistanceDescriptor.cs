﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using System.Globalization;
using Nest.Resolvers;
using Nest.Resolvers.Converters;

namespace Nest.DSL.Descriptors
{
  public class SortGeoDistanceDescriptor
  {
    internal string _Field { get; set; }

    internal string _Missing { get; set; }

    internal string _Order { get; set; }

    internal string _PinLocation { get; set; }

    internal GeoUnit? _GeoUnit { get; set; }
  }

  [JsonConverter(typeof(SortGeoDistanceDescriptorConverter))]
  public class SortGeoDistanceDescriptor<T> : SortGeoDistanceDescriptor where T : class
  {
    

    public SortGeoDistanceDescriptor<T> PinTo(string geoLocationHash)
    {
      geoLocationHash.ThrowIfNullOrEmpty("geoLocationHash");
      this._PinLocation = geoLocationHash;
      return this;
    }
    public SortGeoDistanceDescriptor<T> PinTo(double Lat, double Lon)
    {
      var c = CultureInfo.InvariantCulture;
      Lat.ThrowIfNull("Lat");
      Lon.ThrowIfNull("Lon");
      this._PinLocation = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
      return this;
    }
    public SortGeoDistanceDescriptor<T> Unit(GeoUnit unit)
    {
      unit.ThrowIfNull("unit");
      this._GeoUnit = unit;
      return this;
    }

    public SortGeoDistanceDescriptor<T> OnField(string field)
    {
      this._Field = field;
      return this;
    }
    public SortGeoDistanceDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      var fieldName = new PropertyNameResolver().Resolve(objectPath);
      return this.OnField(fieldName);
    }

    public SortGeoDistanceDescriptor<T> MissingLast()
    {
      this._Missing = "_last";
      return this;
    }
    public SortGeoDistanceDescriptor<T> MissingFirst()
    {
      this._Missing = "_first";
      return this;
    }
    public SortGeoDistanceDescriptor<T> MissingValue(string value)
    {
      this._Missing = value;
      return this;
    }
    public SortGeoDistanceDescriptor<T> Ascending()
    {
      this._Order = "asc";
      return this;
    }
    public SortGeoDistanceDescriptor<T> Descending()
    {
      this._Order = "desc";
      return this;
    }
    /// <summary>
    /// Pass true to sort ascending false to sort descending
    /// </summary>
    public SortGeoDistanceDescriptor<T> ToggleSort(bool ascending)
    {
      this._Order = ascending ? "asc" : "desc";
      return this;
    }

  }
}
