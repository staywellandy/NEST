/**
 * Autogenerated by Thrift
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Thrift;
using Thrift.Protocol;

namespace Nest.Thrift
{
	internal class Rest
	{
		#region Nested type: Client

		public class Client : Iface
		{
			protected TProtocol iprot_;
			protected TProtocol oprot_;
			protected int seqid_;

			public Client(TProtocol prot) : this(prot, prot)
			{
			}

			public Client(TProtocol iprot, TProtocol oprot)
			{
				iprot_ = iprot;
				oprot_ = oprot;
			}

			public TProtocol InputProtocol
			{
				get { return iprot_; }
			}

			public TProtocol OutputProtocol
			{
				get { return oprot_; }
			}

			#region Iface Members

			public RestResponse execute(RestRequest request)
			{
				send_execute(request);
				return recv_execute();
			}

			#endregion

			public void send_execute(RestRequest request)
			{
				oprot_.WriteMessageBegin(new TMessage("execute", TMessageType.Call, seqid_));
				var args = new execute_args();
				args.Request = request;
				args.Write(oprot_);
				oprot_.WriteMessageEnd();
				oprot_.Transport.Flush();
			}

			public RestResponse recv_execute()
			{
				TMessage msg = iprot_.ReadMessageBegin();
				if (msg.Type == TMessageType.Exception)
				{
					TApplicationException x = TApplicationException.Read(iprot_);
					iprot_.ReadMessageEnd();
					throw x;
				}
				var result = new execute_result();
				result.Read(iprot_);
				iprot_.ReadMessageEnd();
				if (result.__isset.success)
				{
					return result.Success;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "execute failed: unknown result");
			}
		}

		#endregion

		#region Nested type: Iface

		public interface Iface
		{
			RestResponse execute(RestRequest request);
		}

		#endregion

		#region Nested type: Processor

		public class Processor : TProcessor
		{
			private readonly Iface iface_;
			protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

			public Processor(Iface iface)
			{
				iface_ = iface;
				processMap_["execute"] = execute_Process;
			}

			#region TProcessor Members

			public bool Process(TProtocol iprot, TProtocol oprot)
			{
				try
				{
					TMessage msg = iprot.ReadMessageBegin();
					ProcessFunction fn;
					processMap_.TryGetValue(msg.Name, out fn);
					if (fn == null)
					{
						TProtocolUtil.Skip(iprot, TType.Struct);
						iprot.ReadMessageEnd();
						var x = new TApplicationException(TApplicationException.ExceptionType.UnknownMethod,
						                                  "Invalid method name: '" + msg.Name + "'");
						oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
						x.Write(oprot);
						oprot.WriteMessageEnd();
						oprot.Transport.Flush();
						return true;
					}
					fn(msg.SeqID, iprot, oprot);
				}
				catch (IOException)
				{
					return false;
				}
				return true;
			}

			#endregion

			public void execute_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				var args = new execute_args();
				args.Read(iprot);
				iprot.ReadMessageEnd();
				var result = new execute_result();
				result.Success = iface_.execute(args.Request);
				oprot.WriteMessageBegin(new TMessage("execute", TMessageType.Reply, seqid));
				result.Write(oprot);
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			#region Nested type: ProcessFunction

			protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);

			#endregion
		}

		#endregion

		#region Nested type: execute_args

		[Serializable]
		public class execute_args : TBase
		{
			public Isset __isset;
			private RestRequest request;

			public RestRequest Request
			{
				get { return request; }
				set
				{
					__isset.request = true;
					request = value;
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
							if (field.Type == TType.Struct)
							{
								request = new RestRequest();
								request.Read(iprot);
								__isset.request = true;
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
				var struc = new TStruct("execute_args");
				oprot.WriteStructBegin(struc);
				var field = new TField();
				if (request != null && __isset.request)
				{
					field.Name = "request";
					field.Type = TType.Struct;
					field.ID = 1;
					oprot.WriteFieldBegin(field);
					request.Write(oprot);
					oprot.WriteFieldEnd();
				}
				oprot.WriteFieldStop();
				oprot.WriteStructEnd();
			}

			#endregion

			public override string ToString()
			{
				var sb = new StringBuilder("execute_args(");
				sb.Append("request: ");
				sb.Append(request == null ? "<null>" : request.ToString());
				sb.Append(")");
				return sb.ToString();
			}

			#region Nested type: Isset

			[Serializable]
			public struct Isset
			{
				public bool request;
			}

			#endregion
		}

		#endregion

		#region Nested type: execute_result

		[Serializable]
		public class execute_result : TBase
		{
			public Isset __isset;
			private RestResponse success;

			public RestResponse Success
			{
				get { return success; }
				set
				{
					__isset.success = true;
					success = value;
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
						case 0:
							if (field.Type == TType.Struct)
							{
								success = new RestResponse();
								success.Read(iprot);
								__isset.success = true;
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
				var struc = new TStruct("execute_result");
				oprot.WriteStructBegin(struc);
				var field = new TField();

				if (__isset.success)
				{
					if (success != null)
					{
						field.Name = "success";
						field.Type = TType.Struct;
						field.ID = 0;
						oprot.WriteFieldBegin(field);
						success.Write(oprot);
						oprot.WriteFieldEnd();
					}
				}
				oprot.WriteFieldStop();
				oprot.WriteStructEnd();
			}

			#endregion

			public override string ToString()
			{
				var sb = new StringBuilder("execute_result(");
				sb.Append("success: ");
				sb.Append(success == null ? "<null>" : success.ToString());
				sb.Append(")");
				return sb.ToString();
			}

			#region Nested type: Isset

			[Serializable]
			public struct Isset
			{
				public bool success;
			}

			#endregion
		}

		#endregion
	}
}