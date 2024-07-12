#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Enemy.Scripts;

public sealed class FinalBossPrefabLoader : IPrefabLoader<FinalBossMono>
{
    readonly PrefabLoaderFromResources<FinalBossMono> _loader;

    public FinalBossPrefabLoader()
    {
        _loader = new PrefabLoaderFromResources<FinalBossMono>("FinalBoss");
    }

    public FinalBossMono Load()
    {
        return _loader.Load();
    }
}