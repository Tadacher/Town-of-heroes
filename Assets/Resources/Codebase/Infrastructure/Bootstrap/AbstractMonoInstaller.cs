using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Base class for any zenject monoInstaller implementation. Consists of "Bind" methods, which are basically instructions 
    /// about which class should be resolved for each binded contract
    /// </summary>
    public abstract class AbstractMonoInstaller : MonoInstaller
    {
        [Header("Scriptables")]
        [SerializeField] protected ScriptableObject[] _scriptableObjects;
        /// <summary>
        /// Binds non-monobehaviour exemplar from new instance of given type
        /// </summary>
        /// <typeparam name="TDependency"> binding type </typeparam>
        /// <returns> object to override binding options of needed </returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindService<TDependency>()
           => Container.Bind<TDependency>().FromNew().AsSingle();
        /// <summary>
        /// Binds non-monobehaviour concrete realization from new instance of given type to given abstract type
        /// </summary>
        /// <typeparam name="TAbstractDependency"> abstract class</typeparam>
        /// <typeparam name="TConcrete">concrete realization of an abstract class</typeparam>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindAbstractClass<TAbstractDependency, TConcrete>() where TConcrete : TAbstractDependency
           => Container.Bind<TAbstractDependency>().To<TConcrete>().AsSingle();
        /// <summary>
        /// Binds non-monobehaviour realization from new instance of given type to given interface type
        /// </summary>
        /// <typeparam name="TInterface">interface we should bind realization to</typeparam>
        /// <typeparam name="TRealization">interface realization itself</typeparam>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder InstallInterfaceBinding<TInterface, TRealization>() where TRealization : TInterface
          => Container.Bind<TInterface>().To<TRealization>().AsSingle();
        /// <summary>
        /// Binds non-monobehaviour realization from given existing instance of given type to given interface type
        /// </summary>
        /// <typeparam name="TInterface">interface we should bind realization to</typeparam>
        /// <typeparam name="TRealization">interface realization itself</typeparam>
        /// <param name="instance">instance we want to bind</param>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindInterfaceFromInstance<TInterface, TRealization>(TRealization instance) where TRealization : TInterface
          => Container.Bind<TInterface>().To<TRealization>().FromInstance(instance);
        /// <summary>
        /// Binds unity component from existing instance of given type to thw same type 
        /// </summary>
        /// <typeparam name="TDependency"> Unity component type</typeparam>
        /// <param name="instance">Unity component instance</param>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindUnityComponent<TDependency>(TDependency instance) where TDependency : Component
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        /// <summary>
        /// Binds Monobehaviour component from existing instance of given type to the same type 
        /// </summary>
        /// <typeparam name="TDependency"> Monobehaviour component type</typeparam>
        /// <param name="instance">Monobehaviour component instance</param>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindMonobehaviour<TDependency>(TDependency instance) where TDependency : MonoBehaviour
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        /// <summary>
        /// Binds ScriptableObject component from existing instance of given type to thw same type 
        /// </summary>
        /// <typeparam name="TDependency"> ScriptableObject component type</typeparam>
        /// <param name="instance">ScriptableObject component instance</param>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindScriptableObject<TDependency>(TDependency instance) where TDependency : ScriptableObject
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        /// <summary>
        /// Binds all class interfaces and class iteself to given type
        /// </summary>
        /// <typeparam name="TService">binding type</typeparam>
        /// <returns>zenject object to override binding options of needed</returns>
        protected ConcreteIdArgConditionCopyNonLazyBinder BindInterfacesAndSelfto<TService>() where TService : class => 
            Container.BindInterfacesAndSelfTo<TService>().AsSingle();
        protected ConcreteIdArgConditionCopyNonLazyBinder BindInterfacesAndSelfto<TService, TAbstractService>() where TService : TAbstractService where TAbstractService : class
        {
            TService service = Container.Instantiate<TService>();
            return Container.BindInterfacesAndSelfTo<TAbstractService>().FromInstance(service);
        }

        protected void InitAndBindConfigs()
        {
            foreach (var scriptable in _scriptableObjects)
            {
                Container.Bind(scriptable.GetType()).FromInstance(scriptable).AsSingle();
                IInitializableConfig initializable = scriptable as IInitializableConfig;

                if (initializable != null)
                    initializable.Initialize();
            }
        }
    }
    }
