#nullable enable
using Daipan.Utility.Scripts;

namespace Daipan.Viewer.Scripts
{
    public class ViewerPrefabLoader
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