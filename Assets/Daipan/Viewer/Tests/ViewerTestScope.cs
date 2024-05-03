#nullable enable
using Daipan.Utility;
using Daipan.Viewer.MonoScripts;
using Daipan.Viewer.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Viewer.Tests
{
    public class ViewerTestScope : LifetimeScope
    {
        [SerializeField] ViewerParameter viewerParameter = null!;

        protected override void Configure(IContainerBuilder builder)
        {
            // Parameter
            builder.RegisterInstance(viewerParameter.viewerNumberParameter);
            builder.RegisterInstance(viewerParameter.daipanParameter);

            // PrefabLoader
            builder.Register<ViewerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<ViewerMono>>();

            // Domain
            builder.Register<ViewerNumber>(Lifetime.Scoped);
            builder.Register<DistributionStatus>(Lifetime.Scoped);
            builder.Register<ViewerFactory>(Lifetime.Scoped);

            builder.Register<DaipanExecutor>(Lifetime.Scoped);

            // Mono
            builder.RegisterComponentInHierarchy<ViewerUIMono>();

            // Test
            builder.RegisterComponentInHierarchy<PlayerTestInput>();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints => { entryPoints.Add<ViewerFactory>(); });
        }
    }
}