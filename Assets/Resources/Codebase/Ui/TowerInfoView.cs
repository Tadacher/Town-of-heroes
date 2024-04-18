using UnityEngine;
using UnityEngine.UI;

public class TowerInfoView : MonoBehaviour
{
    public RectTransform RectTransform;
    public Text Title;
    public Text Description;
    public Text Level;
    public Text Damage;
    public Text Range;
    public Text AttackDelay;

    private void Update()
    {
        RectTransform.anchoredPosition = Input.mousePosition;
    }
}
