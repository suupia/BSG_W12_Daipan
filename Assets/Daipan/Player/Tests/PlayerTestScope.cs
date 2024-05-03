using Daipan.Player.Scripts;
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

        builder.Register<PlayerFactory>(Lifetime.Scoped);
    }
}