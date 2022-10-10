using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<PoolContainer>().FromComponentInHierarchy().AsSingle();

        //Container.Bind<ItemPool>().WithId("Red").To<RedItemPool>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<ItemPool>().WithId("Blue").To<BlueItemPool>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<ItemPool>().WithId("Yellow").To<RedItemPool>().FromComponentInHierarchy().AsSingle();
    }
}