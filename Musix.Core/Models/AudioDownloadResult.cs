using System;

namespace Musix.Core.Models
{
    public struct AudioDownloadResult
    {
        public Exception Exception;
        public bool Success;
        public bool RequiresTranscoding;


        public AudioDownloadResult(Exception exception)
        {
            Success = false;
            RequiresTranscoding = false;
            Exception = exception;
        }

        public AudioDownloadResult(bool passed, bool transcode)
        {
            RequiresTranscoding = transcode;
            Success = passed;
            Exception = null;
        }
    }
}