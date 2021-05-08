namespace Musix.Core.Models
{
    public struct MetadataResult
    {
        public bool Success;

        public MetadataResult(bool passed) => Success = passed;
    }
}