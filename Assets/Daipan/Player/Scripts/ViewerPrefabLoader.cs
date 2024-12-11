#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts;

public class ViewerPrefabLoader : IPrefabLoader<ViewerDifferenceViewMono>
{
    readonly PrefabLoaderFromResources<ViewerDifferenceViewMono> _loader;

    public ViewerPrefabLoader()
    {
        _loader = new PrefabLoaderFromResources<ViewerDifferenceViewMono>("ViewerDifference");
    }

    public ViewerDifferenceViewMono Load()
    {
        return _loader.Load();
    }
}