using System.Threading.Tasks;
using Musix.Core.API;
using Musix.Core.Models.Debug;
using Xabe.FFmpeg;

namespace Musix.Core.Components.Providers
{
    public class XabeFFMPEGProvider : IConversionProvider
    {
        public bool CanConvert(string InputFile, string OutputFile)
        {
            return true;
        }

        public async Task Convert(string Inputfile, string OutputFile)
        {
            StopWatch watcher = new StopWatch();
            IConversion Conversion = await FFmpeg.Conversions.FromSnippet.Convert(Inputfile, OutputFile);
            await Conversion.Start();
            watcher.PrintDur("XABE");
        }
    }
}