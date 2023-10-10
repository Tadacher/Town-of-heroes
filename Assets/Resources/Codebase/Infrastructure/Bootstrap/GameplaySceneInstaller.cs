using BootStrap;
using Services;
using UnityEngine;

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
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
    }
}
