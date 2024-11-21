using Daipan.Comment.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Comment.Scripts
{
    public sealed class AntiCommentPrefabLoader : IPrefabLoader<IAntiCommentMono>
    {
        readonly PrefabLoaderFromResources<IAntiCommentMono> _loader;

        public AntiCommentPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<IAntiCommentMono>("AntiComment");
        }

        public IAntiCommentMono Load()
        {
            return _loader.Load();
        }
    }
}

