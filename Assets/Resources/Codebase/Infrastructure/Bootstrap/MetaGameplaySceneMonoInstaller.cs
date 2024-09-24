using Metagameplay.Buildings;
using Metagameplay.Ui;
using Services;
using Services.CardGeneration;
using System;
using UnityEngine;

namespace Infrastructure
{  
    public sealed class MetaGameplaySceneMonoInstaller : AbstractMonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private MetaUiContainer _metaUiContainer;

        //prefabs
        [SerializeField] private DeckEntry _deckEntry;
        [SerializeField] private CardEntry _cardEntry;
        //monobehaviour services
        [SerializeField] private MetaGridCellInfoView _gridCellInfoView;

        public override void InstallBindings()
        {
            BindUnityComponent(_audioSource);

            BindMonobehaviour(_metaUiContainer);

            BindService<MetaGameplayUiService>().NonLazy();
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            //meta city
            BindService<BuildingDialogEventListenerFactory>().NonLazy();
            BindInterfacesAndSelfTo<MetaBuildingService>();
            BindInterfacesAndSelfTo<BuldingMenuService>();
            BindInterfacesAndSelfTo<MetaBuildingPrefabsLoader>();
            BindInterfacesAndSelfTo<BuildingMenuEntryFactory>().NonLazy();

            BindService<MetaBuildingsInstantiationService>();
            BindService<MetaGridSevice>();
            BindService<MetaCityInfoService>();
            BindService<MetaCitySaveLoadHandler>().NonLazy();
            BindService<MetaGridCellInfoService>();

            //deck editing
            BindInterfacesAndSelfTo<CardDeckEditingMenu>().NonLazy();
            BindService<CardDeckService>();
            BindService<DeckEntryFactory>();
            BindService<CardEntryFactory>();
            BindService<CardDescriptionService>();
            BindService<CardAvalilabilityService>();

            //prefabs
            BindMonobehaviour(_deckEntry);
            BindMonobehaviour(_cardEntry);
            //monobeh services
            BindMonobehaviour(_gridCellInfoView);

            //configs
            InitAndBindConfigs();
        }
    }
}
