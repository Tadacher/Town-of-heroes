using Core.Towers;
using Services.CardGeneration;
using Services.GridSystem;
using UnityEngine;
using Zenject;

namespace Debugtools
{
    public class DebugInput : MonoBehaviour
    {
        private CardInstantiationService _cardInstantiationService;
        private BattleGridService _battleGridService;
        [Inject]
        public void Construct(CardInstantiationService cardInstantiationService, BattleGridService battleGridService)
        {
            _cardInstantiationService = cardInstantiationService;
            _battleGridService = battleGridService;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _cardInstantiationService.ReturnObject(typeof(ArcherTower));
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                _cardInstantiationService.ReturnObject(typeof(TowerOfDeath));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log(_battleGridService.PosToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }
        }
    }
}
