using Daipan.Player.Scripts;
using Daipan.Utility;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

// プレイヤーの処理をテストするためのLifetimeScope
public class PlayerTestScope : LifetimeScope
{
    [SerializeField] PlayerParameter playerParameter;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // ScriptableObjectのPlayerParameterを登録
        builder.RegisterInstance(playerParameter.attackParameter);
        
        // Playerのプレハブをロードするクラスを登録
        builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();
       
        // Playerの生成を行うクラスを登録（今後様々なPlayerを作れるようにFactoryパターンを採用）
        builder.Register<PlayerAttack>(Lifetime.Scoped);
        builder.Register<PlayerFactory>(Lifetime.Scoped);


        builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
        {
            entryPoints.Add<PlayerFactory>();
        });
    }
}