#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outputTable, TraceWriter log)
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
    var result = outputTable.Execute(operation);

    return req.CreateResponse((HttpStatusCode)result.HttpStatusCode);
}

public class Message : TableEntity
{
    public string UserName { get; set; }
    public string Text { get; set; }
}