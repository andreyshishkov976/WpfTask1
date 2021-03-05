using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfTask1.Models;

namespace WpfTask1.DataHandlers
{
    class CSVPeopleImporter : ICsvImporter<People>
    {
        public async Task<ICollection<People>> DataLoaderAsync(string path)
        {
            var fileText = await File.OpenText(path).ReadToEndAsync();
            return fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(v => new People(v)).ToList();
        }
    }
}
