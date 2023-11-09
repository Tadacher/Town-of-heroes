using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupOnPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _popupDistance;
    [SerializeField] private float _popupSpeedX;
    [SerializeField] private float _popupSpeedY;
    [SerializeField] private RectTransform _popupObjectTransform;
    //

    //internal
    private Coroutine _popupCoroutine;
    private Vector3 _direction;
    private Vector3 _origPos;
    //
    private void Start()
    {
        _direction = new Vector3(_popupSpeedX, _popupSpeedY, 0f);
        _origPos = transform.localPosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_popupCoroutine != null)
            StopCoroutine(_popupCoroutine);

        _popupCoroutine = StartCoroutine(PopupCoroutine());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_popupCoroutine != null)
            StopCoroutine(_popupCoroutine);

        _popupCoroutine = StartCoroutine(PopdownCoroutine());
    }

    private IEnumerator PopupCoroutine()
    {
        while (Vector3.Distance(transform.localPosition, _origPos) < _popupDistance)
        {
            _popupObjectTransform.localPosition += _direction * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator PopdownCoroutine()
    {
        while ((transform.localPosition - _origPos).sqrMagnitude > 0.1f)
        {
            _popupObjectTransform.localPosition -= _direction * Time.deltaTime;
            yield return null;
        }
    }
}