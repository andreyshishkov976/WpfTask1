using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Task3.Interfaces;

namespace Task3
{
    class CSVfileHandler : ICsvFileHandler
    {
        public async Task<ICollection<People>> DataLoaderAsync(string path)
        {
            var fileText = await File.OpenText(path).ReadToEndAsync();
            return fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .ToList().Select(v => new People(v)).ToList();
        }
    }
}
