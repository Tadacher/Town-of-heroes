using Codebase.Services.CardGeneration;
using Core.Towers;
using Services.GridSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Debugtools
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

            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log(_battleGridService.PosToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }
        }
    }
}
