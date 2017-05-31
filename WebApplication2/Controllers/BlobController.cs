using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace WebApplication2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BlobController : Controller
    {
        private readonly CloudBlobClient blobClient;
        private readonly CloudBlobContainer blobContainer;
        public BlobController(IConfigurationRoot config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("StorageConnection"));

            // Create the table client.
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference("mycontainer");
        }
        // GET: api/Blob
        [HttpGet]
        public IEnumerable<FileDetails> Get()
        {
            //aici poti folosi
            
            foreach (CloudBlockBlob blob in blobContainer.ListBlobs(null, false))
            {
                yield return new FileDetails
                {
                    Name = blob.Name,
                    Size = blob.Properties.Length,
                    ContentType = blob.Properties.ContentType,
                    Location = blob.Uri.AbsoluteUri
                };
                
            }
        }

        // GET: api/Blob/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Blob
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Blob/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    
}
