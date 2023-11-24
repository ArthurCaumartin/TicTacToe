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

    private int _x;
    private int _y;
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }

    private ViewManager _viewManager;
    private Image _image;
    private Button _button;
    private BoxAnimation _boxAnimation;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClic);

        _viewManager = GetComponentInParent<ViewManager>();
        _boxAnimation = GetComponent<BoxAnimation>();
        _image = GetComponent<Image>();

        _button = GetComponent<Button>();
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
                break;

            case BoxState.Cross:
                _image.sprite = _crossSprite;
                _button.enabled = false;
                break;

            case BoxState.Empty:
                _image.sprite = null;
                _button.enabled = true;
                break;
        }
        _boxAnimation.BounceAnimation();
    }
}


public class Animation
{
    //? oui !
}
