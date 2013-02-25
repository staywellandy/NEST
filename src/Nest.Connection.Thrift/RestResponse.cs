/**
 * Autogenerated by Thrift
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */
using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

namespace Nest.Thrift
{
	[Serializable]
	internal class RestResponse : TBase
	{
		public Isset __isset;
		private byte[] body;
		private Dictionary<string, string> headers;
		private Status status;

		public Status Status
		{
			get { return status; }
			set
			{
				__isset.status = true;
				status = value;
			}
		}

		public Dictionary<string, string> Headers
		{
			get { return headers; }
			set
			{
				__isset.headers = true;
				headers = value;
			}
		}

		public byte[] Body
		{
			get { return body; }
			set
			{
				__isset.body = true;
				body = value;
			}
		}

		#region TBase Members

		public void Read(TProtocol iprot)
		{
			TField field;
			iprot.ReadStructBegin();
			while (true)
			{
				field = iprot.ReadFieldBegin();
				if (field.Type == TType.Stop)
				{
					break;
				}
				switch (field.ID)
				{
					case 1:
						if (field.Type == TType.I32)
						{
							status = (Status) iprot.ReadI32();
							__isset.status = true;
						}
						else
						{
							TProtocolUtil.Skip(iprot, field.Type);
						}
						break;
					case 2:
						if (field.Type == TType.Map)
						{
							{
								headers = new Dictionary<string, string>();
								TMap _map10 = iprot.ReadMapBegin();
								for (int _i11 = 0; _i11 < _map10.Count; ++_i11)
								{
									string _key12;
									string _val13;
									_key12 = iprot.ReadString();
									_val13 = iprot.ReadString();
									headers[_key12] = _val13;
								}
								iprot.ReadMapEnd();
							}
							__isset.headers = true;
						}
						else
						{
							TProtocolUtil.Skip(iprot, field.Type);
						}
						break;
					case 3:
						if (field.Type == TType.String)
						{
							body = iprot.ReadBinary();
							__isset.body = true;
						}
						else
						{
							TProtocolUtil.Skip(iprot, field.Type);
						}
						break;
					default:
						TProtocolUtil.Skip(iprot, field.Type);
						break;
				}
				iprot.ReadFieldEnd();
			}
			iprot.ReadStructEnd();
		}

		public void Write(TProtocol oprot)
		{
			var struc = new TStruct("RestResponse");
			oprot.WriteStructBegin(struc);
			var field = new TField();
			if (__isset.status)
			{
				field.Name = "status";
				field.Type = TType.I32;
				field.ID = 1;
				oprot.WriteFieldBegin(field);
				oprot.WriteI32((int) status);
				oprot.WriteFieldEnd();
			}
			if (headers != null && __isset.headers)
			{
				field.Name = "headers";
				field.Type = TType.Map;
				field.ID = 2;
				oprot.WriteFieldBegin(field);
				{
					oprot.WriteMapBegin(new TMap(TType.String, TType.String, headers.Count));
					foreach (string _iter14 in headers.Keys)
					{
						oprot.WriteString(_iter14);
						oprot.WriteString(headers[_iter14]);
						oprot.WriteMapEnd();
					}
				}
				oprot.WriteFieldEnd();
			}
			if (body != null && __isset.body)
			{
				field.Name = "body";
				field.Type = TType.String;
				field.ID = 3;
				oprot.WriteFieldBegin(field);
				oprot.WriteBinary(body);
				oprot.WriteFieldEnd();
			}
			oprot.WriteFieldStop();
			oprot.WriteStructEnd();
		}

		#endregion

		public string GetBody()
		{
			if (Body != null)
				if (Body.Length > 0)
				{
					return Encoding.UTF8.GetString(Body);
				}
			return string.Empty;
		}

		public void SetBody(string str)
		{
			Body = Encoding.UTF8.GetBytes(str);
		}

		public override string ToString()
		{
			var sb = new StringBuilder("RestResponse(");
			sb.Append("status: ");
			sb.Append(status);
			sb.Append(",headers: ");
			sb.Append(headers);
			sb.Append(",body: ");
			sb.Append(body);
			sb.Append(")");
			return sb.ToString();
		}

		#region Nested type: Isset

		[Serializable]
		public struct Isset
		{
			public bool body;
			public bool headers;
			public bool status;
		}

		#endregion
	}
}