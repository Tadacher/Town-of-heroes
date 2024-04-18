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

        private BuildMenuEntry _lastSelectedEntry;
        [Inject]
        public void Init(MetaUiContainer metaUiContainer)
        {
            _buildButton.onClick
                .AddListener(() => metaUiContainer.BuildMenuUiContainer.gameObject.SetActive(false));
            _buildButton.onClick.AddListener(OnButtonPressed);
        }
        public void UpdateView(MetaBuildingDescriptionParams buildingDescruptionParams, BuildMenuEntry buildMenuEntry)
        {
            _lastSelectedEntry?.Deselect();
            _lastSelectedEntry = buildMenuEntry;


            _image.sprite = buildingDescruptionParams.Image;
            _title.text = buildingDescruptionParams.Name;
            _description.text = buildingDescruptionParams.Description;
        }

        private void OnButtonPressed()
        {
            Debug.Log("Build pressed");
            OnBuildButtonPressed?.Invoke();
        }
    }
}