using System.Threading.Tasks;

namespace XmlParser.Parsers
{
    public interface IParser
    {
        Task RunAsync();
    }
}
