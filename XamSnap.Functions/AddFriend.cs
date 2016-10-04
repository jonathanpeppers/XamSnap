using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace XamSnap.Functions
{
    public static class AddFriend
    {
        public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outTable, TraceWriter log)
        {
            dynamic data = await req.Content.ReadAsAsync<object>();
            string userName = data?.userName;
            string friendName = data?.friendName;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(friendName))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var operation = TableOperation.InsertOrReplace(new TableEntity
            {
                PartitionKey = userName,
                RowKey = friendName,
            });
            var result = outTable.Execute(operation);

            return req.CreateResponse((HttpStatusCode)result.HttpStatusCode);
        }
    }
}
