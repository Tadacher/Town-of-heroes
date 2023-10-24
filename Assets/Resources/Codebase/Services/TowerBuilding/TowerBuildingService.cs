using Core.Towers;
using Services.Input;
using System;
using UnityEngine;

namespace Services.TowerBuilding
{
    public class TowerBuildingService
    {
        private TowerInstantiationService _towerInstantiatingService;


        private AbstractInputService _inputService;
        private AbstractTower _abstractTower;
        private IPoolableObject _activeCard;
        public TowerBuildingService(TowerInstantiationService towerInstantiatingService, AbstractInputService inputService = null)
        {
            _towerInstantiatingService = towerInstantiatingService;
            _inputService = inputService;
        }
        public void InstantiateTowerFromCard(TowerCard towerCard, Type type)
        {
            AbstractTower tower = GetTowerGhost(type);
            _activeCard = towerCard;
            tower.StartFollowPointer();
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
                _abstractTower.StopFollowingPointer();
                _abstractTower.ReturnToPool();
            }
           
        }

        private void PlaceActiveTower()
        {
            //throw new NotImplementedException();
            //_abstractTower.StopFollowingPointer();
        }

        private bool CanBePlacedAtPointer()
        {
            return true;
            //throw new NotImplementedException();
        }

        private AbstractTower GetTowerGhost(Type type) => 
            _towerInstantiatingService.ReturnObject(type).AsGhost();


    }
}
