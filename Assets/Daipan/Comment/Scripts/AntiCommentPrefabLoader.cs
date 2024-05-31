using Daipan.Comment.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Comment.Scripts
{
    public sealed class AntiCommentPrefabLoader : IPrefabLoader<AntiCommentMono>
    {
        readonly PrefabLoaderFromResources<AntiCommentMono> _loader;
    
        public AntiCommentPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<AntiCommentMono>("AntiComment");
        }
    
        public AntiCommentMono Load()
        {
            return _loader.Load();
        }
    }
}

