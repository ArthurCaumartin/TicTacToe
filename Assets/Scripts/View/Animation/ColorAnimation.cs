using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAnimation : MonoBehaviour
{
    [SerializeField] private Gradient _colorGradient;
    [SerializeField] private float _speed = 1;
    private float _animationTime = 1;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if(!_image)
            return;

        _animationTime = Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * _speed));
        // Color newColor = Color.HSVToRGB(_hue, 1, 1);
        Color newColor = _colorGradient.Evaluate(_animationTime);
        _image.color = newColor;
    }
}
