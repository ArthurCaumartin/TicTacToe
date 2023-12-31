using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxControler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprites : ")]
    [SerializeField] private Sprite _circleSprite;
    [SerializeField] private Sprite _crossSprite;
    [Space]
    [SerializeField] private Gradient _colorGradient;
    [SerializeField] private DoTweenAtHome _colorSwap;
    [SerializeField] private DoTweenAtHome _scaleBounce;
    [Space]
    [SerializeField] private float _removeAnimationDuration;
    [SerializeField] private AnimationCurve _removeAnimationCurve;
    [SerializeField] private AnimationCurve _resetAnimationCurve;

    [Header("Circle Move Animation :")]
    [SerializeField] private float _circleMoveSpeed;
    [SerializeField] private float _circleMoveAmplitudeMin;
    [SerializeField] private float _circleMoveAmplitudeMax;
    [SerializeField] private float _circleMoveOffSetMin;
    [SerializeField] private float _circleMoveOffSetMax;

    [Space]
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }

    private ViewManager _viewManager;
    private Image _image;
    private Button _button;
    private RectTransform _rectTransform;
    private Color _colorStartBackup;
    private float _circleAmplitude;
    private float _circleOffset;
    private float _circleAnimationDirection;
    private bool _isRunningIdleAnimation = true;
    private Tweener _removeAnimiation;
    private Tweener _resetAnimiation;
    private Vector3 _circleAnimationPositionOffset;
    

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
        CircleMoveAnimationStart();
    }

    public void OnClic()
    {
        _viewManager.ClicOnBox(_x, _y);
    }

    public void SetButtonValue(bool value)
    {
        _button.enabled = value;
        _image.raycastTarget = value;
    }

    public void UpdateBox(BoxState boxState)
    {
        _colorSwap.EndAnimation();
        switch (boxState)
        {
            case BoxState.Circle:
                _image.sprite = _circleSprite;
                _colorSwap.Start();
                SetButtonValue(false);
                break;

            case BoxState.Cross:
                _image.sprite = _crossSprite;
                _colorSwap.Start();
                SetButtonValue(false);
                break;

            case BoxState.Empty:
                _image.sprite = null;
                _image.color = _colorStartBackup;
                ResetAnimation(() =>
                {
                    SetButtonValue(true);
                    _isRunningIdleAnimation = true;
                });
                break;
        }
        _scaleBounce.Start();
    }

    void Update()
    {
        _colorSwap.Update(Time.deltaTime);
        _scaleBounce.Update(Time.deltaTime);

        if (_isRunningIdleAnimation)
            CicleMoveAnimationUpdate(Time.time);
    }

    void CircleMoveAnimationStart()
    {
        _isRunningIdleAnimation = true;
        _circleOffset = Random.Range(_circleMoveOffSetMin, _circleMoveOffSetMax);
        _circleAmplitude = Random.Range(_circleMoveAmplitudeMin, _circleMoveAmplitudeMax);
        _circleAnimationDirection = Random.Range(0f, 1f) > .5f ? -1 : 1;
    }

    void CicleMoveAnimationUpdate(float time)
    {
        Vector3 rotationOffset = Vector3.zero;

        float sinFactor = Mathf.Sin((time * _circleMoveSpeed * _circleAnimationDirection) + _circleOffset);
        float cosFactor = Mathf.Cos((time * _circleMoveSpeed * _circleAnimationDirection) + _circleOffset);

        _circleAnimationPositionOffset.x = sinFactor;
        _circleAnimationPositionOffset.y = cosFactor;
        _circleAnimationPositionOffset *= _circleAmplitude;

        rotationOffset.z = sinFactor * _circleAmplitude;

        _rectTransform.rotation = Quaternion.Euler(rotationOffset);
        _rectTransform.localPosition = _circleAnimationPositionOffset;
    }

    void ScaleBounceUpdate(float time)
    {
        // print("Scale !");
        _rectTransform.localScale = Vector3.one * time;
    }

    void EndBounceAnimation()
    {
        _rectTransform.localScale = Vector3.one;
    }

    void ColorSwapUpdate(float time)
    {
        _image.color = _colorGradient.Evaluate(time);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _viewManager.SetPreviewFormTarget(_rectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _viewManager.SetPreviewFormTarget(null);
    }

    public void PlayRemoveAnimation()
    {
        RemoveAnimation();
    }

    void RemoveAnimation()
    {
        _isRunningIdleAnimation = false;
        Vector2 startPosition = _rectTransform.localPosition;
        float pixelHeight = Camera.main.pixelHeight;
        Vector2 endPosition = Random.insideUnitCircle.normalized * (pixelHeight + (pixelHeight * 0.1f));
        DOTween.To((time) =>
        {
            _rectTransform.localPosition = Vector2.LerpUnclamped(startPosition, endPosition, time);
        }, 0, 1, _removeAnimationDuration)
        .SetEase(_removeAnimationCurve);
    }

    void ResetAnimation(System.Action endAction)
    {
        Vector2 startPosition = _rectTransform.localPosition;
        DOTween.To((time) =>
        {
            _rectTransform.localPosition = Vector2.LerpUnclamped(startPosition, _circleAnimationPositionOffset, time);
        }, 0, 1, _removeAnimationDuration)
        .SetEase(_resetAnimationCurve)
        .OnComplete(() => { endAction.Invoke(); });
    }
}