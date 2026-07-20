using Zenject;
using UnityEngine;
using System.ComponentModel;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<ScoreManager>().AsSingle().NonLazy();

        //Container.Bind<WaveManager>().AsSingle().NonLazy();

        //Container.Bind<GameManager>().AsSingle().NonLazy();

        Container.Bind<ScoreManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<WaveManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<PlayerIdleState>().AsSingle();
        Container.Bind<PlayerMovingState>().AsSingle();

        Container.Bind<PlayerShooter>().FromComponentInHierarchy().AsSingle();
    }
}
