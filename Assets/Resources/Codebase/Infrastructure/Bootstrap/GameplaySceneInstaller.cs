using BootStrap;
using Services;
using UnityEngine;

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private EnemySpawnPosMarker _spawnPosMarker;
        [SerializeField] private EnemyPrefabContainer _enemyPrefabContainer;
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        public override void InstallBindings()
        {
            //scriptableObjects
            BindScriptableObject(_enemyPrefabContainer);

            //Unity components
            BindUnityComponent(_audioSource);

            //Monobeh
            BindMonobehaviour(_spawnPosMarker);

            //Non-monobeh
            BindService<DamageTextService>();
            BindService<WaveService>();

            //Non monobeh abstract
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            Container.Inject(_sceneBootstrapper);
        }
    }
}
