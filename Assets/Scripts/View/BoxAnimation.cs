using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BoxAnimation : MonoBehaviour
{
    [Header("Mouse Value :")]
    [SerializeField] private float _mouseOverSpeed;

    [Header("Base Value :")]
    [SerializeField] private float _amplitudeMax;
    [SerializeField] private float _amplitudeMin;
    [SerializeField] private float _speedMax;
    [SerializeField] private float _speedMin;
    [SerializeField] private float _offSetMax;
    [SerializeField] private float _offSetMin;
    
    private RectTransform _rectTransform;
    private float _speed;
    private float _offSet;
    float direction;



    void Start()
    {
        _rectTransform = (RectTransform)transform;

        _offSet = Random.Range(_offSetMin, _offSetMax);
        _speed = Random.Range(_speedMin, _speedMax);
        _mouseOverSpeed = 1;
        direction = Random.Range(0f, 1f) > .5f ? -1 : 1;
    }

    void Update()
    {
        Vector3 positionOffset = Vector3.zero;

        positionOffset.x = Mathf.Sin((Time.time * _speed * direction) + _offSet);
        positionOffset.y = Mathf.Cos((Time.time * _speed * direction) + _offSet);

        _rectTransform.localPosition = positionOffset;
    }
}