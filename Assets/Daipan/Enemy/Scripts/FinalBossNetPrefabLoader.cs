#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Enemy.Scripts;

public sealed class FinalBossNetPrefabLoader : IPrefabLoader<FinalBossNet>
{
    readonly PrefabLoaderFromResources<FinalBossNet> _loader;

    public FinalBossNetPrefabLoader()
    {
        _loader = new PrefabLoaderFromResources<FinalBossNet>("FinalBossNet");
    }

    public FinalBossNet Load()
    {
        return _loader.Load();
    }
}