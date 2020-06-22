using Microsoft.Extensions.Options;
using System;
using System.Data;
using XmlParser.Parsers;
using XmlParser.Repositories;
using XmlParser.Settings;

namespace XmlParser.Factories
{
    public interface IParserFactory
    {
        IParser GetParser(Type type);
    }

    public class ParserFactory : IParserFactory
    {
        private readonly DataReader.IDataReader _dataReader;
        private readonly IOptions<AppSettings> _options;
        private readonly ApplicationContext _applicationContext;
        public ParserFactory(DataReader.IDataReader dataReader, 
            IOptions<AppSettings> options,
            ApplicationContext applicationContext)
        {
            _dataReader = dataReader;
            _options = options;
            _applicationContext = applicationContext;
        }
        public IParser GetParser(Type type)
        {
            var genericType = typeof(Parser<>).MakeGenericType(type);
            return (IParser)Activator.CreateInstance(genericType, new object[] { _dataReader, _options, _applicationContext });
        }
    }
}
