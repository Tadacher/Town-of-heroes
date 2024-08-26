using System;
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
    private Vector3 _targetpos;

    //
    private void Start()
    {
        _direction = new Vector3(_popupSpeedX, _popupSpeedY, 0f);
        _origPos = _popupObjectTransform.localPosition;
        _targetpos = _origPos + _direction * _popupDistance;
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
        float t = 0f;
        Vector3 curPos = _popupObjectTransform.localPosition;

        while (t < 1f)
        {
            _popupObjectTransform.localPosition = Vector3.Lerp(curPos, _targetpos, t);
            t += Time.deltaTime * 2;
            yield return null;
        }
    }

    private IEnumerator PopdownCoroutine()
    {
        float t = 0f;
        Vector3 curPos = _popupObjectTransform.localPosition;
        while (t < 1f)
        {
            _popupObjectTransform.localPosition = Vector3.Lerp(curPos, _origPos, t);
            t += Time.deltaTime *2;
            yield return null;
        }
    }

    public void ResetPosition()
    {
        _popupObjectTransform.localPosition = _origPos;
    }
}