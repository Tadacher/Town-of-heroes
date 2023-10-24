using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public abstract class AbstractMonoInstaller : MonoInstaller
    {
        protected ConcreteIdArgConditionCopyNonLazyBinder BindService<TDependency>()
           => Container.Bind<TDependency>().FromNew().AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder BindAbstractClass<TAbstractDependency, TConcrete>() where TConcrete : TAbstractDependency
           => Container.Bind<TAbstractDependency>().To<TConcrete>().AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder InstallInterfaceBinding<TInterface, TRealization>() where TRealization : TInterface
          => Container.Bind<TInterface>().To<TRealization>().AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder BindInterfaceFromInstance<TInterface, TRealization>(TRealization instance) where TRealization : TInterface
          => Container.Bind<TInterface>().To<TRealization>().FromInstance(instance);
        protected ConcreteIdArgConditionCopyNonLazyBinder BindUnityComponent<TDependency>(TDependency instance) where TDependency : Component
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder BindMonobehaviour<TDependency>(TDependency instance) where TDependency : MonoBehaviour
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder BindScriptableObject<TDependency>(TDependency instance) where TDependency : ScriptableObject
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder BindInterfacesAndSelfto<TService>() where TService : class => 
            Container.BindInterfacesAndSelfTo<TService>().AsSingle();
    }
}
