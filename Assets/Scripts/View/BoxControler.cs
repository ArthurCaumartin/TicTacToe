using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxControler : MonoBehaviour
{
    [Header("Sprites : ")]
    [SerializeField] private Sprite _circleSprite;
    [SerializeField] private Sprite _crossSprite;
    // [SerializeField] private Sprite _emptySprite;
    [Space]
    [SerializeField] Gradient _colorGradient;
    [SerializeField] DoTweenAtHome _colorSwap;
    [SerializeField] DoTweenAtHome _scaleBounce;

    private int _x;
    private int _y;
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }

    private ViewManager _viewManager;
    private Image _image;
    private Button _button;
    private BoxAnimation _boxAnimation;
    private RectTransform _rectTransform;

    void Start()
    {
        _viewManager = GetComponentInParent<ViewManager>();
        _boxAnimation = GetComponent<BoxAnimation>();
        _image = GetComponent<Image>();
        _rectTransform = (RectTransform)transform;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClic);

        _colorSwap.UpdateAction = ColorSwap;
        _scaleBounce.UpdateAction = ScaleBounce;

        UpdateBox(BoxState.Empty);
    }

    public void OnClic()
    {
        print("Clic on : X = " + _x + " : Y = " + _y);
        _viewManager.ClicOnBox(_x, _y);
    }

    public void UpdateBox(BoxState boxState)
    {
        switch (boxState)
        {
            case BoxState.Circle:
                _image.sprite = _circleSprite;
                _button.enabled = false;
                _colorSwap.Start();
                break;

            case BoxState.Cross:
                _image.sprite = _crossSprite;
                _button.enabled = false;
                _colorSwap.Start();
                break;

            case BoxState.Empty:
                _image.sprite = null;
                _button.enabled = true;
                break;
        }
        _scaleBounce.Start();
    }

    void Update()
    {
        _colorSwap.Update(Time.deltaTime);
        _scaleBounce.Update(Time.deltaTime);
    }

    public void ScaleBounce(float time)
    {
        print("Scale !");
        _rectTransform.localScale = Vector3.one * time;
    }

    public void ColorSwap(float time)
    {
        _image.color = _colorGradient.Evaluate(time);
    }
}