#nullable enable
using Daipan.Stream.MonoScripts;
using Stream.Utility;
using Stream.Utility.Scripts;

namespace Stream.Viewer.Scripts
{
    public class StreamPrefabLoader : IPrefabLoader<StreamMono>
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