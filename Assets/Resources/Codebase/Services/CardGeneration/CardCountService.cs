using Codebase.MonobehaviourComponents;
using Progress;

namespace Services.CardGeneration
{
    public class CardCountService
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
                _towerCardSpawnMarker.transform.GetChild(0).GetComponent<TowerCard>().ReturnToPool();
                _resourceService.GatherScrolls(1);
            }
        }
    }
}
