using System.Threading.Tasks;

namespace Musix.Core.API
{
    public abstract class ConversionProvider
    {
        public abstract bool CanConvert(string InputFile, string OutputFile);

        public abstract Task Convert(string Inputfile, string OutputFile);
    }
}