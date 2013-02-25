﻿using System;
using Nest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ProtocolLoadTest
{
	internal abstract class Tester
	{
		// Number of messages sent by all ThriftTester instances
		private static int NumSent;

		protected ConnectionSettings CreateSettings(string indexName, int port)
		{
			return new ConnectionSettings("localhost", port)
			   .SetDefaultIndex(indexName)
			   .SetMaximumAsyncConnections(1);
		}

		protected void Connect(ElasticClient client, ConnectionSettings settings)
		{
			ConnectionStatus indexConnectionStatus;

			if (!client.TryConnect(out indexConnectionStatus))
			{
				Console.Error.WriteLine("Could not connect to {0}:\r\n{1}",
					settings.Host, indexConnectionStatus.Error.OriginalException.Message);
				Console.Read();
				return;
			}
		}
		
		protected static void GenerateAndIndex(ElasticClient client, string indexName, int numMessages, int bufferSize)
		{
			// refresh = false is default on elasticsearch's side.
			var bulkParms = new SimpleBulkParameters() { Refresh = false };

			var msgGenerator = new MessageGenerator();
			var tasks = new List<Task>();

			var partitionedMessages = msgGenerator.Generate(numMessages).Partition(bufferSize);
			
			Interlocked.Exchange(ref NumSent, 0);

			foreach (var messages in partitionedMessages)
			{
				var t = client.IndexManyAsync(messages, indexName, bulkParms);
				tasks.Add(t);

				Interlocked.Add(ref NumSent, bufferSize);
				if (NumSent % 10000 == 0)
				{
					Console.WriteLine("Sent {0:0,0} messages to {1}", NumSent, indexName);
				}
			}
			Task.WaitAll(tasks.ToArray());
		}
	}
}
