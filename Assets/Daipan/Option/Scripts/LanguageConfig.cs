#nullable enable
namespace Daipan.Option.Scripts
{
    public sealed class LanguageConfig
    {
        public enum LanguageEnum
        {
            English,
            Japanese
        }

        public LanguageEnum CurrentLanguage { get; set; }
    }
}