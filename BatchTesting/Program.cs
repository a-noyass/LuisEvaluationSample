// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BatchTesting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Prediction resource key
            string key = "";
            // LUIS Application ID
            string appId = "";
            // Prediction Endpoint
            string endpoint = "https://westus.api.cognitive.microsoft.com/";

            var batchTestClient = new LuisBatchTestClient(endpoint, appId, key);

            var utterance = new LabeledTestSetUtterance
            {
                Text = "I want a large pepperoni pizza",
                Entities = new EntityElement[]
                {
                    new EntityElement
                    {
                        Entity = "Order",
                        StartPos = 7,
                        EndPos = 29,
                        Children = new EntityElement[]
                        {
                            new EntityElement
                            {
                                Entity = "FullPizzaWithModifiers",
                                StartPos = 7,
                                EndPos = 29,
                                Children = new EntityElement[]
                                {
                                    new EntityElement
                                    {
                                        Entity = "PizzaType",
                                        StartPos = 15,
                                        EndPos = 29,
                                    },
                                    new EntityElement
                                    {
                                        Entity = "Size",
                                        StartPos = 9,
                                        EndPos = 13,
                                    }
                                }
                            }
                        }
                    }
                },
                Intent = "ModifyOrder"
            };

            var input = new BatchTestingRequest
            {
                LabeledTestSetUtterances = new LabeledTestSetUtterance[] { utterance },
            };

            var operationStatus = await batchTestClient.CreateEvaluationsOperationAsync(input);

            // TODO: handle failures and add timeout
            while (operationStatus.Status != OperationStatusEnum.succeeded)
            {
                operationStatus = await batchTestClient.GetEvaluationsStatusAsync(operationStatus.OperationId);
            }

            var result = await batchTestClient.GetEvaluationsResultAsync(operationStatus.OperationId);

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
