using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public abstract class AbstractMonoInstaller : MonoInstaller
    {
        protected void InstallBinding<TDependency>()
           => Container.Bind<TDependency>().FromNew().AsSingle();
        protected void InstallAbstractClassBinding<TAbstractDependency, TConcrete>() where TConcrete : TAbstractDependency
           => Container.Bind<TAbstractDependency>().To<TConcrete>().AsSingle();
        protected void InstallInterfaceBinding<TInterface, TRealization>() where TRealization : TInterface
          => Container.Bind<TInterface>().To<TRealization>().AsSingle();
        protected void InstallInterfaceBindingFromInstance<TInterface, TRealization>(TRealization instance) where TRealization : TInterface
          => Container.Bind<TInterface>().To<TRealization>().FromInstance(instance);
        protected void InstallUnityComponentBinding<TDependency>(TDependency instance) where TDependency : Component
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        protected void InstallMonobehaviourBinding<TDependency>(TDependency instance) where TDependency : MonoBehaviour
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        protected void InstallScriptableObjecttBinding<TDependency>(TDependency instance) where TDependency : ScriptableObject
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
    }
}
