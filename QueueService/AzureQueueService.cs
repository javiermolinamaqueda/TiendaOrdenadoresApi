using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Newtonsoft.Json;

namespace TiendaOrdenadoresWebApi.QueueService
{
    public class AzureQueueService : IQueueService
    {
        private readonly QueueClient _queueClient;
        public AzureQueueService(QueueClient queueclient)
        {
            _queueClient = queueclient;
        }

        public async Task<IActionResult?> ConsumeMessage()
        {
            var response = await _queueClient.ReceiveMessageAsync();
            if (response.Value != null)
            {
                var messageText = response.Value.MessageText;
                var message = JsonConvert.DeserializeObject<Message>(messageText);
                var controllerType = Type.GetType($"{message.ControllerName}");
                var controllerInstance = Activator.CreateInstance(controllerType);
                var method = controllerType.GetMethod(message.MethodName);
                //var payload = JsonConvert.DeserializeObject(message.Payload);
                //await _queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);

                //method.Invoke(controllerInstance, new object[] { payload });  
                method.Invoke(controllerInstance, new object[] {});

            }
            return null;
        }

        private class Message
        {
            public string ControllerName { get; set; } = "";
            public string MethodName { get; set; } = string.Empty;
            //public string Payload { get; set; } = "";
        }
    }


}
