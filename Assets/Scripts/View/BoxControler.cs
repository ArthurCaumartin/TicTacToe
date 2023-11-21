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

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClic);
        _viewManager = GetComponentInParent<ViewManager>();
        _image = GetComponent<Image>();
    }

    public void OnClic()
    {
        print("Clic on : X = " + _x + " : Y = " + _y);
        _viewManager.ClicOnCase(_x, _y);
    }

    public void SetSprite(Sprite spriteToSet)
    {
        _image.sprite = spriteToSet;
    }
}
