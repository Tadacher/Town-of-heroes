using Codebase.MonobehaviourComponents;
using Progress;
using UnityEngine;

namespace Services.CardGeneration
{
    public class CardCountService : ICardRemovalListener
    {
        private int _maxCardCount = 10;
        private int _currentCount = 0;
        private readonly ResourceService _resourceService;
        private readonly TowerCardSpawnMarker _towerCardSpawnMarker;

        public CardCountService(ResourceService resourceService, TowerCardSpawnMarker towerCardSpawnMarker)
        {
            _resourceService = resourceService;
            _towerCardSpawnMarker = towerCardSpawnMarker;
        }
        public void NotifyAboutNewCard()
        {
            _currentCount ++;
            if(_currentCount == _maxCardCount)
            {
                _currentCount --;
                var towerToDelete = _towerCardSpawnMarker.transform.GetChild(0);
                towerToDelete.GetComponent<TowerCard>().ReturnToPool();
                _resourceService.GatherScrolls(1);
            }
        }
        public void NotifyAboutCardRemoval()
        {
            _currentCount --;
            if (_currentCount < 0)
                Debug.LogError("impossible card in hand count");
        }
    }
}
