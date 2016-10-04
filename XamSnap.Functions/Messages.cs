using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace XamSnap.Functions
{
    public static class Messages
    {
        public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<Message> inputTable, TraceWriter log)
        {
            dynamic data = await req.Content.ReadAsAsync<object>();
            string conversation = data?.conversation;
            if (string.IsNullOrEmpty(conversation))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var results = inputTable
                .Where(r => r.PartitionKey == conversation)
                .Select(r => new { Id = r.RowKey, UserName = r.UserName, Text = r.Text })
                .ToList();
            return req.CreateResponse(HttpStatusCode.OK, results);
        }

        public class Message : TableEntity
        {
            public string UserName { get; set; }
            public string Text { get; set; }
        }
    }
}
