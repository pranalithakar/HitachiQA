

namespace HitachiQA.Helpers
{
    public class Cosmos
    {
        //public static CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("COSMOS_TARGETURL_SECRETNAME")), Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("COSMOS_APIKEY_SECRETNAME")));

        public static async Task<List<dynamic>> GetQuery(string containerA, string queryA)
        {
            //TODO
            //var database = client.GetDatabase("apollo");
            //var container = database.GetContainer(containerA);
            List<dynamic> result = new List<dynamic>();
            //using (FeedIterator<dynamic> feedIterator = container.GetItemQueryIterator<dynamic>(queryA))
            {//
                //while (feedIterator.HasMoreResults)
                //{
                //FeedResponse<dynamic> response = await feedIterator.ReadNextAsync();
                //foreach (var item in response)
                //{
                // result.Add(item);

                //}
            //}
            }
            return result;
        }


        public static void setProperty(string containerA, string identifierQuery,  string key, dynamic value)
        {
            //TODO
            //var database = client.GetDatabase("apollo");
            //var container = database.GetContainer(containerA);


            Log.Debug(containerA);

            List<dynamic> recordsToUpdate = GetQuery(containerA, identifierQuery).Result;

            foreach(var record in recordsToUpdate)
            {
               record[key] = value;
               // _ = container.ReplaceItemAsync(record, (string)record.id).Result;

            }
        }
    }
}
