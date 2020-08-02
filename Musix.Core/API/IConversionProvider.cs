using System.Threading.Tasks;

namespace Musix.Core.API
{
    public interface IConversionProvider
    {
         bool CanConvert(string InputFile, string OutputFile);

         Task Convert(string Inputfile, string OutputFile);
    }
}