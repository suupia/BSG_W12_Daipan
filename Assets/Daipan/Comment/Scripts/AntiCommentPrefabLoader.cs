using Daipan.Comment.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Comment.Scripts;

public sealed class AntiCommentPrefabLoader : IPrefabLoader<AntiMono>
{
    readonly PrefabLoaderFromResources<AntiMono> _loader;

    public AntiCommentPrefabLoader()
    {
        _loader = new PrefabLoaderFromResources<AntiMono>("AntiComment");
    }

    public AntiMono Load()
    {
        return _loader.Load();
    }
}