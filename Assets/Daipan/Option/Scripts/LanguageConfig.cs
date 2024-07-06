#nullable enable
namespace Daipan.Option.Scripts
{
    internal sealed class LanguageConfig
    {
        public enum LanguageEnum
        {
            English,
            Japanese
        }

        public LanguageEnum CurrentLanguage { get; set; }
    }
}