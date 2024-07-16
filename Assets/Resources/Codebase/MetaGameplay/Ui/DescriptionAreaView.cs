using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagameplay.Ui
{
    /// <summary>
    /// Desription of selected building menu entry
    /// </summary>  
    public class DescriptionAreaView : MonoBehaviour
    {
        public event Action OnBuildButtonPressed;

        [SerializeField] private Image _image;
        [SerializeField] private Text _title;
        [SerializeField] private Text _description;
        [SerializeField] private Button _buildButton;

        [SerializeField] private Text _woodCost;
        [SerializeField] private Text _stoneCost;
        [SerializeField] private Text _foodCost;
        [SerializeField] private Text _scrollsCost;

        private BuildMenuEntry _lastSelectedEntry;
        private MetaUiContainer _metaUiContainer;
        [Inject]
        public void Init(MetaUiContainer metaUiContainer)
        {
            _metaUiContainer = metaUiContainer;
            _buildButton.onClick.AddListener(OnButtonPressed);
        }

        public void CloseMenu() => _metaUiContainer.BuildMenuUiContainer.gameObject.SetActive(false);

        public void UpdateView(MetaBuildingDescriptionParams buildingDescruptionParams, BuildMenuEntry buildMenuEntry)
        {
            _lastSelectedEntry?.Deselect();
            _lastSelectedEntry = buildMenuEntry;


            _image.sprite = buildingDescruptionParams.Image;
            _title.text = buildingDescruptionParams.Name;
            _description.text = buildingDescruptionParams.Description;

            _woodCost.text = buildingDescruptionParams.Cost.WoodPieces.ToString();
            _stoneCost.text = buildingDescruptionParams.Cost.StonePieces.ToString();
            _foodCost.text = buildingDescruptionParams.Cost.FoodPieces.ToString();
            _scrollsCost.text = buildingDescruptionParams.Cost.Scrolls.ToString();
        }

        private void OnButtonPressed()
        {
            Debug.Log("Build pressed");
            OnBuildButtonPressed?.Invoke();
        }
    }
}