// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System;
using System.Threading;
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

            var batchTestClient = new LuisEvaluationsClient(endpoint, appId, key);

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

            var input = new LuisEvaluationsRequest
            {
                LabeledTestSetUtterances = new LabeledTestSetUtterance[] { utterance },
            };

            try
            {
                var operationStatus = await batchTestClient.CreateEvaluationsOperationAsync(input);

                // Max number of times to check status before timing out
                int timeoutCounter = 20;
                // Milliseconds to sleep before checking status
                int sleepMilliseconds = 500;
                while (operationStatus.Status != OperationStatus.succeeded)
                {
                    timeoutCounter--;
                    Thread.Sleep(sleepMilliseconds);
                    operationStatus = await batchTestClient.GetEvaluationsStatusAsync(operationStatus.OperationId);
                    if (operationStatus.Status == OperationStatus.failed)
                    {
                        throw new Exception($"Operation failed: {operationStatus.ErrorDetails}");
                    }
                    else if (operationStatus.Status == OperationStatus.unknown)
                    {
                        throw new Exception($"Operation failed: operation status unknown");
                    }
                    else if (timeoutCounter <= 0)
                    {
                        throw new Exception($"Operation {operationStatus.OperationId} took longer than {(float)timeoutCounter * sleepMilliseconds / 1000} seconds");
                    }
                }

                var result = await batchTestClient.GetEvaluationsResultAsync(operationStatus.OperationId);

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
