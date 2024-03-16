using Metagameplay.Buildings;
using Metagameplay.Ui;
using UnityEngine;

namespace Infrastructure
{
    public sealed class MetaGameplaySceneMonoInstaller : AbstractMonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private MetaUiContainer _metaUiContainer;
        public override void InstallBindings()
        {
            BindUnityComponent(_audioSource);

            BindMonobehaviour(_metaUiContainer);
            
            BindService<BuldingMenuService>().NonLazy();
            BindService<MetaGameplayUiService>().NonLazy();
            BindService<MetaGridSevice>();
            BindService<MetaBuildingsInstantiationService>();
            BindService<MetaBuildingService>();
            BindService<MetaCityService>();


            BindService<MetaCityLoadHandler>().NonLazy();
        }
    }
}
