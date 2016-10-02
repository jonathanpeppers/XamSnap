using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace XamSnap.Functions
{
    public static class Login
    {
        private const string PartitionKey = "XamSnap";

        public static HttpResponseMessage Run(User user, CloudTable outTable, TraceWriter log)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            };

            log.Info($"PersonName={user.Username}");

            //Let's hash all incoming passwords
            user.Password = Hash(user.Password);

            var operation = TableOperation.Retrieve(PartitionKey, user.Username);
            var result = outTable.Execute(operation);
            var existing = result.Result as User;
            if (existing == null)
            {
                user.RowKey = user.Username;
                user.PartitionKey = PartitionKey;
                operation = TableOperation.Insert(user);
                result = outTable.Execute(operation);
                return new HttpResponseMessage((HttpStatusCode)result.HttpStatusCode);
            }
            else if (existing.Password != user.Password)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        private static string Hash(string password)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte b in crypto)
            {
                hash.Append(b.ToString("x2"));
            }
            return hash.ToString();
        }

        public class User : TableEntity
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }
    }
}
