#nullable enable
using Daipan.Stream.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Viewer.MonoScripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Daipan.Stream.Tests
{
    public sealed class StreamTestScope : LifetimeScope
    {
        [FormerlySerializedAs("streamParameter")] [FormerlySerializedAs("viewerParameter")] [SerializeField]
        StreamParam streamParam = null!;

        protected override void Configure(IContainerBuilder builder)
        {
            // Parameter
            builder.RegisterInstance(streamParam.viewer);
            builder.RegisterInstance(streamParam.daipan);

            // PrefabLoader
            builder.Register<StreamPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<StreamMono>>();

            // Domain
            builder.Register<ViewerNumber>(Lifetime.Scoped);
            builder.Register<StreamSpawner>(Lifetime.Scoped);
            builder.Register<IrritatedValue>(Lifetime.Scoped).WithParameter(100);

            builder.Register<DaipanExecutor>(Lifetime.Scoped);

            // Mono
            builder.RegisterComponentInHierarchy<StreamUIMono>();

            // View
            builder.RegisterComponentInHierarchy<StreamViewMono>();

            // Test
            builder.RegisterComponentInHierarchy<PlayerTestInput>();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints => { entryPoints.Add<StreamSpawner>(); });
        }
    }
}