#nullable enable
namespace Daipan.Option.Scripts
{
    public sealed class LanguageConfig
    {
        public LanguageEnum CurrentLanguage { get; set; }
    }
    
    public enum LanguageEnum
    {
        English,
        Japanese
    }
}