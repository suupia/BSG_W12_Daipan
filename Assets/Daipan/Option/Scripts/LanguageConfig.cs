#nullable enable
namespace Daipan.Option.Scripts
{
    internal sealed class LanguageConfig
    {
        public enum Language
        {
            English,
            Japanese,
        }
        
        public Language CurrentLanguage { get; set; }
    } 
}

