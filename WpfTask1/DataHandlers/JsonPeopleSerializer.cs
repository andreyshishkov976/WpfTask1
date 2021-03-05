using Newtonsoft.Json;
using System.Collections.Generic;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    class JsonPeopleSerializer : IJsonSerializer<People>
    {
        public void Serialize(ICollection<People> people)
        {
            JsonConvert.SerializeObject(people);
        }
    }
}
