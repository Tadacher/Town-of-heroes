using Services.Input;
using System;
using System.Text;
using UnityEngine;

namespace Services.Ui
{
    /// <summary>
    /// controlls tower/worldcell describition GUI
    /// </summary>
    public class CardInfoUiService
    {
        private CardInfoUIContainer _infoUiContainer;
        private AbstractInputService _input;

        public CardInfoUiService(CardInfoUIContainer container, AbstractInputService abstractInputService)
        {
            _infoUiContainer = container;
            _input = abstractInputService;
        }

        private StringBuilder _sb = new StringBuilder();

        /// <summary>
        /// shows given tower description
        /// </summary>
        /// <param name="towerCardInfoConfig">description container</param>
        public void ShowAsTower(TowerCardInfoConfig towerCardInfoConfig)
        {
            Debug.Log("Shown tower");
            ClearTextFields();
            _infoUiContainer.Title.text = towerCardInfoConfig.Header;
            _infoUiContainer.Description.text = towerCardInfoConfig.Decription;
            _infoUiContainer.Damage.text = BuildDamageText(towerCardInfoConfig.Damage, towerCardInfoConfig.DamagePerLevel);
            _infoUiContainer.Radius.text = BuildRangeText(towerCardInfoConfig.Range, towerCardInfoConfig.RangePerLevel);
            _infoUiContainer.Interval.text = BuildIntervalText(towerCardInfoConfig.Interval, towerCardInfoConfig.IntervalPerLevel);
            _infoUiContainer.transform.position = _input.GetPointerPositionScreen();
            _infoUiContainer.gameObject.SetActive(true);
            _input.OnRightButtonUp += Hide;
        }
        public void ShowAsWorldCell(WorldCellCardInfoConfig worldCellCardInfoConfig)
        {
            ClearTextFields();
            _infoUiContainer.Title.text = worldCellCardInfoConfig.Header;
            _infoUiContainer.Description.text = worldCellCardInfoConfig.Decription;
            _infoUiContainer.Damage.text = worldCellCardInfoConfig.CellTags;
            _infoUiContainer.transform.position = _input.GetPointerPositionScreen();
            _infoUiContainer.gameObject.SetActive(true);
            _input.OnRightButtonUp += Hide;
        }
        public void Hide()
        {
            _infoUiContainer.gameObject.SetActive(false);
            _input.OnRightButtonUp -= Hide;
        }
        private void ClearTextFields()
        {
            _infoUiContainer.Title.text = null;
            _infoUiContainer.Description.text = null;
            _infoUiContainer.Damage.text = null;
            _infoUiContainer.Radius.text = null;
            _infoUiContainer.Interval.text = null;
        }
        private string BuildIntervalText(string interval, string intervalPerLevel)
        {
            _sb.Clear();
            _sb.Append($"Interval: {interval}");
            _sb.Append($"(+{intervalPerLevel})");
            return _sb.ToString();
        }
        private string BuildRangeText(string range, string rangePerLevel)
        {
            _sb.Clear();
            _sb.Append($"Range: {range}");
            _sb.Append($"(+{rangePerLevel})");
            return _sb.ToString();
        }
        private string BuildDamageText(string damage, string damagePerLevel)
        {
            _sb.Clear();
            _sb.Append($"Damage: {damage}");
            _sb.Append($"(+{damagePerLevel})");
            return _sb.ToString();
        }
    }
}