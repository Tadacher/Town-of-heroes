using Core.Towers;
using Infrastructure;
using Services.GridSystem;
using Services.Input;
using System;
using UnityEngine;

namespace Services.TowerBuilding
{
    public class TowerBuildingService : ITowerCountProvider, IAdditionalTowerCountReviever
    {
        public int MaxTowerCount { get => _originalMaxTowerCount; set => _maxTowerCount = value; }


        protected IPoolableObject _activeCard;
        private TowerInstantiationService _towerInstantiatingService;
        private BattleGridService _alignerService;
        private CoreGameplaySceneUiService _gameplaySceneUiService;


        private const int _originalMaxTowerCount = 5;
        private int _maxTowerCount = _originalMaxTowerCount;
        private int _currentTowerCount;

        private AbstractInputService _inputService;
        private AbstractTower _activeTower;


        public TowerBuildingService(TowerInstantiationService towerInstantiatingService,
                                    BattleGridService gridAlignService,
                                    CoreGameplaySceneUiService gameplaySceneUiService,
                                    AbstractInputService inputService = null)
        {
            _gameplaySceneUiService = gameplaySceneUiService;
            _towerInstantiatingService = towerInstantiatingService;
            _inputService = inputService;
            _alignerService = gridAlignService;

            DrawTowerCount();
        }

        private void DrawTowerCount() => _gameplaySceneUiService.SetTowerCount(_currentTowerCount, _maxTowerCount);

        public void InstantiateTowerFromCard(TowerCard towerCard, Type type)
        {
            _activeTower = GetTowerGhost(type);
           
            _activeCard = towerCard;
            _activeTower.StartFollowPointer();
            _inputService.OnPointerUp += TryReleaseActiveTower;
        }

        private void TryReleaseActiveTower()
        {
            if (CanBePlacedAtPointer())
            {
                PlaceActiveTower();
                _currentTowerCount++;
                DrawTowerCount();
                _activeCard.ReturnToPool();
            }
            else
            {
                ((MonoBehaviour)_activeCard).gameObject.SetActive(true);
                _activeTower.StopFollowingPointer();
                _activeTower.ReturnToPool();
            }
            _inputService.OnPointerUp -= TryReleaseActiveTower;
        }

        private void PlaceActiveTower()
        {          
            _activeTower.AsUnGhost().StopFollowingPointer();
            _activeTower.InsertSelfToGrid();
        }

        private bool CanBePlacedAtPointer() => 
            _alignerService.CellAvailable(_activeTower.transform.position);

        private AbstractTower GetTowerGhost(Type type) => 
            _towerInstantiatingService.ReturnObject(type).AsGhost();

        public bool CanPlaceTower()
        {
            return _currentTowerCount < _maxTowerCount;
        }

        void IAdditionalTowerCountReviever.AddTowerCount(int additionalCount)
        {
            _maxTowerCount = _originalMaxTowerCount + additionalCount;
            DrawTowerCount();
        }
    }
}
