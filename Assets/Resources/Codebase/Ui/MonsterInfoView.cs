using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoView : MonoBehaviour
{
    public RectTransform RectTransform;
    public Text Title;
    public Text Description;
    public Text Health;

    public Image HealthBar;

    private void Update()
    {
        //RectTransform.anchoredPosition = Input.mousePosition;
    }
}
