using Daipan.Comment.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Comment.Scripts
{
    public sealed class AntiCommentNetPrefabLoader : IPrefabLoader<AntiCommentNet>
    {
        readonly PrefabLoaderFromResources<AntiCommentNet> _loader;

        public AntiCommentNetPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<AntiCommentNet>("AntiCommentNet");
        }

        public AntiCommentNet Load()
        {
            return _loader.Load();
        }
    }
}

