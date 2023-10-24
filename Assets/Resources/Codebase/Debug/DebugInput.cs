using Codebase.Services.CardGeneration;
using Core.Towers;
using UnityEngine;
using Zenject;

namespace Codebase.Debug
{
    public class DebugInput : MonoBehaviour
    {
        private CardInstantiationService _cardInstantiationService;

        [Inject]
        public void Construct(CardInstantiationService cardInstantiationService)
        {
            _cardInstantiationService = cardInstantiationService;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _cardInstantiationService.ReturnObject(typeof(ArcherTower));
            }
        }
    }
}
