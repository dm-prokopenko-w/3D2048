using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<TouchController>(Lifetime.Scoped).AsSelf();
        builder.Register<CubeSpawner>(Lifetime.Singleton)
            .AsSelf()
            .As<IStartable>();
        var chanceTable = Resources.Load<ChanceTable>("Configs/ChanceTable");
        Debug  .LogError(chanceTable == null);
        builder.RegisterInstance(chanceTable).AsSelf();
    }
}
