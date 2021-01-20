// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace BatchTesting
{
    public class EvaluationResult
    {
        [JsonProperty("intentModelsStats")]
        public IntentModelsStat[] IntentModelsStats { get; set; }

        [JsonProperty("entityModelsStats")]
        public EntityModelsStat[] EntityModelsStats { get; set; }

        [JsonProperty("utterancesStats")]
        public UtterancesStat[] UtterancesStats { get; set; }
    }

    public class EntityModelsStat
    {
        [JsonProperty("modelName")]
        public string ModelName { get; set; }

        [JsonProperty("modelType")]
        public string ModelType { get; set; }

        [JsonProperty("precision")]
        public double Precision { get; set; }

        [JsonProperty("recall")]
        public double Recall { get; set; }

        [JsonProperty("fScore")]
        public double FScore { get; set; }

        [JsonProperty("entityTextFScore")]
        public double EntityTextFScore { get; set; }

        [JsonProperty("entityTypeFScore")]
        public double EntityTypeFScore { get; set; }
    }

    public class IntentModelsStat
    {
        [JsonProperty("modelName")]
        public string ModelName { get; set; }

        [JsonProperty("modelType")]
        public string ModelType { get; set; }

        [JsonProperty("precision")]
        public double Precision { get; set; }

        [JsonProperty("recall")]
        public double Recall { get; set; }

        [JsonProperty("fScore")]
        public double FScore { get; set; }
    }

    public class UtterancesStat
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("predictedIntentName")]
        public string PredictedIntentName { get; set; }

        [JsonProperty("labeledIntentName")]
        public string LabeledIntentName { get; set; }

        [JsonProperty("falsePositiveEntities")]
        public FalsePositiveEntity[] FalsePositiveEntities { get; set; }

        [JsonProperty("falseNegativeEntities")]
        public object[] FalseNegativeEntities { get; set; }
    }

    public class FalsePositiveEntity
    {
        [JsonProperty("entityName")]
        public string EntityName { get; set; }

        [JsonProperty("startCharIndex")]
        public long StartCharIndex { get; set; }

        [JsonProperty("endCharIndex")]
        public long EndCharIndex { get; set; }
    }
}
