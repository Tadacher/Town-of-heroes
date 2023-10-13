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
        public void InstantiateTowerFromCard<TTower>(TowerCard<TTower> towerCard) where TTower : AbstractTower
        {
            AbstractTower tower = GetTowerGhost<TTower>();
            _activeCard = towerCard;
            tower.StartFollowPointer();
            _inputService.OnPointerUp += ReleaseActiveTower;
        }

        private void ReleaseActiveTower()
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
            throw new NotImplementedException();
            _abstractTower.StopFollowingPointer();
        }

        private bool CanBePlacedAtPointer()
        {
            throw new NotImplementedException();
        }

        private AbstractTower GetTowerGhost<TTower>() where TTower : AbstractTower => 
            _towerInstantiatingService.ReturnObject<TTower>().AsGhost();


    }
}
