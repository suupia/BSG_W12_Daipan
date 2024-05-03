#nullable enable
using Daipan.Stream.MonoScripts;
using Stream.Utility;
using Stream.Utility.Scripts;

namespace Stream.Viewer.Scripts
{
    public class ViewerPrefabLoader : IPrefabLoader<ViewerMono>
    {
        readonly PrefabLoaderFromResources<ViewerMono> _loader;

        public ViewerPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<ViewerMono>("Viewer");
        }

        public ViewerMono Load()
        {
            return _loader.Load();
        }
    }
}