using UnityEngine;
using UnityEngine.UI;

public class MetaGridCellInfoView : MonoBehaviour
{
    [SerializeField] private Text _header;
    [SerializeField] private Text _description;
    private void Awake()
    {
        _header.text = string.Empty;
        _description.text = string.Empty;
    }
    public void SetHeaderText(string headerText)
    {
        _header.text = headerText;
    }
    public void SetDescriptionText(string descriptionText)
    {
        _description.text = descriptionText;
    }
}