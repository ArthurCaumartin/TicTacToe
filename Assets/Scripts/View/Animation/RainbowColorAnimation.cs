using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowColorAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _hue = 1;

    void Update()
    {
        if(!_image)
            return;

        _hue = Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * _speed));
        Color newColor = Color.HSVToRGB(_hue, 1, 1);
        newColor.a = .5f;
        _image.color = newColor;
    }
}
