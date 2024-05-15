#nullable enable
using Daipan.Core.Interfaces;
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using VContainer;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public class StreamSpawner : IStart
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

        void IStart.Start()
        {
            var viewerMonoPrefab = _viewerLoader.Load();
            var viewerMono = _container.Instantiate(viewerMonoPrefab);
        }
        

    }
}