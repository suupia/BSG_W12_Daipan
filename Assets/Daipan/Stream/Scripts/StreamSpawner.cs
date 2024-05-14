#nullable enable
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using VContainer;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public class StreamSpawner : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<StreamMono> _viewerLoader;

        [Inject]
        public StreamSpawner(
            IObjectResolver container,
            IPrefabLoader<StreamMono> viewerLoader)
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