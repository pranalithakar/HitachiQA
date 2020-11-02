using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApolloQA.Source.Helpers
{
    public class Cosmos
    {
        public static CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("COSMOS_TARGETURL"), Environment.GetEnvironmentVariable("COSMOS_APIKEY"));



        public static async Task<bool> GetQuery(string containerA, string queryA)
        {
            var database = client.GetDatabase("apollo");
            bool returnValue = false;
            var container = database.GetContainer(containerA);
            using (FeedIterator<dynamic> feedIterator = container.GetItemQueryIterator<dynamic>(queryA))
            {
                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<dynamic> response = await feedIterator.ReadNextAsync();
                    foreach (var item in response)
                    {
                        //simple check for right now
                        Console.WriteLine(item);
                        returnValue = true;
                        
                    }
                }
            }
            return returnValue;

        }

        public static async Task<int> GetLatestPolicyID()
        {
            var database = client.GetDatabase("apollo");
            string queryA = "SELECT * FROM c ORDER BY c._ts DESC OFFSET 0 LIMIT 1";
            var container = database.GetContainer("Policy");
            int policyID = 1;
            using (FeedIterator<dynamic> feedIterator = container.GetItemQueryIterator<dynamic>(queryA))
            {
                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<dynamic> response = await feedIterator.ReadNextAsync();
                    foreach (var item in response)
                    {
                        //simple check for right now
                        Console.WriteLine(item);
                        policyID = item.Id;
                        

                    }
                }
            }
            return policyID;
        }

        public async Task<int> GetLatestClaimID()
        {
            var database = client.GetDatabase("apollo");
            string queryA = "SELECT * FROM c ORDER BY c._ts DESC OFFSET 0 LIMIT 1";
            var container = database.GetContainer("Claim");
            int claimID = 1;
            using (FeedIterator<dynamic> feedIterator = container.GetItemQueryIterator<dynamic>(queryA))
            {
                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<dynamic> response = await feedIterator.ReadNextAsync();
                    foreach (var item in response)
                    {
                        //simple check for right now
                        Console.WriteLine(item);
                        claimID = item.ClaimNumber;


                    }
                }
            }
            return claimID;
        }

    }
}
