using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask1.Interfaces;
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
