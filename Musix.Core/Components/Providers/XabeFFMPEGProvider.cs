using Musix.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            IConversion Conversion = await FFmpeg.Conversions.FromSnippet.Convert(Inputfile, OutputFile);
            await Conversion.Start();
        }
    }
}
