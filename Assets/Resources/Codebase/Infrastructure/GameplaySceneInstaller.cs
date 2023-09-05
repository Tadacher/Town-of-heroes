using BootStrap;
using Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private EnemySpawnPosMarker _spawnPos;
        [SerializeField] private EnemyPrefabContainer _enemyPrefabContainer;
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        public override void InstallBindings()
        {
            //scriptableObjects
            InstallScriptableObjecttBinding(_enemyPrefabContainer);

            //Unity components
            InstallUnityComponentBinding(_audioSource);

            //Monobeh
            InstallMonobehaviourBinding(_spawnPos);

            //Non-monobeh
            InstallBinding<DamageTextService>();
            InstallBinding<WaveService>();

            //Non monobeh abstract
            InstallAbstractClassBinding<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            Container.Inject(_sceneBootstrapper);
        }

        private void InstallBinding<TDependency>()
            => Container.Bind<TDependency>().FromNew().AsSingle();
        private void InstallAbstractClassBinding<TAbstractDependency, TConcrete>() where TConcrete : TAbstractDependency
           => Container.Bind<TAbstractDependency>().To<TConcrete>().AsSingle();
        private void InstallUnityComponentBinding<TDependency>(TDependency instance) where TDependency : Component
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        private void InstallMonobehaviourBinding<TDependency>(TDependency instance) where TDependency : MonoBehaviour
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
        private void InstallScriptableObjecttBinding<TDependency>(TDependency instance) where TDependency : ScriptableObject
            => Container.Bind<TDependency>().FromInstance(instance).AsSingle();
    }
}
