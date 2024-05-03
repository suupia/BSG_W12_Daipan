#nullable enable
using Daipan.Utility;
using Daipan.Utility.Scripts;

namespace Daipan.Viewer.Scripts
{
    public class ViewerPrefabLoader : IPrefabLoader<ViewerMono>
    {
        readonly PrefabLoaderFromResources<ViewerMono> _loader;

        public ViewerPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<ViewerMono>("Viewer/Viewer");
        }

        public ViewerMono Load()
        {
            return _loader.Load();
        }
    }
}