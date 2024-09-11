using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TestApplication.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestApplication.Data
{
    public class TestDataService
    {
        private readonly string _filePath = "tests.json";

        public void SaveTests(List<Test> tests)
        {
            var filePath = "tests.json";
            var json = JsonConvert.SerializeObject(tests, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<Test> LoadTests()
        {
            var filePath = "tests.json";
            if (!File.Exists(filePath))
            {
                return new List<Test>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Test>>(json) ?? new List<Test>();
        }
   
    }
}
