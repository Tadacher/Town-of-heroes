using UnityEngine;
using Zenject;

namespace Infrastructure
{
    class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            

        }

        private void InstallBinding<TDependency>() where TDependency : new()
        {
            Container.Bind<TDependency>().FromNew().AsSingle();
        }
    }
}
