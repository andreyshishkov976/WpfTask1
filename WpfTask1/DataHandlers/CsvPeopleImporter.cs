using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    class CsvPeopleImporter : ICsvImporter<People>
    {
        public async Task<ICollection<People>> DataLoaderAsync(string path)
        {
            var fileText = await File.OpenText(path).ReadToEndAsync().ConfigureAwait(false);
            return fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => new People(v)).ToList();
        }
    }
}
