using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
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
    private RectTransform _rectTransform;
    private Color _colorStartBackup;

    void Start()
    {
        _viewManager = GetComponentInParent<ViewManager>();

        _image = GetComponent<Image>();
        _colorStartBackup = _image.color;

        _rectTransform = (RectTransform)transform;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClic);

        _colorSwap.UpdateAction = ColorSwapUpdate;

        _scaleBounce.UpdateAction = ScaleBounceUpdate;
        _scaleBounce.EndAction = EndBounceAnimation;

        UpdateBox(BoxState.Empty);
    }

    public void OnClic()
    {
        print("Clic on : X = " + _x + " : Y = " + _y);
        _viewManager.ClicOnBox(_x, _y);
    }

    public void SetButtonValue(bool value)
    {
        _button.enabled = value;
    }

    public void UpdateBox(BoxState boxState)
    {
        _colorSwap.EndAnimation();
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
                _image.color = _colorStartBackup;
                SetButtonValue(true);
                break;
        }
        _scaleBounce.Start();
    }

    void Update()
    {
        _colorSwap.Update(Time.deltaTime);
        _scaleBounce.Update(Time.deltaTime);
    }

    void ScaleBounceUpdate(float time)
    {
        print("Scale !");
        _rectTransform.localScale = Vector3.one * time;
    }

    void EndBounceAnimation()
    {
        _rectTransform.localScale = Vector3.one;
    }

    public void ColorSwapUpdate(float time)
    {
        _image.color = _colorGradient.Evaluate(time);
    }
}