#nullable enable
using Daipan.Stream.MonoScripts;
using Stream.Utility;
using VContainer;
using VContainer.Unity;

namespace Stream.Viewer.Scripts
{
    public class ViewerFactory : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<ViewerMono> _viewerLoader;

        [Inject]
        public ViewerFactory(
            IObjectResolver container,
            IPrefabLoader<ViewerMono> viewerLoader)
        {
            _container = container;
            _viewerLoader = viewerLoader;
        }

        void IStartable.Start()
        {
            var viewerMonoPrefab = _viewerLoader.Load();
            var viewerMono = _container.Instantiate(viewerMonoPrefab);
        }
        

    }
}