#nullable enable
using Daipan.Stream.MonoScripts;
using Stream.Utility;
using Stream.Utility.Scripts;

namespace Stream.Viewer.Scripts
{
    public class ViewerPrefabLoader : IPrefabLoader<StreamMono>
    {
        readonly PrefabLoaderFromResources<StreamMono> _loader;

        public ViewerPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<StreamMono>("Viewer");
        }

        public StreamMono Load()
        {
            return _loader.Load();
        }
    }
}