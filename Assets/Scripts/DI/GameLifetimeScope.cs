using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<TouchController>(Lifetime.Scoped).AsSelf();
        builder.Register<CubeController>(Lifetime.Singleton)
            .AsSelf()
            .As<IStartable>();
        var chanceTable = Resources.Load<ChanceTable>(Constants.ChanceTablePath);
        builder.RegisterInstance(chanceTable).AsSelf();
    }
}
