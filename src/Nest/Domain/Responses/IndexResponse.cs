﻿using Newtonsoft.Json;

namespace Nest
{
  public interface IIndexResponse : IResponse
  {
    string Id { get; }
    string Index { get; }
    string Type { get; }
    string Version { get; }
    bool OK { get; }
  }

	[JsonObject]
  public class IndexResponse : BaseResponse, IIndexResponse
	{	
    [JsonProperty("_index")]
		public string Index { get; internal set; }
    [JsonProperty("_type")]
    public string Type { get; internal set; }
    [JsonProperty("_id")]
    public string Id { get; internal set; }
    [JsonProperty("_version")]
    public string Version { get; internal set; }
    [JsonProperty("ok")]
    public bool OK { get; internal set; }
    
	}
}
