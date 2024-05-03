#nullable enable
using Daipan.Player.Scripts;
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
        [SerializeField] ViewerParameter viewerParameter;

        protected override void Configure(IContainerBuilder builder)
        {
            // Parameter
            builder.RegisterInstance(viewerParameter.ViewerNumberParameter);
            builder.RegisterInstance(viewerParameter.DaipanParameter);

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


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<ViewerFactory>();
            });
        }
    }
    


}