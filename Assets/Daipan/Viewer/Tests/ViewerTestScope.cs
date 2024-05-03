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

            // Playerのプレハブをロードするクラスを登録
            builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();

            // Playerの生成を行うクラスを登録（今後様々なPlayerを作れるようにFactoryパターンを採用）
            builder.Register<PlayerAttack>(Lifetime.Scoped);
            builder.Register<PlayerFactory>(Lifetime.Scoped);


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints => { entryPoints.Add<PlayerFactory>(); });
        }
    }
    


}