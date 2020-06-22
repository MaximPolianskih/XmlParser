using Microsoft.Extensions.Options;
using System.IO;
using XmlParser.Settings;

namespace XmlParser.DataReader
{
    public class FileReader : IDataReader
    {
        private readonly AppSettings _appSettings;

        public FileReader(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public Stream GetStream()
        {
            return File.OpenRead(_appSettings.FileByParsing);
        }
    }
}
