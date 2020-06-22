using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using XmlParser.DataReader;
using XmlParser.Models;
using XmlParser.Repositories;
using XmlParser.Settings;

namespace XmlParser.Parsers
{
    public class Parser<T> : BaseParser, IParser where T : class, new()
    {
        private readonly List<T> _packetObject;
        private readonly IDataReader _dataReader;
        private readonly AppSettings _appSettings;
        private readonly ApplicationContext _context;
        private readonly Stopwatch Sw;
        public Parser(IDataReader dataReader,
            IOptions<AppSettings> options,
            ApplicationContext context)
        {
            _packetObject = new List<T>();
            _dataReader = dataReader;
            _appSettings = options.Value;
            _context = context;
            Sw = new Stopwatch();
        }

        public async Task RunAsync()
        {
            Sw.Start();
            using (var stream = new BufferedStream(_dataReader.GetStream()))
            {
                //Читаем xml из потока блоками
                using (var xmlReader = XmlReader.Create(stream, new XmlReaderSettings { Async = true }))
                {
                    while (await xmlReader.ReadAsync())
                    {
                        if (xmlReader.IsStartElement(GetElementName<T>()))
                        {
                            var value = await ParseXmlToObjectAsync<T>(xmlReader);
                            _packetObject.Add(value);
                        }

                        if (_packetObject.Count % _appSettings.PackSize == 0)
                        {
                            await InsertPackageAsync();
                        }
                    }

                    await InsertPackageAsync();
                }
            }
        }

        private async Task InsertPackageAsync()
        {
            if (_packetObject.Count > 0)
            {
                Sw.Stop();
                System.Console.WriteLine($"Парсинг пачки: {Sw.ElapsedMilliseconds} ms.");
                Sw.Reset();

                Sw.Start();

                await _context.AddRangeAsync(_packetObject);
                await _context.SaveChangesAsync();

                Sw.Stop();
                System.Console.WriteLine($"Запись пачки в бд: {Sw.ElapsedMilliseconds} ms.");
                Sw.Reset();
                Sw.Start();

                _packetObject.Clear();
            }
        }
    }
}
