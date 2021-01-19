// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Newtonsoft.Json;

namespace BatchTesting
{
    class BatchTestingRequest
    {
        [JsonProperty("LabeledTestSetUtterances")]
        public LabeledTestSetUtterance[] LabeledTestSetUtterances { get; set; }
    }

    public partial class LabeledTestSetUtterance
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("entities")]
        public EntityElement[] Entities { get; set; }
    }

    public partial class EntityElement
    {
        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("startPos")]
        public long StartPos { get; set; }

        [JsonProperty("endPos")]
        public long EndPos { get; set; }

        [JsonProperty("children")]
        public EntityElement[] Children { get; set; }
    }

}
