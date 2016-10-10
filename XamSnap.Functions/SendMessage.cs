using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;

namespace XamSnap.Functions
{
    public static class SendMessage
    {
        public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outTable, Notification notification, TraceWriter log)
        {
            dynamic data = await req.Content.ReadAsAsync<object>();
            if (data == null)
                return req.CreateResponse(HttpStatusCode.BadRequest);

            var operation = TableOperation.InsertOrReplace(new Message
            {
                PartitionKey = data.Conversation,
                RowKey = data.Id,
                UserName = data.UserName,
                Text = data.Text,
            });
            var result = outTable.Execute(operation);

            await SendPush(data.UserName, data.Text);

            return req.CreateResponse((HttpStatusCode)result.HttpStatusCode);
        }

        private async static Task SendPush(string tag, string message)
        {
            var dictionary = new Dictionary<string, string>();
            dictionary["message"] = message;

            var hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://xamsnap.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=UsBxev0o2YNMdY/poBijA7blfbfAGLkL1gz/YtNHzh4=", "xamsnap");
            await hub.SendTemplateNotificationAsync(dictionary, tag);
        }

        public class Message : TableEntity
        {
            public string UserName { get; set; }
            public string Text { get; set; }
        }
    }
}
