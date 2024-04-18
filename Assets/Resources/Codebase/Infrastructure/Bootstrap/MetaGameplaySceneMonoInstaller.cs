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

        [SerializeField] private ScriptableObject[] _scriptableObjects;

        public override void InstallBindings()
        {
            BindUnityComponent(_audioSource);

            BindMonobehaviour(_metaUiContainer);

            BindService<MetaGameplayUiService>().NonLazy();
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();
            //meta city
            BindService<MetaBuildingService>();
            BindService<BuldingMenuService>().NonLazy();
            BindService<MetaBuildingsInstantiationService>();
            BindService<MetaGridSevice>();
            BindService<MetaCityService>();
            BindService<MetaCityLoadHandler>().NonLazy();
            BindService<MetaGridCellInfoService>();

            //deck editing
            BindInterfacesAndSelfto<CardDeckEditingService>().NonLazy();
            BindService<CardDeckService>();
            BindService<DeckEntryFactory>();
            BindService<CardEntryFactory>();
            BindService<CardDescriptionService>();

            //prefabs
            BindMonobehaviour(_deckEntry);
            BindMonobehaviour(_cardEntry);
            //monobeh services
            BindMonobehaviour(_gridCellInfoView);

            //configs
            InitConfigs();
        }

    

        private void InitConfigs()
        {
            foreach (var scriptable in _scriptableObjects)
            {
                Container.Bind(scriptable.GetType()).FromInstance(scriptable);
                IInitializableConfig initializable = scriptable as IInitializableConfig;

                if (initializable != null)
                    initializable.Initialize();
            }
        }
    }
}
