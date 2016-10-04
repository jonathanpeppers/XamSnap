using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace XamSnap.Functions
{
    public static class SendMessage
    {
        public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outTable, TraceWriter log)
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

            return req.CreateResponse((HttpStatusCode)result.HttpStatusCode);
        }

        public class Message : TableEntity
        {
            public string UserName { get; set; }
            public string Text { get; set; }
        }
    }
}
