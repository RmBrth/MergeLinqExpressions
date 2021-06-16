using MergeLinqExpressions.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MergeLinqExpressions.Data
{
    public class CountryService
    {
        public List<Country> GetCountries()
        {
            using (StreamReader reader = File.OpenText(@"./Data/countries.json"))
            {

                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Country>>(json);
            }
        }
    }
}
