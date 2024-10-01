#nullable enable
using Daipan.Comment.MonoScripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts;

public class ComboPrefabLoader : IPrefabLoader<ComboInstantViewMono>
{
    readonly PrefabLoaderFromResources<ComboInstantViewMono> _loader;

    public ComboPrefabLoader()
    {
        _loader = new PrefabLoaderFromResources<ComboInstantViewMono>("Combo");
    }

    public ComboInstantViewMono Load()
    {
        return _loader.Load();
    } 
}