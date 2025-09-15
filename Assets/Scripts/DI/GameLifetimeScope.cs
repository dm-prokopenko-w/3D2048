using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        var vfxConfig = Resources.Load<VFXConfig>(Constants.VFXPath);
        builder.RegisterInstance(vfxConfig).AsSelf();
        
        builder.Register<VFXController>(Lifetime.Singleton).AsSelf();

        builder.Register<TouchController>(Lifetime.Scoped).AsSelf();
        builder.Register<CubeController>(Lifetime.Singleton)
            .AsSelf()
            .As<IStartable>();
        
        
        var chanceTable = Resources.Load<ChanceTable>(Constants.ChanceTablePath);
        builder.RegisterInstance(chanceTable).AsSelf();
        
    }
}
