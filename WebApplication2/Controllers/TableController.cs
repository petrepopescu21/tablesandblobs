using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private readonly CloudTableClient tableClient;
        public TableController(IConfigurationRoot config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("StorageConnection"));

            // Create the table client.
            tableClient = storageAccount.CreateCloudTableClient();
        }
        // GET api/values
        [HttpGet]
        public async Task<TableQuerySegment<NewsEntity>> Get()
        {
            CloudTable table = tableClient.GetTableReference("Anunturi");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<NewsEntity> query = new TableQuery<NewsEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Anunturi"));

            // Print the fields for each customer.
            var result = await table.ExecuteQuerySegmentedAsync(query, default(TableContinuationToken));
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]NewsEntity value)
        {


            CloudTable table = tableClient.GetTableReference("Anunturi");
            await table.CreateIfNotExistsAsync();


            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(value);

            // Execute the insert operation.
            var x = await table.ExecuteAsync(insertOperation);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
