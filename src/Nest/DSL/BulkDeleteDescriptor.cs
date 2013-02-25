﻿using System;
using System.Globalization;
using Nest.Resolvers;

namespace Nest
{
	public class BulkDeleteDescriptor<T> : BaseBulkOperation
		 where T : class
	{

		internal override Type _ClrType { get { return typeof(T); } }
		internal override string _Operation { get { return "delete"; } }
		internal override object _Object { get; set; }


		private readonly TypeNameResolver _typeNameResolver;

		public BulkDeleteDescriptor()
		{
			this._typeNameResolver = new TypeNameResolver();
		}

		internal override string GetIdForObject(IdResolver resolver)
		{
			if (!this._Id.IsNullOrEmpty())
				return this._Id;

			return resolver.GetIdFor<T>((T)_Object);

		}

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkDeleteDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkDeleteDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkDeleteDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull("type");
			return this.Type(this._typeNameResolver.GetTypeNameFor(type));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkDeleteDescriptor<T> Id(int id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkDeleteDescriptor<T> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// The object to infer the id off, (if id is not passed using Id())
		/// </summary>
		public BulkDeleteDescriptor<T> Object(T @object)
		{
			this._Object = @object;
			return this;
		}


		public BulkDeleteDescriptor<T> Version(string version)
		{
			this._Version = version; 
			return this;
		}

		public BulkDeleteDescriptor<T> VersionType(string versionType)
		{
			this._VersionType = versionType;
			return this;
		}

		public BulkDeleteDescriptor<T> Routing(string routing)
		{
			this._Routing = routing; 
			return this;
		}

		public BulkDeleteDescriptor<T> Percolate(string percolate)
		{
			this._Percolate = percolate; 
			return this;
		}

		public BulkDeleteDescriptor<T> Parent(string parent)
		{
			this._Parent = parent; 
			return this;
		}

		public BulkDeleteDescriptor<T> Timestamp(long timestamp)
		{
			this._Timestamp = timestamp; 
			return this;
		}

		public BulkDeleteDescriptor<T> Ttl(string ttl)
		{
			this._Ttl = ttl; 
			return this;
		}

		public BulkDeleteDescriptor<T> Consistency(Consistency consistency)
		{
			this._Consistency = consistency; 
			return this;
		}

		public BulkDeleteDescriptor<T> Refresh(bool refresh = true)
		{
			this._Refresh = refresh; 
			return this;
		}




	}
}