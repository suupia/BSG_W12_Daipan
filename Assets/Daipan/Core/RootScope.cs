using Daipan.Daipan;
using Daipan.Option.Scripts;
using VContainer;
using VContainer.Unity;

public sealed class RootScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<LanguageConfig>(Lifetime.Singleton);
        
    }
}