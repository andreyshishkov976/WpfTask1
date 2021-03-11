using System.Collections.Generic;

namespace WpfTask1.DataHandlers
{
    interface IJsonSerializer<T> where T : class
    {
        string Serialize(ICollection<T> people);
    }
}
