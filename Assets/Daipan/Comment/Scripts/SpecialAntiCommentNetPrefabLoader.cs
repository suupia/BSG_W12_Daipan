using Daipan.Comment.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Comment.Scripts
{
    public sealed class SpecialAntiCommentNetPrefabLoader : IPrefabLoader<SpecialAntiCommentNet>
    {
        readonly PrefabLoaderFromResources<SpecialAntiCommentNet> _loader;

        public SpecialAntiCommentNetPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<SpecialAntiCommentNet>("SpecialAntiCommentNet");
        }

        public SpecialAntiCommentNet Load()
        {
            return _loader.Load();
        }
    }
}

