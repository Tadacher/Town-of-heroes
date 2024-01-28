using Core.Towers;
using Services.GridSystem;
using Services.Input;
using System;
using UnityEngine;

namespace Services.TowerBuilding
{
    public class TowerBuildingService
    {
        protected IPoolableObject _activeCard;
        private TowerInstantiationService _towerInstantiatingService;
        private BattleGridService _alignerService;

        private AbstractInputService _inputService;
        private AbstractTower _activeTower;
        public TowerBuildingService(TowerInstantiationService towerInstantiatingService, BattleGridService gridAlignService, AbstractInputService inputService = null)
        {
            _towerInstantiatingService = towerInstantiatingService;
            _inputService = inputService;
            _alignerService = gridAlignService;
        }
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
    }
}
