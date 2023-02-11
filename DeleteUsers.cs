using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SiaFunctionApp
{
    public class DeleteUsers
    {
        private IConfiguration _configuration;
        public DeleteUsers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("DeleteUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
            SqlCommand sqlCommand = new SqlCommand("DELETE From Users", sqlConnection);
            sqlConnection.Open();   
            await sqlCommand.ExecuteNonQueryAsync();
            sqlConnection.Close();

            return new OkObjectResult("Users Deleted");
        }
    }
}
