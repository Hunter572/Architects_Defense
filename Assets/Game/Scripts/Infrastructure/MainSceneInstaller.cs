using Zenject;
using UnityEngine;
using System.ComponentModel;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ScoreManager>().AsSingle().NonLazy();

        Container.Bind<WaveManager>().AsSingle().NonLazy();

        Container.Bind<GameManager>().AsSingle().NonLazy();
    }
}
