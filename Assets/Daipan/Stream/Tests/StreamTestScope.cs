#nullable enable
using Daipan.Stream.MonoScripts;
using Stream.Utility;
using Stream.Viewer.MonoScripts;
using Stream.Viewer.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Stream.Viewer.Tests
{
    public class StreamTestScope : LifetimeScope
    {
        [FormerlySerializedAs("viewerParameter")] [SerializeField] StreamParameter streamParameter = null!;

        protected override void Configure(IContainerBuilder builder)
        {
            // Parameter
            builder.RegisterInstance(streamParameter.viewerParameter);
            builder.RegisterInstance(streamParameter.daipanParameter);

            // PrefabLoader
            builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();

            // Domain
            builder.Register<ViewerNumber>(Lifetime.Scoped);
            builder.Register<StreamStatus>(Lifetime.Scoped);
            builder.Register<StreamSpawner>(Lifetime.Scoped);

            builder.Register<DaipanExecutor>(Lifetime.Scoped);

            // Mono
            builder.RegisterComponentInHierarchy<StreamUIMono>();

            // Test
            builder.RegisterComponentInHierarchy<PlayerTestInput>();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints => { entryPoints.Add<StreamSpawner>(); });
        }
    }
}