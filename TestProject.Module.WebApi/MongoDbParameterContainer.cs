using System.Collections.Generic;

namespace TestProject.Module.WebApi
{
    public class MongoDbParameterContainer
    {
        public string DatabaseName { get; set; }
        public List<string> Collections { get; set; }
    }
}