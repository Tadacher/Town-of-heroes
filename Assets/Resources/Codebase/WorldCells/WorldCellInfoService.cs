using Services.Input;

namespace WorldCells
{
    public class WorldCellInfoService
    {
        private WorldCellInfoView _view;
        private AbstractInputService _abstractInputService;
        public WorldCellInfoService(WorldCellInfoView view, AbstractInputService abstractInputService)
        {
            _view = view;
            _abstractInputService = abstractInputService;
        }

        public void Show(IWorldCellInfoProvider infoProvider)
        {
            _view.Title.text = infoProvider.Name;
            _view.Description.text = infoProvider.Description;
            _view.SetTags(infoProvider.CellTypes);

            _abstractInputService.OnRightButtonUp += HideWindow;
            _view.gameObject.SetActive(true);
        }
        private void HideWindow()
        {
            _view.gameObject.SetActive(false);
            _abstractInputService.OnRightButtonUp -= HideWindow;
        }
    }
}