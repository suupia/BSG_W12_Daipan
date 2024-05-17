#nullable enable
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Stream.Scripts
{
    public sealed class StreamPrefabLoader : IPrefabLoader<StreamMono>
    {
        readonly PrefabLoaderFromResources<StreamMono> _loader;

        public StreamPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<StreamMono>("Stream");
        }

        public StreamMono Load()
        {
            return _loader.Load();
        }
    }
}