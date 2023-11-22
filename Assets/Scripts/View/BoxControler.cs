using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxControler : MonoBehaviour
{
    [SerializeField] private int _x;
    [SerializeField] private int _y;

    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }

    private ViewManager _viewManager;
    private Image _image;
    private Button button;
    private BoxAnimation _boxAnimation;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClic);
        _viewManager = GetComponentInParent<ViewManager>();
        _image = GetComponent<Image>();
        button = GetComponent<Button>();
        _boxAnimation = GetComponent<BoxAnimation>();
    }

    public void OnClic()
    {
        print("Clic on : X = " + _x + " : Y = " + _y);
        _viewManager.ClicOnCase(_x, _y);
        button.enabled = false;
    }

    public void ResetButton()
    {
        button.enabled = true;
    }

    public void SetSprite(Sprite spriteToSet)
    {
        _image.sprite = spriteToSet;
        _boxAnimation.BounceAnimation();
    }
}
