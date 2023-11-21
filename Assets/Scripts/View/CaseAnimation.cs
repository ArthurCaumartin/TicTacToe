using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector3 _startPosition;
    private bool _isStartSet = false;

    void Start()
    {
        _rectTransform = (RectTransform)transform;
        _startPosition = _rectTransform.position;
    }

    void LateUpdate()
    {
        Vector3 positionOffset = Vector3.zero;

        positionOffset.x = Mathf.Sin(Time.time);
        positionOffset.y = Mathf.Cos(Time.time);

        _rectTransform.localPosition = _startPosition + positionOffset;
    }


}
