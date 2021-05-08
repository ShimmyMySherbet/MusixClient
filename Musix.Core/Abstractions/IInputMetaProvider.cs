namespace Musix.Core.Abstractions
{
    public interface IInputMetaProvider : IInputMetaParser
    {
        void AddParser<T>() where T : IInputMetaParser;

        void AddParser(IInputMetaParser parser);

        void RemoveParser(IInputMetaParser parser);

        void RemoveParser<T>() where T : IInputMetaParser;
    }
}