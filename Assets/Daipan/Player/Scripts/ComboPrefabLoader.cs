#nullable enable
using Daipan.Comment.MonoScripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts;

public class ComboPrefabLoader : IPrefabLoader<ComboViewMono>
{
    readonly PrefabLoaderFromResources<ComboViewMono> _loader;

    public ComboPrefabLoader()
    {
        _loader = new PrefabLoaderFromResources<ComboViewMono>("Combo");
    }

    public ComboViewMono Load()
    {
        return _loader.Load();
    } 
}