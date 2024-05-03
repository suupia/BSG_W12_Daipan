using Daipan.Player.Scripts;
using Daipan.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerTestScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // builder.RegisterEntryPoint<ActorPresenter>();
        //
        // builder.Register<CharacterService>(Lifetime.Scoped);
        // builder.Register<IRouteSearch, AStarRouteSearch>(Lifetime.Singleton);
        //
        // builder.RegisterComponentInHierarchy<ActorsView>();

        builder.Register<PlayerPrefabLoader>(Lifetime.Scoped).As<IPrefabLoader<PlayerMono>>();
        builder.Register<PlayerFactory>(Lifetime.Scoped);
        Debug.Log($"Log from PlayerTestScope.cs");

        builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<PlayerFactory>();
                // entryPoints.Add<OtherSingletonEntryPointA>();
                // entryPoints.Add<OtherSingletonEntryPointB>();
                // entryPoints.Add<OtherSingletonEntryPointC>();
            })
            ;
    }
}