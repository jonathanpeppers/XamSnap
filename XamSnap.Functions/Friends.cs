using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace XamSnap.Functions
{
    public static class Friends
    {
        public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<TableEntity> inputTable, TraceWriter log)
        {
            dynamic data = await req.Content.ReadAsAsync<object>();
            string userName = data?.userName;
            if (string.IsNullOrEmpty(userName))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var results = inputTable
                .Where(r => r.PartitionKey == userName)
                .Select(r => new { Name = r.RowKey })
                .ToList();
            return req.CreateResponse(HttpStatusCode.OK, results);
        }
    }
}
