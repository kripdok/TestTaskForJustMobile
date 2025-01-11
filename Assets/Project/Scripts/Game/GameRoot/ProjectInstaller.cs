using Project.Scripts.Game.Settings;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISettingsProvider>().To<LocalSettingsProvider>().AsSingle().NonLazy();
        Container.Bind<TestObject>().AsSingle().NonLazy();
        Debug.Log("bind");
    }
}