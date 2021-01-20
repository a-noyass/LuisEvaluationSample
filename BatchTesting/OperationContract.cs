// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace BatchTesting
{
    class OperationContract
    {
        [JsonProperty("operationId")]
        public string OperationId { get; set; }

        [JsonProperty("status")]
        public OperationStatus Status { get; set; }

        [JsonProperty("createdDateTime")]
        public string CreatedDateTime { get; set; }

        [JsonProperty("lastActionDateTime")]
        public string LastActionDateTime { get; set; }

        [JsonProperty("errorDetails")]
        public string ErrorDetails { get; set; }
    }
}
