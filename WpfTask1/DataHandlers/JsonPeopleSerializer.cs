using Newtonsoft.Json;
using System.Collections.Generic;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    class JsonPeopleSerializer : IJsonSerializer<People>
    {
        public string Serialize(ICollection<People> people)
        {
            return JsonConvert.SerializeObject(people);
        }
    }
}
