using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class BoxAnimation : MonoBehaviour
{
    [Header("Rotate Animation :")]
    [SerializeField] private float _rotateSpeed;

    [Header("Circle Move Aniamtion Value :")]
    [SerializeField] private float _speed;
    [SerializeField] private float _amplitudeMax;
    [SerializeField] private float _amplitudeMin;
    [SerializeField] private float _offSetMax;
    [SerializeField] private float _offSetMin;

    [Header("Bounce Animation :")]
    [SerializeField] private float _onClicAnimationDuration;
    [SerializeField] private AnimationCurve _onClicAnimationCurve;


    private RectTransform _rectTransform;
    private float _offSet;
    private float _amplitude;
    float direction;

    void Start()
    {
        _rectTransform = (RectTransform)transform;

        _offSet = Random.Range(_offSetMin, _offSetMax);
        _amplitude = Random.Range(_amplitudeMin, _amplitudeMax);
        direction = Random.Range(0f, 1f) > .5f ? -1 : 1;
    }

    void Update()
    {
        if(_rotateSpeed != 0)
            RotateAnimation();

        if(_speed != 0)
            CircleMoveAnimation();
    }

    private void RotateAnimation()
    {
        _rectTransform.eulerAngles = new Vector3(_rectTransform.eulerAngles.x, _rectTransform.eulerAngles.y, Time.time * _rotateSpeed);
    }

    private void CircleMoveAnimation()
    {
        Vector3 positionOffset = Vector3.zero;

        positionOffset.x = Mathf.Sin((Time.time * _speed * direction) + _offSet);
        positionOffset.y = Mathf.Cos((Time.time * _speed * direction) + _offSet);
        positionOffset *= _amplitude;

        _rectTransform.anchoredPosition = positionOffset;
    }

    public void BounceAnimation()
    {
        DOTween.To((time) =>
        {
            _rectTransform.localScale = Vector3.one * time;
        }, 0, 1, _onClicAnimationDuration)
        .SetEase(_onClicAnimationCurve);
    }


}