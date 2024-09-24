using Services.Input;
using UnityEngine.EventSystems;

namespace Core.Towers
{
    
    public abstract partial class AbstractTower //Tower info panel logic
    {
        private TowerInfoServiceIngame _towerInfoService;
        private AbstractInputService _abstractInputService;
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
                _towerInfoService.Show(this, this);
        }
    }
}