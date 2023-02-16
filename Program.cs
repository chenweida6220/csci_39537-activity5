using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class ZipCodeLookup
    {
        [JsonProperty("zipcode")]
        public int ZipCode  { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryabbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("places")]
        public string Places { get; set; }
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter ZipCode: ");
                    int postalcode = Convert.ToInt32(Console.ReadLine());
                    if (postalcode == 0)
                    {
                        break;
                    }

                    var result = await client.GetAsync("http://api.zippopotam.us/us/" + postalcode);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var postalcode_deserialize = JsonConvert.DeserializeObject<ZipCodeLookup>(resultRead);
                    Console.WriteLine("--------");
                    Console.WriteLine("Country: " + postalcode_deserialize.Country + "  Zip Code: " + postalcode_deserialize.ZipCode);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error not valid");
                }
            }
        }
    }
}