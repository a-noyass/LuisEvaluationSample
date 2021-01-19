using Newtonsoft.Json;

namespace BatchTesting
{
    class OperationStatus
    {
        [JsonProperty("operationId")]
        public string OperationId { get; set; }

        [JsonProperty("status")]
        public OperationStatusEnum Status { get; set; }

        [JsonProperty("createdDateTime")]
        public string CreatedDateTime { get; set; }

        [JsonProperty("lastActionDateTime")]
        public string LastActionDateTime { get; set; }
    }
}
