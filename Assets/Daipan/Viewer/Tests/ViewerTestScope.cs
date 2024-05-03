#nullable enable
using Daipan.Player.Scripts;
using Daipan.Utility;
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
            builder.RegisterInstance(viewerParameter.ViewerNumberParameter);

            builder.Register<ViewerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<ViewerMono>>();

            builder.Register<ViewerFactory>(Lifetime.Scoped);


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints => { entryPoints.Add<ViewerFactory>(); });
        }
    }
    


}